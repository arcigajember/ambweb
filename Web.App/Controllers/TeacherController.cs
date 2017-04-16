using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.App.Util;
using Web.DataLayer.Repositories;
using Web.Models;
using Web.Models.ModelView;
using Web.Models.Tables;
using Web.Models.Tables.Interfaces;

namespace Web.App.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly TeacherRepository _teacherRepo;
        private readonly SubjectRepository _subjectRepo;
        private readonly SectionRepository _sectionRepo;

        public TeacherController()
        {
            _teacherRepo = new TeacherRepository();
            _subjectRepo = new SubjectRepository();
            _sectionRepo = new SectionRepository();
        }

        // GET: Teacher
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
            ViewBag.TeacherFilter = searchString;

            IEnumerable<Teacher> teacher = await _teacherRepo.SelectAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                teacher = teacher.AsQueryable()
                    .Where(m => m.FirstName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                m.LastName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1)
                    .Select(s => s);
            }

            const int pageSize = 8;
            var pageNumber = (page ?? 1);

            return PartialView("Index", teacher.ToPagedList(pageNumber, pageSize));
        }

        // GET: Teacher/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            MessageAlert messageAlert;
            if (id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Invalid request",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;

                return RedirectToAction("Index", "DataManage");
            }
            Teacher teacher = await _teacherRepo.SelectById(id);

            if (teacher == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Cannot found!",
                    ControllerName = "Teacher"
                };

                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            return PartialView(teacher);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Teacher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> Create([Bind(Include = "FirstName, LastName, MiddleName, Address")]Teacher teacher)
        {
            MessageAlert messageAlert;
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    teacher.IsActive = true;
                    await _teacherRepo.Insert(teacher);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Save",
                        Message = "new record has been saved",
                        ControllerName = "Teacher"
                    };

                    TempData["messageAlert"] = messageAlert;

                    return RedirectToAction("Index", "DataManage");
                }
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Save",
                    Message = "Please double check the data you've entered",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persist see systemd administrator.");
            }

            messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Unable to save changes",
                Message = "Try again, and if the problem persist",
                ControllerName = "Teacher"
            };
            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");
        }

        // GET: Teacher/Edit/5
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
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Teacher teacher = await _teacherRepo.SelectById(id);

            if (teacher == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Edit",
                    Message = "Cannot found",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            return PartialView(teacher);
        }

        // POST: Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> Edit(Teacher m)
        {
            MessageAlert messageAlert;
            Teacher teacher = new Teacher();
            try
            {
                // TODO: Add update logic here
                TryUpdateModel<ITeacher>(teacher);
                await _teacherRepo.Update(teacher);

                messageAlert = new MessageAlert
                {
                    Status = "success",
                    Title = "modified",
                    Message = string.Format("{0} {1}", teacher.FirstName, teacher.LastName),
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persist, please see your system administrator.");
            }

            messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Unable to save changes",
                Message = "Try again, and if the problem persist please see your system administrator",
                ControllerName = "Teacher"
            };
            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");
        }

        // GET: Teacher/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            MessageAlert messageAlert;
            if (id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete",
                    Message = "Invalid Request",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;

                return RedirectToAction("Index", "DataManage");
            }
            Teacher teacher = await _teacherRepo.SelectById(id);

            if (teacher == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete failed",
                    Message = string.Format("Cannot find thie id {0}, Maybe its already been deleted", id),
                    ControllerName = "Teacher"
                };

                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            return PartialView(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> DeletePost(int? id)
        {
            try
            {
                // TODO: Add delete logic here
                Teacher teacher = await _teacherRepo.SelectById(id);
                if (teacher != null)
                {
                    await _teacherRepo.Delete(id);
                    MessageAlert messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Delete",
                        Message = string.Format("{0} {1}", teacher.FirstName, teacher.LastName),
                        ControllerName = "Teacher"
                    };

                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
            }
            catch (DataException)
            {
                MessageAlert messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Delete failed",
                    Message = "Try again, and if the problem persist please see your system administrator",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;
            }
            return RedirectToAction("Index", "DataManage");
        }
        
        public async Task<ActionResult> Schedule(int? id)
        {
            MessageAlert messageAlert;
            if (id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete",
                    Message = "Invalid Request",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;

                return RedirectToAction("Index", "DataManage");
            }

            Teacher teacher = await _teacherRepo.SelectById(id);

            if (teacher == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete failed",
                    Message = string.Format("Cannot find thie id {0}, Maybe its already been deleted", id),
                    ControllerName = "Teacher"
                };

                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            TeacherScheduleView modelView = new TeacherScheduleView
            {
                Teacher = teacher,
                SubjectSectionView = await _teacherRepo.SelectDetailsById(id)
            };


            return PartialView(modelView);
        }
        
        public async Task<ActionResult> ScheduleAssign(int? id)
        {
            MessageAlert messageAlert;
            if (id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "ScheduleAssign",
                    Message = "Invalid Request",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Teacher teacher = await _teacherRepo.SelectById(id);

            if (teacher == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Assign",
                    Message = "Cannot found",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            TeacherAssignView modelView = new TeacherAssignView(await _sectionRepo.SelectAllWithRoom())
            {
                Teacher = teacher,
                Subject = await _subjectRepo.SelectAll()
            };

            return PartialView(modelView);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> ScheduleAssign(TeacherSaveSchedule model)
        {
            MessageAlert messageAlert;
            try
            {
                if (model.Teacher.TeacherId == null)
                {
                    messageAlert = new MessageAlert
                    {
                        Status = "error",
                        Title = "ScheduleAssign",
                        Message = "Invalid Request",
                        ControllerName = "Teacher"
                    };
                    TempData["messageAlert"] = messageAlert;

                    return RedirectToAction("Index", "DataManage");
                }

                Teacher teacher = await _teacherRepo.SelectById(model.Teacher.TeacherId);

                if (teacher == null)
                {
                    messageAlert = new MessageAlert
                    {
                        Status = "error",
                        Title = "Assign",
                        Message = "Cannot found",
                        ControllerName = "Teacher"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }

                var count = model.Subject
                    .Count(s => s.IsSelected);

                if (count > 0)
                {
                    await _teacherRepo.InsertSchedule(model);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Schedule",
                        Message = "new record has been saved",
                        ControllerName = "Teacher"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Schedule",
                    Message = "must select atleat one subject",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            catch (Exception)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Unable to save changes",
                    Message = "Try again, and if the problem persist please see your system administrator",
                    ControllerName = "Teacher"
                };
                TempData["messageAlert"] = messageAlert;

            }
            return RedirectToAction("Index", "DataManage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _teacherRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
