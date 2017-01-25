using System.Collections.Generic;

namespace EmployeeVMTut.Models
{
    public class Employee
    {
        public Employee()
        {
            JobPost = new List<JobPost>();
        }
        public int ID { get; set; }
        public string FullName { get; set; }
        
        public virtual ICollection<JobPost> JobPost { get; set; }
    }
}