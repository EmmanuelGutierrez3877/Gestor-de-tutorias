using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tutores
{
    public partial class FormPlan2 : Form
    {
        String plan = "";
        BaseDeDatos BD = new BaseDeDatos();

        public FormPlan2(String plan)
        {
            this.plan = plan;
            InitializeComponent();

            
        }

        private void FormPlan2_Load(object sender, EventArgs e)
        {
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar("SELECT * FROM plan,informe WHERE informe.plan=plan.codigo && plan.codigo=" + plan + ";", conexion);

            if (myReader.Read())
            {
                textBoxPlan.Text = myReader.GetString(myReader.GetOrdinal("plan"));
                textBoxInforme.Text = myReader.GetString(myReader.GetOrdinal("informe"));
            }

            myReader.Close();
            conexion.Close();

            CargarActividades(dataGridView1,plan);
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (textBoxPlan.Text!="")
            {
                BD.Insertar("UPDATE `plan` SET `plan`='" + textBoxPlan.Text + "' WHERE codigo=" + plan + " ;");
                BD.Insertar("UPDATE `informe` SET `informe`='"+textBoxInforme.Text+"' WHERE plan="+plan+";");
                MessageBox.Show("Plan modificado");
            }
           
        }

        private void buttonActividad_Click(object sender, EventArgs e)
        {
            FormActividad FA = new FormActividad(plan,1);
            FA.ShowDialog();
            CargarActividades(dataGridView1, plan);

        }

        private void CargarActividades(DataGridView DG , String plan)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT DATE_FORMAT(fecha, '%d/%m/%Y') as f,codigo,descripcion from actividad WHERE plan ='" + plan+"';", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("f"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("descripcion"));
                
            }
            conexion.Close();
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            String cod = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["ColumnCodigo1"].Value.ToString();

            FormActividad FA = new FormActividad(cod,2);
            FA.ShowDialog();
            CargarActividades(dataGridView1, plan);
        }
    }
}
