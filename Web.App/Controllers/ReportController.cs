using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using Web.DataLayer.Repositories;
using Web.DataLayer.Util;
using Web.Models;
using Web.Models.ModelView;
using Web.Models.Tables;
using System.Web;
using Web.App.Util;
using System;
using System.Net;

namespace Web.App.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly SectionRepository _sectionRepo;
        private readonly AttendanceSectionRepository _attendanceRepo;
        private readonly StudentRepository _studentRepo;
        private readonly AuditLogRepository _auditRepo;

        public ReportController()
        {
            _studentRepo = new StudentRepository();
            _attendanceRepo = new AttendanceSectionRepository();
            _sectionRepo = new SectionRepository();
            _auditRepo = new AuditLogRepository();
        }
        // GET: Report
        [Audit]
        public ActionResult Index()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);  // HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
            return View();
        }

        [Audit]
        public ActionResult Attendance()
        {

            return PartialView();
        }

        [Audit]
        public ActionResult MessageLog()
        {
            var model = new MessageLogView();
            return PartialView(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> MessageLogResult(MessageLogView model)
        {
             if (ModelState.IsValid)
            {
                if (model.SearchId == 1)
                {
                    return PartialView("MessagelogAttendance", await _attendanceRepo.AttendanceLog(model));
                }

                if (model.SearchId == 2)
                {
                    return PartialView("SmsLog", await _attendanceRepo.SmsLog(model));
                }
            }
            MessageAlert messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Message Log",
                Message = "Invalid Request",
                ControllerName = "Reports"
            };
            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");

        }

        [Audit]
        public async Task<ActionResult> SectionOption()
        {
            var modelView = new SectionReportView(await _sectionRepo.SelectAllWithRoom());
            return PartialView(modelView);
        }

        [HttpPost]
        [Audit]
        public async Task<ActionResult> SectionSearch(SectionSearch model, int? page)
        {
            if (!ModelState.IsValid)
            {
                MessageAlert messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Section option",
                    Message = "Invalid Request",
                    ControllerName = "Reports"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            var result = await _attendanceRepo.AttendanceSection(model.SectionId);
            var studentLst = result as IList<Student> ?? result.ToList();
            if (studentLst.IsAny())
            {
                foreach (var student in studentLst)
                {
                    student.AttendanceDetails =
                        await _attendanceRepo.AttendanceStudent(student.StudentId, model.DateFrom, model.DateTo);
                }
            }
            return PartialView(studentLst);
        }

        [Audit]
        public async Task<ActionResult> StudentOption()
        {
            IEnumerable<Student> section = await _studentRepo.SelectAll();
            StudentSearchOption modelView = new StudentSearchOption
            {
                StudentList = new MultiSelectList(section, "StudentId", "FullName")
            };
            return PartialView(modelView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Audit]
        public async Task<ActionResult> StudentSearch(StudentSearchOption modelView)
        {
            if (ModelState.IsValid)
            {
                List<Student> model = new List<Student>();

                foreach (var id in modelView.StudentId)
                {
                    model.Add(await _attendanceRepo.AttendanceReportStudent(id, modelView.DateFrom, modelView.DateTo));
                }

                return PartialView(model);
            }
            MessageAlert messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Search option",
                Message = "Invalid Request",
                ControllerName = "Reports"
            };
            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");
        }


        public async Task<ActionResult> Audit(DateTime? searchFrom, DateTime? searchTo, DateTime? currentSearchFrom, DateTime? currentSearchTo, int? page)
        {
            try
            {
                if (searchFrom != null && searchTo != null)
                {
                    page = 1;
                }
                else
                {
                    searchFrom = currentSearchFrom;
                    searchTo = currentSearchTo;
                }
                ViewBag.SearchFrom = searchFrom;
                ViewBag.SearchTo = searchTo;

                var auditList = await _auditRepo.SelectAll();

                if (searchFrom != null && searchTo != null)
                {
                    auditList = auditList.AsQueryable()
                        .Where(m => m.Timeaccessed.Date >= searchFrom &&
                                  m.Timeaccessed.Date <= searchTo)
                        .Select(s => s);
                }

                const int pageSize = 15;
                int pageNumber = (page ?? 1);

                return PartialView("Audit", auditList.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public async Task<ActionResult> View(Guid? id)
        {
            try
            {
                if(id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var result = await _auditRepo.SelectById(id);
                result.Parameters = result.Parameters
                    .Replace("\r", "<br/>")
                    .Replace("\n", "<br/>")
                    .Replace("\"", "");

                return PartialView(result);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}
