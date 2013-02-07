using GenericRepository.EntityFramework;
using System.Data.Entity;

namespace WebApiEFMultiTenantSample.Domain.Master.Entities {
    
    public class MasterEntities : EntitiesContext {

        public MasterEntities()
            : base("MultiTenantSample.Master") { 
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}