using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearBuild : Matrix
{
    public float[] CenterXY = new float[] { 0.0f, 0.0f };                    //Змінна для знаходження центу будівлі
    public double rad = 0;                                                   //Змінна для знаходження радіусу
    // Use this for initialization
    public int dx = 1, dy = 1;                                               //Розміри клітинки

    public int X = 0, Y = 0;                                                        //Координати верхнього правого кута      
    public int iter = 0;
    //Замітка (ЗРОБИТЬ ГЛОБАЛЬНО або ПЕРЕВІРЯТИ ЧИ В РАДІУСІ Є БУДІВЛЯ)
    public int[,] ThisNotacces = new int[96, 96];                            //Змінна для визначення заражених клітинок 
        
    // Use this for initialization
    void Start()
    {
        UpdateSelection();
        
        X = selectionX;
        Y = selectionY;
        CenterXY[0] = (selectionX + (X + 1)) / 2.0f;                            //Знаходження центрального Х
        CenterXY[1] = (selectionY + (Y + 1)) / 2.0f;                            //Знаходження центрального У

        rad = Math.Sqrt((Math.Pow((X + 1) - selectionX, 2) + Math.Pow((Y + 1) - selectionY, 2))) / 2.0f;            //Знаходження радіусу
        Debug.Log("R = " + rad);
        ZoneClear(CenterXY[0], CenterXY[1], (float)rad);
        Debug.Log("X: " + X + "|||||" + "Y: " + Y);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (X >= 0 && Y >= 0)
        {
            Debug.DrawLine(
                  Vector3.forward * selectionY + Vector3.right * selectionX,
                  Vector3.forward * (selectionY + (Y + 1 - selectionY)) + Vector3.right * (selectionX + (X + 1 - selectionX)));

            Debug.DrawLine(
                Vector3.forward * (selectionY + (Y + 1 - selectionY)) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + (X + 1 - selectionX)));
        }
    }

    public void ZoneClear(float x, float y, float Rad)
    {
        Rad += 0.001f;////??????
        for (int i = 0; i < SizeMatrix; i++)
        {
            for (int j = 0; j < SizeMatrix; j++)
            {
                //Debug.Log(" I = " + i + "  J = " + j);

                ///Перевіряю вершини, якщо вона потрапляє в радіус та вже ще не була перевірена то вершина перефарбовується
                if ((((Math.Pow((i * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
                           ((Math.Pow((i * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
                           ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(Rad, 2)) ||
                           ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(Rad, 2))) &&
                           ThisNotacces[i, j] != 1)
                {
                    
                    ThisNotacces[i, j] = 3;                                                     //Вказує, що дана вершина вже пройдена                    
                    
                    //SpawnObject(0, i, j);                                                       //Будує червону зону по координатах
                }
            }
        }
    }
}
