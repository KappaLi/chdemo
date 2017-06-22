using chdemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace chdemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult loaddata()
        {
            using (chdemo.Database1Entities dc = new chdemo.Database1Entities())
            {
                var data = dc.Table.OrderBy(a => a.Time).ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Contact()
        {
            List<Suggest> result = new List<Suggest>();

            using (Database1Entities2 db = new Database1Entities2())
            {
                result = (from s in db.Suggest select s).ToList();
                return View(result);
            }
        }

        public ActionResult Suggest()
        {
            return View();
        }



        public ActionResult GetChartData()
        {
            List<Table> data = new List<Table>();
            //Here MyDatabaseEntities  is our dbContext
            using (Database1Entities dc = new Database1Entities())
            {
                data = dc.Table.ToList();
            }


            var dt = new VisualizationDataTable();

            dt.AddColumn("Date", "string");
            dt.AddColumn("Events", "number");

            foreach (var item in data)
            {
                dt.NewRow(item.Time, item.Pressure);
            }

            var chart = new ChartViewModel
            {
                Title = "Events",
                Subtitle = "per date",
                DataTable = dt
            };


            return Content(JsonConvert.SerializeObject(chart), "application/json");
        }
    }
}