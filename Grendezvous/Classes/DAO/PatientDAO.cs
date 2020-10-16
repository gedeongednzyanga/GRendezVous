using GestionProduit.Classes;
using Grendezvous.Classes.Beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous.Classes.DAO
{
    class PatientDAO : DAO<Patient>
    {
        private Patient getPatient(IDataReader dr)
        {
            Patient patient = new Patient();
            patient.Id = Convert.ToInt32(dr["id"].ToString());
            patient.Nom = dr["nom"].ToString();
            patient.Postnom = dr["postnom"].ToString();
            patient.Age = Convert.ToInt32(dr["age"].ToString());
            patient.Adresse = dr["adresse"].ToString();
            patient.Telephone = dr["telephone"].ToString();
            patient.Medicaments = dr["medicaments"].ToString();
            patient.Jours = Convert.ToInt32(dr["jours"].ToString());
            patient.Etat = dr["etat"].ToString();
            return patient;
        }

        public override Patient find(int id)
        {
            Patient patient = new Patient();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "ONEMALADE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        patient.Id = Convert.ToInt32(dr["id"].ToString()); patient.Nom = dr["nom"].ToString();
                        patient.Postnom = dr["postnom"].ToString(); patient.Age = Convert.ToInt32(dr["age"].ToString());
                        patient.Adresse = dr["adresse"].ToString(); patient.Telephone = dr["telephone"].ToString();
                        patient.Medicaments = dr["medicaments"].ToString(); patient.Jours = Convert.ToInt32(dr["jours"].ToString());
                        patient.Etat = dr["etat"].ToString();
                    }
                    cmd.Dispose();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur survenue...");
            }
            finally
            {
                this.con.Close();
            }
            return patient;
        }

        public override List<Patient > getAll()
        {
            List<Patient> liste = new List<Patient>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SELECT_PATIENT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getPatient(dr));
                    }
                    cmd.Dispose();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur survenue...");
            }
            finally
            {
                this.con.Close();
            }
            return liste;
        }

        public override Patient save(int action, Patient obj)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SAVE_MALADE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@action", 10, DbType.Int32, action));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, obj.Id));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, obj.Nom));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@postnom", 50, DbType.String, obj.Postnom));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@age", 50, DbType.Int32, obj.Age));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@adresse", 50, DbType.String, obj.Adresse));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@telephone", 50, DbType.String, obj.Telephone));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@medicaments", 50, DbType.String, obj.Medicaments));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@jour", 50, DbType.Int32, obj.Jours));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@etat", 50, DbType.String, obj.Etat));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur survenue...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.con.Close();
            }
            return obj;
        }

        public override List<Patient> search(string text)
        {
            List<Patient> liste = new List<Patient>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SEARCH_PATIENT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, text));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getPatient(dr));
                    }
                    cmd.Dispose();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur survenue...");
            }
            finally
            {
                this.con.Close();
            }
            return liste;
        }
    }
}
