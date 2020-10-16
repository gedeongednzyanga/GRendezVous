using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grendezvous.Classes.Beans;
using Grendezvous.Classes.DAO;
using Grendezvous.Forms;

namespace Grendezvous.Users
{
    public partial class Identification : UserControl
    {
        private int idPatient = 0;
        public Identification()
        {
            InitializeComponent();
        }
        #region FONCTIONS

        void initilizeControls()
        {
            textNom.Text = "";
            textPostnom.Text = "";
            textAge.Text = "";
            textAdresse.Text = "";
            textPhone.Text = "";
            textMedicament.Text= "";
            textJour.Text = ""; textEtat.Text = "";
        }
        void save(bool btn, int action)
        {
            if (string.IsNullOrEmpty(textNom.Text.Trim()) || string.IsNullOrEmpty(textPostnom.Text.Trim()) ||
                string.IsNullOrEmpty(textAge.Text.Trim()) || string.IsNullOrEmpty(textAdresse.Text.Trim()) ||
                string.IsNullOrEmpty(textPhone.Text.Trim()) || string.IsNullOrEmpty(textMedicament.Text.Trim()) ||
                string.IsNullOrEmpty(textJour.Text.Trim()) || string.IsNullOrEmpty(textEtat.Text.Trim()))
            {
                MessageBox.Show("Complètez tous les champs.", "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Patient patient = new Patient();
                patient.Id = Convert.ToInt32(idPatient);
                patient.Nom = textNom.Text.Trim();
                patient.Postnom = textPostnom.Text.Trim();
                patient.Age = Convert.ToInt32(textAge.Text.Trim());
                patient.Adresse = textAdresse.Text.Trim();
                patient.Telephone = textPhone.Text.Trim();
                patient.Medicaments = textMedicament.Text.Trim();
                patient.Jours =Convert.ToInt32(textJour.Text.Trim());
                patient.Etat = textEtat.Text.Trim();
                if (btn)
                {
                    new PatientDAO().save(action, patient);
                }
                new Success("Patient enregistré !!!").ShowDialog();
                initilizeControls();
            }
        }

        #endregion

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            save(true, 1);
        }

        private void textAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void textJour_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
    }
}
