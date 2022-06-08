using System;
using System.Collections.Generic;

namespace backend.Models.DB
{
    public partial class SysRole
    {
        // public SysRole()
        // {
        //     SysUsers = new HashSet<SysUser>();
        // }

        public string RlsRole { get; set; }

        // public virtual ICollection<SysUser> SysUsers { get; set; }
    }
}
