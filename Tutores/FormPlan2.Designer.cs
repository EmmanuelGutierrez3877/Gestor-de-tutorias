namespace Tutores
{
    partial class FormPlan2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonGuardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlan = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxInforme = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnCodigo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFecha1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescripcion1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonActividad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGuardar
            // 
            this.buttonGuardar.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGuardar.Location = new System.Drawing.Point(200, 295);
            this.buttonGuardar.Name = "buttonGuardar";
            this.buttonGuardar.Size = new System.Drawing.Size(101, 32);
            this.buttonGuardar.TabIndex = 5;
            this.buttonGuardar.Text = "Guardar";
            this.buttonGuardar.UseVisualStyleBackColor = true;
            this.buttonGuardar.Click += new System.EventHandler(this.buttonGuardar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "Plan:";
            // 
            // textBoxPlan
            // 
            this.textBoxPlan.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPlan.Location = new System.Drawing.Point(12, 44);
            this.textBoxPlan.Multiline = true;
            this.textBoxPlan.Name = "textBoxPlan";
            this.textBoxPlan.Size = new System.Drawing.Size(499, 98);
            this.textBoxPlan.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "Informe";
            // 
            // textBoxInforme
            // 
            this.textBoxInforme.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInforme.Location = new System.Drawing.Point(11, 194);
            this.textBoxInforme.Multiline = true;
            this.textBoxInforme.Name = "textBoxInforme";
            this.textBoxInforme.Size = new System.Drawing.Size(499, 85);
            this.textBoxInforme.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCodigo1,
            this.ColumnFecha1,
            this.ColumnDescripcion1});
            this.dataGridView1.Location = new System.Drawing.Point(535, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(456, 232);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseDoubleClick);
            // 
            // ColumnCodigo1
            // 
            this.ColumnCodigo1.HeaderText = "Codigo";
            this.ColumnCodigo1.Name = "ColumnCodigo1";
            this.ColumnCodigo1.ReadOnly = true;
            this.ColumnCodigo1.Width = 60;
            // 
            // ColumnFecha1
            // 
            this.ColumnFecha1.HeaderText = "Fecha";
            this.ColumnFecha1.Name = "ColumnFecha1";
            this.ColumnFecha1.ReadOnly = true;
            // 
            // ColumnDescripcion1
            // 
            this.ColumnDescripcion1.HeaderText = "Descripcion";
            this.ColumnDescripcion1.Name = "ColumnDescripcion1";
            this.ColumnDescripcion1.ReadOnly = true;
            this.ColumnDescripcion1.Width = 150;
            // 
            // buttonActividad
            // 
            this.buttonActividad.Location = new System.Drawing.Point(652, 44);
            this.buttonActividad.Name = "buttonActividad";
            this.buttonActividad.Size = new System.Drawing.Size(170, 33);
            this.buttonActividad.TabIndex = 9;
            this.buttonActividad.Text = "Agregar actividad";
            this.buttonActividad.UseVisualStyleBackColor = true;
            this.buttonActividad.Click += new System.EventHandler(this.buttonActividad_Click);
            // 
            // FormPlan2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 346);
            this.Controls.Add(this.buttonActividad);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxInforme);
            this.Controls.Add(this.buttonGuardar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPlan);
            this.Name = "FormPlan2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plan";
            this.Load += new System.EventHandler(this.FormPlan2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGuardar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPlan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxInforme;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCodigo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFecha1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescripcion1;
        private System.Windows.Forms.Button buttonActividad;
    }
}