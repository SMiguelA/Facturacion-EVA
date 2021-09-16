using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace facProyecto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cod;
            string nom;
            float precio;

            cod = cmbProducto.SelectedIndex;
            nom = cmbProducto.SelectedItem.ToString();
            precio = cmbProducto.SelectedIndex;

            switch (cod)
            {
                case 0: lblCodigo.Text = "0011"; break;
                case 1: lblCodigo.Text = "0022"; break;
                case 2: lblCodigo.Text = "0033"; break;
                case 3: lblCodigo.Text = "0044"; break;
                case 4: lblCodigo.Text = "0055"; break;
                case 5: lblCodigo.Text = "0066"; break;
                default: lblCodigo.Text = "0077"; break;
            }
            
            switch (nom)
            {
                case "PLATO EJECUTIVO": lblNombre.Text = "PLATO EJECUTIVO";break;
                case "AJIACO": lblNombre.Text = "AJIACO"; break;
                case "SALMON PRIMAVERA": lblNombre.Text = "SALMON PRIMAVERA"; break;
                case "SNACK OVER TIME": lblNombre.Text = "SNACK OVER TIME"; break;
                case "CALLO PICOSO": lblNombre.Text = "CALLO PICOSO"; break;
                case "SALTEADO DE VEGETALES": lblNombre.Text = "SALTEADO DE VEGETALES"; break;
                default: lblNombre.Text = "ENCEBOLLADO COLOMBIANO"; break;

            }

            switch (precio)
            {
                case 0: lblPrecio.Text = "17000";break;
                case 1: lblPrecio.Text = "15000"; break;
                case 2: lblPrecio.Text = "28000"; break;
                case 3: lblPrecio.Text = "8000"; break;
                case 4: lblPrecio.Text = "12000"; break;
                case 5: lblPrecio.Text = "18000"; break;
                default: lblPrecio.Text = "15000"; break;
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DataGridViewRow file = new DataGridViewRow();
            file.CreateCells(dgvLista);

            file.Cells[0].Value = lblCodigo.Text;
            file.Cells[1].Value = lblNombre.Text;
            file.Cells[2].Value = lblPrecio.Text;
            file.Cells[3].Value = txtCantidad.Text;
            file.Cells[4].Value = (float.Parse(lblPrecio.Text) * float.Parse(txtCantidad.Text)).ToString();

            dgvLista.Rows.Add(file);

            lblCodigo.Text = lblNombre.Text = lblPrecio.Text = txtCantidad.Text = "";

            obtenerTotal();

        }

        public void obtenerTotal()
        {
            float costot = 0;
            int contador = 0;

            contador = dgvLista.RowCount;

            for (int i = 0; i < contador; i++)
            {
                costot += float.Parse(dgvLista.Rows[i].Cells[4].Value.ToString());
            }

            lblTotal.Text = costot.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try 
            {
                DialogResult rppta = MessageBox.Show("¿Desea eliminar el producto?",
                    "Eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (rppta == DialogResult.Yes) 
                {
                     dgvLista.Rows.Remove(dgvLista.CurrentRow);
                }
            }

            catch
            {

            }

            obtenerTotal();
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            try 
            {
            lblDevolucion.Text = (float.Parse(txtEfectivo.Text) - float.Parse(lblTotal.Text)).ToString();
            }
            catch 
            {
            
            }
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            clsFactura.CreaTicket Ticket1 = new clsFactura.CreaTicket();

            Ticket1.TextoCentro("EVA'S RESTAURANT"); //imprime una linea de descripcion
            Ticket1.TextoCentro("FACTURA");

            Ticket1.TextoIzquierda("");
            Ticket1.TextoCentro("Factura de Venta"); //imprime una linea de descripcion
            Ticket1.TextoIzquierda("No Fac: 0000000" );
            Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
            Ticket1.TextoIzquierda("Le Atendio: xxxx");
            Ticket1.TextoIzquierda("");
            clsFactura.CreaTicket.LineasGuion();

            clsFactura.CreaTicket.EncabezadoVenta();
            clsFactura.CreaTicket.LineasGuion();
            foreach (DataGridViewRow r in dgvLista.Rows)
            {
                // PROD                     //PrECIO                                    CANT                         TOTAL
                Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString())); //imprime una linea de descripcion
            }


            clsFactura.CreaTicket.LineasGuion();
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Total", double.Parse(lblTotal.Text)); // imprime linea con total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(txtEfectivo.Text));
            Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(lblDevolucion.Text));


            // Ticket1.LineasTotales(); // imprime linea 

            Ticket1.TextoIzquierda(" ");
            Ticket1.TextoCentro("************");
            Ticket1.TextoCentro("*     Gracias por preferirnos    *");

            Ticket1.TextoCentro("************");
            Ticket1.TextoIzquierda(" ");
            string impresora = "Microsoft XPS Document Writer";
            Ticket1.ImprimirTiket(impresora);

            MessageBox.Show("Gracias por preferirnos");

            this.Close();
        }
    }
}
