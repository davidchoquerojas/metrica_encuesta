using Metricaencuesta.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Metricaencuesta.Data
{
    public class ResultadoDB
    {
        Int32 difftime = Convert.ToInt32(ConfigurationManager.AppSettings.Get("hDiff"));
        public List<resultado> listAll()
        {
            try
            {
                using (var db = new PruebaContext())
                {
                    return db.resultadoes.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<encuesta> listPregunta()
        {
            var listEncuesta = new List<encuesta>();
            try
            {
                using (var db = new PruebaContext())
                {
                    var query = db.Database.SqlQuery<encuesta>("pr_sel_gpregunta").ToList();
                    foreach (var item in query)
                    {
                        var encuesta = new encuesta()
                        {
                            descripcion = item.descripcion,
                            id_gpregunta = item.id_gpregunta,
                            id_pregunta = item.id_pregunta
                        };
                        var id_gpregunta = new SqlParameter("@id_gpregunta", item.id_gpregunta);
                        //lista subentity 
                        var subencuesta = new List<encuesta>();
                        foreach(var sub in db.Database.SqlQuery<encuesta>("pr_sel_gpregunta @id_gpregunta", id_gpregunta).ToList()){
                            var subentity = new encuesta()
                            {
                                descripcion = sub.descripcion,
                                id_gpregunta = sub.id_gpregunta,
                                id_pregunta = sub.id_pregunta
                            };
                            subencuesta.Add(subentity);
                        }

                        encuesta.subencuesta = subencuesta;

                        listEncuesta.Add(encuesta);
                    }
                }
                return listEncuesta;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Boolean save(List<resultado> o)
        {
            try
            {
                using (var db = new PruebaContext())
                {
                    db.resultadoes.AddRange(o);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int getEncuestaByColaborador(int id_empleado)
        {
            try
            {
                var dateIni = Convert.ToDateTime(System.DateTime.Now.AddHours(difftime).ToString("MMMM dd, yyyy") + " 00:00:00");
                var dateEnd = Convert.ToDateTime(System.DateTime.Now.AddHours(difftime).ToString("MMMM dd, yyyy") + " 23:59:59");
                using (var db = new PruebaContext())
                {
                    return db.resultadoes.Where(x => x.id_empleado == id_empleado && x.fecha_evaluacion >= dateIni && x.fecha_evaluacion <= dateEnd).Count();
                }
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message));
            }
        }
    }
}