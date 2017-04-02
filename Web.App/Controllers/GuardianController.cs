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
    public class GuardianController : Controller
    {
        private readonly GuardianRepository _guardianRepo;

        public GuardianController()
        {
            _guardianRepo = new GuardianRepository();
        }

        // GET: Guardian
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
            ViewBag.GuardianFilter = searchString;

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

            return PartialView("Index", guardian.ToPagedList(pageNumber, pageSize));
        }

        // GET: Guardian/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            MessageAlert messageAlert;
            if (id == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Invalid Request",
                    ControllerName = "Guardian"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Guardian guardian = await _guardianRepo.SelectById(id);

            if (guardian == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Cannot found!",
                    ControllerName = "Guardian"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            return PartialView(guardian);
        }

        // GET: Guardian/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Guardian/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FirstName, LastName, MiddleName, Street, Barangay, Municipality, Province, ContactNumber")]Guardian guardian)
        {
            MessageAlert messageAlert;
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    guardian.IsActive = true;
                    await _guardianRepo.Insert(guardian);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Save",
                        Message = "new record has been saved",
                        ControllerName = "Guardian"
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
                        ControllerName = "Guardian"
                    };

                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persist see your system administrator.");
            }

            messageAlert = new MessageAlert
            {
                Status = "error",
                Title = "Unable to save changes",
                Message = "Try again, and if the problem persist please see your system administrator.",
                ControllerName = "Guardian"
            };

            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");
        }

        // GET: Guardian/Edit/5
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
                    ControllerName = "Guardian"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Guardian guardian = await _guardianRepo.SelectById(id);

            if (guardian == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Edit",
                    Message = "Cannot found!",
                    ControllerName = "Guardian"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            return PartialView(guardian);
        }

        // POST: Guardian/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit()
        {
            MessageAlert messageAlert;
            Guardian guardian = new Guardian();
            try
            {
                // TODO: Add update logic here

                if (ModelState.IsValid)
                {
                    TryUpdateModel<IGuardian>(guardian);
                    await _guardianRepo.Update(guardian);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "modified",
                        Message = string.Format("{0} {1}", guardian.FirstName, guardian.LastName),
                        ControllerName = "Guardian"
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
                ControllerName = "Guardian"
            };

            TempData["messageAlert"] = messageAlert;

            return RedirectToAction("Index", "DataManage");
        }

        // GET: Guardian/Delete/5
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
                    ControllerName = "Guardian"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Guardian guardian = await _guardianRepo.SelectById(id);

            if (guardian == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete failed",
                    Message = string.Format("Cannot find this id {0}, Maybe its already been deleted", id),
                    ControllerName = "Guardian"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            return PartialView(guardian);
        }

        // POST: Guardian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int? id)
        {
            try
            {
                // TODO: Add delete logic here
                Guardian guardian = await _guardianRepo.SelectById(id);
                if (guardian != null)
                {
                    await _guardianRepo.Delete(id);
                    var messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Delete",
                        Message = string.Format("{0} {1}", guardian.FirstName, guardian.LastName),
                        ControllerName = "Guardian"
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
                    Message = "Try again, and if the problem persist please see your system administrator.",
                    ControllerName = "Guardian"
                };
                TempData["messageAlert"] = messageAlert;
            }

            return RedirectToAction("Index","DataManage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _guardianRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
