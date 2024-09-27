using DataGridViewEjemplo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DataGridViewEjemplo
{
    public partial class Form1 : Form
    {
        Conexion _conexion = new Conexion();
        public List<Productos> Productos { get;set; }
        public Form1()
        {
            InitializeComponent();

            Productos = new List<Productos>();
           
           
        }

        private void CargarDatosGrid()
        {

            var dataset = _conexion.ObtenerDatos("SELECT * FROM Productos");

            if (dataset != null && dataset.Tables != null && dataset.Tables[0] != null)
            {
                foreach (DataRow row in dataset.Tables[0].Rows)
                {
                    Productos.Add(new Productos()
                    {
                        Id= int.Parse(row["Id"].ToString()),
                        Nombre = row["Nombre"].ToString(),
                        FechaVencimiento = (DateTime)row["FechaVencimiento"],
                        Descripcion = row["Descripcion"].ToString(),
                        Cantidad = int.Parse(row["Cantidad"].ToString()),
                        Precio = decimal.Parse(row["Cantidad"].ToString())
                    }); ;
                }
            }

            this.dataGridView1.DataSource = new BindingList<Productos>(Productos);

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var cantidad = 0;
            var precio = 0.0M;

            var cantidadValida =int.TryParse(txtCantidad.Text, out cantidad);

            if(cantidadValida == false)
            {
                MessageBox.Show("Por favor ingrese un valor valido para cantidad");
                txtCantidad.Focus();
                errorProvider1.SetError(txtCantidad, "Por favor ingrese un valor valido para cantidad");
                return;
            }
            else
            {
                errorProvider1.Clear();
            }

            var precioValido = decimal.TryParse(txtPrecio.Text, out precio);

            if (precioValido == false)
            {
                MessageBox.Show("Por favor ingrese un valor valido para precio");
                txtPrecio.Focus();

                return;
            }

            var producto = new Productos();
            producto.Nombre = txtNombre.Text;
            producto.Descripcion = txtDescripcion.Text;
            producto.Cantidad = cantidad;
            producto.Precio = precio;
            producto.FechaVencimiento = dateTimePicker1.Value;

            var query 
                = $"INSERT INTO PRODUCTOS" +
                $"(Nombre, Descripcion,FechaVencimiento,Cantidad,Precio)"+
                $"VALUES ('{producto.Nombre}', '{producto.Descripcion}','{producto.FechaVencimiento.ToString("yyyy-MM-dd")}', {producto.Cantidad}, {producto.Precio})";
           
            _conexion.InsertToDatabase(query);


            CargarDatosGrid();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            CargarDatosGrid();
        }
    }
}
