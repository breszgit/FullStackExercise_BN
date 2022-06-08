using System;
using System.Collections.Generic;

namespace backend.Models.DB
{
    public partial class SysToken
    {
        /// <summary>
        /// UserID
        /// </summary>
        public string TknUid { get; set; }
        public string TknToken { get; set; }
        public DateTime? TknCreatedate { get; set; }
        public DateTime? TknExpire { get; set; }

        // public virtual SysUser TknU { get; set; }
    }
}
