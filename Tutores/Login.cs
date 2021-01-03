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
    public partial class Login : Form
    {
        BaseDeDatos BD = new BaseDeDatos();
        public Login()
        {
            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxCodigo.Text = "10000";
            textBoxContraseña.Text = "123";
        }


        private void textBoxNombre_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            if (textBoxCodigo.Text != "" && textBoxContraseña.Text != "") 
            {
                MySqlConnection conexion = BD.conectar();
                MySqlDataReader myReader = BD.consultar("SELECT * FROM usuario WHERE codigo=" + textBoxCodigo.Text + ";",conexion);

                if (myReader.Read())
                {
                    int codigo = myReader.GetInt32(myReader.GetOrdinal("codigo"));
                    String password = myReader.GetString(myReader.GetOrdinal("password"));
                    myReader.Close();
                    conexion.Close();
                    if(textBoxContraseña.Text == password)
                    {
                        myReader = BD.consultar("SELECT * FROM profesor WHERE codigo=" + textBoxCodigo.Text + ";", conexion);
                        if (myReader.Read())
                        {
                            myReader.Close();
                            Form1 f1 = new Form1(codigo, "profesor", this);
                            f1.ShowDialog();
                            
                            //this.Close();
                        }
                        else {
                            myReader.Close();
                            conexion.Close();
                            conexion = BD.conectar();

                            myReader = BD.consultar("SELECT * FROM estudiante WHERE codigo=" + textBoxCodigo.Text + ";",conexion);
                            if (myReader.Read())
                            {
                                myReader.Close();
                                conexion.Close();
                                Form1 f1 = new Form1(codigo, "estudiante", this);
                                f1.ShowDialog();
                                
                                //this.Close();
                            }
                            else
                            {
                                myReader.Close();
                                conexion.Close();
                                FormAdministrardor f1 = new FormAdministrardor(this,codigo);
                                f1.ShowDialog();
                                
                                //this.Close();
                            }
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("DATOS INVALIDOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("DATOS INVALIDOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
