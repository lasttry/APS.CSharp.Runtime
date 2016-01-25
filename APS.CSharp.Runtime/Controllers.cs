using APS.CSharp.Runtime.Internal;
using APS.CSharp.SDK;
using APS.CSharp.SDK.Attributes;
using APS.CSharp.SDK.Types.Core;
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
                        where typeof(Application).IsAssignableFrom(type)
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
        public static string GetResourceName(string[] urlSegments, out string actionOrLink)
        {
            actionOrLink = null;
            if (urlSegments.Length < 2)
                // url segments must always be greater than 2
                throw new Exception(string.Format("Invalid Url passed. Application can't proceed ('{0}')", string.Join("", urlSegments)));
            // http://doc.apsstandard.org/2.1/spec/api/application/endpoint/#spec-api-app-endpoint
            // first segment is always the service id, except if we are upgrading the application
            string resourceName = urlSegments[2].Trim('/');
            if (urlSegments.Length < 4)
                return resourceName;
            // second segment is always the resource id
            Guid g;
            if (!Guid.TryParse(urlSegments[3].Trim('/'), out g))
                throw new Exception(string.Format("Invalid UUID in the request to the endpoint ('{0}')", string.Join("", urlSegments)));

            if (urlSegments.Length > 4)
                // we have a custom action or a link to perform
                actionOrLink = urlSegments[4];
            return resourceName;
        }


        public static Type GetResourceByType(Controller controller, string name, string expectedType)
        {
            Type currentType = null;
            // Means that the work will be done in a resource
            try
            {
                currentType = controller.Assembly.GetTypes().Single(t => t.Name == name);
                // checking if the aps type defined in POA is the same as in the class
                if (!string.IsNullOrEmpty(expectedType) && expectedType.ToLowerInvariant() != currentType.GetCustomAttribute<ResourceBaseAttribute>().Id.ToLowerInvariant())
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

        public static object ProcessIncomingRequest(HttpRequest request, string documentContents, out object exception)
        {
            exception = new APSException();

            string actionOrLink = null;
            // let's get what resource we are going to work on.
            string resourceName = GetResourceName(request.Url.Segments, out actionOrLink);

            Controller currentController = CurrentController(request);
            Type currentType = null;
            object instanciatedType = null;
            // used to save the custom Method
            MethodInfo currentMethod = null;
            // used to save the custom link
            PropertyInfo currentProperty = null;

            dynamic contents = null;
            if (request.ContentType == "application/json" && !string.IsNullOrEmpty(documentContents))
            {
                // we use this the determine if we are using a method or a resource
                contents = JObject.Parse(documentContents);
            }

            try
            {
                // let's get the type of the resource we are going to work with.
                string apsExpectedType = "";
                if (string.IsNullOrEmpty(actionOrLink))
                    apsExpectedType = contents.aps.type;
                currentType = GetResourceByType(currentController, resourceName, apsExpectedType);
            }
            catch (Exception e)
            {
                exception = new APSException(500, e.Message);
                return false;
            }
            if(!String.IsNullOrEmpty(actionOrLink))
            {
                // We need to determin if we are performing a link or a custom method
                
                currentProperty = currentType.GetProperty(actionOrLink);
                if (currentProperty == null)
                {
                    // we are working with a custom method.
                    // new versions can contain async custom methods
                    // so we need to check before checking the name.
                    if (request.Headers.AllKeys.Contains<string>(APSCHeaders.APSRequestPhase) && request.Headers[APSCHeaders.APSRequestPhase] == "async")
                        actionOrLink = actionOrLink + "Async";

                    currentMethod = currentType.GetMethod(actionOrLink);
                }
                if (currentMethod == null)
                {
                    // current controller doesn't implement this action log and return true
                    Trace.TraceWarning("Controller doesn't implment any action ('{0}')", request.RawUrl);
                    return true;
                }
            }
            // let's create the instance of our object to work with
            if (documentContents.Length > 0)
                instanciatedType = JsonConvert.DeserializeObject(documentContents, currentType);
            else
                instanciatedType = Activator.CreateInstance(currentType);
            //We need to set the controller to the resource
            APSC apsc = new APSC(request);
            // set the instanceid so we can call back to the APSC
            apsc.InstanceId = (string)request.Headers["APS-Instance-ID"];
            //currentType.GetProperty("APSC").SetValue(instanciatedType, apsc);

            // we are performing a link.
            if(currentProperty != null)
            {
                object currentResource = Activator.CreateInstance(currentType);
                currentProperty.SetValue(instanciatedType, currentResource);
                return true;
            }
            // currentMethod is initialized, so POA is calling the custom method.
            else if (currentMethod != null)
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
                    // since we will have more methods that can be async we need to take care of it
                    // instead of harcoding the Async only for the provision we will asume that all can be async, so when the new async
                    // methods will arrive the Runtime is already prepared for it.
                    if (request.Headers.AllKeys.Contains<string>(APSCHeaders.APSRequestPhase) && request.Headers[APSCHeaders.APSRequestPhase] == "async")
                        methodName = method + "Async";

                    method = currentType.GetMethod(methodName);
                    if(method != null)
                        method.Invoke(instanciatedType, null);
                }
                catch (Exception e)
                {
                    string failedMessage = string.Format("Failed to invoke method '{0}' for class '{1}.{2}', with message: ", methodName, currentType.Namespace, currentType.Name);
                    if (e.InnerException.GetType() == typeof(APSException))
                    {
                        exception = new APSException(failedMessage + e.Message, (APSException)e.InnerException);
                    }
                    // this means we are going do to async operations
                    // this is not really one exception but we need to handle it.
                    else if(e.InnerException.GetType() == typeof(APSAsync))
                    {
                        exception = (APSAsync)e.InnerException;
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
                Where(m => m.GetCustomAttributes<OperationAttribute>(false).Count() > 0).
                // The method path property must be the same as the requested custom method name
                Where(n => ((OperationAttribute)n.GetCustomAttribute<OperationAttribute>()).Path.ToLower() == "/" + customMethodName).
                // Just to make sure we get them all.
                ToArray();
            if (customMethods.Length == 0)
                throw new Exception(string.Format("Method {0} was not found in class {1}.", customMethodName, currentType.Name));

            // we iterate through all the methods, but it should only have one.
            foreach (MethodInfo customMethod in customMethods)
            {
                // Let's get the Method attribute
                OperationAttribute customMethodAttrib = customMethod.GetCustomAttribute<OperationAttribute>(false);
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
                        if (paramAttrib.Kind == ParamSource.Query)
                        {
                            if (!parameters.ContainsKey(paramAttrib.Name))
                                throw new Exception(string.Format("No input parameter with the name {0} was received to perform the request.", paramAttrib.Name));
                            parametersInput.Add(parameters[paramAttrib.Name]);
                        }
                        else if (paramAttrib.Kind == ParamSource.Body)
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
