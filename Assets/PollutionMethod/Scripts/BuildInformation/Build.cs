using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Build : MonoBehaviour
{
    public GameObject cam;
    public GameObject panel;

    public GameObject upgr1;
    public GameObject upgr2;
    public GameObject Sell;
    public GameObject InfoLabel;

    public string Name { get; set; }          //Назва будівлі
    public string TypeBuild { get; set; }     //Тип забруднення\очистка
    public double Contamination { get; set; } //Забруднення
    public int Upgrade { get; set; }          //Покращення
    public int[] Position { get; set; }       //Розташування
    public double Radius { get; set; }        //Радіус
    public int Profit { get; set; }       //Прибуток
    public int Shtraf { get; set; }           //штраф


}
