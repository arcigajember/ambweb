using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using Web.DataLayer.Repositories;
using Web.Models;
using Web.Models.ModelView;
using Web.Models.ModelView.Interfaces;
using Web.Models.Tables;
using Web.App.Util;

namespace Web.App.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly StudentRepository _studentRepo;
        private readonly GuardianRepository _guardianRepo;
        private readonly SectionRepository _sectionRepo;

        public StudentController()
        {
            _studentRepo = new StudentRepository();
            _guardianRepo = new GuardianRepository();
            _sectionRepo = new SectionRepository();
        }
        // GET: Student
        public async Task<PartialViewResult> Index(string searchString, string currentFilter, int? page)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.StudentFilter = searchString;

            IEnumerable<Student> student = await _studentRepo.SelectAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                student =
                    student.AsQueryable()
                        .Where(
                            m =>
                                m.FirstName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                m.LastName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1)
                        .Select(s => s);
            }

            const int pageSize = 8;
            var pageNumber = (page ?? 1);

            return PartialView("Index", student.ToPagedList(pageNumber, pageSize));
        }

        // GET: Student/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            MessageAlert messageAlert;
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Invalid Request",
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }


            Student student = await _studentRepo.SelectById(id);

            if (student == null)
            {
                //return HttpNotFound("Student not found!");
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Cannot found!",
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            return PartialView(student);
        }

        // GET: Student/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var id = Convert.ToString(await _studentRepo.GetIdentity());
            string code = null;

            switch (id.Length)
            {
                case 1:
                    code = "STU000" + id;
                    break;
                case 2:
                    code = "STU00" + id;
                    break;
                case 3:
                    code = "STU0" + id;
                    break;
                case 4:
                    code = "STU" + id;
                    break;
            }

            ViewBag.StudentCode = code;
            PopulateDropdownList();

            return PartialView();
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> Create([Bind(Include = "StudentNumber, FirstName, LastName, MiddleName, Street, Barangay, Municipality, Province, ContactNumber, Status, Gender")]Student student)
        {
            MessageAlert messageAlert;
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    student.IsActive = true;
                    await _studentRepo.Insert(student);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Save",
                        Message = "new record has been saved",
                        ControllerName = "Student"
                    };

                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Save",
                    Message = "Please double check the data you've entered!",
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persist see system administrator.");
            }

            messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Unable to save changes",
                Message = "Try again, and if the problem persist please see your system administrator.",
                ControllerName = "Student"
            };

            TempData["messageAlert"] = messageAlert;

            return RedirectToAction("Index", "DataManage");
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            MessageAlert messageAlert;
            if (id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Edit",
                    Message = "Invalid Request",
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }


            //Student student = await _studentRepo.SelectById(id);

            StudentDetailsView modelView = await _studentRepo.StudentDetails(id);

            if (modelView.Student == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Edit",
                    Message = "Cannot found",
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            PopulateDropdownList();
            return PartialView(modelView);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> Edit(StudentDetailsView m)
        {
            MessageAlert messageAlert;
            StudentDetailsView modelView = new StudentDetailsView();
            try
            {

                if (ModelState.IsValid)
                {
                    TryUpdateModel<IStudentDetailsView>(modelView);
                    await _studentRepo.Update(modelView.Student);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "modified",
                        Message = string.Format("{0} {1}", modelView.Student.FirstName, modelView.Student.LastName),
                        ControllerName = "Student"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }

            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persist, please see your system administrator.");

            }
            messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Unable to save changes",
                Message = "Try again, and if the problem persist please see your system administrator.",
                ControllerName = "Student"
            };

            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");

        }

        // GET: Student/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            MessageAlert messageAlert;
            if (id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Delete",
                    Message = "Invalid Request",
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Student student = await _studentRepo.SelectById(id);

            if (student == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete failed",
                    Message = string.Format("Cannot find this id {0}, Maybe its already been deleted", id),
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            return PartialView(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> DeletePost(int? id)
        {

            try
            {
                // TODO: Add delete logic here
                MessageAlert messageAlert;
                Student student = await _studentRepo.SelectById(id);
                if (student != null)
                {
                    await _studentRepo.Delete(id);
                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Delete",
                        Message = string.Format("{0} {1}", student.FirstName, student.LastName),
                        ControllerName = "Student"
                    };

                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete failed",
                    Message = string.Format("Cannot find this Id {0}, Maybe its already been deleted", id),
                    ControllerName = "Student"
                };

                TempData["messageAlert"] = messageAlert;

            }
            catch (DataException)
            {
                //return RedirectToAction("Delete", new { id, saveChangesError = true });

                MessageAlert messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Delete failed",
                    Message = "Try again, and if the problem persist please see your system administrator.",
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
            }

            return RedirectToAction("Index", "DataManage");
        }
        
        public async Task<ActionResult> GuardianSelectAll(string searchString, string currentFilter, int? page)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.GuardianPartialFilter = currentFilter;

            IEnumerable<Guardian> guardian = await _guardianRepo.SelectAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                guardian = guardian.AsQueryable()
                    .Where(m => m.FirstName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                m.LastName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1)
                    .Select(s => s);

            }

            const int pageSize = 8;
            int pageNumber = (page ?? 1);

            return PartialView("GuardianSelectAll", guardian.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult> GuardianCreate(StudentView model)
        {
            try
            {
                MessageAlert messageAlert;
                if (model.StudentId == null)
                {
                    messageAlert = new MessageAlert
                    {
                        Status = "error",
                        Title = "Guardian Create",
                        Message = "Invalid Request",
                        ControllerName = "Student"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
                if (model.GuardianId == null)
                {
                    messageAlert = new MessageAlert
                    {
                        Status = "error",
                        Title = "Guardian Create",
                        Message = "Invalid Request",
                        ControllerName = "Student"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
                await _studentRepo.InsertStudentGuardian(model);

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                MessageAlert messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Save failed",
                    Message = "Try again, and if the problem persist please see your system administrator.",
                    ControllerName = "Student"
                };
                TempData["messageAlert"] = messageAlert;
            }

            return RedirectToAction("Index", "DataManage");

        }

        [HttpGet]
        public async Task<ActionResult> SectionSelectAll(string searchString, string currentFilter, int? page)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.SectionPartialFilter = searchString;

            IEnumerable<Section> section = await _sectionRepo.SelectAllWithRoom();

            if (!string.IsNullOrEmpty(searchString))
            {
                section = section.AsQueryable()
                    .Where(m => m.SectionName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1)
                    .Select(s => s);
            }

            const int pageSize = 8;
            var pageNumber = (page ?? 1);
            return PartialView(section.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult> StudentSectionCreate(StudentSectionView model)
        {
            await _studentRepo.SectionInsert(model);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> StudentGuardianDelete(int? studentId, int? guardianId)
        {
            try
            {
                await _studentRepo.StudentGuardianDelete(studentId, guardianId);
                return RedirectToAction("Edit", new {id = studentId});
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public void PopulateDropdownList()
        {
            List<SelectListItem> gender = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Male",
                    Value = "Male"
                },
                new SelectListItem
                {
                    Text = "Female",
                    Value = "Female"
                }
            };

            ViewData["gender"] = gender;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _studentRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
