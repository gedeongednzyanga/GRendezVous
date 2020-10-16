using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProduit.Classes
{
    class Parametre
    {
        private Parametre() { }
        public static Parametre _instance = null;
        public static Parametre Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Parametre();
                return _instance;
            }
        }

        public IDbDataParameter AjouterParametre(IDbCommand command, string nomParametre, int taille, DbType type, object valeur)
        {
            IDbDataParameter param = command.CreateParameter();
            param.ParameterName = nomParametre;
            param.Size = taille;
            param.DbType = type;
            param.Value = valeur;
            return param;
        }
    }
}
