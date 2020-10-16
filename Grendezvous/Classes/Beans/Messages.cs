using Grendezvous.Forms;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous.Classes.Beans
{
    class Messages
    {
        public static int port, baudRate, timeout;
        public GsmCommMain comm;
        public static Messages message = null;

        public static Messages instance()
        {
            if (message == null)
                message = new Messages();
            return message;
        }

        //Méthode pour recuperer les ports

        public void SetData(int por, int baudRat, int timeoute)
        {
            port = por;
            baudRate = baudRat;
            timeout = timeoute;
        }

        //Méthode pour recuperer le port du modem

        public void GetAllPorts(ComboBox port)
        {
            //string modems = "";
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_POTSModem ");
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if ((string)queryObj["Status"] == "OK")
                    {
                        string pr = "";
                        string por = queryObj["AttachedTo"].ToString(); 
                        for (int i = 0; i < por.Length; i++)
                        {
                            if (por[i] != 'C' && por[i] != 'O' && por[i] != 'M')
                                pr = pr + por[i];
                        }
                        port.Items.Add(pr);
                    }
                    if (port.Items.Count > 0)
                    {
                        port.SelectedIndex = 0;
                    }
                }
                //return modems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la requette", "Erreur de" + ex.Message);
                //return "";
            }
        }

        //Méthode pour tester la configuration des ports
        public void Test_port(Label infomodem)
        {
            Cursor.Current = Cursors.WaitCursor;
            comm = new GsmCommMain(port, baudRate, timeout);
            try
            {
                comm.Open();
                if (!comm.IsConnected())
                {
                    Cursor.Current = Cursors.Default;
                    infomodem.Text = "La connexion au peripherique mobile a echoué.";
                    if (infomodem.Text == "La connexion au peripherique mobile a echoué.")
                    {
                        comm.Close();
                        return;
                    }
                    Cursor.Current = Cursors.WaitCursor;
                }
                else
                {
                    infomodem.Text = "Successfully connected to the phone.";
                    comm.Close();
                }              
            }
            catch (Exception)
            {
                new Error("Branchez un Modem SVP...").ShowDialog();
                return;
            }
        }

        //Méthode pour envoyer un message
        public void Send(string message, string number, string nothing)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                SmsSubmitPdu pdu;
                pdu = new SmsSubmitPdu(message, number, nothing);  // "" indicate SMSC No
                if (!comm.IsOpen())
                    comm.Open();
                comm.SendMessage(pdu);
                new Success("Message envoyé").ShowDialog();
                //MessageBox.Show("", "ENVOIE SIMPLE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comm.Close();
            }
            catch (Exception)
            {
                new Error("L'envoie a échoué").ShowDialog();
                //MessageBox.Show("", "ENVOIE SIMPLE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Cursor.Current = Cursors.Default;
        }

    }
}
