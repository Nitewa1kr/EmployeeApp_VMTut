using System.Collections.Generic;

namespace EmployeeVMTut.Models
{
    public class JobPost
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<JobTag> JobTag { get; set; }
    }
}