using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DB
{
    [Table("Map")]
    class Map
    {        
        public int ID { get; set; }
        public string Name_Map { get; set; }
        public int ID_Factory { get; set; }   
        public string Location_Factory { get; set; } 
    }
}
