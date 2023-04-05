using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Models
{
    public class ProductoModel
    {

        public int AgregarProducto(ProductoEnt entidad)
        {

            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                Product producto = new Product();
                producto.name = entidad.Nombre;
                producto.description = entidad.Descripcion;
                producto.stock = entidad.Stock;
                producto.price = entidad.Precio;
                producto.picture_url = entidad.ImagenURL;
                producto.id_store = entidad.IdTienda;


                conexion.Product.Add(producto);
                return conexion.SaveChanges();
            }


        }

        public List<ProductoEnt> ConsultarProductos()
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {

                var datos = (from x in conexion.Product
                             select x).ToList();

                List<ProductoEnt> listaEntidadResultado = new List<ProductoEnt>();
                foreach (var item in datos)
                {
                    listaEntidadResultado.Add(new ProductoEnt
                    {
                        IdProducto = item.id_product,
                        Nombre = item.name,
                        Descripcion = item.description,
                        Stock = item.stock,
                        Precio = item.price,
                        ImagenURL = item.picture_url,
                        IdTienda = item.id_store,
                    });

                }
                return listaEntidadResultado;
            }

        }

        public ProductoEnt ConsultarProducto(int q)
        {

            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.Product
                             where x.id_product == q
                             select x).FirstOrDefault();

                ProductoEnt EntidadResultado = new ProductoEnt();

                EntidadResultado.IdProducto = datos.id_product;
                EntidadResultado.Nombre = datos.name;
                EntidadResultado.Descripcion = datos.description;
                EntidadResultado.Stock = datos.stock;
                EntidadResultado.Precio = datos.price;
                EntidadResultado.ImagenURL = datos.picture_url;
                EntidadResultado.IdTienda = datos.id_store;


                return EntidadResultado;

            }
        }


        public void ActualizarProducto(ProductoEnt entidad)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.Product
                             where x.id_product == entidad.IdProducto
                             select x).FirstOrDefault();

                datos.name = entidad.Nombre;
                datos.description = entidad.Descripcion;    
                datos.stock = entidad.Stock;
                datos.price = entidad.Precio;
                datos.picture_url = entidad.ImagenURL;
                datos.id_store = entidad.IdTienda;

                conexion.SaveChanges();

            }

        }

        public void EliminarProducto(int id)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.Product
                             where x.id_product == id
                             select x).FirstOrDefault();

                conexion.Product.Remove(datos);
                conexion.SaveChanges();
            }

        }

    }
}