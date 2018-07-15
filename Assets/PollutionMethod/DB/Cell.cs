using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PollutionMethod.DB
{
    [Table("Cell")]
    public class Cell
    {        
        public int ID { get; set; }
        public int IDMap { get; set; }
        public int Iter { get; set; }
        public string Position { get; set; }
        public double Pollution { get; set; }
    }
}
