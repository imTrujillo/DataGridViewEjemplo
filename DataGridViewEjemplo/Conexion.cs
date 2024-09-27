using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataGridViewEjemplo
{
    public class Conexion
    {
        public string connectionString 
            = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial catalog=Almacen;Trusted_Connection=True";

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

    }
}
