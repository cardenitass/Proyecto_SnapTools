using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Models
{
    public class FacturaModel
    {
        public void ConfirmarPago(FacturaEnt entidad)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                // Store Procedure
                conexion.ConfirmPayment(entidad.IdUsuario);  
            }
        }


        public List<FacturaEnt> MostrarFactura(int IdUsuario)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                // Store Procedure
                var datos = conexion.ViewInvoice(IdUsuario).ToList(); 

                List<FacturaEnt> factura = new List<FacturaEnt>();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        factura.Add(new FacturaEnt
                        {
                            IdFactura = item.id_invoice,
                            FechaCompra = item.date_time,
                            SubTotal = item.sub_total,
                            Impuesto = item.tax,
                            Total = item.total
                        });
                    }

                }
                return factura;
            }
        }
    }
}