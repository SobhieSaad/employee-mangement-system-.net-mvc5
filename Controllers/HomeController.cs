using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChartsInMvc.Controllers
{
    public class HomeController : Controller
    {
        
        public static DataTable GetVehicleSummary()
        {
            DataTable dt = new DataTable("VehicleSummary");
            string query = "Select Vehicletype,str(count(Vehicletype)* 100.0 / (Select Count(*) From VehicleMaster), 5,1) as percentage ";
            query+="from VehicleMaster group by Vehicletype";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=.;" + "Initial Catalog=Transport;" + "Integrated Security=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            return dt;
        }
        public class Summary
        {
            public double Item { get; set; }            public string Value { get; set; }
        }

        [HttpGet]
        public JsonResult VehicleSummary()
        {
            List<Summary> lstSummary = new List<Summary>();
            foreach (DataRow dr in GetVehicleSummary().Rows)
            {
                Summary summary = new Summary();
                summary.Value = dr[0].ToString().Trim();
                summary.Item = Convert.ToDouble(dr[1]);
                lstSummary.Add(summary);
            }
            return Json(lstSummary.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
