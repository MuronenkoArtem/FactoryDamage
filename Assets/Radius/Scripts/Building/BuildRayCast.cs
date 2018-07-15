using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Assets.Scripts;
using System.Threading;
public class BuildRayCast : Matrix
{
    public float[] CenterXY = new float[] { 0.0f, 0.0f };                    //Змінна для знаходження центу будівлі
    public double rad = 0;                                                   //Змінна для знаходження радіусу
    // Use this for initialization
    public int dx = 1, dy = 1;                                               //Розміри клітинки
   
    public int X = 0, Y = 0;                                                        //Координати верхнього правого кута      
    public int iterat = 0;
    //Замітка (ЗРОБИТЬ ГЛОБАЛЬНО або ПЕРЕВІРЯТИ ЧИ В РАДІУСІ Є БУДІВЛЯ)
    public int[,] ThisNotacces = new int[96, 96];                            //Змінна для визначення заражених клітинок 

    public float K_first;
    public float K_second;

    public float Br1 = 0f, Br2 = 0f;

    void Start()
    {
        Br1 = 0f;
        Br2 = 0f;
        UpdateSelection();

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
                X = (int)massright.point.x;
        }
        for (int i = 0; i < hitY.Length; i++)                                   //Цикл для проходу по всім елементам верхнього луча луча
        {
            massup = hitY[i];
            //Debug.Log((int)massup.point.x + "Y" + (int)massup.point.z);            
            if (Y <= (int)massup.point.z)                                       //Якщо крайній Y <= координіті в масиві то присвоюємо йому нове значення
                Y = (int)massup.point.z;
        }

        CenterXY[0] = (selectionX + (X + 1)) / 2.0f;                            //Знаходження центрального Х
        CenterXY[1] = (selectionY + (Y + 1)) / 2.0f;                            //Знаходження центрального У

        rad = Math.Sqrt((Math.Pow((X + 1) - selectionX, 2) + Math.Pow((Y + 1) - selectionY, 2))) / 2.0f;            //Знаходження радіусу
        Debug.Log("R = " + rad);

        // Debug.Log("X: " + X + "|||||" + "Y: " + Y);

        //Debug.Log("Center X: " + CenterXY[0] + " Y: " + CenterXY[1]);

        //NaturalClear((float)rad, (float)rad * 2, CenterXY[0], CenterXY[1]);

        Zone(CenterXY[0], CenterXY[1], (float)rad);                             //Викликаю метод першої зони зону та перередаю в нього координати епіцентру
                                                                                //забруднення та радіус забруднення    

        SecondZone(CenterXY[0], CenterXY[1], (float)rad * 2);                       //Викликаю метод другої зони зону та перередаю в нього координати епіцентру
                                                                                    //забруднення та радіус забруднення  

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

    //Метод першої зони
    public void Zone(float x, float y, float Rad)
    {
       // Rad += 0.001f;////??????
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

                    ThisNotacces[i, j] = 1;                                                     //Вказує, що дана вершина вже пройдена                    
                    //SendMatrix(i, j, ThisNotacces[i, j]);
                    //DestroyZone();
                    //SpawnObject(0, i, j);                                                       //Будує червону зону по координатах
                }
            }
        }
    }

    //Метод другої зони
    public void SecondZone(float x, float y, float Rad)
    {
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
                        ThisNotacces[i, j] != 1 && ThisNotacces[i, j] != 2)
                {
                    ThisNotacces[i, j] = 2;                                                     //Вказує, що дана вершина вже пройдена       
                   // SendMatrix(i,j,ThisNotacces[i, j]);
                    //DestroyZone();
                    //SpawnObject(1, i, j);                                                       //Будує зелену зону по координатах
                }
            }
        }
    }

    public void SendMatrix(int massi,int massj, int ThisNotAcces)
    {
        GameObject matr = GameObject.FindGameObjectWithTag("EditorOnly");
        for (int i = 0; i < SizeMatrix; i++)
        {
            for (int j = 0; j < SizeMatrix; j++)
            {
                matr.GetComponent<Matrix>().Board[massi, massj] = ThisNotAcces;
            }
        }
    }

    //IEnumerator метод для автоматичної ітерації(затримка в циклі)
    public IEnumerator ContaminationIterationAuto(float Radius1, float Radius2, float CentrX, float CentrY, int iter, bool upgrade)
    {
        K_first = 0.5f;      //Знаходження поля для введення коеф. 1ї зони
        K_second = 0.5f;     //Знаходження поля для введення коеф. 2ї зони

        float K1 = K_first;                //Коефіцієнт
        float K2 = K_second;              //Коефіцієнт
        double[] R1 = new double[48 * 2];                               //Масив радіусів забруднення для 1ї зони
        double[] R2 = new double[48 * 2];                               //Масив радіусів забруднення для 2ї зони
        float nature = 0.2f;
        double nat = 0;
        if (upgrade == true)
        {
            for (int i = 0; i < 30; i++)
            {
                R1[i] = Br1 - 0.2f;                   //Заповнення масиву 1го радіусу
                R2[i] = Br2 - 0.2f;                   //Заповнення масиву 2го радіусу
                Br1 = (float)R1[i] - nature;
                Br2 = (float)R2[i] - nature;

                if (R1[i] <= 0.2)
                {
                    Br1 = 0;
                    Br2 = 0;
                    
                    Zone(CentrX, CentrY, (float)Br1);                         //Виклик 1ї зони з і-м радіусом
                    SecondZone(CentrX, CentrY, (float)Br2);                   //Виклик 2ї зони з і-м радіусом
                    iterat++;
                    break;
                }
                Zone(CentrX, CentrY, Br1);                         //Виклик 1ї зони з і-м радіусом
                SecondZone(CentrX, CentrY, Br2);                   //Виклик 2ї зони з і-м радіусом
                iterat++;
                yield return new WaitForSeconds(1f);                      //Затримка в 0,5 секунди
            }
        }
        if (iter <= 20 && upgrade == false)
        {
            for (int i = iter; i <= 30; i++)
            {
                nat = Radius1 - (K1 * Math.Log(i + 1));
                Debug.Log(nat);
                R1[i] = Radius1 + (K1 * Math.Log(i + 1));                   //Заповнення масиву 1го радіусу
                R2[i] = Radius2 + (K2 * Math.Log(i + 1));                   //Заповнення масиву 2го радіусу

                Br1 = (float)R1[i] - nature;//
                Br2 = (float)R2[i] - nature;//

                //R1[i] = Br1 - (K1 * Math.Log(i + 1));
                //R2[i] = Br2 - (K2 * Math.Log(i + 1));

                Zone(CentrX, CentrY, Br1);                         //Виклик 1ї зони з і-м радіусом
                SecondZone(CentrX, CentrY, Br2);                   //Виклик 2ї зони з і-м радіусом
                iterat++;
                yield return new WaitForSeconds(1f);                      //Затримка в 0,5 секунди
            }            
        }
        if (iter > 20)
        {
            Zone(CentrX, CentrY, Br1);                         //Виклик 1ї зони з і-м радіусом
            SecondZone(CentrX, CentrY, Br2);                   //Виклик 2ї зони з і-м радіусом
        }
        
    }

    //Видалення зони
    public void DestroyZone()
    {
        GameObject[] ObjectsZone = GameObject.FindGameObjectsWithTag("GameController"); //зона 
        for (int j = 0; j < ObjectsZone.Length; j++)
        {
            Destroy(ObjectsZone[j]);
        }
    }

    //Ітераційний метод поодний ітерації
    public void ContaminationIteration(float Radius1, float Radius2, float CentrX, float CentrY, int iter,bool upgrade)
    {

        K_first = 0.5f;       //Знаходження поля для введення коеф. 1ї зони
        K_second = 0.5f;     //Знаходження поля для введення коеф. 2ї зони

        float K1 = K_first;                //Коефіцієнт
        float K2 = K_second;              //Коефіцієнт
        double[] R1 = new double[SizeMatrix * 2];                               //Масив радіусів забруднення для 1ї зони
        double[] R2 = new double[SizeMatrix * 2];                               //Масив радіусів забруднення для 2ї зони
        float nature = 0.2f;
        double nat = 0;
        if (iter <= 20 && upgrade == false)
        {
            for (int i = iter; i <= iter; i++)
            {
                nat = Radius1 - (K1 * Math.Log(i + 1));
                Debug.Log(nat);
                R1[i] = Radius1 + (K1 * Math.Log(i + 1));                   //Заповнення масиву 1го радіусу
                R2[i] = Radius2 + (K2 * Math.Log(i + 1));                   //Заповнення масиву 2го радіусу

                Br1 = (float)R1[i] - nature;//
                Br2 = (float)R2[i] - nature;//

                //R1[i] = Br1 - (K1 * Math.Log(i + 1));
                //R2[i] = Br2 - (K2 * Math.Log(i + 1));

                Zone(CentrX, CentrY, Br1);                         //Виклик 1ї зони з і-м радіусом
                SecondZone(CentrX, CentrY, Br2);                   //Виклик 2ї зони з і-м радіусом

                //yield return new WaitForSeconds(0.5f);                      //Затримка в 0,5 секунди
            }
            iterat++;
            if (iter > 20)
            {
                Zone(CentrX, CentrY, Br1);                         //Виклик 1ї зони з і-м радіусом
                SecondZone(CentrX, CentrY, Br2);                   //Виклик 2ї зони з і-м радіусом
            }
        }
        if (upgrade == true)
        {
            for (int i = 0; i < 80; i++)
            {
                R1[i] = Br1 - 0.2f;                   //Заповнення масиву 1го радіусу
                R2[i] = Br2 - 0.2f;                   //Заповнення масиву 2го радіусу
                Br1 = Br1 - 0.2f;
                Br2 = Br2 - 0.2f;
                //R1[i] = Br1 - (K1 * Math.Log(i + 1));
                //R2[i] = Br2 - (K2 * Math.Log(i + 1));
                // DestroyZone();

                if (R1[i] <= 0.2)
                {
                    Br1 = 0;
                    Br2 = 0;
                    DestroyZone();
                    //Zone(CentrX, CentrY, (float)R1[i]);                         //Виклик 1ї зони з і-м радіусом
                    //SecondZone(CentrX, CentrY, (float)R2[i]);                   //Виклик 2ї зони з і-м радіусом
                    iterat++;
                    break;
                }
                Zone(CentrX, CentrY, (float)R1[i]);                         //Виклик 1ї зони з і-м радіусом
                SecondZone(CentrX, CentrY, (float)R2[i]);                   //Виклик 2ї зони з і-м радіусом
                iterat++;
                //yield return new WaitForSeconds(1f);                      //Затримка в 0,5 секунди
            }
        }
    }

    //Ітераційний автоматичний метод забруднення в потоці
    public void AutoIteration()
    {
        StopAllCoroutines();

        GameObject[] build = GameObject.FindGameObjectsWithTag("Building");             //всі будівлі
        GameObject[] ObjectsZone = GameObject.FindGameObjectsWithTag("GameController"); //зона 

        for (int i = 0; i < ObjectsZone.Length; i++)
        {
            Destroy(ObjectsZone[i]);
        }
        foreach (GameObject BuildInfo in build)
        {
            ClearZone(BuildInfo.GetComponent<BuildRayCast>().ThisNotacces);
            
            //Виклик ітераційного методу в  потоці та надання йому параметрів         
            BuildInfo.GetComponent<BuildRayCast>().StartCoroutine(BuildInfo.GetComponent<BuildRayCast>().ContaminationIterationAuto((float)BuildInfo.GetComponent<BuildRayCast>().rad,
             (float)BuildInfo.GetComponent<BuildRayCast>().rad * 2,
             BuildInfo.GetComponent<BuildRayCast>().CenterXY[0], BuildInfo.GetComponent<BuildRayCast>().CenterXY[1],
             BuildInfo.GetComponent<BuildRayCast>().iterat, BuildInfo.GetComponent<Buildings>().Upgrade));
            // iteration.Add(new Iteration() { ID = BuildInfo.GetComponent<BuildRayCast>().iter, IDMap = 1, Build = BuildInfo.name, RadiusContamination1 = BuildInfo.GetComponent<BuildRayCast>().CenterXY[0], RadiusContamination2 = BuildInfo.GetComponent<BuildRayCast>().CenterXY[1] });
                        
        }
    }

    public void AutoNaturalClear()
    {
        StopAllCoroutines();

        GameObject[] build = GameObject.FindGameObjectsWithTag("Building");             //всі будівлі
        GameObject[] ObjectsZone = GameObject.FindGameObjectsWithTag("GameController"); //зона 

        for (int i = 0; i < ObjectsZone.Length; i++)
        {
            Destroy(ObjectsZone[i]);
        }
        foreach (GameObject BuildInfo in build)
        {
            ClearZone(BuildInfo.GetComponent<BuildRayCast>().ThisNotacces);

            //Виклик ітераційного методу в  потоці та надання йому параметрів         
            BuildInfo.GetComponent<BuildRayCast>().StartCoroutine(BuildInfo.GetComponent<BuildRayCast>().ContaminationIterationAuto((float)BuildInfo.GetComponent<BuildRayCast>().rad,
             (float)BuildInfo.GetComponent<BuildRayCast>().rad * 2,
             BuildInfo.GetComponent<BuildRayCast>().CenterXY[0], BuildInfo.GetComponent<BuildRayCast>().CenterXY[1],
             BuildInfo.GetComponent<BuildRayCast>().iterat, BuildInfo.GetComponent<Buildings>().Upgrade));
            // iteration.Add(new Iteration() { ID = BuildInfo.GetComponent<BuildRayCast>().iter, IDMap = 1, Build = BuildInfo.name, RadiusContamination1 = BuildInfo.GetComponent<BuildRayCast>().CenterXY[0], RadiusContamination2 = BuildInfo.GetComponent<BuildRayCast>().CenterXY[1] });

            if (BuildInfo.GetComponent<BuildRayCast>().iterat <= 20)            //Якщо ітерація для будівлі менше 20 то інкрементуємо
                BuildInfo.GetComponent<BuildRayCast>().iterat++;


        }
    }

    //Одиничний ітераційний метод
    public void OneIteration()
    {
        StopCoroutines();
        FakeClear();
        GameObject[] build = GameObject.FindGameObjectsWithTag("Building");             //всі будівлі
        GameObject[] ObjectsZone = GameObject.FindGameObjectsWithTag("GameController"); //зона 

        for (int i = 0; i < ObjectsZone.Length; i++)
        {
            Destroy(ObjectsZone[i]);
        }
        foreach (GameObject BuildInfo in build)
        {
            Debug.Log("iter: " + BuildInfo.GetComponent<BuildRayCast>().iterat);
            //          
            //Виклик ітераційного методу та надання йому параметрів
            BuildInfo.GetComponent<BuildRayCast>().ContaminationIteration((float)BuildInfo.GetComponent<BuildRayCast>().rad,
            (float)BuildInfo.GetComponent<BuildRayCast>().rad * 2,
            BuildInfo.GetComponent<BuildRayCast>().CenterXY[0], BuildInfo.GetComponent<BuildRayCast>().CenterXY[1],
            BuildInfo.GetComponent<BuildRayCast>().iterat,BuildInfo.GetComponent<Buildings>().Upgrade);

            // iteration.Add(new Iteration() { ID = BuildInfo.GetComponent<BuildRayCast>().iter, IDMap = 1, Build = BuildInfo.name, RadiusContamination1 = BuildInfo.GetComponent<BuildRayCast>().CenterXY[0], RadiusContamination2 = BuildInfo.GetComponent<BuildRayCast>().CenterXY[1] });
                        
        }
    }

    //Метод для природної очистки
    public void NaturalClear(float Radius1, float Radius2, float CentrX, float CentrY, int iter)
    {
        K_first = 0.5f; ;       //Знаходження поля для введення коеф. 1ї зони
        K_second = 0.5f;   //Знаходження поля для введення коеф. 2ї зони

        float K1 = K_first;                //Коефіцієнт
        float K2 = K_second;              //Коефіцієнт
        double[] R1 = new double[48 * 48];                               //Масив радіусів забруднення для 1ї зони
        double[] R2 = new double[48 * 48];                               //Масив радіусів забруднення для 2ї зони

        GameObject[] ObjectsZone = GameObject.FindGameObjectsWithTag("GameController"); //зона 

        for (int i = 0; i <= iter; i++)
        {
            for (int j = 0; j < ObjectsZone.Length; j++)
            {
                Destroy(ObjectsZone[j]);
            }
            if ((Radius1 - (K1 * Math.Log(i + 1))) >= 0 && (R2[i] = Radius2 - (K2 * Math.Log(i + 1))) >= 0)
            {
                R1[i] = Radius1 - (K1 * Math.Log(i + 1));                   //Заповнення масиву 1го радіусу
                R2[i] = Radius2 - (K2 * Math.Log(i + 1));                   //Заповнення масиву 2го радіусу                              
                if (R1[i] <= 0.5)
                {

                    R1[i] = 0;
                    R2[i] = 0;
                    iter = 0;
                }
                Zone(CentrX, CentrY, (float)R1[i]);                         //Виклик 1ї зони з і-м радіусом
                SecondZone(CentrX, CentrY, (float)R2[i]);                   //Виклик 2ї зони з і-м радіусом

                //yield return new WaitForSeconds(0.5f);                      //Затримка в 0,5 секунди

                Debug.Log("First radius: " + R1[i] + " Second radius: " + R2[i]);
                iter++;
            }
        }
    }

    //Метод для очищення масиву зон    
    void ClearZone(int[,] ThisNotacces)
    {
        for (int i = 0; i < SizeMatrix; i++)
        {
            for (int j = 0; j < SizeMatrix; j++)
            {
                ThisNotacces[i, j] = 0;
            }
        }
    }

    //Метод для очищення масиву зон
    void FakeClear()
    {
        for (int i = 0; i < SizeMatrix; i++)
        {
            for (int j = 0; j < SizeMatrix; j++)
            {
                ThisNotacces[i, j] = 0;
            }
        }
    }

    //Зупинка всіх потоків
    public void StopCoroutines()
    {
        GameObject[] build = GameObject.FindGameObjectsWithTag("Building");             //всі будівлі
        foreach (GameObject BuildInfo in build)
        {
            BuildInfo.GetComponent<BuildRayCast>().StopAllCoroutines();
        }
    }
    
}