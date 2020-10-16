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
using System.IO;

namespace Grendezvous.Users
{
    public partial class Utilisateurs : UserControl
    {
        private string pathPhoto = "";
        private int iduser = 0;
        public Utilisateurs()
        {
            InitializeComponent();
        }

        public Utilisateurs(int id, string nom, string postnom, string prenom, string compte, string username, string motpasse )
        {
            InitializeComponent();
            this.iduser = id;
            this.textNom.Text = nom;
            this.textPostnom.Text = postnom;
            this.textPrenom.Text = prenom;
            this.fonction.Text = compte;
            this.textUsername.Text = username;
            this.textPassword.Text = motpasse;
            this.btnSave.ButtonText = "Modifier";
            new UtilisateurDAO().retreview(iduser, photoUser);
        }

        #region FONCTIONS
        void initilizeControls()
        {
            textNom.Text = "";
            textPostnom.Text = "";
            textPrenom.Text="";
            fonction.Text = "";
            textUsername.Text = "";
            textPassword.Text = "";
        }
        void getUser()
        {
            //int i = gridUtilisateur.CurrentRow.Index;
            //iduser = Convert.ToInt32(gridUtilisateur["Column1", i].Value.ToString());
            //textNom.Text = gridUtilisateur["Column2", i].Value.ToString();
            //textPostnom.Text = gridUtilisateur["Column3", i].Value.ToString();
            //textPrenom.Text = gridUtilisateur["Column4", i].Value.ToString();
            //comboCompte.Text = gridUtilisateur["Column5", i].Value.ToString();
            //textUsername.Text = gridUtilisateur["Column6", i].Value.ToString();
            //textPassword.Text = gridUtilisateur["Column7", i].Value.ToString();
            new UtilisateurDAO().retreview(iduser, photoUser);
            //btnSave.ButtonText = "Modifier";
            //DialogResult dlr = MessageBox.Show("Voulez-vous supprimer ?", "Message...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dlr == DialogResult.Yes) { save(true, 3); }
        }
        void loadUser(UtilisateurDAO userdao)
        {
           // gridUtilisateur.DataSource = userdao.getAll();
        }

        private Byte[] convertImageToBinary(Image image)
        {
            Byte[] photo;
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            photo = ms.ToArray();
            return photo;
        }

        void save(bool btn, int action)
        {
            if (string.IsNullOrEmpty(textNom.Text.Trim()) || string.IsNullOrEmpty(textPostnom.Text.Trim()) || 
                string.IsNullOrEmpty(textPrenom.Text.Trim()) || string.IsNullOrEmpty(fonction.Text.Trim()) ||
                string.IsNullOrEmpty(textUsername.Text.Trim()) || string.IsNullOrEmpty(textPassword.Text.Trim()))
            {
                MessageBox.Show("Complètez tous les champs.", "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Utilisateur utilisateur = new Utilisateur();
                utilisateur.Id = Convert.ToInt32(iduser);
                utilisateur.Nom = textNom.Text.Trim();
                utilisateur.Postnom = textPostnom.Text.Trim();
                utilisateur.Prenom = textPrenom.Text.Trim();
                utilisateur.Username = textUsername.Text.Trim();
                utilisateur.Password = textPassword.Text.Trim();
                utilisateur.Compte = fonction.Text.Trim();
                utilisateur.Profil = convertImageToBinary(photoUser.Image);
                if (btn)
                {
                    new UtilisateurDAO().save(action, utilisateur);
                }
                if(action == 3)
                {
                    new Success("Utilisateur supprimé !!!").ShowDialog();
                }
                else
                {
                    new Success("Utilisateur enregistré !!!").ShowDialog();
                }
                
                loadUser(new UtilisateurDAO());
                initilizeControls();
            }
        }

        #endregion


        private void Utilisateurs_Load(object sender, EventArgs e)
        {
            loadUser(new UtilisateurDAO());
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "JPG Files (*.jpg)|*.jpg| all files(*.*)|*.*";
                fd.ShowDialog();
                pathPhoto = fd.FileName.ToString();
                photoUser.Image = Image.FromFile(pathPhoto);
               // bunifuMaterialTextbox1.Text = pathPhoto;
            }
            catch(Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void gridUtilisateur_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            getUser();
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
               // gridUtilisateur.DataSource = new UtilisateurDAO().search(bunifuMaterialTextbox1.Text.Trim());
            }catch(Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (btnSave.ButtonText) 
            {
                case "Create Account":
                    save(true, 1);
                    break;
                case "Modifier":
                    save(true, 2);
                    btnSave.ButtonText = "Create Account";
                    break;
            }

        }

        private void comboCompte_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
