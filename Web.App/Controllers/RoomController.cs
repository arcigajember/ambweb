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
    public class RoomController : Controller
    {
        private readonly RoomRepository _roomRepo;

        public RoomController()
        {
            _roomRepo = new RoomRepository();
        }
        // GET: Room
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
            ViewBag.RoomFilter = currentFilter;

            IEnumerable<Room> room = await _roomRepo.SelectAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                room = room.AsQueryable()
                    .Where(m => m.RoomName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1 ||
                                m.RoomNumber.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) != -1)
                    .Select(s => s);
            }

            const int pageSize = 8;
            int pageNumber = (page ?? 1);

            return PartialView("Index", room.ToPagedList(pageNumber, pageSize));
        }

        // GET: Room/Details/5
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
                    ControllerName = "Room"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Room room = await _roomRepo.SelectById(id);

            if (room == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Details",
                    Message = "Cannot found!",
                    ControllerName = "Room"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            return PartialView(room);
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RoomNumber, RoomName")]Room room)
        {
            MessageAlert messageAlert;
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    room.IsActive = true;
                    await _roomRepo.Insert(room);

                    messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Save",
                        Message = "new record has been saved",
                        ControllerName = "Room"
                    };

                    TempData["messageAlert"] = messageAlert;
                    return RedirectToAction("Index", "DataManage");
                }
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Save",
                    Message = "Please double check the data you've entered!",
                    ControllerName = "Room"
                };

                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
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
                ControllerName = "Room"
            };

            TempData["messageAlert"] = messageAlert;
            return RedirectToAction("Index", "DataManage");
        }

        // GET: Room/Edit/5
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
                    ControllerName = "Room"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            
            Room room = await _roomRepo.SelectById(id);

            if (room == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "error",
                    Title = "Edit",
                    Message = "Cannot found!",
                    ControllerName = "Room"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            return PartialView(room);
        }

        // POST: Room/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit()
        {
            MessageAlert messageAlert;
            Room room = new Room();
            try
            {
                // TODO: Add update logic here

                TryUpdateModel<IRoom>(room);
                await _roomRepo.Update(room);

                messageAlert = new MessageAlert
                {
                    Status = "success",
                    Title = "modified",
                    Message = $"{room.RoomName} {room.RoomNumber}",
                    ControllerName = "Room"
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
                Message = "Try again, and if the problem persist please see your system administrator.",
                ControllerName = "Room"
            };

            TempData["messageAlert"] = messageAlert;

            return RedirectToAction("Index", "DataManage");
        }

        // GET: Room/Delete/5
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
                    ControllerName = "Room"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }

            Room room = await _roomRepo.SelectById(id);

            if (room == null)
            {
                messageAlert = new MessageAlert
                {
                    Status = "info",
                    Title = "Delete failed",
                    Message = $"Cannot find this id {id}, Maybe its already been deleted",
                    ControllerName = "Room"
                };
                TempData["messageAlert"] = messageAlert;
                return RedirectToAction("Index", "DataManage");
            }
            return PartialView(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int? id)
        {
            try
            {
                // TODO: Add delete logic here
                Room room = await _roomRepo.SelectById(id);
                if (room != null)
                {
                    await _roomRepo.Delete(id);
                    var messageAlert = new MessageAlert
                    {
                        Status = "success",
                        Title = "Delete",
                        Message = $"{room.RoomName} {room.RoomNumber}",
                        ControllerName = "Room"
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
                    ControllerName = "Room"
                };
                TempData["messageAlert"] = messageAlert;
            }

            return RedirectToAction("Index", "DataManage");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _roomRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
