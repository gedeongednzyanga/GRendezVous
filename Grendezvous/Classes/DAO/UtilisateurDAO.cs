using GestionProduit.Classes;
using Grendezvous.Classes.Beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous.Classes.DAO
{
    class UtilisateurDAO : DAO<Utilisateur>
    {
        private Utilisateur getUtilisateur(IDataReader dr)
        {
            Utilisateur utilisateur = new Utilisateur();
            utilisateur.Id = Convert.ToInt32(dr["id"].ToString());
            utilisateur.Nom = dr["nom"].ToString();
            utilisateur.Postnom = dr["postnom"].ToString();
            utilisateur.Prenom = dr["prenom"].ToString();
            utilisateur.Compte = dr["compte"].ToString();
            utilisateur.Username = dr["username"].ToString();
            utilisateur.Password = dr["passworduser"].ToString();
            return utilisateur;
        }

        public override Utilisateur find(int id)
        {
            Utilisateur utilisateur = new Utilisateur();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "ONEUSER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        utilisateur.Id = Convert.ToInt32(dr["id"].ToString()); utilisateur.Nom = dr["nom"].ToString();
                        utilisateur.Postnom = dr["postnom"].ToString(); utilisateur.Prenom = dr["prenom"].ToString();
                        utilisateur.Username = dr["username"].ToString(); utilisateur.Password = dr["passworduser"].ToString();
                        utilisateur.Compte = dr["compte"].ToString();
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
            return utilisateur;
        }

        public Utilisateur Test(string name, string password)
        {
            Utilisateur utilisateur = new Utilisateur();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "TEST_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@name", 50, DbType.String, name));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@password", 50, DbType.String, password));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        utilisateur.Id = Convert.ToInt32(dr["id"].ToString());
                        utilisateur.Username = dr["username"].ToString();
                        utilisateur.Password = dr["passworduser"].ToString();
                        utilisateur.Nom = dr["nom"].ToString();
                        utilisateur.Postnom = dr["postnom"].ToString();
                        utilisateur.Prenom = dr["prenom"].ToString();
                        utilisateur.Compte = dr["compte"].ToString();
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
            return utilisateur;
        }

        public override List<Utilisateur> getAll()
        {
            List<Utilisateur> liste = new List<Utilisateur>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SELECT_USERS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getUtilisateur(dr));
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

        public override Utilisateur save(int action, Utilisateur obj)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using(IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SAVE_AGENT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@action", 10, DbType.Int32, action));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, obj.Id));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, obj.Nom));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@postnom", 50, DbType.String, obj.Postnom));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@prenom", 50, DbType.String, obj.Prenom));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@username", 50, DbType.String, obj.Username));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@password", 50, DbType.String, obj.Password));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@compte", 50, DbType.String, obj.Compte));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@profil", int.MaxValue, DbType.Binary, obj.Profil));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur survenue...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.con.Close();
            }
            return obj;
        }

        public override List<Utilisateur> search(string text)
        {
            List<Utilisateur> liste = new List<Utilisateur>();
            try
            {         
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SEARCH_USER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, text));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getUtilisateur(dr));
                    }
                    cmd.Dispose();
                }             
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur survenue...");
            }
            finally
            {
                this.con.Close();
            }
            return liste;
        }

        private Byte[] convertImageToBinary(Image image)
        {
            Byte[] photo;
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            photo = ms.ToArray();
            return photo;
        }

        public void retreview(int id, PictureBox picP)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (SqlCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "SELECT_PROFIL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Byte[] img = (Byte[])(dr[0]);
                        if (img == null)
                            picP.Image = null;
                        else
                        {
                            MemoryStream ms = new MemoryStream(img);
                            picP.Image = Image.FromStream(ms);
                        }
                    }
                    this.con.Close();
                    cmd.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public void RetreivePhoto(int id, PictureBox photo)
        //{
        //    try
        //    {
        //        if (this.con.State == ConnectionState.Closed)
        //            this.con.Open();
        //        using (IDbCommand cmd = this.con.CreateCommand())
        //        {
        //            cmd.CommandText = "SELECT_PROFIL";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, id));
        //            SqlDataAdapter dt = new SqlDataAdapter((SqlCommand)cmd);
        //            Object resultat = cmd.ExecuteScalar();
        //            if (DBNull.Value == (resultat))
        //            {
        //            }
        //            else
        //            {
        //                Byte[] buffer = (Byte[])resultat;
        //                MemoryStream ms = new MemoryStream(buffer);
        //                Image image = Image.FromStream(ms);
        //                photo.Image = image;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //       // MessageBox.Show("Cette erreur est survenue lors du chargement de la photo : " + ex.Message);
        //    }

        //}
    }
}
