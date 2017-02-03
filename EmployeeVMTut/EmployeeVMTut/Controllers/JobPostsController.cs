using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EmployeeVMTut.Models;
using EmployeeVMTut.ViewModel;

/*TODO: CREATE INITIAL DATA FOR YOUR APPLICATION AND THE DROPDOWN LISTS*/

namespace EmployeeVMTut.Controllers
{
    public class JobPostsController : Controller
    {
        private EmployeeContext db = new EmployeeContext();

        // GET: JobPosts
        public ActionResult Index()
        {
            var jobPosts = db.JobPosts.Include(j => j.Employee);
            return View(jobPosts.ToList());
        }

        // GET: JobPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // GET: JobPosts/Create
        public ActionResult Create()
        {
            ViewBag.EmpRefID = new SelectList(db.Employees, "ID", "FullName");
            return View();
        }

        // POST: JobPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,EmpRefID")] JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                db.JobPosts.Add(jobPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpRefID = new SelectList(db.Employees, "ID", "FullName", jobPost.EmpRefID);
            return View(jobPost);
        }

        // GET: JobPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //JobPost jobPost = db.JobPosts.Find(id);
            //if (jobPost == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.EmpRefID = new SelectList(db.Employees, "ID", "FullName", jobPost.EmpRefID);
            //return View(jobPost);

            var jobPostViewModel = new JobPostViewModel
            {
                JobPost = db.JobPosts.Include(i => i.JobTags).First(i => i.ID == id),
            };
            if(jobPostViewModel.JobPost == null)
            {
                return HttpNotFound();
            }
            var allJobTagsList = db.JobTags.ToList();
            jobPostViewModel.AllJobTags = allJobTagsList.Select(o => new SelectListItem
            {
                Text = o.Tag,
                Value = o.ID.ToString()
            });
            ViewBag.EmpRefID = new SelectList(db.Employees, "ID", "FullName", jobPostViewModel.JobPost.EmpRefID);
            return View(jobPostViewModel);
        }

        // POST: JobPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JobPostViewModel JobPostVM)
        {
            if (JobPostVM == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            if (ModelState.IsValid)
            {
				var jobToUpdate = db.JobPosts
                    .Include(i => i.JobTags).First(i => i.ID == JobPostVM.JobPost.ID);

	            if (TryUpdateModel(jobToUpdate,"JobPost",new string[]{"Title","EmpRefID"} ))
	            {
		            var newJobTags = db.JobTags.Where(
                        m => JobPostVM.selectedJobTags.Contains(m.ID)).ToList();
                    var updatedJobTags = new HashSet<int>(JobPostVM.selectedJobTags);
					foreach (JobTag jobTag in db.JobTags)
					{
						if (!updatedJobTags.Contains(jobTag.ID))
						{
							jobToUpdate.JobTags.Remove(jobTag);
						}
						else
						{
							jobToUpdate.JobTags.Add((jobTag));
						}
					}
					db.Entry(jobToUpdate).State = EntityState.Modified;
					db.SaveChanges();
	            }
			     
                return RedirectToAction("Index");
            }
            ViewBag.EmployerID = new SelectList(db.Employees, "ID", "FullName", JobPostVM.JobPost.EmpRefID);
            return View(JobPostVM);
        }

        // GET: JobPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobPost jobPost = db.JobPosts.Find(id);
            if (jobPost == null)
            {
                return HttpNotFound();
            }
            return View(jobPost);
        }

        // POST: JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobPost jobPost = db.JobPosts.Find(id);
            db.JobPosts.Remove(jobPost);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
