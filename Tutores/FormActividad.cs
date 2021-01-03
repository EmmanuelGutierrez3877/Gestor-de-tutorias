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
    public partial class FormActividad : Form
    {
        String plan;
        int tipo = 0;
        BaseDeDatos BD = new BaseDeDatos();
        public FormActividad(String plan,int tipo)
        {
            this.plan = plan;
            this.tipo = tipo;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Ingrese una actividad");
            }
            else if (tipo == 1) {
                BD.Insertar("INSERT INTO `actividad`( `fecha`, `descripcion`, `plan`) " +
                    "VALUES ('"+dateTimePicker1.Value.Year+"-"+dateTimePicker1.Value.Month+"-"+dateTimePicker1.Value.Day+"'," +
                    "'"+textBox1.Text+"'," +
                    "'"+plan+"');");
                MessageBox.Show("Actividad guardada");
                this.Close();
            }
            else if (tipo == 2)
            {
                BD.Insertar("UPDATE `actividad` SET " +
                    "`fecha`='" + dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day + "'," +
                    "`descripcion`='" + textBox1.Text + "' " +
                    " WHERE codigo='" +plan+"';");
                MessageBox.Show("Actividad guardada");
                this.Close();
                
            }
        }

        private void FormActividad_Load(object sender, EventArgs e)
        {
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar(
                "SELECT * From actividad where codigo = '"+plan+"';"
                , conexion
                );

            while (myReader.Read())
            {
                dateTimePicker1.Value = myReader.GetDateTime(myReader.GetOrdinal("fecha"));
                textBox1.Text = myReader.GetString(myReader.GetOrdinal("descripcion"));
                
                
            }
            conexion.Close();
        }
    }
}
