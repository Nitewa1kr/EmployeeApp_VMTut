using System.Collections.Generic;

namespace EmployeeVMTut.Models
{
    public class JobTag
    {
        public JobTag()
        {
            this.JobPost = new HashSet<JobPost>();
        }
        public int ID { get; set; }
        public string Tag { get; set; }
        
        public virtual ICollection<JobPost> JobPost { get; set; }
    }
}