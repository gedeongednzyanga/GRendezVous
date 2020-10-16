using Grendezvous.Classes.Beans;
using Grendezvous.Classes.DAO;
using Grendezvous.Forms;
using Grendezvous.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous
{
    public partial class Form1 : Form
    {
        Utilisateur utilisateur = null;
        public Form1()
        {
            InitializeComponent();
        }

        #region FONCTIONS SHOWS

        void showIdentification(object obj)
        {
            Identification identification = obj as Identification;
            identification.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(identification);
            panelParent.Show();
            lab_title.Text = "Identification du patient";
        }

        void showPatients(object obj)
        {
            Patients patients = obj as Patients;
            patients.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(patients);
            panelParent.Show();
            lab_title.Text = "Patients Identifiés";
        }

        void showUtilisateurs(object obj)
        {
            Utilisateurs utilisateurs = obj as Utilisateurs;
            utilisateurs.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(utilisateurs);
            panelParent.Show();
            lab_title.Text = "Mon Compte";
        }

        void showPrendevous(object obj)
        {
            Prendevous rendevous = obj as Prendevous;
            rendevous.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(rendevous);
            panelParent.Show();
            lab_title.Text = "Programmer rendez-vous";
        }

        void showAllrendevous(object obj)
        {
            Allrendevous allrendevous = obj as Allrendevous;
            allrendevous.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(allrendevous);
            panelParent.Show();
            lab_title.Text = "Tous les rendez-vous";
        }
        void showProrendevous(object obj)
        {
            AllRprogramme allrendevous = obj as AllRprogramme;
            allrendevous.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(allrendevous);
            panelParent.Show();
            lab_title.Text = "Rendez-vous programmés";
        }
        void showArendevous(object obj)
        {
            AllRannule allrendevous = obj as AllRannule;
            allrendevous.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(allrendevous);
            panelParent.Show();
            lab_title.Text = "Rendez-vous annulés";
        }
        void showFrendevous(object obj)
        {
            AllRfait allrendevous = obj as AllRfait;
            allrendevous.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(allrendevous);
            panelParent.Show();
            lab_title.Text = "Rendez-vous réalisés";
        }

        void testConnexion()
        {
            try
            {
                utilisateur = new UtilisateurDAO().Test(textUsername.Text.Trim(), textPassword.Text.Trim());
                if (string.IsNullOrEmpty(utilisateur.Username) || string.IsNullOrEmpty(utilisateur.Password))
                {
                    new Error("Champs vides...").ShowDialog();
                    utilisateur = null;
                }
                else
                {
                    new UtilisateurDAO().retreview(utilisateur.Id, gunaCirclePictureBox2);
                    new Success("Vous êtes connectés").ShowDialog();
                    new UtilisateurDAO().retreview(utilisateur.Id, profil1);
                    labcompte.Text = utilisateur.Compte;
                    labuser.Text = utilisateur.Nom + " " + utilisateur.Prenom;
                    showAllrendevous(new Allrendevous(utilisateur.Id));
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur...");
            }
        }

        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showIdentification(new Identification());
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showPatients(new Patients());
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showUtilisateurs(new Utilisateurs(utilisateur.Id, utilisateur.Nom, utilisateur.Postnom, utilisateur.Prenom, utilisateur.Compte, utilisateur.Username, utilisateur.Password));
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showPatients(new Patients());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            testConnexion();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showPrendevous(new Prendevous(utilisateur.Id));
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if( utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showAllrendevous(new Allrendevous(utilisateur.Id));
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            showUtilisateurs(new Utilisateurs());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panelParent.Controls.Clear();
            panelParent.Controls.Add(label2);
            panelParent.Controls.Add(groupBox1);
            panelParent.Controls.Add(bunifuThinButton21);
            panelParent.Show();
            utilisateur = null;
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            panelParent.Controls.Clear();
            panelParent.Controls.Add(label2);
            panelParent.Controls.Add(groupBox1);
            panelParent.Controls.Add(bunifuThinButton21);
            panelParent.Show();
            utilisateur = null;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showProrendevous(new AllRprogramme(utilisateur.Id));
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showArendevous(new AllRannule(utilisateur.Id));
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showFrendevous(new AllRfait(utilisateur.Id));
        }
    }
}
