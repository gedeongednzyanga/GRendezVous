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
    public partial class Allrendevous : UserControl
    {
        private int idPatient = 0;
        private int idrende = 0;
        private int iduser = 0;
        public Allrendevous()
        {
            InitializeComponent();
        }

        public Allrendevous(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }

        #region FONCTIONS

        void loadRenevous(RendevousDAO rendedao)
        {
            gridPatient.DataSource = rendedao.getAll(iduser);
            label7.Text = rendedao.count1(iduser).ToString();
            label15.Text = rendedao.count2(iduser).ToString();
            label9.Text = rendedao.count3(iduser).ToString();
            label13.Text = rendedao.count4(iduser).ToString();
        }

        void getPatient()
        {
            int i = gridPatient.CurrentRow.Index;
            idrende = Convert.ToInt32(gridPatient["Column1", i].Value.ToString());
            labpatient.Text = gridPatient["Column2", i].Value.ToString();
            labetat.Text = gridPatient["Column3", i].Value.ToString();
            //labage.Text = gridPatient["Column4", i].Value.ToString();
            labadresse.Text = gridPatient["Column6", i].Value.ToString();
            labphone.Text = gridPatient["Column4", i].Value.ToString();
            //labmedi.Text = gridPatient["Column7", i].Value.ToString();
            //labjour.Text = gridPatient["Column8", i].Value.ToString();
            //labetat.Text = gridPatient["Column3", i].Value.ToString();
            label3.Text = gridPatient["Column9", i].Value.ToString();
        }

        void showReport(object obj)
        {
            Reports report = obj as Reports;
            report.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(report);
            panel1.Show();
            //lab_title.Text = "Identification du patient";
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
                rendevous.Date_ = DateTime.Parse(labphone.Text.Trim());
                rendevous.Heure = label3.Text.Trim();
                if(radioButton1.Checked == true)
                    rendevous.Statut = "Annulé";
                else
                    rendevous.Statut = "Déjà passé";
                rendevous.Gynecologue = new UtilisateurDAO().find(1);
                rendevous.Malade = new PatientDAO().find(idPatient);
                if (btn)
                {
                    new RendevousDAO().save(action, rendevous);
                    new Success("Bien enregistré !!!").ShowDialog();
                }
                loadRenevous(new RendevousDAO());
            }
        }

        #endregion

        private void Allrendevous_Load(object sender, EventArgs e)
        {
            loadRenevous(new RendevousDAO());
          
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            save(true, 2);
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            Reports report = new Reports();
            new RendevousDAO().Call_Report(iduser, report.reportViewer1, "Grendezvous.Reports.ListeRendevous.rdlc");
            showReport(report);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Controls.Add(groupBox1);
            panel1.Show();
        }

        private void textsearch_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridPatient.DataSource = new RendevousDAO().search(textsearch.Text.Trim(), iduser);
            }
            catch (Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Reports report = new Reports();
                new RendevousDAO().Call_Reportt("RENDE_P", iduser, report.reportViewer1, "Grendezvous.Reports.ListeRendevous1.rdlc");
                showReport(report);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            try
            {
                Reports report = new Reports();
                new RendevousDAO().Call_Reportt("RENDE_A", iduser, report.reportViewer1, "Grendezvous.Reports.ListeRendevous1.rdlc");
                showReport(report);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Reports report = new Reports();
                new RendevousDAO().Call_Reportt("RENDE_F", iduser, report.reportViewer1, "Grendezvous.Reports.ListeRendevous1.rdlc");
                showReport(report);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
