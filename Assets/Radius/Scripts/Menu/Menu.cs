using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject panel;
    public GameObject setting;

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
    public Text text;
    public void Tree()
    {
        GameObject mat = GameObject.Find("Matrix");
        mat.GetComponent<Matrix>().tree = true;
        text.text = "true";
    }

    public void Build()
    {
        GameObject mat = GameObject.Find("Matrix");
        mat.GetComponent<Matrix>().B1 = true;
    }

    public void Build1()
    {
        GameObject mat = GameObject.Find("Matrix");
        mat.GetComponent<Matrix>().B2 = true;
    }
    //See Zone
    public void ZoneTrue()
    {
        GameObject mat = GameObject.Find("Matrix");
        GameObject but = GameObject.Find("ZoneSee");
        if (mat.GetComponent<Matrix>().dobro == false)
        {
            mat.GetComponent<Matrix>().dobro = true;
            but.GetComponentInChildren<Text>().text = "Приховати зону";
        }
        else
        {
            mat.GetComponent<Matrix>().dobro = false;
            but.GetComponentInChildren<Text>().text = "Показати зону";
        }
    }
    public void LoadGrafic()
    {
        SceneManager.LoadScene("Grafics");
    }
    public void LoadSample()
    {
        SceneManager.LoadScene("PollutionMeth");
    }
    public void Close()
    {
        Application.Quit();
    }
}