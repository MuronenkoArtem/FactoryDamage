using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellRoot : MonoBehaviour
{
    public GameObject panel;

    public GameObject upgr1;
    public GameObject upgr2;
    public GameObject Sell;
    public GameObject InfoLabel;

    public int[] Position = new int[2];
    public Color col = Color.green;//
    public double Pollution = 0;
    public int PriceRecovery = 0;
    

    new Renderer renderer;
    int z1 = 5;
    int z2 = 1;

    // Use this for initialization
    void Start()
    {
        Position[0] = (int)this.transform.position.x;
        Position[1] = (int)this.transform.position.z;

        renderer = GetComponent<Renderer>();
        col = this.renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Pollution <= 0)
        {
            this.Pollution = 0;
            renderer.material.color = Color.green;
        }

        if (this.Pollution < z2)
            renderer.material.color = Color.green;

        if (this.Pollution > z1)
        {
            this.PriceRecovery = 50;
            renderer.material.color = Color.red;
            if (this.Pollution >= 20)
                this.PriceRecovery = 200;
            if (this.Pollution >= 50)
                this.PriceRecovery = 1000;
        }       
        if (this.Pollution > z2 && this.Pollution < z1)
        {
            renderer.material.color = Color.yellow;
            this.PriceRecovery = 10;
        }
    }
    /// <summary>
    /// Метод забруднення та відновлення клітинки
    /// </summary>
    /// <param name="R"></param>
    /// <param name="DP"></param>
    public void ChangePollution(double R, double DP)
    {
        double K = DP;
        double D = Math.Abs(K / R);
        if(Double.IsInfinity(D))
            this.Pollution += DP;
        if (!Double.IsInfinity(D) && D != 0)
            this.Pollution += D;
    }
    /// <summary>
    /// Медод для очмсних споруд
    /// </summary>
    /// <param name="R"></param>
    /// <param name="DP"></param>
    public void RecoveryPollytion(double R, double DP)
    {
        double K = DP;
        double D = Math.Abs(K / R);
        if ((this.Pollution - D) >= 0)
            this.Pollution -= D;
    }
    /// <summary>
    /// Природнє відновлення
    /// </summary>
    public void NaturalRestore()
    {
        if (this.Pollution >= 0.02)
            this.Pollution -= 0.02;
    }

    private void OnMouseDown()
    {
        panel = GameObject.Find("BuildInf");
        upgr1 = GameObject.Find("FirstUpgrade");
        upgr2 = GameObject.Find("SecondUpgrade");
        Sell = GameObject.Find("SellButton");
        InfoLabel = GameObject.Find("InfoLabel");

        upgr1.GetComponent<Button>().onClick.RemoveAllListeners();
        upgr2.GetComponent<Button>().onClick.RemoveAllListeners();
        Sell.GetComponent<Button>().onClick.RemoveAllListeners();

        InfoLabel.GetComponent<Text>().text = "Забруднення: " + this.Pollution + "\nЦіна очищення " + this.PriceRecovery;

        Sell.GetComponent<Button>().onClick.AddListener(delegate { SellUpgrade(); });
    }
    void SellUpgrade()
    {        
        if (GameObject.Find("Main Camera").GetComponent<Economic>().Money >= this.PriceRecovery && this.Pollution != 0)
        {
            GameObject.Find("Main Camera").GetComponent<Economic>().Money -= this.PriceRecovery;
            this.Pollution = 0;
            InfoLabel.GetComponent<Text>().text = "Забруднення: " + this.Pollution + "\nЦіна очищення " + this.PriceRecovery;
        }
        else
        {           
            InfoLabel.GetComponent<Text>().text = "Забруднення: " + this.Pollution + "\nЦіна очищення " + this.PriceRecovery;
        }
           
    }
}
