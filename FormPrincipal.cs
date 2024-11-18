using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using static CRUD_CloudWaves.FormPrincipal;
using Newtonsoft.Json;

namespace CRUD_CloudWaves
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }


        bool Actualizar = false;
        int id = 1;
        int rows = 0;


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            Actualizar = false;
            btnGuardar.Enabled = true;

            btnCrear.Enabled = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Actualizar)
            {
                //Crear producto
                int x = dtgvProductos.Rows.Add();
                Producto producto = new Producto();

                producto.ID = id;
                producto.Marca = txtMarca.Text;
                producto.Sabor = txtSabor.Text;
                producto.Cantidad = Convert.ToInt32(txtCantidad.Text);
                producto.Precio = Convert.ToInt32(txtPrecio.Text);
                producto.Persona = txtPersona.Text;


                dtgvProductos.Rows[x].Cells[0].Value = producto.ID;
                dtgvProductos.Rows[x].Cells[1].Value = producto.Marca;
                dtgvProductos.Rows[x].Cells[2].Value = producto.Sabor;
                dtgvProductos.Rows[x].Cells[3].Value = producto.Cantidad;
                dtgvProductos.Rows[x].Cells[4].Value = producto.Precio;
                dtgvProductos.Rows[x].Cells[5].Value = producto.Persona;

                //
                BorrarDatos();
                id++;

            }
            else
            {
                if (dtgvProductos.Rows.Count > 0)
                {
                    Producto producto = new Producto();

                    producto.Marca = txtMarca.Text;
                    producto.Sabor = txtSabor.Text;
                    producto.Cantidad = Convert.ToInt32(txtCantidad.Text);
                    producto.Precio = Convert.ToInt32(txtPrecio.Text);


                    dtgvProductos.Rows[rows].Cells[1].Value = producto.Marca;
                    dtgvProductos.Rows[rows].Cells[2].Value = producto.Sabor;
                    dtgvProductos.Rows[rows].Cells[3].Value = producto.Cantidad;
                    dtgvProductos.Rows[rows].Cells[4].Value = producto.Precio;
                    dtgvProductos.Rows[rows].Cells[5].Value = producto.Persona;

                    rows = 0;
                    BorrarDatos();
                    Actualizar = false;

                }
            }
        }

        private void BorrarDatos()
        {
            txtCantidad.Clear();
            txtMarca.Clear();
            txtPersona.Clear();
            txtPrecio.Clear();
            txtSabor.Clear();

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dtgvProductos.SelectedRows.Count > 0)
            {
                btnGuardar.Enabled = true;

                //
                btnCrear.Enabled = false;

                Actualizar = true;
                rows = dtgvProductos.SelectedRows[0].Index;
                txtMarca.Text = dtgvProductos.CurrentRow.Cells[1].Value.ToString();
                txtSabor.Text = dtgvProductos.CurrentRow.Cells[2].Value.ToString();
                txtCantidad.Text = dtgvProductos.CurrentRow.Cells[3].Value.ToString();
                txtPrecio.Text = dtgvProductos.CurrentRow.Cells[4].Value.ToString();
                txtPersona.Text = dtgvProductos.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dtgvProductos.SelectedRows.Count > 0)
            {
                int index = dtgvProductos.SelectedRows[0].Index;
                dtgvProductos.Rows.RemoveAt(index);
            }
        }

        //JSON
        private void btnExportarJSON_Click(object sender, EventArgs e)
        {
            // Crear una lista de productos desde el DataGridView
            List<Producto> productos = new List<Producto>();
            foreach (DataGridViewRow row in dtgvProductos.Rows)
            {
                if (row.Cells[0].Value != null) // Asegurarse de que la fila no esté vacía
                {
                    Producto producto = new Producto
                    {
                        ID = Convert.ToInt32(row.Cells[0].Value),
                        Marca = row.Cells[1].Value.ToString(),
                        Sabor = row.Cells[2].Value.ToString(),
                        Cantidad = Convert.ToInt32(row.Cells[3].Value),
                        Precio = Convert.ToInt32(row.Cells[4].Value),
                        Persona = row.Cells[5].Value.ToString()
                    };
                    productos.Add(producto);
                }
            }

            // Convertir la lista a JSON
            string jsonString = JsonConvert.SerializeObject(productos, Formatting.Indented);

            // Guardar el archivo JSON
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "JSON Files (*.json)|*.json",
                FileName = "Productos.json"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, jsonString);
                MessageBox.Show("Datos exportados correctamente a JSON.", "Exportar a JSON", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


    }
}
