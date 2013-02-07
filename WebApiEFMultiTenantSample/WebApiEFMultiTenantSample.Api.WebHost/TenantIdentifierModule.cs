using GenericRepository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using WebApiEFMultiTenantSample.Domain.Master.Entities;

namespace WebApiEFMultiTenantSample.Api.WebHost {
    
    public class TenantIdentifierModule : IHttpModule {

        public void Init(HttpApplication context) {

            MasterEntities entitiesCtx = new MasterEntities();
            IEntityRepository<Tenant, Guid> tenantRepository = new EntityRepository<Tenant, Guid>(entitiesCtx);
            try {
                ValidateRequest(new HttpContextWrapper(context.Context), tenantRepository);
            }
            catch (HttpException) // will throw at the app start-up
            { 
            }
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

        public void Dispose() { }
    }
}