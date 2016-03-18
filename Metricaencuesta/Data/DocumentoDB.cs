using Metricaencuesta.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Metricaencuesta.Data
{
    public class DocumentoDB
    {
        public documento save(documento documento)
        {
            try
            {
                using (var db = new PruebaContext())
                {
                    db.documento.Add(documento);
                    db.SaveChanges();
                    return documento;
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }
        public List<documento> listDocumento()
        {
            try
            {
                using (var db = new PruebaContext())
                {
                    return db.documento.ToList();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }

        public documento update(documento documento)
        {
            try
            {
                using (var db = new PruebaContext())
                {
                    var query = db.documento.Find(documento.id_documento);
                    query.ruta = documento.ruta;
                    query.nombre = documento.nombre;
                    query.leido_por = null;
                    db.SaveChanges();
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }

        public List<documento> updateVistoDocumento(empleado empleado, documento documento)
        {
            try
            {
                using (var db = new PruebaContext())
                {
                    var query = db.documento.Find(documento.id_documento);
                    query.leido_por = string.IsNullOrEmpty(query.leido_por) ? empleado.id_empleado.ToString() : query.leido_por +("," + empleado.id_empleado.ToString());
                    db.SaveChanges();
                    return db.documento.ToList();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }
    }
}