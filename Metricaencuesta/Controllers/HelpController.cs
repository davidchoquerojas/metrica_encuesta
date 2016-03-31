using System.Web.Mvc;
using Metricaencuesta.Data;
using Metricaencuesta.Models.helper;
using Metricaencuesta.Utils;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System;

namespace Metricaencuesta.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        [SessionValidator]
        public ActionResult Index()
        {
            if (Session["tipo_usuario"].ToString().ToUpper().Equals("ADM"))
                return View(new queryHelper());
            return RedirectToAction("asistencia", "home");
            
        }
        [HttpPost]
        [SessionValidator]
        public ActionResult listQuery(queryHelper query, string Cammand)
        {
            try
            {
                if (Session["tipo_usuario"].ToString().ToUpper().Equals("ADM"))
                {
                    var modelQuery = new queryHelper();
                    modelQuery.data = new QueryHelperDB().createData(query);
                    if (Cammand.Equals("Exportar"))
                    {
                        var grid = Exportar(modelQuery);
                        Response.Output.Write(grid.ToString());
                        Response.Flush();
                        Response.End();
                    }
                    return View("Index", modelQuery);
                }
                return RedirectToAction("asistencia", "home");
            }
            catch (System.Exception ex)
            {
                ViewData["ErrorName"] = ex.Message.ToString();
                ViewBag.ErrorName = ex.Message.ToString();
                return View("../Shared/Error");
            }
        }

        public StringWriter Exportar(queryHelper model)
        {
            var grid = new GridView();
            grid.DataSource = model.data;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename=data_{0}.xls",DateTime.Now.ToString("yyyyMMdd")));
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);
            return sw;
        }
    }
}