using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Infrastructure.Entities
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Summary { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
