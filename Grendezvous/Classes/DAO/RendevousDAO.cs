using GestionProduit.Classes;
using Grendezvous.Classes.Beans;
using Microsoft.Reporting.WinForms;
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
    class RendevousDAO : DAO<Rendevous>
    {
        private Rendevous getRendevous(IDataReader dr)
        {
            Rendevous rende = new Rendevous();
            rende.Id = Convert.ToInt32(dr["id"].ToString());
            rende.Date_ = DateTime.Parse(dr["date_"].ToString());
            rende.Heure = dr["heure"].ToString();
            rende.Statut = dr["statut"].ToString();
            rende.Patient = dr["malade"].ToString();
            rende.Telephone = dr["telephone"].ToString();
            rende.Etat = dr["etat"].ToString();
            rende.Agent = dr["agent"].ToString();
            rende.Gynecologue = new UtilisateurDAO().find(Convert.ToInt32( dr["ida"].ToString()));
            rende.Malade = new PatientDAO().find(Convert.ToInt32(dr["idm"].ToString()));
            return rende;
        }

        public override Rendevous find(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Rendevous> getAll()
        {
            List<Rendevous> liste = new List<Rendevous>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SELECT_RENDE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getRendevous(dr));
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

        public List<Rendevous> getAll(int id)
        {
            List<Rendevous> liste = new List<Rendevous>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SELECT_RENDE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@user", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getRendevous(dr));
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

        public List<Rendevous> getAll(int id, string procedure)
        {
            List<Rendevous> liste = new List<Rendevous>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getRendevous(dr));
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
        public override Rendevous save(int action, Rendevous obj)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SAVE_RENDEZ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@action", 10, DbType.Int32, action));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, obj.Id));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@date", 50, DbType.Date, obj.Date_));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@heure", 50, DbType.String, obj.Heure));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@statut", 50, DbType.String, obj.Statut));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@gyneco", 50, DbType.String, obj.Gynecologue.Id));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@malade", 50, DbType.String, obj.Malade.Id));
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

        public override List<Rendevous> search(string text)
        {
            List<Rendevous> liste = new List<Rendevous>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SEARCH_RENDE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, text));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getRendevous(dr));
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

        public List<Rendevous> search(string text, int user)
        {
            List<Rendevous> liste = new List<Rendevous>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SEARCH_RENDE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, text));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@user", 10, DbType.Int32, user));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getRendevous(dr));
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

        public int count1(int ida)
        {
            int nbr = 0;
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "COUNT1";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, ida));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nbr = Convert.ToInt32(dr["nbrendevous"].ToString());
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
            return nbr;
        }

        public int count2(int ida)
        {
            int nbr = 0;
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "COUNT2";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, ida));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nbr = Convert.ToInt32(dr["nbrendevous"].ToString());
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
            return nbr;
        }

        public int count3(int ida)
        {
            int nbr = 0;
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "COUNT3";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, ida));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nbr = Convert.ToInt32(dr["nbrendevous"].ToString());
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
            return nbr;
        }

        public int count4(int ida)
        {
            int nbr = 0;
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "COUNT4";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, ida));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nbr = Convert.ToInt32(dr["nbrendevous"].ToString());
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
            return nbr;
        }

        public void Call_Report(int id, ReportViewer reportView, string path)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SELECT_RENDE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@user", 10, DbType.Int32, id));
                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataSet_Rendevous");
                    reportView.LocalReport.DataSources.Clear();
                    reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet_Rendevous", ds.Tables[0]));
                    reportView.LocalReport.ReportEmbeddedResource = path;
                    reportView.RefreshReport();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (this.con != null)
                {
                    if (this.con.State == ConnectionState.Open)
                        this.con.Close();
                }
            }
        }

        public void Call_Reportt(string procedure, int id, ReportViewer reportView, string path)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, id));
                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "DataSet_Rendevous");
                    reportView.LocalReport.DataSources.Clear();
                    reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet_Rendevous", ds.Tables[0]));
                    reportView.LocalReport.ReportEmbeddedResource = path;
                    reportView.RefreshReport();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (this.con != null)
                {
                    if (this.con.State == ConnectionState.Open)
                        this.con.Close();
                }
            }
        }

    }
}
