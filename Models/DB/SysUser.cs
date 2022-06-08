using System;
using System.Collections.Generic;

namespace backend.Models.DB
{
    public partial class SysUser
    {
        // public SysUser()
        // {
        //     DatTaskDtkAssignToUs = new HashSet<DatTask>();
        //     DatTaskDtkCreateByUs = new HashSet<DatTask>();
        // }

        /// <summary>
        /// UserID
        /// </summary>
        public string UsrUid { get; set; }
        public string UsrUsername { get; set; }
        public string UsrPwd { get; set; }
        public string UsrRole { get; set; }
        public string UsrFirstname { get; set; }
        public string UsrLastname { get; set; }
        public DateTime? UsrCreatedate { get; set; }

        // public virtual SysRole UsrRoleNavigation { get; set; }
        // public virtual SysToken SysToken { get; set; }
        // public virtual ICollection<DatTask> DatTaskDtkAssignToUs { get; set; }
        // public virtual ICollection<DatTask> DatTaskDtkCreateByUs { get; set; }
    }
}
