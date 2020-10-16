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
    public partial class AllRfait : UserControl
    {
        private int iduser = 0;
        public AllRfait()
        {
            InitializeComponent();
        }

        public AllRfait(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }


        #region FONCTION

        void loadRenevous(RendevousDAO rendedao)
        {
            gridPatient.DataSource = rendedao.getAll(iduser, "RENDE_F");
            label3.Text = rendedao.count4(iduser).ToString();
        }

        #endregion

        private void AllRfait_Load(object sender, EventArgs e)
        {
            loadRenevous(new RendevousDAO());
        }
    }
}
