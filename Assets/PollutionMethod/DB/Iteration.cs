using UnityEngine;
using System;
using SQLite4Unity3d;

namespace Assets.Scripts
{  
   [Table("Iteration")]
    public class Iteration
    {        
        public int ID { get; set; }
        public int IDMap { get; set; }
        public int NumbIter { get; set; }
        public string BuildName { get; set; }
        public string BuildPosition { get; set; }
        public int BuildUpgrade { get; set; }
        public double RadiusContamination1 { get; set; }
        public double Contamination { get; set; }
        public double Contamination_Sum { get; set; }
        public int Profit_Sum { get; set; }
        public int Shtraf { get; set; }
    }
}
