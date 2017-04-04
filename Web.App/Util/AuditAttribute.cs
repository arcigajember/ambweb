using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Web.DataLayer.Repositories;
using Web.Models.Tables;

namespace Web.App.Util
{
    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;

            var paramsString = JsonConvert.SerializeObject(filterContext.ActionParameters, Formatting.Indented);

            var audit = new AuditModel
            {
                AuditId = Guid.NewGuid(),
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                AreaAccessed = request.RawUrl,
                Timeaccessed = DateTime.UtcNow,
                IPAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                Parameters = paramsString
            };
            if (audit != null)
            {
                var repo = new AuditLogRepository();
                repo.Insert(audit);
            }
            base.OnActionExecuting(filterContext);
        }

    }
}