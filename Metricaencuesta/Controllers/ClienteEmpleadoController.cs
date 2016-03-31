using Metricaencuesta.Data;
using Metricaencuesta.Models;
using Metricaencuesta.Utils;
using System;
using System.Web.Mvc;

namespace Metricaencuesta.Controllers
{
    [SessionValidator]
    public class ClienteEmpleadoController : Controller
    {
        [HttpGet]
        public JsonResult listAll()
        {
            return new JsonResult { Data = new ClienteEmpleadoDB().listAll(),JsonRequestBehavior = JsonRequestBehavior.AllowGet,MaxJsonLength = Int32.MaxValue};
        }

        [HttpPost]
        public JsonResult save(cliente_empleado o)
        {
            return new JsonResult { Data = new ClienteEmpleadoDB().save(o) };
        }

        [HttpPost]
        public JsonResult update(cliente_empleado o,int id)
        {
            return new JsonResult { Data = new ClienteEmpleadoDB().update(o, id) };
        }
    }
}