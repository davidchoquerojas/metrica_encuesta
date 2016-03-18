using Metricaencuesta.Data;
using Metricaencuesta.Models;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Metricaencuesta.Controllers
{
    public class DocumentoController : Controller
    {
        [HttpPost]
        public JsonResult save(documento documento)
        {
            try
            {
                var path = string.Empty;
                foreach (string FileLetter in Request.Files)
                {
                    var files = Request.Files[FileLetter];
                    var stream = files.InputStream;
                    path = files.FileName;
                    using (var filestreaim = System.IO.File.Create(@"" + Server.MapPath("~/Utils/pdfs/" + path)))
                    {
                        stream.CopyTo(filestreaim);
                    }
                }

                documento.estado = "A";
                documento.fecha_registro = DateTime.Now;
                documento.usuario_registro = Session["usuario"].ToString();
                documento.tipo = "TD";
                documento.ruta = path;
                var dbDocumento = new DocumentoDB();
                var respuesta = documento.id_documento == 0 ? dbDocumento.save(documento) : dbDocumento.update(documento);
                return new JsonResult { Data = respuesta };

            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message };
            }
        }
        [HttpPost]
        public JsonResult list(empleado empleado)
        {
            try
            {
                var listDocumento = new DocumentoDB().listDocumento();
                return new JsonResult { Data = empleado.id_empleado == 0?listDocumento:verificaLeidoPorUsuario(empleado, listDocumento)};
            }
            catch (Exception ex)
            {
                return new JsonResult { Data = ex.Message};
            }
        }
        [HttpPost]
        public JsonResult listUser(documento documento)
        {
            var returnList = new List<empleado>();
            var listUser = new EmpleadoDB().listAll();
            var listUserId =  string.IsNullOrEmpty(documento.leido_por)? new string[0] { }: documento.leido_por.Split(',');
            foreach (var userId in listUserId)
            {
                returnList.Add(listUser.Find(x => x.id_empleado == Convert.ToInt32(userId)));
            }
            return new JsonResult { Data = returnList };
        }
        [HttpPost]
        public JsonResult updateVistoDocumento(empleado empleado,documento documento)
        {
            var respuestaDoc = new DocumentoDB().updateVistoDocumento(empleado,documento);
            return new JsonResult { Data = verificaLeidoPorUsuario(empleado,respuestaDoc) };
        }

        protected List<documento> verificaLeidoPorUsuario(empleado empleado,List<documento> listDocumento)
        {
            var returnListDocumento = new List<documento>();
            foreach (var item in listDocumento)
            {
                var employeId = String.IsNullOrEmpty(item.leido_por)?new string[0] { } :item.leido_por.Split(',');
                var encontro = false;
                foreach (var id in employeId)
                {
                    if (Convert.ToInt32(id) == empleado.id_empleado)
                    {
                        item.leido_por = empleado.id_empleado.ToString();
                        encontro = true;
                        break;
                    }
                }
                item.leido_por = encontro ? item.leido_por : null;
                returnListDocumento.Add(item);
            }
            return returnListDocumento;
        }
    }
}