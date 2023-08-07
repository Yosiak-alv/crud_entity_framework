using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_ENTITY_FRAMEWORK
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CargarDatos();
        }
        private bool Editarse = false;
        void CargarDatos()
        {
            using (CineEntitites23 db = new CineEntitites23())
            {
                dataGridView1.DataSource = null;
                dataGridView1.Update();
                dataGridView1.Refresh();
                dataGridView1.DataSource = db.EMPLEADOS.ToList();
            }
        }
        void limpiar()
        {
            txtelefono.Clear();
            txtemail.Clear();
            txtnombre.Clear();
            Editarse = false;
            dataGridView1.ClearSelection();
        }
        private void btnguardar_Click(object sender, EventArgs e)
        {
            if(Editarse == false)
            {
                using (CineEntitites23 db = new CineEntitites23()) //como la capa datos
                {
                    EMPLEADOS entities = new EMPLEADOS(); // clase de la como capa entitades
                    entities.nombre = txtnombre.Text;
                    entities.email = txtemail.Text;
                    entities.phone = txtelefono.Text;
                    db.EMPLEADOS.Add(entities);//agrega un registro, como metodo de la capa datos
                    db.SaveChanges();//confirma los cambios
                }
                MessageBox.Show("tarea exitosa!!!");
                CargarDatos();
                limpiar();
            }
            else if(Editarse == true)
            {
                using(CineEntitites23 db = new CineEntitites23())
                {
                    EMPLEADOS entities = new EMPLEADOS(); // clase de la como capa entitades
                    entities.id = IDEmpledo;
                    entities.nombre = txtnombre.Text;
                    entities.email = txtemail.Text;
                    entities.phone = txtelefono.Text;
                    db.Entry(entities).State = System.Data.Entity.EntityState.Modified; // equivalente a update
                    db.SaveChanges();
                }
                MessageBox.Show("tarea exitosa!!!");
                CargarDatos();
                limpiar();
            }

            
        }
        private int IDEmpledo;
        private void BTNEDITAR_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                Editarse = true;
                IDEmpledo = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                txtnombre.Text = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
                txtemail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
                txtelefono.Text = dataGridView1.CurrentRow.Cells["phone"].Value.ToString();
            }
            else
            {
                MessageBox.Show("seleccione registro a eliminar");
                limpiar();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void BTNDELETE_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                using (CineEntitites23 db = new CineEntitites23())
                {
                    EMPLEADOS entities = db.EMPLEADOS.Find(Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString()));
                    db.EMPLEADOS.Remove(entities); //delete
                    db.SaveChanges();//confirmacion de cambios
                }
                MessageBox.Show("tarea exitosa!!!");
                CargarDatos();
                limpiar();
            }
            else
            {
                MessageBox.Show("Seleccione la fila que desea eliminar...");
                limpiar();
            }
        }
    }
}
