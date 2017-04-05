using PagedList;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Web.App.Util;
using Web.DataLayer.Repositories;

namespace Web.App.Controllers
{
    [Authorize]
    public class AuditController : Controller
    {
        private readonly AuditLogRepository _auditRepo;

        public AuditController()
        {
            _auditRepo = new AuditLogRepository();
        }

        //TODO: Need to add UI
        // GET: Audit
        [Audit]
        public async Task<ActionResult> Index(DateTime? searchFrom, DateTime? searchTo, DateTime? currentSearchFrom, DateTime? currentSearchTo, int? page)
        {
            if(searchFrom != null && searchTo != null)
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

            const int pageSize = 8;
            int pageNumber = (page ?? 1);

            return PartialView("Index", auditList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Audit/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Audit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Audit/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Audit/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Audit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Audit/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Audit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
