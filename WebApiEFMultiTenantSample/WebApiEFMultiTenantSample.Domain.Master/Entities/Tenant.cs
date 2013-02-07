using GenericRepository;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiEFMultiTenantSample.Domain.Master.Entities {

    public class Tenant : IEntity<Guid> {

        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    }
}