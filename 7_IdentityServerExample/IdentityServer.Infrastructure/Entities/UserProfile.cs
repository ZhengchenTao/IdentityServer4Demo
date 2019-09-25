using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Infrastructure.Entities
{
    public class UserProfile
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int? Sex { get; set; }
        public DateTime? Brithday { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
