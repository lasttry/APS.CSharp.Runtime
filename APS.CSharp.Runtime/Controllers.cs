﻿using APS.CSharp.Runtime.Internal;
using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace APS.CSharp.Runtime
{
    public class Controllers
    {
        // Gets all registred controllers
        // for a controller to be registred it needs to be named: APS.Controller.{project}.dll
        public static List<Controller> GetControllers()
        {
            // Define the type of the resource we are looking for
            var type = typeof(ResourceBase);
            // Get all the current assemblies with the define interface
            var types = AppDomain.CurrentDomain.GetAssemblies().ToList()
                .SelectMany(a => a.GetTypes())
                .Where(t => type.IsAssignableFrom(t));

            List<Controller> controllers = new List<Controller>();

            // Now we just want the DLL
            foreach (Type dllType in types)
            {
                if (controllers.Where(o => o.FullName == dllType.Assembly.FullName).Count() > 0)
                    continue;
                if (dllType.Assembly.FullName == Assembly.GetExecutingAssembly().FullName ||
                    dllType.Assembly.FullName.StartsWith("APS.CSharp.SDK,"))
                    continue;

                string configAttribute = null;
                object[] config = dllType.Assembly.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
                if (config.Length > 0)
                {
                    AssemblyConfigurationAttribute aca = (AssemblyConfigurationAttribute)config[0];
                    if (!string.IsNullOrEmpty(aca.Configuration))
                        configAttribute = aca.Configuration;
                }
                controllers.Add(new Controller()
                {
                    FullName = dllType.Assembly.FullName,
                    Assembly = dllType.Assembly,
                    Endpoint = configAttribute,
                    Types = GetAllTypes(dllType.Assembly)
                });
            }
            return controllers;
        }

        public static List<string> GetApplications(Assembly assembly)
        {
            var types = from type in assembly.GetTypes()
                        where typeof(SDK.Application).IsAssignableFrom(type)
                        select type;
            List<string> classes = new List<string>();
            foreach (Type t in types)
            {
                classes.Add(t.Namespace + "." + t.Name + "<br />" +
                    GetResourceAttributes(t).ToHtmlString());
            }
            return classes;
            //return null;
        }

        private static ResourceBaseAttribute GetResourceAttributes(Type t)
        {
            foreach (object o in t.GetCustomAttributes(typeof(ResourceBaseAttribute), false))
                return (ResourceBaseAttribute)o;
            return new ResourceBaseAttribute();
        }

        public static Controller CurrentController(HttpRequest request)
        {
            if (request.Url.Segments.Length < 2)
                return null;
            List<Controller> controllers = GetControllers();

            string urlController = request.Url.Segments[1].Replace("/", "").ToLower();

            Controller currentController = controllers.Find(o => o.Endpoint != null && o.Endpoint.ToLower() == urlController);
            return currentController;
        }



        public static List<string> GetAllTypes(Assembly assembly)
        {
            //List<string> allTypes = new List<string>();
            //foreach (Type type in assembly.GetTypes())
            //    if (type.GetCustomAttributes(typeof(ResourceBaseAttribute), true).Length > 0)
            //        allTypes.Add(type.Name);
            //return allTypes;
            return null;
        }
        
        class TestResource
        {
            public string name { get; set; }
            public string id { get; set; }
        }


        /// <summary>
        /// Returns the name of the object to work with
        /// It's strips down the Url.Segments and goes down until the actual object we are working with.
        /// </summary>
        /// <param name="urlSegments">from request.Url.Segments</param>
        /// <param name="parentResource">If contains a parent resource</param>
        /// <returns></returns>
        public static string GetActionName(string[] urlSegments, out string parentResource)
        {
            // /application/{id}/resource
            parentResource = null;
            string resourceOrFunction = null;
            //string id = null;
            // we shall iterate throught all the segments of the URL to understand what to do!
            for(int segment = 2;segment< urlSegments.Length; segment++)
            {
                
                if (parentResource == null)
                    // means this is the first segment we are checking
                    parentResource = urlSegments[segment].Trim('/');
                else if(resourceOrFunction == null)
                    // means we already have a parent Resource and we are going to perform actions on the child resource or function
                    resourceOrFunction = urlSegments[segment].Trim('/');
                else
                {
                    // means we need to go deeper into the resources, this should only happen when we have such thing like /application/{guid}/product/{guid}/resource
                    parentResource = resourceOrFunction;
                    resourceOrFunction = urlSegments[segment].Trim('/');
                }
                // this means we probably have some ID after the name of the resource
                if (urlSegments.Length > segment + 1)
                {
                    Guid g;
                    if(Guid.TryParse(urlSegments[segment + 1].Trim('/'), out g))
                        // it's actual a GUID so we should ignore it, since we will receive this guid in the content and set it up in the resource.
                        segment++;
                }
            }
            // means that we only have one resource to work with, so parentResource should be sent null.
            if (resourceOrFunction == null)
            {
                resourceOrFunction = parentResource;
                parentResource = null;
            }
            return resourceOrFunction;
        }


        public static Type GetResourceByType(Controller controller, string name, string expectedType)
        {
            Type currentType = null;
            // Means that the work will be done in a resource
            try
            {
                currentType = controller.Assembly.GetTypes().Single(t => t.Name == name);
                // checking if the aps type defined in POA is the same as in the class
                if (expectedType.ToLowerInvariant() != currentType.GetCustomAttribute<ResourceBaseAttribute>().Id.ToLowerInvariant())
                {
                    throw new APSException(500, "The type expected by Operations Automation is '{0}' is not the same as provided '{1}' by class '{2}'.", expectedType, currentType.GetCustomAttribute<ResourceBaseAttribute>().Id, name);
                }
            }
            catch (InvalidOperationException)
            {
                throw new Exception(string.Format("There is no Resource with the type {0}", name));
            }
            return currentType;
        }

        public static object ProcessIncomingRequest(HttpRequest request, string documentContents, out APSException exception)
        {
            exception = new APSException();

            string parentResource = null;
            // let's get what resource we are going to work on.
            string functionOrResource = GetActionName(request.Url.Segments, out parentResource);

            Controller currentController = CurrentController(request);
            Type currentType = null;
            object instanciatedType = null;
            MethodInfo currentMethod = null;

            dynamic contents = null;
            if (request.ContentType == "application/json" && !string.IsNullOrEmpty(documentContents))
            {
                // we use this the determine if we are using a method or a resource
                contents = JObject.Parse(documentContents);
            }

            if (parentResource == null)
            {
                // Means that the work will be done in a resource
                try
                {
                    currentType = GetResourceByType(currentController, functionOrResource, ((string)contents.aps.type));
                }
                catch (Exception e)
                {
                    exception = new APSException(500, e.Message);
                    return false;
                }
            }
            else
            {
                // we need to determ if we are going to use a method or a resource.

                // first lets get the parent resource, either method or resource has a parent resource.
                Type parentType = null;
                try { parentType = currentController.Assembly.GetTypes().Single(t => t.Name == parentResource); }
                catch (InvalidOperationException)
                {
                    exception = new APSException(500, "The parent resource type '{0}' of the method/resource doesn't exist.", functionOrResource);
                    return false;
                }

                bool tryMethod = false;
                try
                {
                    // try to get the resource using the received parameters.
                    currentType = GetResourceByType(currentController, functionOrResource, ((string)contents.aps.type));
                }
                catch(APSException apse) {
                    exception = apse;
                    return false;
                }
                catch(Exception e)
                {
                    // means that something failed, it should mean that POA is calling some custom method
                    Trace.TraceWarning(e.Message + e.StackTrace);
                    tryMethod = true;
                }
                if (tryMethod)
                {
                    // try to get the method to call, if this fail we don't know what to do, so error is returned
                    try
                    {
                        // trying to get the method
                        currentMethod = parentType.GetMethod(functionOrResource);
                        // setting the work object of type to the parent.
                        currentType = parentType;
                    }
                    catch
                    {
                        // we don't know what to do, so we just reply with error
                        exception = new APSException(500, "Couldn't determine the operation to perform when calling path: " + request.Url + ". Class or Custom Method is missing.");
                        return false;
                    }
                }
            }
            // let's create the instance of our object to work with
            if (documentContents.Length > 0)
                instanciatedType = JsonConvert.DeserializeObject(documentContents, currentType);
            else
                instanciatedType = Activator.CreateInstance(currentType);
            //We need to set the controller to the resource
            APSC apsc = new APSC();
            // set the instanceid so we can call back to the APSC
            apsc.InstanceId = (string)request.Headers["APS-Instance-ID"];
            currentType.GetProperty("APSC").SetValue(instanciatedType, apsc);

            // currentMethod is initialized, so POA is calling the custom method.
            if (currentMethod != null)
            {
                try { return CallCustomMethod(currentType, request, instanciatedType, documentContents); }
                catch (Exception ex)
                {
                    exception = new APSException(500, ex.Message + ex.StackTrace);
                    return false;
                }
            }
            else {
                MethodInfo method = null;
                string methodName = "";

                try {
                    switch (request.HttpMethod)
                    {
                        case "POST":
                            methodName = "Provision";
                            break;
                        case "DELETE":
                            methodName = "Unprovision";
                            break;
                        case "GET":
                            methodName = "Retrieve";
                            break;
                        case "PUT":
                            methodName = "Configure";
                            break;
                        default:
                            break;
                    }
                    method = currentType.GetMethod(methodName);
                    if(method != null)
                        method.Invoke(instanciatedType, null);
                }
                catch (Exception e)
                {
                    string failedMessage = string.Format("Failed to invoke method '{0}' for class '{1}.{2}', with message: ", methodName, currentType.Namespace, currentType.Name);
                    if (e.InnerException.GetType() == typeof(APSException))
                    {
                        exception = (APSException)e.InnerException;
                        exception.message = failedMessage + exception.message; 
                    }
                    else
                        exception = new APSException(500, failedMessage + e.InnerException.Message);
                    return false;
                }
            }
            return true;
        }

        private static object CallCustomMethod(Type currentType, HttpRequest request, object instanciatedType, string documentContents)
        {
            // Lets get the custom method name the APCS is calling
            string customMethodName = request.Url.Segments[4].TrimEnd('/').ToLower();
            MethodInfo[] customMethods = currentType.
                GetMethods().
                // The method must contain the MethodAttribute
                Where(m => m.GetCustomAttributes<MethodAttribute>(false).Count() > 0).
                // The method path property must be the same as the requested custom method name
                Where(n => ((MethodAttribute)n.GetCustomAttribute<MethodAttribute>()).Path.ToLower() == "/" + customMethodName).
                // Just to make sure we get them all.
                ToArray();
            if (customMethods.Length == 0)
                throw new Exception(string.Format("Method {0} was not found in class {1}.", customMethodName, currentType.Name));

            // we iterate through all the methods, but it should only have one.
            foreach (MethodInfo customMethod in customMethods)
            {
                // Let's get the Method attribute
                MethodAttribute customMethodAttrib = customMethod.GetCustomAttribute<MethodAttribute>(false);
                if (customMethodAttrib == null || customMethodAttrib.Path.ToLower() != "/" + customMethodName.ToLower())
                    continue;
                if (customMethodAttrib.Verb.ToString() == request.HttpMethod)
                {
                    Dictionary<string, string> parameters = null;

                    // getting the querystring into a dictionary object.
                    if (request.Url.Query.Length > 0)
                    {
                        string[] queryString = request.Url.Query.TrimStart('?').Split('&');
                        if (queryString.Length != 0)
                            parameters = queryString.ToDictionary(q => q.Split('=')[0], v => v.Split('=')[1]);
                    }
                    List<object> parametersInput = new List<object>();

                    // Setting the values of the parameters into the right order for the method.
                    foreach (ParamAttribute paramAttrib in customMethod.GetCustomAttributes<ParamAttribute>())
                        if (paramAttrib.Source == ParamSource.Query)
                        {
                            if (!parameters.ContainsKey(paramAttrib.Name))
                                throw new Exception(string.Format("No input parameter with the name {0} was received to perform the request.", paramAttrib.Name));
                            parametersInput.Add(parameters[paramAttrib.Name]);
                        }
                        else if (paramAttrib.Source == ParamSource.Body)
                            parametersInput.Add(documentContents);

                    // if we have no parametersInput means that the method doesn't accept any parameters, so we should send null.
                    object[] methodParameters = null;
                    if (parametersInput.Count > 0)
                        methodParameters = parametersInput.ToArray();
                    // invoking the custom method, using the parameters.
                    object result = customMethod.Invoke(instanciatedType, methodParameters);

                    if (customMethod.ReturnType == typeof(bool))
                        return (bool)result ? 1 : 0;
                    else if (customMethod.ReturnType == typeof(int) || customMethod.ReturnType == typeof(string))
                        return result;
                    else
                        return JsonConvert.SerializeObject(result);
                }
            }
            return null;
        }
    }
}
