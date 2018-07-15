using Assets.Scripts;
using SQLite4Unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapher : MonoBehaviour
{
    [Range(2, 100)]
    public int resolution = 100;
    private int currentResolution;
    private ParticleSystem.Particle[] points;
    private void CreatePoints()
    {
        currentResolution = resolution;
        points = new ParticleSystem.Particle[resolution];
        float increment = 1f / (resolution - 1);
        for (int i = 0; i < resolution; i++)
        {
            float x = i * increment;
           
            points[i].position = new Vector3(x, 0f, 0f);
            points[i].color = new Color(x, 0f, 0f);
            points[i].size = 0.1f;
        }
    }

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
                
                
                //db.ID + " " + db.IDMap + " " + db.NumbIter + " " + db.BuildName + " " + db.BuildUpgrade + " " + db.BuildPosition + " " + db.RadiusContamination1 + " " + " " + db.Contamination_Sum + " " + db.Profit_Sum + "\n";
            }
        }
    }

    void Update()
    {
        if (currentResolution != resolution)
        {
            CreatePoints();
        }
        for (int i = 0; i < resolution; i++)
        {
            Vector3 p = points[i].position;
            p.y = 1;
            points[i].position = p;
            Color c = points[i].color;
            c.g = p.y;
            points[i].color = c;
        }



        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
    }

    private static float Sine(float x)
    {
        return 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * x);
    }
    private static float Linear(float x)
    {
        return x;
    }

}
