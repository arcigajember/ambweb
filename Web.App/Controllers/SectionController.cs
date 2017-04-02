using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.DataLayer.Repositories;
using Web.DataLayer.Util;
using Web.Models;
using Web.Models.ModelView;
using Web.Models.Tables;

namespace Web.App.Controllers
{
    [Authorize]
    public class SectionController : Controller
    {
        private readonly SectionRepository _sectionRepo;
        private readonly RoomRepository _roomRepo;

        public SectionController()
        {
            _sectionRepo = new SectionRepository();
            _roomRepo = new RoomRepository();
        }

        // GET: Section
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

            ViewBag.SectionFilter = currentFilter;

            IEnumerable<Section> section = await _sectionRepo.SelectAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                section = section.AsQueryable()
                    .Where(m => m.SectionName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1)
                    .Select(s => s);
            }

            const int pageSize = 8;
            int pageNumber = (page ?? 1);

            return PartialView("Index", section.ToPagedList(pageNumber, pageSize));
        }

        // GET: Section/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Section/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SectionName")]Section section)
        {
            MessageAlert messageAlert;
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    section.IsActive = true;
                    await _sectionRepo.Insert(section);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Save",
                        Message = "new record has been saved",
                        ControllerName = "Section"
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
                        ControllerName = "Section"
                    };
                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persist please see your system administrator");
            }

            messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Unable to save changes",
                Message = "Try again, and if the problem persist please see your system administrator",
                ControllerName = "Section"
            };
            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");

        }

        // GET: Section/Edit/5
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
                    ControllerName = "Section"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Section section = await _sectionRepo.SectionSubjectSelectById(id);

            if (section == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Edit",
                    Message = "Cannot found!",
                    ControllerName = "Section"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            SectionView modelView = new SectionView(await _roomRepo.SelectAll())
            {
                Section = section,
                Subject = section.Subject
            };
            return PartialView(modelView);
        }

        // POST: Section/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SectionCreateView modelView)
        {
            try
            {
                var selectedSubjectId = modelView.Subject.ToList()
                    .Where(s => s.IsSelected)
                    .Select(s => s.SubjectId);

                if (selectedSubjectId.IsAny())
                {
                    foreach (var id in selectedSubjectId)
                    {
                        await _sectionRepo.SectionSubjectInsert(modelView.Section.SectionId, id);
                    }
                }
                await _sectionRepo.SectionRoomInsert(modelView.Section.SectionId, modelView.Section.RoomId);
                MessageAlert messageAlert = new MessageAlert
                {
                    Status = "success",
                    Title = "Edit",
                    Message = "Subject and room has been saved",
                    ControllerName = "Section"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            catch (Exception)
            {
                MessageAlert messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Assign",
                    Message = "Unable to save data",
                    ControllerName = "Section"
                };
                TempData["messageAlert"] = messageAlert;
            }
            return RedirectToAction("Index", "DataManage");
        }

        // GET: Section/Delete/5
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
                    ControllerName = "Section"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Section section = await _sectionRepo.SelectById(id);

            if (section == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete failed",
                    Message = string.Format("Cannot find this id {0}, Maybe its already been deleted", id),
                    ControllerName = "Section"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            return PartialView(section);
        }

        // POST: Section/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int? id)
        {
            try
            {
                // TODO: Add delete logic here
                Section section = await _sectionRepo.SelectById(id);
                if (section != null)
                {
                    await _sectionRepo.Delete(id);
                    var messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Delete",
                        Message = section.SectionName,
                        ControllerName = "Section"
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
                    ControllerName = "Section"
                };
                TempData["messageAlert"] = messageAlert;
            }
            return RedirectToAction("Index", "DataManage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sectionRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
