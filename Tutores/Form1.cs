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
    public partial class Form1 : Form
    {
        int codigo;
        String tipo;
        Form ant;
        BaseDeDatos BD = new BaseDeDatos();
        List<int> codigosPeriodo = new List<int>();

        public Form1(int codigo, String tipo, Form ant)
        {
            this.codigo = codigo;
            this.tipo = tipo;
            this.ant = ant;

            InitializeComponent();
            this.Text = codigo + ": "+tipo;
     
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.TabPages[tabControl1.SelectedIndex].Text == "Datos personales")
            {
                if (tipo == "Profesor")
                {
                    textBoxTelefono.Visible = true;
                    labelTelefono.Visible = true;
                }
                cargarDP();
            }
            if (tabControl1.TabPages[tabControl1.SelectedIndex].Text == "Tutoria Grupal")
            {
                cargarDGtutoriaG(dataGridView1);
            }
            if (tabControl1.TabPages[tabControl1.SelectedIndex].Text == "Tutoria Individual")
            {
                cargarDGtutoriaI(dataGridView2);
                
            }
            if (tabControl1.TabPages[tabControl1.SelectedIndex].Text == "Tutoria Par")
            {
                CargarComboPeriodo(comboBoxPeriodo, codigosPeriodo);
                cargarDGestudiantes(dataGridView5);
                cargarDGestudiantes(dataGridView6);
                cargarDGtPar(dataGridView7);
            }
            if (tabControl1.TabPages[tabControl1.SelectedIndex].Text == "Tutoria")
            {
                cargarDGtPar(dataGridView9,codigo.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ant.Visible = false;
            tabControl1_Selected(sender,null);
            toolStripStatusLabel1.Text = System.DateTime.Now.ToLongDateString();

            if (tipo != "Estudiante")
            {
                tabPage5.Parent = null;
            }
            if (tipo != "Profesor")
            {
                tabPage2.Parent = null;
                tabPage3.Parent = null;
                tabPage4.Parent = null;
            }
            else
            {
                MySqlConnection conexion = BD.conectar();
                MySqlDataReader myReader = BD.consultar("SELECT * FROM tutor WHERE codigo=" + codigo + ";", conexion);

                if (!myReader.Read())
                {
                    tabPage2.Parent = null;
                    tabPage3.Parent = null;
                    tabPage4.Parent = null;
                }
                

                myReader.Close();
                conexion.Close();
            }

        }


        private void cargarDP(){

            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar("SELECT * FROM " + tipo + " WHERE codigo=" + codigo + ";",conexion);

            if (myReader.Read())
            {
                textBoxCodigo.Text = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();                
                textBoxNombre.Text = myReader.GetString(myReader.GetOrdinal("nombre"));
                textBoxApellidos.Text = myReader.GetString(myReader.GetOrdinal("apellidos"));
                textBoxCorreo.Text = myReader.GetString(myReader.GetOrdinal("email"));
                if (tipo=="Profesor")
                {
                    textBoxTelefono.Text = myReader.GetString(myReader.GetOrdinal("telefono"));
                }

            }

            myReader.Close();
            conexion.Close();

            conexion = BD.conectar();
            myReader = BD.consultar("SELECT * FROM usuario WHERE codigo=" + codigo + ";", conexion);

            if (myReader.Read())
            {
                
                textBoxContraseña.Text = myReader.GetString(myReader.GetOrdinal("password"));

            }

            myReader.Close();
            conexion.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ant.Visible = true;
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (textBoxCodigo.Text != "")
            {
                String sentencia = "";
                if (textBoxCodigo.Text != "")
                {
                    MySqlConnection conexion = BD.conectar();
                    if (tipo=="Profesor")
                    {
                        sentencia = "UPDATE profesor SET " +
                        "nombre='" + textBoxNombre.Text + 
                        "', apellidos='" + textBoxApellidos.Text + 
                        "',telefono='" + textBoxTelefono.Text + 
                        "',email='" + textBoxCorreo.Text +
                        "',telefono='" + textBoxTelefono.Text +
                        "'WHERE codigo=" + textBoxCodigo.Text + ";";
                    }
                    else if(tipo == "Estudiante")
                    {
                        sentencia = "UPDATE estudiante SET " +
                        "nombre='" + textBoxNombre.Text +
                        "', apellidos='" +textBoxApellidos.Text + 
                        "',email='" +textBoxCorreo.Text + 
                        "'WHERE codigo=" + textBoxCodigo.Text + ";";
                    }
                    else if (tipo == "Administrador")
                    {
                        sentencia = "UPDATE administrador SET " +
                        "nombre='" + textBoxNombre.Text +
                        "', apellidos='" + textBoxApellidos.Text +
                        "',email='" + textBoxCorreo.Text +
                        "'WHERE codigo=" + textBoxCodigo.Text + ";";
                    }

                    BD.Insertar(sentencia);

                    conexion = BD.conectar();
                    sentencia = "UPDATE usuario SET password='"+textBoxContraseña.Text+"' where codigo='"+codigo+"' ;";
                    BD.Insertar(sentencia);
                    MessageBox.Show("Usuario modificado");
                }
            }
        }

        private void cargarDGtutoriaG(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT " +
                "grupo.codigo," +
                "grupo.nombre, " +
                "tutoria.codigo as C " +

                "from grupal,tutoria,profesor,grupo " +

                "where tutoria.tutor='"+codigo+"' " +
                "&& profesor.codigo=tutoria.tutor " +
                "&& grupal.tutoria=tutoria.codigo " +
                "&& grupal.grupo=grupo.codigo " +
               
                " ;", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("nombre")) ;
                DG.Rows[renglon].Cells[2].Value = myReader.GetInt32(myReader.GetOrdinal("C")).ToString();
            }
            conexion.Close();
        }

        private void cargarDGtutoriaI(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT " +
                "estudiante.codigo," +
                "estudiante.nombre, " +
                "estudiante.apellidos, " +
                "tutoria.codigo as C "+
                "from individual,tutoria,profesor,estudiante " +
                "where profesor.codigo='" + codigo + "' " +
                "&& profesor.codigo=tutoria.tutor " +
                "&& individual.tutoria=tutoria.codigo " +
                "&& individual.estudiante=estudiante.codigo" +
                " ;", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("apellidos"))+" "+myReader.GetString(myReader.GetOrdinal("nombre"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetInt32(myReader.GetOrdinal("C")).ToString();

            }
            conexion.Close();
        }

        private void obtenerCodTutoria(DataGridView DG, TextBox textBox)
        {
            int cod = 0;
            Int32.TryParse(DG.Rows[DG.SelectedRows[0].Index].Cells[2].Value.ToString(), out cod);
            textBox.Text = cod.ToString();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            obtenerCodTutoria(dataGridView1, textBoxCT2);

            cargarDGplan(dataGridView4, textBoxCT2.Text);
        }

        private void buttonNueva_Click(object sender, EventArgs e)
        {
            if (textBoxCT3.Text != "")
            {
                FormBitacora FB = new FormBitacora(textBoxCT3.Text);
                FB.ShowDialog();
            }
            
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            obtenerCodTutoria(dataGridView2, textBoxCT3);
            cargarDGbitacoras(dataGridView3, textBoxCT3.Text);
        }



        private void buttonNueva3_Click(object sender, EventArgs e)
        {
            if (textBoxCT3.Text != "")
            {
                FormBitacora FB = new FormBitacora(textBoxCT3.Text);
                FB.ShowDialog();
                cargarDGbitacoras(dataGridView3, textBoxCT3.Text);
            }
        }

        private void cargarDGbitacoras(DataGridView DG,String indivivual)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT * FROM `bitacora` where individual="+indivivual, conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("objetivo"));
               

            }
            conexion.Close();
        }

        private void cargarDGtPar(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT * FROM par ", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetString(myReader.GetOrdinal("estudianteI"));
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("estudianteR"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("tutoria"));

            }
            conexion.Close();
        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            String cod = dataGridView3.Rows[dataGridView3.SelectedRows[0].Index].Cells[0].Value.ToString();

            if (cod != "")
            {
                FormBitacora FB = new FormBitacora(textBoxCT3.Text,cod);
                FB.ShowDialog();
                cargarDGbitacoras(dataGridView3, textBoxCT3.Text);
            }
        }

        private void buttonNuevoPlan_Click(object sender, EventArgs e)
        {
            if (textBoxCT2.Text!="") {
                FormPlan FP = new FormPlan(textBoxCT2.Text);
                FP.ShowDialog();
                cargarDGplan(dataGridView4, textBoxCT2.Text);
            }
            else
            {
                MessageBox.Show("Seleccione una tutoria", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarDGplan(DataGridView DG, String tutoria)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT * FROM `plan` where tutoria=" + tutoria, conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("plan"));
            }
            conexion.Close();
        }

        private void dataGridView4_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            String cod = dataGridView4.Rows[dataGridView4.SelectedRows[0].Index].Cells[0].Value.ToString();

            if (cod != "")
            {
                FormPlan2 FP2 = new FormPlan2(cod);
                FP2.ShowDialog();
                cargarDGplan(dataGridView4, textBoxCT2.Text);
            }
        }

        public void cargarDGestudiantes(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT * FROM estudiante;", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("apellidos"))+" "+myReader.GetString(myReader.GetOrdinal("nombre")) ;
                
            }
            conexion.Close();
        }

        private void CodigoAtextbox(DataGridView DG, TextBox textBox)
        {
            int cod = 0;
            Int32.TryParse(DG.Rows[DG.SelectedRows[0].Index].Cells[0].Value.ToString(), out cod);
            textBox.Text = cod.ToString();
        }

        public void CargarComboPeriodo(ComboBox CB, List<int> LC)
        {
            CB.Text = "";
            LC.Clear();
            CB.Items.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT codigo," +
                "DATE_FORMAT(inicio,'%d/%m/%Y') AS inicio ," +
                "DATE_FORMAT(final,'%d/%m/%Y') AS final  FROM periodo;", conexion);

            while (myReader.Read())
            {
                int cod = myReader.GetInt32(myReader.GetOrdinal("codigo"));
                String nombre = cod + ": " + myReader.GetString(myReader.GetOrdinal("inicio")) + " -> " + myReader.GetString(myReader.GetOrdinal("final"));
                LC.Add(cod);
                CB.Items.Add(nombre);
            }
            conexion.Close();
        }

        private void dataGridView5_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CodigoAtextbox(dataGridView5,textBoxImparte);
        }

        private void dataGridView6_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CodigoAtextbox(dataGridView6, textBoxRecibe);
        }

        private void buttonPar_Click(object sender, EventArgs e)
        {
            int cod=0;
            if (textBoxImparte.Text != "" && textBoxRecibe.Text != "" && textBoxImparte.Text != textBoxRecibe.Text)
            {

                BD.Insertar("INSERT INTO `tutoria` ( `tutor`, `fecha`, `tipo`, `periodo`) VALUES ( '" + codigo + "', " +
                    "'" + System.DateTime.Now.Year + "" +
                    "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "', 'par', '" + codigosPeriodo[comboBoxPeriodo.SelectedIndex] + "');");


                MySqlConnection conexion = BD.conectar();
                MySqlDataReader myReader = BD.consultar("SELECT codigo FROM tutoria;", conexion);
                while (myReader.Read())
                {
                    cod = myReader.GetInt32(myReader.GetOrdinal("codigo"));
                }
                conexion.Close();

                BD.Insertar("INSERT INTO `par`(" +
                    "`tutoria`, `estudianteI`, `estudianteR`) " +
                    "VALUES ('"+cod+"','"+textBoxImparte.Text+"','"+textBoxRecibe.Text+"')");

                textBoxImparte.Text = "";
                textBoxRecibe.Text = "";
                MessageBox.Show("Estudiantes Asignados");
                cargarDGtPar(dataGridView7);
            }
            else
            {
                MessageBox.Show("Estudiantes no validos");
            }
        }


        private void dataGridView7_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            obtenerCodTutoria(dataGridView7, textBoxParPlan);
            cargarDGplan(dataGridView8,textBoxParPlan.Text);
        }

        private void buttonAplan_Click(object sender, EventArgs e)
        {
            if (textBoxParPlan.Text != "")
            {
                FormPlan FP = new FormPlan(textBoxParPlan.Text);
                FP.ShowDialog();
                cargarDGplan(dataGridView8, textBoxParPlan.Text);
            }
            else
            {
                MessageBox.Show("Seleccione una tutoria", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView8_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            String cod = dataGridView8.Rows[dataGridView8.SelectedRows[0].Index].Cells[0].Value.ToString();

            if (cod != "")
            {
                FormPlan2 FP2 = new FormPlan2(cod);
                FP2.ShowDialog();
                cargarDGplan(dataGridView8, textBoxParPlan.Text);
            }
        }

        private void cargarDGtPar(DataGridView DG,String estudiante)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT * FROM par where estudianteI='"+codigo+ "' || estudianteR='" + codigo + "';", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetString(myReader.GetOrdinal("estudianteI"));
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("estudianteR"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("tutoria"));

            }
            conexion.Close();
        }

        private void dataGridView9_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            obtenerCodTutoria(dataGridView9,textBoxAux);
            cargarDGplan(dataGridView10, textBoxAux.Text);
        }

        private void buttonAP_Click(object sender, EventArgs e)
        {
            if (textBoxAux.Text != "")
            {
                FormPlan FP = new FormPlan(textBoxAux.Text);
                FP.ShowDialog();
                cargarDGplan(dataGridView10, textBoxAux.Text);
            }
            else
            {
                MessageBox.Show("Seleccione una tutoria", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView8_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView10_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            String cod = dataGridView10.Rows[dataGridView10.SelectedRows[0].Index].Cells[0].Value.ToString();

            if (cod != "")
            {
                FormPlan2 FP2 = new FormPlan2(cod);
                FP2.ShowDialog();
                cargarDGplan(dataGridView10, textBoxAux.Text);
            }
        }
    }
}
