using EmployeeVMTut.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeVMTut.ViewModel
{
    public class JobPostViewModel
    {
        public JobPost JobPost { get; set; }
        public IEnumerable<SelectListItem> AllJobTags { get; set; }

        private List<int> _selectedJobTags;
        public List<int> selectedJobTags
        {
            get
            {
                if(_selectedJobTags == null)
                {
                    _selectedJobTags = JobPost.JobTags.Select(m => m.ID).ToList();
                }
                return _selectedJobTags;
            }
            set { _selectedJobTags = value; }
        }
    }
}