using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contamination : BuildRayCast
{


    //public void ContaminationIteration(float Radius1, float Radius2, float CentrX, float CentrY, int iter)      //Ітераційний метод
    //{
    //    K_first = GameObject.Find("K_first").GetComponent<InputField>();       //Знаходження поля для введення коеф. 1ї зони
    //    K_second = GameObject.Find("K_second").GetComponent<InputField>();     //Знаходження поля для введення коеф. 2ї зони

    //    float K1 = (float)Convert.ToDouble(K_first.text);                //Коефіцієнт
    //    float K2 = (float)Convert.ToDouble(K_second.text);              //Коефіцієнт
    //    double[] R1 = new double[20 * 2];                               //Масив радіусів забруднення для 1ї зони
    //    double[] R2 = new double[20 * 2];                               //Масив радіусів забруднення для 2ї зони

    //    for (int i = iter; i <= iter; i++)
    //    {
    //        R1[i] = Radius1 + (K1 * Math.Log(i + 1));                   //Заповнення масиву 1го радіусу
    //        R2[i] = Radius2 + (K2 * Math.Log(i + 1));                   //Заповнення масиву 2го радіусу

    //        //r1 = (float)R1[i];//
    //        //r2 = (float)R2[i];//

    //        Zone(CentrX, CentrY, (float)R1[i]);                         //Виклик 1ї зони з і-м радіусом
    //        SecondZone(CentrX, CentrY, (float)R2[i]);                   //Виклик 2ї зони з і-м радіусом

    //        //iteration.Add(new Iteration() { RadiusContamination1 = (float)R1[i], RadiusContamination2 = (float)R2[i] });

    //        Debug.Log("First radius: " + R1[i] + " Second radius: " + R2[i]);
    //    }
    //}

    ////Метод першої зони
    //public void Zone(float x, float y, double Rad)
    //{
    //    Rad += 0.001;////??????
    //    for (int i = 0; i < 48; i++)
    //    {
    //        for (int j = 0; j < 48; j++)
    //        {
    //            //Debug.Log(" I = " + i + "  J = " + j);

    //            ///Перевіряю вершини, якщо вона потрапляє в радіус та вже ще не була перевірена то вершина перефарбовується
    //            if ((((Math.Pow((i * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
    //                       ((Math.Pow((i * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
    //                       ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
    //                       ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(Rad, 2))) &&
    //                       ThisNotacces[i, j] != 1)
    //            {
    //                ThisNotacces[i, j] = 1;                                                     //Вказує, що дана вершина вже пройдена
    //                //Debug.Log(" I = " + i + "  J = " + j + "|| i * dx and j * dy <= Rad");
    //                SpawnObject(0, i, j);                                                       //Будує червону зону по координатах
    //            }

               
    //        }
    //    }
    //}
    ////Метод другої зони
    //public void SecondZone(float x, float y, float Rad)
    //{
    //    Rad = Rad * 2;

    //    for (int i = 0; i < 48; i++)
    //    {
    //        for (int j = 0; j < 48; j++)
    //        {
    //            //Debug.Log(" I = " + i + "  J = " + j);
    //            ///Перевіряю вершини, якщо вона потрапляє в радіус та вже ще не була перевірена то вершина перефарбовується
    //            if ((((Math.Pow((i * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
    //                    ((Math.Pow((i * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
    //                    ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
    //                    ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(Rad, 2))) &&
    //                    ThisNotacces[i, j] != 1)
    //            {
    //                ThisNotacces[i, j] = 1;                                                     //Вказує, що дана вершина вже пройдена                    
    //                SpawnObject(1, i, j);                                                       //Будує зелену зону по координатах
    //            }

    //        }
    //    }
    //}
}
