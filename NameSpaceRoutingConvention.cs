using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;

namespace VersionRouting{
    ///<summary>
    /// Grab version number from controller namespace and define route to that controller by combining api prefix, version number and controller name.
    /// </summary> 
    public class NameSpaceRoutingConvention : IApplicationModelConvention {

        private readonly string apiPrefix;
        private const string urlTemplate = "{0}/{1}/{2}";

        public NameSpaceRoutingConvention(IConfigurationSection apiSettings) {
            apiPrefix = apiSettings.GetValue<string>("Prefix");
        }

        public void Apply(ApplicationModel application) {
            foreach (var controller in application.Controllers) {
                var hasRouteAttribute = controller.Selectors.Any(x => x.AttributeRouteModel != null);
                if (hasRouteAttribute) {
                    continue;
                }
                var nameSpace = controller.ControllerType.Namespace.Split('.');
                var version = nameSpace.FirstOrDefault(x => Regex.IsMatch(x, @"[v\d*]"));
                if (string.IsNullOrEmpty(version)) {
                    continue;
                }
                controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel() {
                    Template = string.Format(urlTemplate, apiPrefix, version, controller.ControllerName)
                };
            }
        }
    }
}
