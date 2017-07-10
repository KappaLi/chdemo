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
            using (chdemo.kappaEntities dc = new chdemo.kappaEntities())
            {
                var data = dc.test.OrderBy(a => a.Time).ToList();
                return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Contact()
        {
            List<test> result = new List<test>();

            using (kappaEntities db = new kappaEntities())
            {
                result = (from s in db.test select s).ToList();
                return View(result);
            }
        }

        public ActionResult Suggest()
        {
            return View();
        }



        public ActionResult GetChartData()
        {
            List<test> data = new List<test>();
            //Here MyDatabaseEntities  is our dbContext
            using (kappaEntities dc = new kappaEntities())
            {
                data = dc.test.ToList();
            }


            var dt = new VisualizationDataTable();

            dt.AddColumn("Time", "string");
            dt.AddColumn("Pressure", "number");

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