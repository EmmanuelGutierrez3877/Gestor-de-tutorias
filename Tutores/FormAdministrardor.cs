using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tutores
{
    public partial class FormAdministrardor : Form
    {
        Form ant;
        int cod;
        int grupoSelect = 0;
        int carreraSelect = 0;
        BaseDeDatos BD = new BaseDeDatos();
        List<int> codigosPeriodo = new List<int>();
        List<int> codigosCarreras = new List<int>();

        public FormAdministrardor(Form ant,int cod)
        {
            this.ant = ant;
            this.cod = cod;
            InitializeComponent();
            this.Text = cod + ": Administrador";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                cargarDataGrid();
            }
            else if(tabControl1.SelectedIndex==1)
            {
                cargarGridCarreras();
                cargarComboGrupos();
            }
            else if (tabControl1.SelectedIndex ==2)
            {
                cargarComboEstudiantes();
                cargarDGgrupo(dataGridView2);
                CargarComboPeriodo(comboBoxPeriodo2,codigosPeriodo);
                CargarComboCarreras(comboBoxCarrera2,codigosCarreras);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                cargartutores(dataGridView4);
                cargarProfesoresNOtutores();
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                CargarDGPeriodos();
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                cargartutores(dataGridView8);
                cargarDGgrupoST(dataGridView9);
                cargarDGGrupo_Profesor(dataGridView10);
                CargarComboPeriodo(comboBoxPeriodo6,codigosPeriodo);
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                CargarDGestudiantesSintutor(dataGridView12);
                cargartutores(dataGridView13);
                CargarComboPeriodo(comboBoxPer7, codigosPeriodo);
                cargarDGIndividual(dataGridView11);
            }


        }

        private void cargarDataGrid()
        {
            List<int> codigos = new List<int>();

            dataGridView1.Rows.Clear();
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar("SELECT codigo FROM usuario;",conexion);

            while (myReader.Read())
            {
                codigos.Add(myReader.GetInt32(myReader.GetOrdinal("codigo")));
            }
            conexion.Close();


            foreach (int c in codigos)
            {
                conexion = BD.conectar();
                myReader = BD.consultar("SELECT nombre,apellidos FROM profesor WHERE codigo=" + c + " ;",conexion);
                
                if (myReader.Read())
                {
                    int renglon = dataGridView1.Rows.Add();
                    dataGridView1.Rows[renglon].Cells["ColumnCodigo"].Value = c;
                    dataGridView1.Rows[renglon].Cells["ColumnNombre"].Value = myReader.GetString(myReader.GetOrdinal("nombre"));
                    dataGridView1.Rows[renglon].Cells["ColumnApellido"].Value = myReader.GetString(myReader.GetOrdinal("apellidos"));
                    dataGridView1.Rows[renglon].Cells["ColumnTipo"].Value = "Profesor";
                    conexion.Close();
                }
                else
                {
                    conexion = BD.conectar();
                    myReader = BD.consultar("SELECT nombre,apellidos FROM estudiante WHERE codigo=" + c + " ;",conexion);

                    if (myReader.Read())
                    {
                        int renglon = dataGridView1.Rows.Add();
                        dataGridView1.Rows[renglon].Cells["ColumnCodigo"].Value = c;
                        dataGridView1.Rows[renglon].Cells["ColumnNombre"].Value = myReader.GetString(myReader.GetOrdinal("nombre"));
                        dataGridView1.Rows[renglon].Cells["ColumnApellido"].Value = myReader.GetString(myReader.GetOrdinal("apellidos"));
                        dataGridView1.Rows[renglon].Cells["ColumnTipo"].Value = "Estudiante";
                        conexion.Close();
                    }
                    else
                    {
                        conexion = BD.conectar();
                        myReader = BD.consultar("SELECT nombre,apellidos FROM administrador WHERE codigo=" + c + " ;",conexion);
                        if (myReader.Read())
                        {
                            int renglon = dataGridView1.Rows.Add();
                            dataGridView1.Rows[renglon].Cells["ColumnCodigo"].Value = c;
                            dataGridView1.Rows[renglon].Cells["ColumnNombre"].Value = myReader.GetString(myReader.GetOrdinal("nombre"));
                            dataGridView1.Rows[renglon].Cells["ColumnApellido"].Value = myReader.GetString(myReader.GetOrdinal("apellidos"));
                            dataGridView1.Rows[renglon].Cells["ColumnTipo"].Value = "Administrador";
                            conexion.Close();
                        }
                    }
                }

            }

        }

        private void Administrardor_Load(object sender, EventArgs e)
        {
            ant.Visible = false;
            cargarDataGrid();
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (textBoxNombre.Text != "" && textBoxApellidos.Text != "" && textBoxPassword.Text != "" && comboBoxTipo.Text != "")
            {
                MySqlConnection conexion = BD.conectar();
                BD.Insertar( "INSERT INTO `usuario` (`codigo`, `password`) VALUES (NULL, '" + textBoxPassword.Text + "');");

                String sentencia="";
                int codigo=0;
                conexion = BD.conectar();
                MySqlDataReader myReader = BD.consultar("SELECT codigo FROM usuario;",conexion);
                while (myReader.Read())
                {
                    codigo=myReader.GetInt32(myReader.GetOrdinal("codigo"));
                }
                conexion.Close();

                if (comboBoxTipo.Text == "Profesor")
                {
                    sentencia = "INSERT INTO `profesor` (`codigo`, `nombre`, `apellidos`, `email`, `carrera_adscrito`) VALUES ('"+codigo+"', '"+textBoxNombre.Text+"', '"+textBoxApellidos.Text+"', '', NULL);";
                }
                else if (comboBoxTipo.Text == "Estudiante")
                {
                    sentencia = "INSERT INTO `estudiante` (`codigo`, `nombre`, `apellidos`) VALUES ('" + codigo + "', '" + textBoxNombre.Text + "', '" + textBoxApellidos.Text + "');";

                }
                else if (comboBoxTipo.Text == "Administrador")
                {
                    sentencia = "INSERT INTO `administrador` (`codigo`, `nombre`, `apellidos`) VALUES ('" + codigo + "', '" + textBoxNombre.Text + "', '" + textBoxApellidos.Text + "');";

                }
                conexion = BD.conectar();

                BD.Insertar(sentencia);

                
                MessageBox.Show("Usuario registrado");
                cargarDataGrid();

                comboBoxTipo.Text = "";
                textBoxNombre.Text = "";
                textBoxApellidos.Text = "";
                textBoxPassword.Text = "";

            }
            else
            {
                MessageBox.Show("DATOS INSUFICIENTES", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Administrardor_FormClosing(object sender, FormClosingEventArgs e)
        {
            ant.Visible = true;
        }

        

        private void cargarDGgrupo(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar("SELECT " +
                "grupo.codigo as CG, " +
                "grupo.nombre as NG, " +
                "DATE_FORMAT(inicio,'%d/%m/%Y') as IP, " +
                "DATE_FORMAT(final,'%d/%m/%Y') as FP, " +
                "carrera.nombre as NC " +
                "FROM grupo,periodo,carrera WHERE grupo.periodo=periodo.codigo&&grupo.carrera=carrera.codigo;",conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("CG")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("NG"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("IP"));
                DG.Rows[renglon].Cells[3].Value = myReader.GetString(myReader.GetOrdinal("FP"));
                DG.Rows[renglon].Cells[4].Value = myReader.GetString(myReader.GetOrdinal("NC"));


            }
            conexion.Close();
        }

        private void cargarDGgrupoST(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar("SELECT " +
                "grupo.codigo as CG, " +
                "grupo.nombre as NG, " +
                "DATE_FORMAT(inicio,'%d/%m/%Y') as IP, " +
                "DATE_FORMAT(final,'%d/%m/%Y') as FP, " +
                "carrera.nombre as NC " +
                "FROM grupo,periodo,carrera WHERE grupo.periodo=periodo.codigo" +
                "&&grupo.carrera=carrera.codigo && grupo.tutor is null;", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("CG")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("NG"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("IP"));
                DG.Rows[renglon].Cells[3].Value = myReader.GetString(myReader.GetOrdinal("FP"));
                DG.Rows[renglon].Cells[4].Value = myReader.GetString(myReader.GetOrdinal("NC"));


            }
            conexion.Close();
        }

        private void buttonGuardarGrupo_Click(object sender, EventArgs e)
        {
            if (textBoxNombreGrupo.Text != ""&&comboBoxPeriodo2.Text!=""&&comboBoxCarrera2.Text!=""){
              
                BD.Insertar("INSERT INTO `grupo` (`nombre`,periodo,carrera) VALUES ('" + textBoxNombreGrupo.Text + "','"+codigosPeriodo[comboBoxPeriodo2.SelectedIndex]+"','"+codigosCarreras[comboBoxCarrera2.SelectedIndex]+"');");
                cargarDGgrupo(dataGridView2);
                
            }
        }


        private void CargarDGestudiantes(int grupo)
        {

            dataGridView3.Rows.Clear();
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar(
                "SELECT * FROM estudiante where grupo='" + 
                grupo + "';"
                ,conexion
                );

            while (myReader.Read())
            {
                int renglon = dataGridView3.Rows.Add();
                dataGridView3.Rows[renglon].Cells["ColumnCodigo3"].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                dataGridView3.Rows[renglon].Cells["ColumnNombre3"].Value = myReader.GetString(myReader.GetOrdinal("nombre"));
                dataGridView3.Rows[renglon].Cells["ColumnApellido3"].Value = myReader.GetString(myReader.GetOrdinal("apellidos"));
            }
            conexion.Close();
        }

        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int cod = 0;
            Int32.TryParse(dataGridView2.Rows[dataGridView2.SelectedRows[0].Index].Cells["ColumnID"].Value.ToString(),out cod);
            labelGrupoE.Text = "Estudiantes del grupo "+cod;
            grupoSelect = cod;
            CargarDGestudiantes(cod);
        }

        private void cargarComboEstudiantes()
        {
            comboBoxEstudiantes.Text = "";
            comboBoxEstudiantes.Items.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT * FROM estudiante;",conexion);

            while (myReader.Read())
            {
                comboBoxEstudiantes.Items.Add(myReader.GetString(myReader.GetOrdinal("codigo")));

            }
            conexion.Close();
        }

        private void buttonRegistrarEstudiante_Click(object sender, EventArgs e)
        {
            if (grupoSelect>0 && comboBoxEstudiantes.Text != "")
            {
                BD.Insertar("UPDATE estudiante SET grupo=" + grupoSelect.ToString() + " WHERE CODIGO='" + comboBoxEstudiantes.Text + "';");
                
                MessageBox.Show("Estudiante registrado");
                CargarDGestudiantes(grupoSelect);
            }
        }

        private void cargartutores(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();          

            MySqlDataReader myReader = BD.consultar("SELECT * FROM tutor,profesor where tutor.codigo=profesor.codigo;",conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("nombre"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("apellidos"));
            }
            conexion.Close();
        }

        private void cargarProfesoresNOtutores()
        {
            List<int> Profesores = new List<int>();
            List<int> Tutores = new List<int>();
            //List<int> NoTutores = new List<int>();
            comboBoxNoTutores.Items.Clear();

            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar("SELECT codigo FROM profesor;",conexion);

            while (myReader.Read())
            {
                Profesores.Add(myReader.GetInt32(myReader.GetOrdinal("codigo")));
            }
            conexion.Close();

            conexion = BD.conectar();
            myReader = BD.consultar("SELECT codigo FROM tutor;",conexion);

            while (myReader.Read())
            {
                Tutores.Add(myReader.GetInt32(myReader.GetOrdinal("codigo")));
            }
            conexion.Close();

            foreach (int i in Profesores)
            {
                if (!Tutores.Contains(i))
                {
                    //NoTutores.Add(i);
                    comboBoxNoTutores.Items.Add(i);
                }
            }

            
        }

        private void buttonVolverTutor_Click(object sender, EventArgs e)
        {
            if (comboBoxNoTutores.Text != "")
            {
                BD.Insertar("INSERT INTO `tutor` (`codigo`) VALUES ('" + comboBoxNoTutores.Text + "');");
            }

            cargarProfesoresNOtutores();
            cargartutores(dataGridView4);
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int cod = 0;
            Int32.TryParse(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["ColumnCodigo"].Value.ToString(), out cod);
            String tipo = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["ColumnTipo"].Value.ToString();
            Form1 f1 = new Form1(cod,tipo,this);
            f1.ShowDialog();
            cargarDataGrid();
        }

        private void cargarGridCarreras()
        {
            dataGridView5.Rows.Clear();
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar(
                "SELECT * FROM carrera;"
                , conexion
                );

            while (myReader.Read())
            {
                int renglon = dataGridView5.Rows.Add();
                dataGridView5.Rows[renglon].Cells["ColumnCodigo5"].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                dataGridView5.Rows[renglon].Cells["ColumnNombre5"].Value = myReader.GetString(myReader.GetOrdinal("nombre"));
                dataGridView5.Rows[renglon].Cells["ColumnNivel5"].Value = myReader.GetString(myReader.GetOrdinal("nivel"));
                dataGridView5.Rows[renglon].Cells["ColumnArea5"].Value = myReader.GetString(myReader.GetOrdinal("area"));
                //dataGridView5.Rows[renglon].Cells["ColumnApellido3"].Value = myReader.GetString(myReader.GetOrdinal("apellidos"));
            }
            conexion.Close();
        }

        private void buttonRegistrarCarrera_Click(object sender, EventArgs e)
        {
            if (textBoxNombreC.Text != "" &&
                textBoxAreaC.Text != "" &&
                textBoxNivelC.Text != "")
            {
                BD.Insertar("INSERT INTO `carrera` ( `nombre`, `nivel`, `area`) VALUES ('"+
                    textBoxNombreC.Text+"', '"+textBoxNivelC.Text+"', '"+textBoxAreaC.Text+"');");
                textBoxNombreC.Text = "";
                textBoxAreaC.Text = "";
                textBoxNivelC.Text = "";
            }
            
            cargarGridCarreras();
        }

        private void dataGridView5_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int cod = 0;
            Int32.TryParse(dataGridView5.Rows[dataGridView5.SelectedRows[0].Index].Cells["ColumnCodigo5"].Value.ToString(), out cod);
            label6.Text = "Grupos de la carrera " + cod;
            carreraSelect = cod;
            CargarDGgrupos(cod);
        }

        private void CargarDGgrupos(int cod)
        {
            dataGridView6.Rows.Clear();
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar(
                "SELECT * FROM Grupo where carrera='" + cod + "';"
                , conexion
                );

            while (myReader.Read())
            {
                int renglon = dataGridView6.Rows.Add();
                dataGridView6.Rows[renglon].Cells["ColumnID6"].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                dataGridView6.Rows[renglon].Cells["ColumnNombre6"].Value = myReader.GetString(myReader.GetOrdinal("nombre"));
               
            }
            conexion.Close();
        }

        private void cargarComboGrupos()
        {
            comboBoxGrupos.Text = "";
            comboBoxGrupos.Items.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT * FROM grupo;", conexion);

            while (myReader.Read())
            {
                comboBoxGrupos.Items.Add(myReader.GetString(myReader.GetOrdinal("codigo")));

            }
            conexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (carreraSelect > 0 && comboBoxGrupos.Text != "")
            {
                BD.Insertar("UPDATE grupo SET carrera=" + carreraSelect.ToString() + " WHERE CODIGO='" + comboBoxGrupos.Text + "';");

                MessageBox.Show("Grupo registrado");
                int aux = 0;
                Int32.TryParse(comboBoxGrupos.Text,out aux);
                CargarDGgrupos(aux);
            }
        }

        private void CargarDGPeriodos()
        {
            dataGridView7.Rows.Clear();
            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar(
                "SELECT codigo," +
                "DATE_FORMAT(inicio,'%d/%m/%Y') AS inicio ," +
                "DATE_FORMAT(final,'%d/%m/%Y') AS final  FROM periodo;"
                , conexion
                );

            while (myReader.Read())
            {
                int renglon = dataGridView7.Rows.Add();
                dataGridView7.Rows[renglon].Cells["ColumnCodigo7"].Value = myReader.GetInt32(myReader.GetOrdinal("codigo")).ToString();
                dataGridView7.Rows[renglon].Cells["ColumnInicio7"].Value = myReader.GetString(myReader.GetOrdinal("inicio"));
                dataGridView7.Rows[renglon].Cells["ColumnFinal7"].Value = myReader.GetString(myReader.GetOrdinal("final"));

            }
            conexion.Close();
        }

        private void buttonAgregarPeriodo_Click(object sender, EventArgs e)
        {
            if (dateTimePickerPeriodoInicio.Value < dateTimePickerPeriodoFinal.Value)
            {
                BD.Insertar("INSERT INTO `periodo` ( `inicio`, `final`) VALUES (" +
                    "'"+dateTimePickerPeriodoInicio.Value.Year+"-"+dateTimePickerPeriodoInicio.Value.Month+"-"+dateTimePickerPeriodoInicio.Value.Day+"', " +
                    "'"+dateTimePickerPeriodoFinal.Value.Year+"-"+dateTimePickerPeriodoFinal.Value.Month+"-"+dateTimePickerPeriodoFinal.Value.Day+"'" +
                    ");");
                MessageBox.Show("Periodo Registrado");
                CargarDGPeriodos();
            }
            else
            {
                MessageBox.Show("FECHAS INCONSISTENTES", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView7_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CodigoAtextbox(dataGridView7, textBoxModificarPeriodo);
        }

        private void CodigoAtextbox(DataGridView DG,TextBox textBox)
        {
            int cod = 0;
            Int32.TryParse(DG.Rows[DG.SelectedRows[0].Index].Cells[0].Value.ToString(), out cod);
            textBox.Text = cod.ToString();
        }

        private void buttonModificarPeriodo_Click(object sender, EventArgs e)
        {
            if (dateTimePickerModificarInicio.Value < dateTimePickerModificarFinal.Value)
            {
                BD.Insertar("UPDATE `periodo` SET inicio=" +
                    "'" + dateTimePickerModificarInicio.Value.Year + "-" + dateTimePickerModificarInicio.Value.Month + "-" + dateTimePickerModificarInicio.Value.Day + "', " +
                    "final ="+
                    "'" + dateTimePickerModificarFinal.Value.Year + "-" + dateTimePickerModificarFinal.Value.Month + "-" + dateTimePickerModificarFinal.Value.Day + "'" +
                    " WHERE codigo="+textBoxModificarPeriodo.Text+";");
                MessageBox.Show("Periodo Modificado");
                CargarDGPeriodos();
            }
            else
            {
                MessageBox.Show("FECHAS INCONSISTENTES", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView8_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CodigoAtextbox(dataGridView8,textBoxCodProf);
        }

        private void dataGridView9_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CodigoAtextbox(dataGridView9, textBoxCodGrup);
        }

        private void buttonAsignarTutor_Click(object sender, EventArgs e)
        {
            int cod = 0;
            if (textBoxCodGrup.Text != "" && textBoxCodProf.Text != "" &&comboBoxPeriodo6.Text!="")
            {
                BD.Insertar("UPDATE `grupo` SET tutor="+textBoxCodProf.Text+" where codigo="+textBoxCodGrup.Text+";");

                MySqlConnection conexion = BD.conectar();
                MySqlDataReader myReader = BD.consultar("SELECT codigo FROM tutoria;", conexion);
                while (myReader.Read())
                {
                    cod = myReader.GetInt32(myReader.GetOrdinal("codigo"));
                }
                conexion.Close();
                cod++;

                BD.Insertar("INSERT INTO `tutoria` (`tutor`, `fecha`, `tipo`, `periodo`) VALUES ('"+ textBoxCodProf.Text + "', " +
                    "'"+System.DateTime.Now.Year+"" +
                    "-"+DateTime.Now.Month+"-"+DateTime.Now.Day+"', 'grupal', '"+codigosPeriodo[comboBoxPeriodo6.SelectedIndex]+"');");
                cargarDGGrupo_Profesor(dataGridView10);


                

                BD.Insertar("INSERT INTO `grupal` ( `tutoria`,`grupo`) VALUES ( '" + cod+"','"+ textBoxCodGrup.Text + "');");
                cargarDGGrupo_Profesor(dataGridView10);
                cargarDGgrupoST(dataGridView9);
                textBoxCodGrup.Text = "";
                textBoxCodProf.Text = "";
                MessageBox.Show("Profesor Asignado");

            }
            else
            {
                MessageBox.Show("DATOS INSUFICIENTES", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarDGGrupo_Profesor(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT grupo.codigo as GC,grupo.nombre as GN, " +
                "profesor.codigo as PC, profesor.Apellidos as PA , profesor.nombre as PN " +
                "FROM grupo,profesor where grupo.tutor=profesor.codigo;", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("GC")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("GN"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("PC"));
                DG.Rows[renglon].Cells[3].Value = myReader.GetString(myReader.GetOrdinal("PA"))+" "+ myReader.GetString(myReader.GetOrdinal("PN"));
            }
            conexion.Close();
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
                String nombre = cod + ": " + myReader.GetString(myReader.GetOrdinal("inicio"))+" -> " + myReader.GetString(myReader.GetOrdinal("final"));
                LC.Add(cod);
                CB.Items.Add(nombre);
            }
            conexion.Close();
        }

        public void CargarComboCarreras(ComboBox CB, List<int> LC)
        {
            CB.Text = "";
            LC.Clear();
            CB.Items.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT * FROM carrera;", conexion);

            while (myReader.Read())
            {
                int cod = myReader.GetInt32(myReader.GetOrdinal("codigo"));
                String nombre = cod + ": " + myReader.GetString(myReader.GetOrdinal("nombre")) ;
                LC.Add(cod);
                CB.Items.Add(nombre);
            }
            conexion.Close();
        }

        private void CargarDGestudiantesSintutor(DataGridView DG)
        {
            List<int> ind = new List<int>();

            MySqlConnection conexion = BD.conectar();
            MySqlDataReader myReader = BD.consultar("SELECT estudiante FROM individual;", conexion);

            while (myReader.Read())
            {               
                ind.Add(myReader.GetInt32(myReader.GetOrdinal("estudiante")));
            }
            conexion.Close();


            DG.Rows.Clear();
            conexion = BD.conectar();
            myReader = BD.consultar("SELECT * FROM estudiante;", conexion);

            while (myReader.Read())
            {
                int cod = myReader.GetInt32(myReader.GetOrdinal("codigo"));
                if (!ind.Contains(cod))
                {
                    int renglon = DG.Rows.Add();
                    DG.Rows[renglon].Cells[0].Value = cod.ToString();
                    DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("apellidos"));
                    DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("nombre"));
                    
                }

            }
            conexion.Close();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int cod = 0;
            if (textBoxCodEst7.Text != "" && textBoxCodProf7.Text != "" && comboBoxPer7.Text != "")
            {
                
                BD.Insertar("INSERT INTO `tutoria` ( `tutor`, `fecha`, `tipo`, `periodo`) VALUES ( '" + textBoxCodProf7.Text + "', " +
                    "'" + System.DateTime.Now.Year + "" +
                    "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "', 'individual', '" + codigosPeriodo[comboBoxPer7.SelectedIndex] + "');");


                MySqlConnection conexion = BD.conectar();
                MySqlDataReader myReader = BD.consultar("SELECT codigo FROM tutoria;", conexion);
                while (myReader.Read())
                {
                    cod = myReader.GetInt32(myReader.GetOrdinal("codigo"));
                }
                conexion.Close();

                BD.Insertar("INSERT INTO `individual` ( `tutoria`,`estudiante`) VALUES ( '" + cod + "','"+ textBoxCodEst7.Text + "');");

                CargarDGestudiantesSintutor(dataGridView12);
                cargartutores(dataGridView13);
                CargarComboPeriodo(comboBoxPer7, codigosPeriodo);
                cargarDGIndividual(dataGridView11);
                textBoxCodEst7.Text = "";
                textBoxCodProf7.Text = "";
                MessageBox.Show("Profesor Asignado");

                
            }
            else
            {
                MessageBox.Show("DATOS INSUFICIENTES", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView12_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CodigoAtextbox(dataGridView12, textBoxCodEst7);
        }

        private void dataGridView13_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            CodigoAtextbox(dataGridView13, textBoxCodProf7);
        }

        public void cargarDGIndividual(DataGridView DG)
        {
            DG.Rows.Clear();
            MySqlConnection conexion = BD.conectar();

            MySqlDataReader myReader = BD.consultar("SELECT " +
                "estudiante.codigo as EC, " +
                "estudiante.nombre as EN, " +
                "estudiante.apellidos as EA, " +
                "profesor.codigo as PC, " +
                "profesor.nombre as PN, " +
                "profesor.apellidos as PA " +
                " FROM tutoria,individual,profesor,estudiante where " +
                "individual.tutoria=tutoria.codigo && " +
                "tutoria.tutor=profesor.codigo && " +
                "individual.estudiante=estudiante.codigo ;", conexion);

            while (myReader.Read())
            {
                int renglon = DG.Rows.Add();
                DG.Rows[renglon].Cells[0].Value = myReader.GetInt32(myReader.GetOrdinal("EC")).ToString();
                DG.Rows[renglon].Cells[1].Value = myReader.GetString(myReader.GetOrdinal("EA"))+" "+ myReader.GetString(myReader.GetOrdinal("EN"));
                DG.Rows[renglon].Cells[2].Value = myReader.GetString(myReader.GetOrdinal("PC"));
                DG.Rows[renglon].Cells[3].Value = myReader.GetString(myReader.GetOrdinal("PA")) + " " + myReader.GetString(myReader.GetOrdinal("PN"));
            }
            conexion.Close();

        }
    }
}
