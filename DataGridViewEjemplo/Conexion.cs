using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
namespace DataGridViewEjemplo
{
    public class Conexion
    {
        public string connectionString 
            = ConfigurationManager.AppSettings["connectionString"];

        private SqlConnection sqlConnection { get; set; }


        public Conexion() {
            sqlConnection = new SqlConnection(connectionString);
        

        }

        public void Open() {
            sqlConnection.Open();
        }

        public void Close()
        {
            sqlConnection.Close();
        }

        public int InsertToDatabase(string queryInsert)
        {
            SqlCommand comando = new SqlCommand(queryInsert, sqlConnection);
            Open();
            var resultado = comando.ExecuteNonQuery();
            Close();

            return resultado;
        }

        public DataSet ObtenerDatos(string querySelect)
        {
            DataSet datosMemoria = new DataSet();

            SqlCommand sqlCommand = new SqlCommand(querySelect, sqlConnection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(datosMemoria);

            return datosMemoria;
        }

        public int EliminarRegistro(string queryDelete)
        {
            SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection);
            Open();
            var resultado = sqlCommand.ExecuteNonQuery();
            Close();

            return resultado;
        }

        public int Actualizar(string queryUpdate)
        {
            SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection);
            Open();
            var resultado = sqlCommand.ExecuteNonQuery();
            Close();

            return resultado;
        }

    }
}
