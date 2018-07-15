using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.UI;
using System.Linq;

public class IterationPollution : MonoBehaviour
{
    CreateDB create = new CreateDB();


    public GameObject Text;
    public int dx = 1, dy = 1;
    public int KingIter = 0;
    public bool FactoryOrClear = false;
    //public Build[] build { get; set; }

    /// <summary>
    /// Поітераційне забруднення зони
    /// </summary>
    public void CellChange()
    {
        GameObject[] grass = GameObject.FindGameObjectsWithTag("Grass");
        GameObject[] build = GameObject.FindGameObjectsWithTag("Building");
        double x = 0, y = 0;                                                //Епіцентр по координіті Х та Y
        double radius = 0;                                                  //Радіус будівлі
        int Upgrade = 0;
        double K1 = 0.5f; //коеф. забруднення
        double K2 = 0.1f; //коеф. відновлення

        double R1 = 0;
        double R2 = 0;
        double dp = 0;

        int income = 0;

        //create.DeleteInfoTable();                                         //очищення таблиці

        foreach (GameObject InfoBuild in build)
        {
            int shtraf = 0;
            x = InfoBuild.GetComponent<BuildPollution>().CenterXY[0];
            y = InfoBuild.GetComponent<BuildPollution>().CenterXY[1];

            Upgrade = InfoBuild.GetComponent<Build>().Upgrade;
            radius = InfoBuild.GetComponent<BuildPollution>().rad;        //Отримання радіусу з будівлі
            int iter = InfoBuild.GetComponent<BuildPollution>().iterat;   //Отримання ітерації на будівлі            

            if (InfoBuild.GetComponent<Build>().TypeBuild == "Забруднює")
            {
                FactoryOrClear = true;
                dp = Math.Abs(K1 * Math.Log(iter + 1) - K2);                      //Коеф. забруднення зони

                if (Upgrade == 1)
                    dp = dp / 2;
                if (Upgrade == 2)
                {
                    dp = 0;
                    R1 = 0;
                    R2 = 0;
                }
                else
                {
                    R1 = radius + (K1 * Math.Log(iter + 1));                       //Збільшений 1й радіус
                    R2 = (radius * 2) + (K1 * Math.Log(iter + 1));                 //Збільшений 2й радіус
                }
            }
            if (InfoBuild.GetComponent<Build>().TypeBuild == "Очищення")
            {
                FactoryOrClear = false;
                dp = Math.Abs(K1 * Math.Log(iter + 1) - K2);                      //Коеф. забруднення зони

                if (Upgrade == 1)
                {
                    R1 = radius + 1;
                    R2 = (radius * 2) + 1;
                }
                if (Upgrade == 2)
                {
                    dp = 0;
                    R1 = radius + 2;
                    R2 = (radius * 2) + 2;
                }
                else
                {
                    R1 = radius;                       //1й радіус
                    R2 = radius * 2;                   //2й радіус
                }
            }

            InfoBuild.GetComponent<Build>().Radius = R1;
            double radPoluttion = 0;
            int[,] acces = new int[96, 96];
            double Contamination = 0;
            foreach (GameObject Info in grass)
            {
                int i = Info.GetComponent<CellRoot>().Position[0];        //Позиція клітинки по х
                int j = Info.GetComponent<CellRoot>().Position[1];        //Позиція клітинки по у

                ///Перевіряю клітинку, якщо вона потрапляє в 1й радіус радіус то забруднюється
                if ((((Math.Pow((i * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(R1, 2)) ||
                ((Math.Pow((i * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(R1, 2)) ||
                ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(R1, 2)) ||
                ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(R1, 2))) && acces[i, j] != 1)
                {
                    acces[i, j] = 1;
                    radPoluttion = Math.Pow(i * dx - x, 2) + Math.Pow(j * dx - y, 2);
                    if (FactoryOrClear == true)
                        Info.GetComponent<CellRoot>().ChangePollution(radPoluttion, dp);          //Забруднення клітинки на величину коеф.
                    else
                        Info.GetComponent<CellRoot>().RecoveryPollytion(radPoluttion, dp);
                    Contamination += Info.GetComponent<CellRoot>().Pollution;
                }

                ///Перевіряю клітинку, якщо вона потрапляє в 2й радіус радіус то забруднюється
                if ((((Math.Pow((i * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(R2, 2)) ||
                ((Math.Pow((i * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(R2, 2)) ||
                ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(R2, 2)) ||
                ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(R2, 2))) && acces[i, j] != 1)
                {
                    acces[i, j] = 1;
                    radPoluttion = Math.Pow(i * dx - x, 2) + Math.Pow(j * dx - y, 2);
                    if (FactoryOrClear == true)
                        Info.GetComponent<CellRoot>().ChangePollution(radPoluttion, dp);          //Забруднення клітинки на величину коеф.
                    else
                        Info.GetComponent<CellRoot>().RecoveryPollytion(radPoluttion, dp);
                    Contamination += Info.GetComponent<CellRoot>().Pollution;
                }
                acces[i, j] = 0;
            }
            
            double SumContamination = 0;
            foreach (GameObject Info in grass)
            {
                SumContamination += Info.GetComponent<CellRoot>().Pollution;
            }
            if (KingIter < InfoBuild.GetComponent<BuildPollution>().iterat)
                KingIter = InfoBuild.GetComponent<BuildPollution>().iterat;         //Головна ітерація

            if ((KingIter % 10) == 0 && Upgrade != 2 && InfoBuild.GetComponent<Build>().TypeBuild == "Забруднює" && KingIter != 0)
            {
                shtraf = (int)R1 * 20;
                InfoBuild.GetComponent<Build>().Shtraf = shtraf;
            }
            if (Upgrade == 2)
            {
                shtraf = 0;
                InfoBuild.GetComponent<Build>().Shtraf = shtraf;
            }
            InfoBuild.GetComponent<Build>().Profit -= shtraf;                                   //Штрафи санстанції
            income += InfoBuild.GetComponent<Build>().Profit;

            this.GetComponent<Economic>().Iter = KingIter;                                      //Ітерація
            this.GetComponent<Economic>().Money += InfoBuild.GetComponent<Build>().Profit;      //Кошти
            this.GetComponent<Economic>().Income = income;                                      //Прибуток

            //this.GetComponent<Economic>().Money -= shtraf;                                      //Штрафи санстанцій

            string NameBuild = InfoBuild.GetComponent<Build>().Name;                            //Назва будівлі
            string Position = InfoBuild.GetComponent<BuildPollution>().selectionX + ";" + InfoBuild.GetComponent<BuildPollution>().selectionY; //Координати
            int ProfitBuild = InfoBuild.GetComponent<Build>().Profit;                           //Прибуток
            Text.GetComponent<Text>().text = "";
            create.CreateTable(Text, KingIter, 1, iter, NameBuild, Position, Upgrade, R1, Contamination, this.GetComponent<Economic>().Money, shtraf, SumContamination);

            InfoBuild.GetComponent<BuildPollution>().iterat++;                  //Збільшення ітерації на будівлі
        }
        foreach (GameObject Info in grass)
        {
            Info.GetComponent<CellRoot>().NaturalRestore();                     //Природнє очищення   
        }
    }
    /// <summary>
    /// Збереження ландшафту
    /// </summary>
    /// <returns></returns>
    public IEnumerator SaveLand()
    {
        GameObject[] grass = GameObject.FindGameObjectsWithTag("Grass");
        foreach (GameObject Info in grass)
        {
            int i = Info.GetComponent<CellRoot>().Position[0];        //Позиція клітинки по х
            int j = Info.GetComponent<CellRoot>().Position[1];        //Позиція клітинки по у
            string Possit = i + ";" + j;
            create.CreateTableCell(1, KingIter, Possit, Info.GetComponent<CellRoot>().Pollution);
        }
        yield return new WaitForSeconds(0);
    }
    public void Save()
    {
        StartCoroutine(SaveLand());
    }
    /// <summary>
    /// IEnumerator метод для автоматичної ітерації
    /// </summary>
    /// <returns></returns>
    public IEnumerator Change()
    {
        GameObject[] grass = GameObject.FindGameObjectsWithTag("Grass");
        GameObject[] build = GameObject.FindGameObjectsWithTag("Building");
        double x = 0, y = 0;                                                //Епіцентр по координіті Х та Y
        double radius = 0;                                                  //Радіус будівлі
        int Upgrade = 0;
        double K1 = 0.5f; //коеф. забруднення
        double K2 = 0.1f; //коеф. відновлення

        double R1 = 0;
        double R2 = 0;
        double dp = 0;

        int income = 0;

        while (true)
        {
            //create.DeleteInfoTable();
            foreach (GameObject InfoBuild in build)
            {
                int shtraf = 0;
                x = InfoBuild.GetComponent<BuildPollution>().CenterXY[0];
                y = InfoBuild.GetComponent<BuildPollution>().CenterXY[1];

                Upgrade = InfoBuild.GetComponent<Build>().Upgrade;
                radius = InfoBuild.GetComponent<BuildPollution>().rad;        //Отримання радіусу з будівлі
                int iter = InfoBuild.GetComponent<BuildPollution>().iterat;   //Отримання ітерації на будівлі            

                if (InfoBuild.GetComponent<Build>().TypeBuild == "Забруднює")
                {
                    FactoryOrClear = true;
                    dp = Math.Abs(K1 * Math.Log(iter + 1) - K2);                      //Коеф. забруднення зони

                    if (Upgrade == 1)
                        dp = dp / 2;
                    if (Upgrade == 2)
                    {
                        dp = 0;
                        R1 = 0;
                        R2 = 0;
                    }
                    else
                    {
                        R1 = radius + (K1 * Math.Log(iter + 1));                       //Збільшений 1й радіус
                        R2 = (radius * 2) + (K1 * Math.Log(iter + 1));                 //Збільшений 2й радіус
                    }
                }
                if (InfoBuild.GetComponent<Build>().TypeBuild == "Очищення")
                {
                    FactoryOrClear = false;
                    dp = Math.Abs(K1 * Math.Log(iter + 1) - K2);                      //Коеф. забруднення зони

                    if (Upgrade == 1)
                    {
                        R1 = radius + 1;
                        R2 = (radius * 2) + 1;
                    }
                    if (Upgrade == 2)
                    {
                        dp = 0;
                        R1 = radius + 2;
                        R2 = (radius * 2) + 2;
                    }
                    else
                    {
                        R1 = radius;                       //1й радіус
                        R2 = radius * 2;                   //2й радіус
                    }
                }

                InfoBuild.GetComponent<Build>().Radius = R1;
                double radPoluttion = 0;
                int[,] acces = new int[96, 96];
                double Contamination = 0;
                foreach (GameObject Info in grass)
                {
                    int i = Info.GetComponent<CellRoot>().Position[0];        //Позиція клітинки по х
                    int j = Info.GetComponent<CellRoot>().Position[1];        //Позиція клітинки по у

                    ///Перевіряю клітинку, якщо вона потрапляє в 1й радіус радіус то забруднюється
                    if ((((Math.Pow((i * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(R1, 2)) ||
                    ((Math.Pow((i * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(R1, 2)) ||
                    ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(R1, 2)) ||
                    ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(R1, 2))) && acces[i, j] != 1)
                    {
                        acces[i, j] = 1;
                        radPoluttion = Math.Pow(i * dx - x, 2) + Math.Pow(j * dx - y, 2);
                        if (FactoryOrClear == true)
                            Info.GetComponent<CellRoot>().ChangePollution(radPoluttion, dp);          //Забруднення клітинки на величину коеф.
                        else
                            Info.GetComponent<CellRoot>().RecoveryPollytion(radPoluttion, dp);
                        Contamination += Info.GetComponent<CellRoot>().Pollution;
                    }

                    ///Перевіряю клітинку, якщо вона потрапляє в 2й радіус радіус то забруднюється
                    if ((((Math.Pow((i * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(R2, 2)) ||
                    ((Math.Pow((i * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(R2, 2)) ||
                    ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow((j * dy - y), 2))) <= Math.Pow(R2, 2)) ||
                    ((Math.Pow(((i + 1) * dx - x), 2) + (Math.Pow(((j + 1) * dy - y), 2))) <= Math.Pow(R2, 2))) && acces[i, j] != 1)
                    {
                        acces[i, j] = 1;
                        radPoluttion = Math.Pow(i * dx - x, 2) + Math.Pow(j * dx - y, 2);
                        if (FactoryOrClear == true)
                            Info.GetComponent<CellRoot>().ChangePollution(radPoluttion, dp);          //Забруднення клітинки на величину коеф.
                        else
                            Info.GetComponent<CellRoot>().RecoveryPollytion(radPoluttion, dp);
                        Contamination += Info.GetComponent<CellRoot>().Pollution;
                    }
                    acces[i, j] = 0;
                }

                double SumContamination = 0;
                foreach (GameObject Info in grass)
                {
                    SumContamination += Info.GetComponent<CellRoot>().Pollution;
                }
                if (KingIter < InfoBuild.GetComponent<BuildPollution>().iterat)
                    KingIter = InfoBuild.GetComponent<BuildPollution>().iterat;         //Головна ітерація

                if ((KingIter % 10) == 0 && Upgrade != 2 && InfoBuild.GetComponent<Build>().TypeBuild == "Забруднює" && KingIter != 0)
                {
                    shtraf = (int)R1 * 20;
                    InfoBuild.GetComponent<Build>().Shtraf = shtraf;
                }
                if (Upgrade == 2)
                {
                    shtraf = 0;
                    InfoBuild.GetComponent<Build>().Shtraf = shtraf;
                }
                InfoBuild.GetComponent<Build>().Profit -= shtraf;                                   //Штрафи санстанції
                income += InfoBuild.GetComponent<Build>().Profit;

                this.GetComponent<Economic>().Iter = KingIter;                                      //Ітерація
                this.GetComponent<Economic>().Money += InfoBuild.GetComponent<Build>().Profit;      //Кошти
                this.GetComponent<Economic>().Income = income;                                      //Прибуток

                //this.GetComponent<Economic>().Money -= shtraf;                                      //Штрафи санстанцій

                string NameBuild = InfoBuild.GetComponent<Build>().Name;                            //Назва будівлі
                string Position = InfoBuild.GetComponent<BuildPollution>().selectionX + ";" + InfoBuild.GetComponent<BuildPollution>().selectionY; //Координати
                int ProfitBuild = InfoBuild.GetComponent<Build>().Profit;                           //Прибуток
                Text.GetComponent<Text>().text = "";
                create.CreateTable(Text, KingIter, 1, iter, NameBuild, Position, Upgrade, R1, Contamination, this.GetComponent<Economic>().Money, shtraf, SumContamination);

                InfoBuild.GetComponent<BuildPollution>().iterat++;                  //Збільшення ітерації на будівлі
            }
            foreach (GameObject Info in grass)
            {
                Info.GetComponent<CellRoot>().NaturalRestore();                     //Природнє очищення   
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// Метод для автоматичної ітерації
    /// </summary>
    public void ChangeAuto()
    {
        StartCoroutine(Change());
    }

    /// <summary>
    /// Зупинка всіх потоків
    /// </summary>
    public void StopCotoutine()
    {
        StopAllCoroutines();
    }
}
