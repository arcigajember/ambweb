using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Web.App.Reports;
using Web.App.Util;

namespace Web.App.Controllers
{
    public class AttendanceReportingController : Controller
    {
        // GET: AttendanceReporting
        [Authorize]
        public ActionResult Index(int? sectionId, string dateFrom, string dateTo)
        {
            try
            {
                var rpt = new AttendancePerSection();
                rpt.Load();
                rpt.SetParameterValue("@SectionId", sectionId);
                rpt.SetParameterValue("@DateFrom", dateFrom);
                rpt.SetParameterValue("@DateTo", dateTo);
                rpt.SetDatabaseLogon("sa", "1234567");
                Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

                return File(s, "application/pdf");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}   