using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Infrastructure.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(11)]
        public string Phone { get; set; }
        public string Provider { get; set; }
        public string Identity { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
