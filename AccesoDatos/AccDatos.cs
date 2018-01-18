﻿using AccesoDatos.Entidades;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    /// <summary>
    /// Clase en donde se realizan las diversas transacciones con la base de datos prueba
    /// </summary>
    public class AccDatos
    {
        private static NpgsqlConnection conn=null;
        static List<Usuario> lstUsuarios = new List<Usuario>();
        string connstring = null;
        /// <summary>
        /// Método para inicializar la cadena de conexión con la Base de Datos
        /// </summary>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public string setConnString(string ConnectionString)
        {
            connstring = ConnectionString;
            return connstring;
        }
        public Boolean connectDB()
        {
            try
            {
                conn = new NpgsqlConnection(connstring);
                conn.Open();
            }
            catch (Exception)
            {
                return false;
            }
        return true;
        }
        public string insertCasaSalesiana(CasaSalesiana casa)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("insert into tb_casasalesiana(nombre_cas,direccion_cas,telefono_cas,correo_cas,director_cas,nombrecorto_cas,estado_cas,pathicono_cas) values('" + casa.nombre_cas+ "','" + casa.direccion_cas + "','" + casa.telefono_cas + "','" + casa.correo_cas + "','" + casa.director_cas + "','" + casa.nombrecorto_cas + "','"+casa.estado_cas+ "','"+casa.pathicono_cas+"')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception)
            {
                return "false";
            }
            return "true";
        }
        public List<string> obtenerListaNombreCasaSalesiana()
        {
            List<string> lstCasasSalesianas = new List<string>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select nombre_cas from tb_casasalesiana", conn);
                NpgsqlDataReader dr = command.ExecuteReader();                
                while (dr.Read())
                    lstCasasSalesianas.Add(dr[0].ToString());
                conn.Close();
            }
            catch(Exception)
            {
                lstCasasSalesianas = null;
            }
            return lstCasasSalesianas;
        }
        public List<Usuario> obtenerListaUsuarios()
        {
            try
            {
                
                NpgsqlCommand command = new NpgsqlCommand("select usuario_usu,contrasenia_usu from tb_usuario", conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Usuario user = new Usuario();
                    user.Nombre_Usuario = dr[0].ToString();
                    user.Contrasenia = dr[01].ToString();
                    lstUsuarios.Add(user);
                }
                conn.Close();
            }
            catch (Exception)
            {
                lstUsuarios = null;
            }
            return lstUsuarios;
        }
        public String comprobarUsuario(string usuario, string clave)
        {
            String sw="true";
            try
            {
                foreach (var item in lstUsuarios)
                {
                    if (usuario.Trim() == item.Nombre_Usuario.Trim() && clave.Trim() == item.Contrasenia.Trim())
                    {
                        sw = "true";
                    }
                    else
                    {
                        sw="false";
                    }
                }
            }
            catch(Exception)
            {
                sw="false";
            }
            return sw;
        }

        
        public List<string> obtenerListaTipoCasaSalesiana()
        {
            List<string> lstTipoObras = new List<string>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select descripcion_tobr from tb_tipoobra", conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                    lstTipoObras.Add(dr[0].ToString());
                conn.Close();
            }
            catch (Exception)
            {
                lstTipoObras = null;
            }
            return lstTipoObras;
        }
        public List<string> obtenerListaObrasPorCasa(string NombreCasa)
        {
            List<string> lstObras = new List<string>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select denominacion_obr from tb_obrasalesiana where id_cas in (Select id_cas from tb_casasalesiana where nombre_cas='"+NombreCasa.Trim()+"')", conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                    lstObras.Add(dr[0].ToString());
                conn.Close();
            }
            catch (Exception)
            {
                lstObras = null;
            }
            return lstObras;
        }
        public List<string> obtenerListaLugarPorObra(string DenominacionObra)
        {
            List<string> lstLugar = new List<string>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select nombre_lug from tb_lugar where id_obr in (Select id_obr from tb_obrasalesiana where denominacion_obr='"+DenominacionObra+"')", conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                    lstLugar.Add(dr[0].ToString());
                conn.Close();
            }
            catch (Exception)
            {
                lstLugar = null;
            }
            return lstLugar;
        }
        public List<string> obtenerListaTipoColaborador()
        {
            List<string> lstColaborador = new List<string>();
            try
            {
                NpgsqlCommand command = new NpgsqlCommand("select descripcion_tcol from tb_tipocolaborador", conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                    lstColaborador.Add(dr[0].ToString());
                conn.Close();
            }
            catch (Exception)
            {
                lstColaborador = null;
            }
            return lstColaborador;
        }
    }
}
