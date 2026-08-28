using BdiExamen.Model.Entities;
using BdiExamen.WinFormsExamen.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BdiExamen.WinFormsExamen
{
    // Formulario principal para la gestión de exámenes
    public partial class frmExamen : Form
    {
        private readonly IExamenService _service;
        private int? _selectedId = null;
        // Constructor del formulario que inicializa los componentes y el servicio de examen
        // Se inyecta una instancia concreta de ExamenService para manejar la lógica de negocio relacionada con los exámenes.
        public frmExamen()
        {
            InitializeComponent();
            _service = new ExamenService();
        }

        private async void frmExamen_Load(object sender, EventArgs e)
        {
            await CargarExamenes();
        }
        // Evento que se dispara al hacer clic en el botón de consulta
        // Recoge los filtros de la interfaz, llama al servicio para obtener los resultados y actualiza el DataGridView con los datos obtenidos.
        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                int? id = string.IsNullOrEmpty(txtFiltroId.Text) ? (int?)null : int.Parse(txtFiltroId.Text);
                string nombre = string.IsNullOrEmpty(txtFiltroNombre.Text) ? null : txtFiltroNombre.Text;
                string descripcion = string.IsNullOrEmpty(txtFiltroDescripcion.Text) ? null : txtFiltroDescripcion.Text;

                var resultado = await _service.ConsultarAsync(id, nombre, descripcion);

                if (resultado.Exitoso)
                {
                    dgvExamenes.DataSource = resultado.Resultados;
                    MostrarMensaje("Consulta exitosa", false);
                }
                else
                {
                    MostrarMensaje(resultado.DescripcionRetorno, true);
                    dgvExamenes.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error: {ex.Message}", true);
            }
        }
        // Método privado que carga los exámenes desde el servicio y los muestra en el DataGridView
        private async Task CargarExamenes()
        {
            try
            {
                var resultado = await _service.ConsultarAsync();
                if (resultado.Exitoso)
                    dgvExamenes.DataSource = resultado.Resultados;
                else
                    MostrarMensaje(resultado.DescripcionRetorno, true);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar: {ex.Message}", true);
            }
        }
        // Evento que se dispara al hacer clic en una celda del DataGridView
        // Permite seleccionar un examen y cargar sus datos en los campos de texto para su edición
        private void dgvExamenes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var fila = dgvExamenes.Rows[e.RowIndex];
                _selectedId = (int)fila.Cells["Id"].Value;
                txtNombre.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtDescripcion.Text = fila.Cells["Descripcion"].Value?.ToString() ?? "";
            }
        }
        // Evento que se dispara al hacer clic en el botón de guardar
        // Valida los campos, determina si se trata de una actualización o un nuevo registro y llama al servicio correspondiente
        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                try
                {
                    if (_selectedId.HasValue)
                    {
                        var resultado = await _service.ActualizarAsync(_selectedId.Value, txtNombre.Text, txtDescripcion.Text);
                        if (resultado.Exitoso)
                        {
                            MostrarMensaje($"{resultado.DescripcionRetorno}", false);
                            await CargarExamenes();
                            LimpiarInterfaz();
                        }
                        else
                        {
                            MostrarMensaje(resultado.DescripcionRetorno, true);
                        }
                    }
                    else
                    {
                        var resultado = await _service.AgregarAsync(txtNombre.Text, txtDescripcion.Text);
                        if (resultado.Exitoso)
                        {
                            MostrarMensaje($"{resultado.DescripcionRetorno} (ID: {resultado.IdGenerado})", false);
                            await CargarExamenes();
                            LimpiarInterfaz();
                        }
                        else
                        {
                            MostrarMensaje(resultado.DescripcionRetorno, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error: {ex.Message}", true);
                }
            }
        }
        // Evento que se dispara al hacer clic en el botón de eliminar
        // Valida si hay un examen seleccionado, solicita confirmación al usuario y llama al servicio para eliminar el examen
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MostrarMensaje("Selecciona un examen para eliminar", true);
                return;
            }

            if (MessageBox.Show("¿Seguro que quieres eliminar este examen?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var resultado = await _service.EliminarAsync(_selectedId.Value);
                    if (resultado.Exitoso)
                    {
                        MostrarMensaje($"{resultado.DescripcionRetorno}", false);
                        await CargarExamenes();
                        LimpiarInterfaz();
                    }
                    else
                    {
                        MostrarMensaje(resultado.DescripcionRetorno, true);
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error: {ex.Message}", true);
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarInterfaz();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }
        // Método privado que valida los campos de entrada antes de guardar o actualizar un examen
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarMensaje("El nombre es obligatorio", true);
                return false;
            }
            return true;
        }

        private void LimpiarInterfaz()
        {
            _selectedId = null;
            txtNombre.Text = "";
            txtDescripcion.Text = "";
        }

        private async void LimpiarFiltros()
        {
            txtFiltroId.Text = "";
            txtFiltroNombre.Text = "";
            txtFiltroDescripcion.Text = "";
            await CargarExamenes();
        }
        // Método privado que muestra un mensaje en la barra de estado del formulario
        // Cambia el color de fondo y del texto según si es un mensaje de error o de éxito
        private void MostrarMensaje(string mensaje, bool esError)
        {
            statusStrip1.BackColor = esError ? System.Drawing.Color.FromArgb(255, 200, 200) : System.Drawing.Color.FromArgb(200, 255, 200);
            toolStripStatusLabel1.Text = mensaje;
            toolStripStatusLabel1.ForeColor = esError ? System.Drawing.Color.FromArgb(139, 0, 0) : System.Drawing.Color.FromArgb(0, 100, 0);
        }
    }
}
