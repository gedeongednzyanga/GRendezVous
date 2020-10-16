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

namespace Grendezvous.Users
{
    public partial class AllRprogramme : UserControl
    {
        private int iduser = 0;
        public AllRprogramme()
        {
            InitializeComponent();
        }

        public AllRprogramme(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }

        #region FONCTION

        void loadRenevous(RendevousDAO rendedao)
        {
            gridPatient.DataSource = rendedao.getAll(iduser, "RENDE_P");
            label3.Text = rendedao.count2(iduser).ToString();
        }

        #endregion

        private void AllRprogramme_Load(object sender, EventArgs e)
        {
            loadRenevous(new RendevousDAO());
        }
    }
}
