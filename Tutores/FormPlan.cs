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
    public partial class FormPlan : Form
    {
        BaseDeDatos BD = new BaseDeDatos();
        String tutoria;
        public FormPlan(string tutoria)
        {
            this.tutoria = tutoria;
            InitializeComponent();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (textBoxPlan.Text != "")
            {
                int cod = 0;

                BD.Insertar(
                "INSERT INTO `plan`( `plan`, `tutoria`) VALUES('" + textBoxPlan.Text + "','" + tutoria + "');"
                );

                MySqlConnection conexion = BD.conectar();
                MySqlDataReader myReader = BD.consultar("SELECT codigo FROM plan;", conexion);
                while (myReader.Read())
                {
                    cod = myReader.GetInt32(myReader.GetOrdinal("codigo"));
                }
                conexion.Close();
                

                BD.Insertar(
                "INSERT INTO `informe`( `plan`, `informe`) VALUES ('"+cod+"',' ')"
                );

                MessageBox.Show("Plan Guardado");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ingresa un plan");
            }
            
        }
    }
}
