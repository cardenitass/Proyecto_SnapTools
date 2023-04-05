using ProyectoFinal_API.App_Start;
using ProyectoFinal_API.Entities;
using ProyectoFinal_API.ModeloBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoFinal_API.Models
{
    public class UsuarioModel
    {
        TokenGenerator tokenGenerator = new TokenGenerator();

        // Metodo para validar que el usuario exista
        public UsuarioEnt ValidarUsuario(UsuarioEnt entidad)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var resultado = (from x in conexion.User_tb
                                 where x.email == entidad.CorreoElectronico
                                 && x.password == entidad.Contrasenna
                                 && x.active == true
                                 select x).FirstOrDefault();

                UsuarioEnt entidadResultado = new UsuarioEnt();
                if (resultado != null)
                {
                    entidadResultado.Token = tokenGenerator.GenerateTokenJwt(resultado.email);
                    entidadResultado.IdUsuario = resultado.id_user;
                    entidadResultado.CorreoElectronico = resultado.email;
                    entidadResultado.Estado = resultado.active; 
                    return entidadResultado;
                }

                return null;
            }
        }

        // Metodo de query para agarrar los datos del usuario 
        public List<UsuarioEnt> ConsultarUsuarios()
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.User_tb
                             select x).ToList();

                List<UsuarioEnt> listaEntidadResultado = new List<UsuarioEnt>();
                foreach (var item in datos)
                {
                    listaEntidadResultado.Add(new UsuarioEnt
                    {
                        IdUsuario = item.id_user,
                        CorreoElectronico = item.email,
                        Estado = item.active,
                        Nombre = item.name,
                        Identificacion = item.identification
                    });
                }

                return listaEntidadResultado;
            }
        }

        // Metodo para solo consultar 1 usuario al actualizar
        public UsuarioEnt ConsultarUsuario(int q)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.User_tb
                             where x.id_user == q
                             select x).FirstOrDefault();

                UsuarioEnt EntidadResultado = new UsuarioEnt();

                EntidadResultado.IdUsuario = datos.id_user;
                EntidadResultado.CorreoElectronico = datos.email;
                EntidadResultado.Estado = datos.active;
                EntidadResultado.Nombre= datos.name;    
                EntidadResultado.Identificacion = datos.identification;
                EntidadResultado.IdProvincia = datos.id_province;
                EntidadResultado.Rol = datos.id_role; 

                return EntidadResultado;
            }
        }

        // Metodo para actualizar al usuario en la bd
        public void ActualizarUsuario(UsuarioEnt entidad)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.User_tb
                             where x.id_user == entidad.IdUsuario
                             select x).FirstOrDefault();

                datos.identification = entidad.Identificacion;
                datos.name = entidad.Nombre;
                datos.id_role = entidad.Rol;
                datos.id_province = entidad.IdProvincia;

                if (!string.IsNullOrEmpty(entidad.Contrasenna))
                    datos.password = entidad.Contrasenna;

                conexion.SaveChanges();

            }
        }

        // Metodo para cambiar el estado del usuario en la bd
        public void CambiarEstado(int q)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var datos = (from x in conexion.User_tb
                             where x.id_user == q
                             select x).FirstOrDefault();

                // Condicion ternaria
                datos.active = (datos.active = true ? false : true );
                conexion.SaveChanges();

            }
        }


        // Metodo para verificar si el correo existe en la bd
        public string BuscarCorreo(string correoElectronico)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                var resultado = (from x in conexion.User_tb
                                 where x.email== correoElectronico
                                 select x).FirstOrDefault();

                if (resultado == null)
                    return string.Empty;
                else
                {
                    if (resultado.active)
                        return "Esta cuenta de correo ya fue registrada anteriormente";
                    else
                        return "Esta cuenta de correo se encuentra inactiva";
                }
            }
        }

        // Metodo para realizar el insert del nuevo usuario en la bd

        public int RegistrarUsuario(UsuarioEnt entidad)
        {
            using (var conexion = new ProyectoFinal_KN_BDEntities())
            {
                User_tb usuario = new User_tb();
                usuario.name = entidad.Nombre;
                usuario.email = entidad.CorreoElectronico;
                usuario.password = entidad.Contrasenna;
                usuario.active = true;
                usuario.id_role = 2;
                usuario.identification = entidad.Identificacion;                                            
                usuario.id_province = entidad.IdProvincia;


                conexion.User_tb.Add(usuario);
                return conexion.SaveChanges();
            }
        }
    }
}