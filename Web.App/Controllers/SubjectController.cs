using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.DataLayer.Repositories;
using Web.Models;
using Web.Models.Tables;
using Web.Models.Tables.Interfaces;

namespace Web.App.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        private readonly SubjectRepository _subjectRepo;

        public SubjectController()
        {
            _subjectRepo = new SubjectRepository();
        }

        // GET: Subject
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.SubjectFilter = searchString;

            IEnumerable<Subject> subject = await _subjectRepo.SelectAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                subject = subject.AsQueryable()
                    .Where(m => m.SubjectName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1)
                    .Select(s => s);
            }

            const int pageSize = 8;
            int pageNumber = (page ?? 1);

            return PartialView("Index",subject.ToPagedList(pageNumber, pageSize));
        }

        // GET: Subject/Details/5
        public async Task<ActionResult>Details(int? id)
        {
            MessageAlert messageAlert;
            if(id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Invalid Request",
                    ControllerName = "Subject"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Subject subject = await _subjectRepo.SelectById(id);

            if(subject == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Cannot found!",
                    ControllerName = "Subject"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            return PartialView(subject);
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        public async Task<ActionResult>Create([Bind(Include = "SubjectName, Description")]Subject subject)
        {
            MessageAlert messageAlert;
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    subject.IsActive = true;
                    await _subjectRepo.Insert(subject);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Save",
                        Message = "new record has been saved",
                        ControllerName = "Subject"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
                else
                {
                    messageAlert = new MessageAlert
                    {
                        Status = "error",
                        Title = "Save",
                        Message = "Please double check the data you've entered!",
                        ControllerName = "Subject"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persist please see your system administrator.");
            }
            messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Unable to save changes",
                Message = "Try again, and if the problem persist please see your system administrator",
                ControllerName = "Subject"
            };
            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");
        }

        // GET: Subject/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            MessageAlert messageAlert;
            if(id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Edit",
                    Message = "Invalid Request",
                    ControllerName = "Subject"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            Subject subject = await _subjectRepo.SelectById(id);

            if(subject == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Edit",
                    Message = "Cannot found!",
                    ControllerName = "Subject"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            return PartialView(subject);
        }

        // POST: Subject/Edit/5
        [HttpPost, ActionName("Edit")]
        public async Task<ActionResult> Edit()
        {
            MessageAlert messageAlert;
            Subject subject = new Subject();
            try
            {
                // TODO: Add update logic here
                TryUpdateModel<ISubject>(subject);
                await _subjectRepo.Update(subject);

                messageAlert = new MessageAlert
                {
                    Status = "success",
                    Title = "modified",
                    Message = subject.SubjectName,
                    ControllerName = "Subject"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index","DataManage");
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes, Try again, and if the problem persist please see your system administrator.");
            }

            messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Unable to save changes",
                Message = "Try again, and if the problem persist please see your system administrator",
                ControllerName = "Subject"
            };
            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");
        }

        // GET: Subject/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            MessageAlert messageAlert;
            if(id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Delete",
                    Message = "Invalid Request",
                    ControllerName = "Subject"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Subject subject = await _subjectRepo.SelectById(id);

            if(subject == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Delete failed",
                    Message = string.Format("Cannot find this id {0}, Maybe its already been deleted", id),
                    ControllerName = "Subject"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            return PartialView(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeletePost(int? id)
        {
            try
            {
                // TODO: Add delete logic here
                MessageAlert messageAlert;
                Subject subject = await _subjectRepo.SelectById(id);
                if(subject != null)
                {
                    await _subjectRepo.Delete(id);
                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Delete",
                        Message = subject.SubjectName,
                        ControllerName = "Subject"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
            }
            catch(DataException)
            {
                MessageAlert messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Delete failed",
                    Message = "Try again, and if the problem persist please see your system administrator.",
                    ControllerName = "Guardian"
                };
                TempData["messageAlert"] = messageAlert;
            }
            return RedirectToAction("Index", "DataManage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _subjectRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
