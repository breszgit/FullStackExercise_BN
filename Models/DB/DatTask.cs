using System;
using System.Collections.Generic;

namespace backend.Models.DB
{
    public partial class DatTask
    {
        public int DtkId { get; set; }
        public string DtkTaskName { get; set; }
        public int? DtkTktId { get; set; }
        public int? DtkPriority { get; set; }
        public DateTime? DtkCreatedate { get; set; }
        public int? DtkDuration { get; set; }
        public string DtkDurationType { get; set; }
        public string DtkDescription { get; set; }
        public string DtkCreateByUid { get; set; }
        public string DtkAssignToUid { get; set; }
        public string DtkStatus { get; set; }
        public ulong? DtkComplete { get; set; }

        // public virtual SysUser DtkAssignToU { get; set; }
        // public virtual SysUser DtkCreateByU { get; set; }
        // public virtual MstTaskType DtkTkt { get; set; }
    }
}
