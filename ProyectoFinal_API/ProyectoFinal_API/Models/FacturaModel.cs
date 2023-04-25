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

        // Mostrar varios productos de la factura
        public List<DetalleFacturaEnt> MostrarDetalleFactura(int IdFactura)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = conexion.ShowInvoiceDetails(IdFactura).ToList();

                List<DetalleFacturaEnt> detalleFactura = new List<DetalleFacturaEnt>();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        detalleFactura.Add(new DetalleFacturaEnt
                        {
                            IdFactura = item.id_invoice,
                            IdProducto = item.id_product,
                            Cantidad = item.quantity,
                            Precio = item.price
                        });
                    }

                }
                return detalleFactura;
            }
        }
    }
}