using Metricaencuesta.Models;
using Metricaencuesta.Models.helper;
using System;
using System.Data;

namespace Metricaencuesta.Data
{
    public class QueryHelperDB
    {
        public DataTable createData(queryHelper query)
        {
            if (!string.IsNullOrEmpty(query.strQuery))
                return loadFromDatabase(query);
            else
                return new DataTable();
        }

        private DataTable loadFromDatabase(queryHelper query)
        {
            var dataResponse = new DataTable();
            try
            {
                using (var db = new PruebaContext())
                {
                    using (var conn = db.Database.Connection)
                    {
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = query.strQuery;
                        cmd.CommandType = CommandType.Text;
                        using (var reader = cmd.ExecuteReader())
                        {
                            dataResponse.Load(reader);
                        }
                    } 
                }
                return dataResponse;
            }
            catch (Exception ex)
            {
                throw(new Exception(ex.Message,ex.InnerException));
            }
        }
    }
}