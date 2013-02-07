using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using WebApiEFMultiTenantSample.Domain.Master.Entities;

namespace WebApiEFMultiTenantSample.Api.WebHost.MessageHandlers {
    
    public class TenantIdentifierMessageHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            HttpContextBase httpContext = request.Properties["MS_HttpContext"] as HttpContextBase;
            MasterEntities entitiesCtx = new MasterEntities();
            IEntityRepository<Tenant, Guid> tenantRepository = new EntityRepository<Tenant, Guid>(entitiesCtx);

            ValidateRequest(httpContext, tenantRepository);

            var tenant = httpContext.Items["App:Tenant"] as string;
            if (tenant == null) {

                return Task.FromResult(request.CreateResponse(HttpStatusCode.NotFound));
            }

            return base.SendAsync(request, cancellationToken);
        }

        public void ValidateRequest(HttpContextBase httpContext, IEntityRepository<Tenant, Guid> tenantRepo) {

            object tenantName;
            httpContext.Request.RequestContext.RouteData.Values.TryGetValue("tenant", out tenantName);
            if (tenantName != null) {

                string tenantNameStr = tenantName.ToString();
                Tenant tenant = tenantRepo.FindBy(x => x.Name == tenantNameStr).FirstOrDefault();
                if (tenant != null) {

                    httpContext.Items["App:Tenant"] = tenant.Name;
                    return;
                }
            }
        }
    }
}