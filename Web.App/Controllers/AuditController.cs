﻿using System.Web.Mvc;
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
        public ActionResult Index()
        {
            return View();
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
