using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.App.Models;
using Web.DataLayer.Repositories;
using Web.DataLayer.Util;
using Web.Models;
using Web.Models.ModelView;
using Web.Models.Tables;
using System.Security.Claims;

namespace Web.App.Controllers
{
    [Authorize]
    public class DataManageController : Controller
    {
        private readonly SectionRepository _sectionRepo;
        private readonly MessageRepository _messageRepo;

        public DataManageController()
        {
            _messageRepo = new MessageRepository();
            _sectionRepo = new SectionRepository();
        }
        // GET: DataManage
        public ActionResult Index()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);  // HTTP 1.1.
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
            MessageAlert alert = TempData["messageAlert"] as MessageAlert;
            if (alert != null)
            {
                return PartialView("DataNotify", alert);
            }
            return View();
        }

        public async Task<ActionResult> Message()
        {
            IEnumerable<Section> section = await _sectionRepo.SelectAllWithRoom();
            MessageView modelView = new MessageView
            {
                SectionList = new MultiSelectList(section, "SectionId", "SectionName")
            };

            return PartialView(modelView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Message(MessageView model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser member =
                        await
                            System.Web.HttpContext.Current.GetOwinContext()
                                .GetUserManager<ApplicationUserManager>()
                                .FindByIdAsync(System.Web.HttpContext.Current.User.Identity.GetUserId());

                    await Task.Factory.StartNew(async () =>
                    {
                        foreach (int t in model.SectionId)
                        {

                            IEnumerable<GuardianContact> contacts = await _messageRepo.GuardianContact(t);
                            var guardianContacts = contacts as IList<GuardianContact> ?? contacts.ToList();

                            if (guardianContacts.IsAny())
                            {

                                foreach (var s in guardianContacts)
                                {
                                    string response = await _messageRepo.SendMessage(s.ContactNumber, model.TextMessage);
                                    SmsDetails modelDetails = new SmsDetails
                                    {
                                        SmsType = "manual",
                                        StudentGuardianId = s.StudentGuardianId,
                                        AspNetUserId = member.Id,
                                        DateSent = DateTime.Now,
                                        Status = response,
                                        Message = model.TextMessage
                                    };
                                    await _messageRepo.InsertSmsDetails(modelDetails);

                                }


                            }
                        }
                    });
                    return PartialView("Success");
                }
            }
            catch (Exception)
            {
                return PartialView("Error");
            }
            return PartialView("Error");
        }
        public async Task<ActionResult> Login(string returnUrl)
        {
            ApplicationUser member =
                  await
                      System.Web.HttpContext.Current.GetOwinContext()
                          .GetUserManager<ApplicationUserManager>()
                          .FindByIdAsync(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string claimRole = null;
            if (member != null)
            {

                claimRole = member.Claims.AsQueryable()
                    .Where(s => s.ClaimValue.IndexOf("Admin", StringComparison.OrdinalIgnoreCase) != -1)
                    .Select(s => s.ClaimValue)
                    .FirstOrDefault();
            }
            var claims = new[]
            {
                new Claim("name", member == null ? "none" : member.UserName),
                new Claim("role", claimRole ?? "user")
            };
            var id = new ClaimsIdentity(claims, "Cookies");
            Request.GetOwinContext().Authentication.SignIn(id);
            return Redirect(returnUrl);
        }
    }
}