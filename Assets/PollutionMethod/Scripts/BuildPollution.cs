using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuildPollution : MatrixPollution
{

    public float[] CenterXY = new float[] { 0.0f, 0.0f };                    //Змінна для знаходження центу будівлі

    public double rad = 0;                                                   //Змінна для знаходження радіусу

    public int dx = 1, dy = 1;                                               //Розміри клітинки

    public int X = 0, Y = 0;                                                 //Координати верхнього правого кута      

    public int iterat = 0;

    public float K_first = 0.5f;
    public float K_second = 0.5f;

    public void Start()
    {
        X = 0;
        Y = 0;
        iterat = 0;
        UpdateSelection();

        //selectionX = selectionX;
        //selectionY = selectionY;

        this.gameObject.transform.position = new Vector3(selectionX + 0.5f, 0, selectionY + 0.5f);

        RaycastHit[] hitX;                                                      //Масив елементів по координаті Х в котрі потряпляє луч
        RaycastHit[] hitY;                                                      //Масив елементів по координаті У в котрі потряпляє луч

        hitX = Physics.RaycastAll(transform.position, transform.right, 6.0F);   //Запуск луча вправо та отримання всіх елементів в які він потрапив
        hitY = Physics.RaycastAll(transform.position, transform.forward, 6.0F); //Запуск луча вверх та отримання всіх елементів в які він потрапив


        RaycastHit massup;                                                      //Змінна за допомогою можна переглянути елемент луча з масиву hitX
        RaycastHit massright;                                                   //Змінна за допомогою можна переглянути елемент луча з масиву hitY

        /// Блок для визначення верхнього правого кута

        for (int i = 0; i < hitX.Length; i++)                                   //Цикл для проходу по всім елементам правого луча
        {
            massright = hitX[i];
            Debug.Log((int)massright.point.x + "X" + (int)massright.point.z);
            if (X <= (int)massright.point.x)                                    //Якщо крайній Х <= координіті в масиві то присвоюємо йому нове значення
            {
                if (massright.collider.tag == "Player")
                    X = (int)massright.point.x;
            }
        }
        for (int i = 0; i < hitY.Length; i++)                                   //Цикл для проходу по всім елементам верхнього луча луча
        {
            massup = hitY[i];
            //Debug.Log((int)massup.point.x + "Y" + (int)massup.point.z);            
            if (Y <= (int)massup.point.z)                                       //Якщо крайній Y <= координіті в масиві то присвоюємо йому нове значення
            {
                if (massup.collider.tag == "Player")
                    Y = (int)massup.point.z;
            }
        }
        //CenterXY[0] = (selectionX + (X + 1)) / 2.0f;
        //CenterXY[1] = (selectionY + (Y + 1)) / 2.0f;
        CenterXY[0] = (selectionX + (X)) / 2.0f;                            //Знаходження центрального Х
        CenterXY[1] = (selectionY + (Y)) / 2.0f;                            //Знаходження центрального У
        //Debug.Log(CenterXY[0] + "|" + CenterXY[1] + "X " + X + "Y " + Y);
        rad = Math.Sqrt((Math.Pow((X + 1) - selectionX, 2) + Math.Pow((Y + 1) - selectionY, 2))) / 2.0f;            //Знаходження радіусу
        Debug.Log("R = " + rad);

    }

    // Update is called once per frame
    void Update()
    {
        //if (X >= 0 && Y >= 0)
        //{
        //    Debug.DrawLine(
        //          Vector3.forward * selectionY + Vector3.right * selectionX,
        //          Vector3.forward * (selectionY + (Y + 1 - selectionY)) + Vector3.right * (selectionX + (X + 1 - selectionX)));

        //    Debug.DrawLine(
        //        Vector3.forward * (selectionY + (Y + 1 - selectionY)) + Vector3.right * selectionX,
        //        Vector3.forward * selectionY + Vector3.right * (selectionX + (X + 1 - selectionX)));
        //}
    }


}
