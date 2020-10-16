using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grendezvous.Classes.Beans
{
    class Rendevous
    {
        private int id;
        private DateTime date_;
        private string heure;
        private string statut;
        private Utilisateur gynecologue;
        private Patient malade;
        private string agent, patient, telephone, etat;


        public int Id { get => id; set => id = value; }
        public DateTime Date_ { get => date_; set => date_ = value; }
        public string Heure { get => heure; set => heure = value; }
        public string Statut { get => statut; set => statut = value; }
        public string Agent { get => agent; set => agent = value; }
        public string Patient { get => patient; set => patient = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Etat { get => etat; set => etat = value; }
        internal Utilisateur Gynecologue { get => gynecologue; set => gynecologue = value; }
        internal Patient Malade { get => malade; set => malade = value; }
    }
}
