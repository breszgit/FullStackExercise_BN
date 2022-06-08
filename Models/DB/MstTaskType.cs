using System;
using System.Collections.Generic;

namespace backend.Models.DB
{
    public partial class MstTaskType
    {
        // public MstTaskType()
        // {
        //     DatTasks = new HashSet<DatTask>();
        // }

        public int TktId { get; set; }
        public string TktTypeName { get; set; }
        public DateTime? TktCreatedate { get; set; }
        public DateTime? TktLastupdate { get; set; }

        // public virtual ICollection<DatTask> DatTasks { get; set; }
    }
}
