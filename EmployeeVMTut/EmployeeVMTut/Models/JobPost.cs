using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeVMTut.Models
{
    public class JobPost
    {
        public JobPost()
        {
            this.JobTags = new HashSet<JobTag>();
        }
        public int ID { get; set; }
        public string Title { get; set; }

        public int EmpRefID { get; set; }

        [ForeignKey("EmpRefID")]
        public virtual Employee Employee { get; set; }

        public virtual ICollection<JobTag> JobTags { get; set; }
    }
}