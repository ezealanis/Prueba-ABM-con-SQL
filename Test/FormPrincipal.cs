using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Test
{
    public delegate void Actualizar();

    public partial class FormPrincipal : Form
    {
        
        private int auxID;
        Persona auxPersona;
        public event Actualizar RefrescarLista;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            try
            {
                this.lstPersonas.DataSource = PersonaDAO.Leer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                PersonaDAO.Modificar(new Persona(auxID, this.txtNombre.Text, this.txtApellido.Text));
                RefrescarLista.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtNombre.Text) && !string.IsNullOrEmpty(this.txtApellido.Text))
                {
                    auxPersona = new Persona(this.txtNombre.Text, this.txtApellido.Text);
                    PersonaDAO.Guardar(auxPersona);
                    RefrescarLista.Invoke();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lstPersonas_DoubleClick(object sender, EventArgs e)
        {
            this.auxPersona = ((Persona)this.lstPersonas.SelectedItem);
            this.auxID = auxPersona.Id;
            this.txtNombre.Text = ((Persona)this.lstPersonas.SelectedItem).Nombre;
            this.txtApellido.Text = ((Persona)this.lstPersonas.SelectedItem).Apellido;
        }

        private void ActualizarLista()
        {
            this.lstPersonas.DataSource = PersonaDAO.Leer();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                PersonaDAO.Borrar(auxPersona);
                RefrescarLista.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            RefrescarLista += ActualizarLista;
            RefrescarLista.Invoke();
        }
    }
}
