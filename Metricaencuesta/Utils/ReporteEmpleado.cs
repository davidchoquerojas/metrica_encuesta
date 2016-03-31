using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Web;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System;
using Metricaencuesta.Data;

namespace Metricaencuesta.Utils
{
    public class ReporteEmpleado
    {
        XSSFWorkbook book = new XSSFWorkbook();
        public string generateExcelEmpleado(DateTime fecha_ini, DateTime fecha_fin)
        {
            var font = new ReporteEncuestaExcel();
            var styleHeader = font.setFontText(12, true, book);
            var styleBody = font.setFontText(10, false, book);
            String[] month = {"","ENERO","FEBRERO","MARZO","ABRIL","MAYO","JUNIO","JULIO","AGOSTO","SEPTIEMBRE","OCTUBRE","NOVIEMBRE","DICIEMBRE"};
            var anioEvaluacion = new ReporteEncuestaDB().consultReportEmpleado(fecha_ini,fecha_fin,1);
            if (anioEvaluacion.Count == 0)
                return string.Empty;
            var sheet = book.CreateSheet("Reporte lista colaboradores");

            var rHeader = sheet.CreateRow(1);
            ICell cHeader;
            var cellMerge = 2;

            cHeader = rHeader.CreateCell(1);
            sheet.AddMergedRegion(new CellRangeAddress(1, 2, 1, 1));
            cHeader.SetCellValue("APELLIDOS Y NOMBRES");
            cHeader.CellStyle = styleHeader;
            cHeader.CellStyle.WrapText = true;
            sheet.SetColumnWidth(1, 8000);

            for (int i = 0; i < anioEvaluacion.Count; i++)
            {
                cHeader = rHeader.CreateCell(i+2);
                if (anioEvaluacion[i].mes_evaluacion > 1)
                    sheet.AddMergedRegion(new CellRangeAddress(1, 1, cellMerge, anioEvaluacion[i].mes_evaluacion + cellMerge));
                cHeader.SetCellValue(anioEvaluacion[i].anio_evaluacion);
                cHeader.CellStyle = styleHeader;
                cHeader.CellStyle.WrapText = true;
                cellMerge += anioEvaluacion[i].mes_evaluacion;
            }

            var mesEvaluacion = new ReporteEncuestaDB().consultReportEmpleado(fecha_ini, fecha_fin, 2);
            var rMonth = sheet.CreateRow(2);
            ICell cMonth;
            for (int i = 0; i < mesEvaluacion.Count; i++)
            {
                cMonth = rMonth.CreateCell(2+i);
                cMonth.SetCellValue(month[mesEvaluacion[i].mes_evaluacion]);
                cMonth.CellStyle = styleHeader;
                sheet.SetColumnWidth(2 + i, 4000);
            }

            var datoEmpleado = new ReporteEncuestaDB().consultReportEmpleado(fecha_ini, fecha_fin, 3);
            var encuestaPuntaje = new ReporteEncuestaDB().consultReportEmpleado(fecha_ini, fecha_fin, 4);
            IRow rNombres;
            for (int i = 0; i < datoEmpleado.Count; i++)
            {
                rNombres = sheet.CreateRow(3+i);
                ICell cNombres = rNombres.CreateCell(1);
                cNombres.SetCellValue(datoEmpleado[i].nombres);
                cNombres.CellStyle = styleHeader;
                var columnIndex = 2;
                for (int a = 0; a < anioEvaluacion.Count; a++)
                {
                    var listPuntaje = encuestaPuntaje.FindAll(an => an.anio_evaluacion == anioEvaluacion[a].anio_evaluacion && an.id_empleado == datoEmpleado[i].id_empleado);
                    if (listPuntaje.Count > 0)
                    {
                        var m = 0;
                        while (m < listPuntaje.Count)
                        {
                            ICell cPuntaje = rNombres.CreateCell(columnIndex);
                            cPuntaje.SetCellValue(listPuntaje[m].puntaje);
                            cPuntaje.CellStyle = styleBody;
                            columnIndex++;
                            m++;
                        }
                    }
                    else
                        columnIndex++;
                }
            }
            var guide = "Reporte_consolidado_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            using (var file = new FileStream(@HttpContext.Current.Server.MapPath("~/Utils/xlsxs/") + guide.ToString() + ".xlsx", FileMode.Create, FileAccess.ReadWrite))
            {
                book.Write(file);
                file.Close();
            }
            return guide.ToString();
        }
    }
}
