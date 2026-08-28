namespace BdiExamen.WinFormsExamen
{
    partial class frmExamen
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.txtFiltroDescripcion = new System.Windows.Forms.TextBox();
            this.txtFiltroNombre = new System.Windows.Forms.TextBox();
            this.txtFiltroId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelCentral = new System.Windows.Forms.Panel();
            this.dgvExamenes = new System.Windows.Forms.DataGridView();
            this.panelEdicion = new System.Windows.Forms.Panel();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelFiltros.SuspendLayout();
            this.panelCentral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamenes)).BeginInit();
            this.panelEdicion.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            this.panelFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltros.Height = 100;
            this.panelFiltros.Padding = new System.Windows.Forms.Padding(10);

            this.label1.AutoSize = true;
            this.label1.Text = "FILTROS DE BÚSQUEDA";
            this.label1.Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            this.panelFiltros.Controls.Add(this.label1);

            this.label4.AutoSize = true;
            this.label4.Text = "ID:";
            this.label4.Location = new System.Drawing.Point(10, 30);
            this.panelFiltros.Controls.Add(this.label4);

            this.txtFiltroId.Location = new System.Drawing.Point(50, 30);
            this.txtFiltroId.Width = 80;
            this.panelFiltros.Controls.Add(this.txtFiltroId);

            this.label5.AutoSize = true;
            this.label5.Text = "Nombre:";
            this.label5.Location = new System.Drawing.Point(150, 30);
            this.panelFiltros.Controls.Add(this.label5);

            this.txtFiltroNombre.Location = new System.Drawing.Point(220, 30);
            this.txtFiltroNombre.Width = 150;
            this.panelFiltros.Controls.Add(this.txtFiltroNombre);

            this.label6.AutoSize = true;
            this.label6.Text = "Descripción:";
            this.label6.Location = new System.Drawing.Point(390, 30);
            this.panelFiltros.Controls.Add(this.label6);

            this.txtFiltroDescripcion.Location = new System.Drawing.Point(480, 30);
            this.txtFiltroDescripcion.Width = 150;
            this.panelFiltros.Controls.Add(this.txtFiltroDescripcion);

            this.btnConsultar.Location = new System.Drawing.Point(650, 30);
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.Width = 80;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            this.panelFiltros.Controls.Add(this.btnConsultar);

            this.btnLimpiar.Location = new System.Drawing.Point(740, 30);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Width = 80;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            this.panelFiltros.Controls.Add(this.btnLimpiar);

            this.panelCentral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentral.Padding = new System.Windows.Forms.Padding(10);

            this.dgvExamenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExamenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExamenes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvExamenes_CellClick);
            this.panelCentral.Controls.Add(this.dgvExamenes);

            this.panelEdicion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEdicion.Height = 120;
            this.panelEdicion.Padding = new System.Windows.Forms.Padding(10);

            this.label2.AutoSize = true;
            this.label2.Text = "Nombre:";
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.panelEdicion.Controls.Add(this.label2);

            this.txtNombre.Location = new System.Drawing.Point(80, 10);
            this.txtNombre.Width = 200;
            this.panelEdicion.Controls.Add(this.txtNombre);

            this.label3.AutoSize = true;
            this.label3.Text = "Descripción:";
            this.label3.Location = new System.Drawing.Point(300, 10);
            this.panelEdicion.Controls.Add(this.label3);

            this.txtDescripcion.Location = new System.Drawing.Point(390, 10);
            this.txtDescripcion.Width = 200;
            this.panelEdicion.Controls.Add(this.txtDescripcion);

            this.btnNuevo.Location = new System.Drawing.Point(80, 50);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Width = 80;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            this.panelEdicion.Controls.Add(this.btnNuevo);

            this.btnGuardar.Location = new System.Drawing.Point(170, 50);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Width = 80;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.panelEdicion.Controls.Add(this.btnGuardar);

            this.btnEliminar.Location = new System.Drawing.Point(260, 50);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Width = 80;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            this.panelEdicion.Controls.Add(this.btnEliminar);

            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusStrip1.Height = 30;
            this.toolStripStatusLabel1.Text = "Listo";
            this.statusStrip1.Items.Add(this.toolStripStatusLabel1);

            this.Controls.Add(this.panelCentral);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.panelEdicion);
            this.Controls.Add(this.statusStrip1);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Name = "frmExamen";
            this.Text = "CRUD de Examen";
            this.Load += new System.EventHandler(this.frmExamen_Load);

            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.panelCentral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamenes)).EndInit();
            this.panelEdicion.ResumeLayout(false);
            this.panelEdicion.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFiltroId;
        private System.Windows.Forms.TextBox txtFiltroNombre;
        private System.Windows.Forms.TextBox txtFiltroDescripcion;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panelCentral;
        private System.Windows.Forms.DataGridView dgvExamenes;
        private System.Windows.Forms.Panel panelEdicion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
