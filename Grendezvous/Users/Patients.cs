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
    public partial class Patients : UserControl
    {
        private int idPatient = 0;
        public Patients()
        {
            InitializeComponent();
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
            labnom.Text = gridPatient["Column2", i].Value.ToString();
            labpostnom.Text = gridPatient["Column3", i].Value.ToString();
            labage.Text = gridPatient["Column4", i].Value.ToString();
            labadresse.Text = gridPatient["Column5", i].Value.ToString();
            labphone.Text = gridPatient["Column6", i].Value.ToString();
            labmedi.Text = gridPatient["Column7", i].Value.ToString();
            labjour.Text = gridPatient["Column8", i].Value.ToString();
            labetat.Text = gridPatient["Column9", i].Value.ToString();
        }

        void delete(int action)
        {
            Patient patient = new Patient();
            patient.Id = Convert.ToInt32(idPatient);
            patient.Nom = labnom.Text.Trim();
            patient.Postnom = labpostnom.Text.Trim();
            patient.Age = Convert.ToInt32(labage.Text.Trim());
            patient.Adresse = labadresse.Text.Trim();
            patient.Telephone = labphone.Text.Trim();
            patient.Medicaments = labmedi.Text.Trim();
            patient.Jours = Convert.ToInt32(labjour.Text.Trim());
            patient.Etat = labetat.Text.Trim();
            new PatientDAO().save(action, patient);
            new Success("Patient supprimé !!!").ShowDialog();
            loadPatient(new PatientDAO());
        }

        #endregion

        private void Patients_Load(object sender, EventArgs e)
        {
            loadPatient(new PatientDAO());
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridPatient.DataSource = new PatientDAO().search(bunifuMaterialTextbox1.Text.Trim());
            }catch(Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void gridPatient_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                getPatient();
            }catch(Exception ex)
            {
                new Error(ex.Message);
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            new PatientUpdate(idPatient, labnom.Text.Trim(), labpostnom.Text.Trim(), 
                Convert.ToInt32(labage.Text.Trim()), labadresse.Text.Trim(), labphone.Text.Trim(), 
                labmedi.Text.Trim(), Convert.ToInt32(labjour.Text.Trim()), labetat.Text.Trim()).ShowDialog();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            delete(3);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void labadresse_Click(object sender, EventArgs e)
        {

        }

        private void labnom_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator1_Load(object sender, EventArgs e)
        {

        }
    }
}
