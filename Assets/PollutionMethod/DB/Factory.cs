using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DB
{
    [Table("Factory")]
    public class Factory
    {        
        public int ID { get; set; }
        public int IDMap { get; set; }
        public int Iter { get; set; }
        public string Name_Factory { get; set; }
        public string Position { get; set; }
        public double Contamination { get; set; }
        public int Shtraf { get; set; }
    }
}
