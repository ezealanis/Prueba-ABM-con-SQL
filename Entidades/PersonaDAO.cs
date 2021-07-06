using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class PersonaDAO
    {
        private static SqlConnection conexion;
        private static SqlCommand comando;
        private static string conexionString = "Data Source=.;Initial Catalog =Persona; Integrated Security = True";

        static PersonaDAO()
        {
            conexion = new SqlConnection(conexionString);
            comando = new SqlCommand();
            comando.Connection = conexion;
        }

        public static void Guardar(Persona p)
        {
            comando.CommandText = "insert into Personas values(@nombre, @apellido)";
            comando.Parameters.AddWithValue("nombre", p.Nombre);
            comando.Parameters.AddWithValue("apellido", p.Apellido);

            try
            {
                conexion.Open();

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexion.State != System.Data.ConnectionState.Closed)
                {
                    conexion.Close();
                    comando.Parameters.Clear();
                }
            }
        }

        public static List<Persona> Leer()
        {
            List<Persona> lista = new List<Persona>();

            comando.CommandText = "select * from Personas";

            try
            {
                conexion.Open();
                SqlDataReader oDr = comando.ExecuteReader();

                while (oDr.Read())
                {
                    //int.TryParse(oDr["ID"].ToString(), out int auxID);
                    //auxPersona.Id = auxID;

                    //auxPersona.Nombre = oDr["Nombre"].ToString();
                    //auxPersona.Apellido = oDr["Apellido"].ToString();

                    Persona aux = new Persona(oDr.GetInt32(0), oDr.GetString(1), oDr.GetString(2));
                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexion.State != System.Data.ConnectionState.Closed)
                {
                    conexion.Close();
                    comando.Parameters.Clear();
                }
            }

            return lista;
        }

        public static Persona LeerPorID(int id)
        {
            comando.CommandText = "select * from Personas where id = @id";
            comando.Parameters.AddWithValue("@id", id);

            try
            {
                conexion.Open();
                SqlDataReader oDr = comando.ExecuteReader();

                Persona auxPersona = new Persona(id, oDr.GetString(1), oDr.GetString(2));
                return auxPersona;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexion.State != System.Data.ConnectionState.Closed)
                {
                    conexion.Close();
                    comando.Parameters.Clear();
                }
            }            
        }

        public static void Modificar(Persona p)
        {
            int auxModif = 0;

            comando.CommandText = "update Personas set Nombre = @nombre, Apellido = @apellido where id = @id";
            comando.Parameters.AddWithValue("@nombre", p.Nombre);
            comando.Parameters.AddWithValue("@apellido", p.Apellido);
            comando.Parameters.AddWithValue("@id", p.Id);

            try
            {
                conexion.Open();

                auxModif = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexion.State != System.Data.ConnectionState.Closed)
                {
                    conexion.Close();
                    comando.Parameters.Clear();
                }
            }
        }

        public static int Borrar(Persona p)
        {
            int auxBorrar = 0;            

            try
            {
                comando.CommandText = "delete from Personas where id = @id";
                comando.Parameters.AddWithValue("@id", p.Id);

                conexion.Open();

                auxBorrar = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conexion.State != System.Data.ConnectionState.Closed)
                {
                    conexion.Close();
                    comando.Parameters.Clear();
                }
            }

            return auxBorrar;
        }
    }
}
