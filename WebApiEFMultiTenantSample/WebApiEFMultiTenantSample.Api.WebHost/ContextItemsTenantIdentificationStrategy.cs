using Autofac.Extras.Multitenant;
using System.Web;

namespace WebApiEFMultiTenantSample.Api.WebHost {

    public class ContextItemsTenantIdentificationStrategy : ITenantIdentificationStrategy {

        // TODO: Wrote in a hurry. A few stuff missing.

        private string _parameterName;
        public ContextItemsTenantIdentificationStrategy(string parameterName) {

            _parameterName = parameterName;
        }

        public bool TryIdentifyTenant(out object tenantId) {

            var context = HttpContext.Current;

            try {
                if (context == null || context.Request == null) {
                    tenantId = null;
                    return false;
                }
            }
            catch (HttpException) {

                // This will happen at application startup in MVC3
                // integration since the ILifetimeScopeProvider tries
                // to be resolved from the container at the point where
                // a new AutofacDependencyResolver is created.
                tenantId = null;
                return false;
            }

            tenantId = context.Items[_parameterName];
            return tenantId != null;
        }
    }
}