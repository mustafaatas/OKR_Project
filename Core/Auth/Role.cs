using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Auth
{
    public class Role: IdentityRole<Guid>
    {
        public List<User>? UserList { get; set; }
    }
}
