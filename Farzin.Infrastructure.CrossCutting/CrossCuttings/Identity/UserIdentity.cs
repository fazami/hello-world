using System;
using System.Collections.Generic;

namespace Farzin.Infrastructure.CrossCutting.Identity
{
    public class UserIdentity
    {
        public UserIdentity()
        {
            Roles = new List<Role>();
        }
        public Guid Id { get; set; }
        public long MessengerId { get; set; }
        public List<Role> Roles { get; set; }
        public bool IsActive { get; set; }
        public int PersonelCode { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }
}
