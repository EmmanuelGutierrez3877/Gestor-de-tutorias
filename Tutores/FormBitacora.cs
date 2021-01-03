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
    public partial class FormBitacora : Form
    {
        BaseDeDatos BD = new BaseDeDatos();
        String bitacora = "";
        String individual = "";
        int tipo = 0;

        public FormBitacora(String individual)
        {
            this.individual = individual;
            this.tipo = 1;
            InitializeComponent();
        }

        public FormBitacora(String individual, String bitacora)
        {
            this.individual = individual;
            this.bitacora = bitacora;
            this.tipo = 2;
            InitializeComponent();
        }

        private void FormBitacora_Load(object sender, EventArgs e)
        {
            if (tipo==2)
            {
                MySqlConnection conexion = BD.conectar();

                MySqlDataReader myReader = BD.consultar("SELECT * FROM `bitacora` where codigo="+bitacora+";", conexion);

                while (myReader.Read())
                {
                    
                    textBoxObjetivo.Text = myReader.GetString(myReader.GetOrdinal("objetivo"));
                    textBoxMeta.Text = myReader.GetString(myReader.GetOrdinal("meta"));
                    textBoxAcuerdo.Text = myReader.GetString(myReader.GetOrdinal("acuerdo"));
                }
                conexion.Close();
            }
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (tipo==1)
            {
                BD.Insertar("INSERT INTO `bitacora` (`individual`, `objetivo`, `meta`, `acuerdo`) VALUES ( '"+individual+"', '"+textBoxObjetivo.Text+"', '"+textBoxMeta.Text+"', '"+textBoxAcuerdo.Text+"');");
                MessageBox.Show("Bitacora registrada");
                this.Close();
            }
            else if (tipo == 2)
            {
                BD.Insertar("UPDATE `bitacora` SET `objetivo` = '"+textBoxObjetivo.Text+"', `meta` = '"+textBoxMeta.Text+"', `acuerdo` = '"+textBoxAcuerdo.Text+"' WHERE `bitacora`.`codigo` = "+bitacora+";");
                MessageBox.Show("Bitacora registrada");
                this.Close();
            }
        }
    }
}
