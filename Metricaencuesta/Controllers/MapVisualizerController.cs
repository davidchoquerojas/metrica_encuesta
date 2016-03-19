using Metricaencuesta.Utils;
using System.Web.Mvc;

namespace Metricaencuesta.Controllers
{
    public class MapVisualizerController : Controller
    {
        // GET: MapVisualizer
        [SessionValidator]
        public ActionResult Index()
        {
            if (Session["tipo_usuario"].ToString().ToUpper().Equals("ADM"))
                return View();
            return RedirectToAction("asistencia", "home");
        }
    }
}