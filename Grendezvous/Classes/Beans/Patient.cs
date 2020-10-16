using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grendezvous.Classes.Beans
{
    class Patient
    {
        private int id, age, jours;
        private string nom, postnom, adresse, telephone, medicaments, etat;

        public int Id { get => id; set => id = value; }
        public int Age { get => age; set => age = value; }
        public int Jours { get => jours; set => jours = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Postnom { get => postnom; set => postnom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Medicaments { get => medicaments; set => medicaments = value; }
        public string Etat { get => etat; set => etat = value; }
    }
}
