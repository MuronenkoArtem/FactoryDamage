using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Economic : MonoBehaviour
{
    
    public int Money { get; set; }
    public int Income { get; set; }
    public int Iter { get; set; }

    public GameObject TextEconom;

    private void Start()
    {
        Money = 5000;
        Income = 0;
        Iter = 0;
    }
    public void Update()
    {
        TextEconom.GetComponent<Text>().text = "Ітерація: " + Iter + "\nКошти: " + Money + "\nПрибуток: " + Income;
    }
}
