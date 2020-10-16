using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Classes
{
    public abstract class DAO<T>
    {
        protected SqlConnection con = ConnexionDb.getConnexion();
  
        public abstract T save(int action, T obj);
        public abstract T find(int id);
        public abstract List<T> getAll();
        public abstract List<T> search(string text);
    }
}
