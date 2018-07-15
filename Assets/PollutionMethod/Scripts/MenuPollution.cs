using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuPollution : MonoBehaviour
{

    public GameObject panel;
    public GameObject setting;

    public GameObject RedactorButton;
    public Text text;
    private void Start()
    {
        setting.SetActive(true);
        panel.SetActive(false);

    }

    //Розгортаю або приховую меню
    public void OnMenu()
    {
        if (setting.activeSelf)
        {
            setting.SetActive(false);
            panel.SetActive(true);

        }
        else
        {
            setting.SetActive(true);
            panel.SetActive(false);
        }
    }


    //Дерево
    public void Tree()
    {
        if (this.gameObject.GetComponent<Economic>().Money >= 20)
        {
            GameObject mat = GameObject.Find("Matrix");
            mat.GetComponent<MatrixPollution>().tree = true;
            text.text = "Виберіть місце для посадки дерева.";
        }
        else
        {
            text.text = "Не достатньо коштів";
        }
    }
    //Електростанція
    public void Build()
    {
        if (this.gameObject.GetComponent<Economic>().Money >= 2000)
        {
            GameObject mat = GameObject.Find("Matrix");
            mat.GetComponent<MatrixPollution>().B1 = true;
            text.text = "Виберіть місце для побудови будівлі.";
        }
        else
        {
            text.text = "Не достатньо коштів";
        }

    }
    //Склад
    public void Build1()
    {
        if (this.gameObject.GetComponent<Economic>().Money >= 1000)
        {
            GameObject mat = GameObject.Find("Matrix");
            mat.GetComponent<MatrixPollution>().B2 = true;
            text.text = "Виберіть місце для побудови будівлі.";
        }
        else
        {
            text.text = "Не достатньо коштів";
        }
    }
    //Аераційна станція
    public void ClearTrue()
    {
        if (this.gameObject.GetComponent<Economic>().Money >= 3000)
        {
            GameObject mat = GameObject.Find("Matrix");
            mat.GetComponent<MatrixPollution>().clear = true;
            text.text = "Виберіть місце для побудови будівлі.";
        }
        else
        {
            text.text = "Не достатньо коштів";
        }
    }
    //Дозвіл на редагування мапи
    public void RedactMAp()
    {
        GameObject[] build = GameObject.FindGameObjectsWithTag("Building");
        if (RedactorButton.GetComponentInChildren<Text>().text == "Редагувати мапу")
        {
            foreach (GameObject info in build)
            {
                info.GetComponent<MoveBuild>().redact = true;
            }
            RedactorButton.GetComponentInChildren<Text>().text = "Зберезти зміни";
        }
        else
        {
            foreach (GameObject info in build)
            {
                info.GetComponent<MoveBuild>().redact = false;
            }
            RedactorButton.GetComponentInChildren<Text>().text = "Редагувати мапу";
        }
    }
    public void LoadGrafic()
    {
        SceneManager.LoadScene("Grafics");
    }
    public void LoadSample()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Close()
    {
        Application.Quit();
    }
}
