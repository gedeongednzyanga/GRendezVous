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
    public partial class AllRannule : UserControl
    {
        private int iduser = 0;
        public AllRannule()
        {
            InitializeComponent();
        }
        public AllRannule(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }


        #region FONCTION

        void loadRenevous(RendevousDAO rendedao)
        {
            gridPatient.DataSource = rendedao.getAll(iduser, "RENDE_A");
            label3.Text = rendedao.count3(iduser).ToString();
        }

        #endregion
        private void AllRannule_Load(object sender, EventArgs e)
        {
            loadRenevous(new RendevousDAO());
        }
    }
}
