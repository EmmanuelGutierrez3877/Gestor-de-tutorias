using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Tutores
{
    class BaseDeDatos {

        public MySqlConnection conectar()
        {
            MySqlConnection conexion = new MySqlConnection();
            conexion.ConnectionString = "server=localhost;password=;database=tutorias;user Id=root";
            return conexion;
        }

        public void Insertar(String sentencia)
        {
            MySqlConnection conexion = conectar();
            MySqlCommand comando = new MySqlCommand(sentencia, conexion);
            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public MySqlDataReader consultar(String sentencia, MySqlConnection conexion)
        {
            MySqlCommand comando = new MySqlCommand(sentencia, conexion);
            conexion.Open();
            MySqlDataReader myReader = comando.ExecuteReader();
            return myReader;
        }

    }
}
