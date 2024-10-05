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

        public int productoIdSeleccionado;
        public Form1()
        {
            InitializeComponent();

            Productos = new List<Productos>();
           
           
        }

        private void CargarDatosGrid()
        {
            var dataset = _conexion.ObtenerDatos("SELECT * FROM Productos");
            Productos.Clear();
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
                        Precio = decimal.Parse(row["Precio"].ToString())
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1 != null && dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count >0) 
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    var elementoSeleccionado = row.Index;
                    var producto = this.Productos[elementoSeleccionado];

                    _conexion.EliminarRegistro($"Delete from productos where id={producto.Id}");
                }

                CargarDatosGrid();
                
            }

            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1 == null || dataGridView1.SelectedRows == null || dataGridView1.SelectedRows.Count == 0)
            {

                this.txtNombre.Text ="";
                this.txtCantidad.Text = "";
                this.txtDescripcion.Text = "";
                this.dateTimePicker1.Value =DateTime.Now;
                this.txtPrecio.Text = "";
                productoIdSeleccionado = 0;
                return;
            }
                

            var seleccionado = dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count-1].Index;
            var producto = Productos[seleccionado];
            this.txtNombre.Text = producto.Nombre;
            this.txtCantidad.Text = producto.Cantidad.ToString();
            this.txtDescripcion.Text = producto.Descripcion;
            this.dateTimePicker1.Value = producto.FechaVencimiento;
            this.txtPrecio.Text = producto.Precio.ToString();
            this.productoIdSeleccionado = producto.Id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sql = $"Update productos set Nombre='{txtNombre.Text}', Descripcion='{txtDescripcion.Text}', cantidad={txtCantidad.Text}, FechaVencimiento='{dateTimePicker1.Value.ToString("yyy-MM-dd HH:mm:ss")}' where id={productoIdSeleccionado}";
            _conexion.Actualizar(sql);
            CargarDatosGrid();
        }
    }
}
