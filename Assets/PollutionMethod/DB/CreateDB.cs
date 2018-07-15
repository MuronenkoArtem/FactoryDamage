using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using SQLite4Unity3d;
using UnityEngine.UI;
using System.IO;
using Assets.PollutionMethod.DB;
using Assets.Scripts.DB;

public class CreateDB : MonoBehaviour
{
   

    //public GameObject text;
    // Use this for initialization
    public void CreateTable(GameObject text, int ID, int IDMap, int Iter, string BuildName, string BuildPosition, int Upgrade, double R1, double Contamination, int Profit, int shtraf, double SumContamin)
    {
        //string DBName = "Example.db";
        //string path = string.Format("{0}/{1}", Application.persistentDataPath, DBName);
        string path = "Example.db";
        using (var connection = new SQLiteConnection(path))
        {
            connection.CreateTable<Iteration>();
            connection.Insert(new Iteration { ID = ID, IDMap = IDMap, NumbIter = Iter, BuildName = BuildName, BuildPosition = BuildPosition, BuildUpgrade = Upgrade, RadiusContamination1 = R1,Contamination = Contamination, Contamination_Sum = SumContamin, Profit_Sum = Profit, Shtraf = shtraf });

        }
        CreateTableFactory(ID, IDMap, Iter, BuildName, BuildPosition, Contamination, shtraf);
    }
    public void CreateTableCell(int IDMap, int Iter, string Position, double Pollution)
    {
        //string DBName = "Example.db";
        //string path = string.Format("{0}/{1}", Application.persistentDataPath, DBName);
        string path = "Example.db";
        using (var connection = new SQLiteConnection(path))
        {
            connection.CreateTable<Cell>();
            connection.Insert(new Cell { IDMap = IDMap, Iter = Iter, Position = Position, Pollution = Pollution });
        }
    }
    public void CreateTableFactory(int ID, int IDMap, int Iter, string NameBuild, string Position, double Contamnat, int shtraf)
    {
        //string DBName = "Example.db";
        //string path = string.Format("{0}/{1}", Application.persistentDataPath, DBName);
        string path = "Example.db";
        using (var connection = new SQLiteConnection(path))
        {
            connection.CreateTable<Factory>();
            connection.Insert(new Factory { ID = ID, IDMap = IDMap, Iter = Iter, Name_Factory = NameBuild, Position = Position, Contamination = Contamnat, Shtraf = shtraf });
        }
    }
    public void DeleteInfoTable()
    {
        //string DBName = "Example.db";
        //string path = string.Format("{0}/{1}", Application.persistentDataPath, DBName);
        string path = "Example.db";
        using (var connection = new SQLiteConnection(path))
        {
            connection.DeleteAll<Iteration>();
            connection.DeleteAll<Factory>();
            //connection.DeleteAll<Map>();
            connection.DeleteAll<Cell>();
        }
    }
}