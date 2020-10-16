using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grendezvous.Classes.DAO;
using Grendezvous.Forms;
using Grendezvous.Classes.Beans;

namespace Grendezvous.Users
{
    public partial class Prendevous : UserControl
    {
        private int idrende = 0;
        private int idPatient = 0;
        private int iduser = 0;
        //private string port = "";

        public Prendevous()
        {
            InitializeComponent();
        }
        public Prendevous(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }

        #region FONCTIONS

        void loadPatient(PatientDAO patientdoa)
        {
            gridPatient.DataSource = patientdoa.getAll();
        }
        void getPatient()
        {
            int i = gridPatient.CurrentRow.Index;
            idPatient = Convert.ToInt32(gridPatient["Column1", i].Value.ToString());
            labpatient.Text = gridPatient["Column2", i].Value.ToString()+" "+ gridPatient["Column3", i].Value.ToString();
            labadresse.Text = gridPatient["Column5", i].Value.ToString();
            labphone.Text = gridPatient["Column6", i].Value.ToString();
            labetat.Text = gridPatient["Column9", i].Value.ToString();
        }
        void initilizeControls()
        {
            labpatient.Text = "";
            labetat.Text = "";
            labadresse.Text = "";
            labphone.Text = "";
        }

        void save(bool btn, int action)
        {
            if (string.IsNullOrEmpty(labpatient.Text.Trim()))
            {
                MessageBox.Show("Select Patient...", "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Rendevous rendevous = new Rendevous();
                rendevous.Id = Convert.ToInt32(idrende);
                rendevous.Date_ = dateTimePicker1.Value;
                rendevous.Heure = dateTimePicker2.Value.ToShortTimeString();
                rendevous.Statut = "Programmé";
                rendevous.Gynecologue = new UtilisateurDAO().find(iduser);
                rendevous.Malade= new PatientDAO().find(idPatient);
                if (btn)
                {
                    new RendevousDAO().save(action, rendevous);
                    new Success("Bien enregistré !!!").ShowDialog();
                    //initilizeControls();
                }
               
            }
        }

        void sendMessage()
        {
            
        }

        #endregion

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Prendevous_Load(object sender, EventArgs e)
        {
            loadPatient(new PatientDAO());
             Messages.instance().GetAllPorts(comboBox1);
            if(comboBox1.Text == "")
             Messages.instance().SetData(0, 9600, 300);
            else
                Messages.instance().SetData(Convert.ToInt32(comboBox1.Text.Trim()), 9600, 300);
            Messages.instance().Test_port(label6);
        }

        private void gridPatient_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                getPatient();
            }
            catch (Exception ex)
            {
                new Error(ex.Message);
            }
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridPatient.DataSource = new PatientDAO().search(bunifuMaterialTextbox1.Text.Trim());
            }
            catch (Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            save(true, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Messages.instance().GetAllPorts(comboBox1);
                if (comboBox1.Text == "")
                    Messages.instance().SetData(Convert.ToInt32(comboBox1.Text.Trim()), 9600, 300);
                else
                    Messages.instance().SetData(Convert.ToInt32(comboBox1.Text.Trim()), 9600, 300);
                Messages.instance().Test_port(label6);
            }catch(Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Messages.instance().Send(richTextBox1.Text.Trim(), labphone.Text.Trim(), "");
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Bonjour "+labpatient.Text.Trim() +", votre rendez avec le docteur est fixé le "+dateTimePicker1.Text.Trim()
                +" à "+dateTimePicker2.Text.Trim()+". Merci et bonne suite. HealthCare Software";
        }
    }
}
