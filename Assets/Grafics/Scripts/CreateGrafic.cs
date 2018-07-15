using Assets.Scripts;
using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGrafic : MonoBehaviour
{

    public GameObject text;
    void Start()
    {
        string path = "Example.db";
        using (var connection = new SQLiteConnection(path))
        {
            connection.CreateTable<Iteration>();

            //connection.DeleteAll<Iteration>();
            //connection.Insert(new Iteration { ID = ID, IDMap = IDMap, NumbIter = Iter, BuildName = BuildName, BuildUpgrade = Upgrade, BuildPosition = BuildPosition, RadiusContamination1 = R1,  Contamination_Sum = SumContamination, Profit_Sum = Profit });
            foreach (var db in connection.Table<Iteration>())
            {
                text.GetComponent<Text>().text += " Ітерація на мапі: " + db.ID + " id Мапи: " + db.IDMap + " Ітерація на будівлі: " + db.NumbIter + " Назва будівлі: " + db.BuildName + " Покращення будівлі: " + db.BuildUpgrade + " Розташування будівлі: " + db.BuildPosition + " Радіус враження: " + db.RadiusContamination1 + " Загальне забруднення: " + db.Contamination_Sum + " Прибуток: " + db.Profit_Sum + "\n";
            }
        }
    }
    
}
