using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectroBuild : Build
{
    
    private void Start()
    {
        Name = "ТЕС";
        Upgrade = 0;
        Profit = 500;
        TypeBuild = "Забруднює";
        Shtraf = 0;

        cam = GameObject.Find("Main Camera");
        cam.GetComponent<Economic>().Money -= 2000;
        panel = GameObject.Find("BuildInf");
        upgr1 = GameObject.Find("FirstUpgrade");
        upgr2 = GameObject.Find("SecondUpgrade");
        Sell = GameObject.Find("SellButton");
        InfoLabel = GameObject.Find("InfoLabel");
    }

    private void OnMouseDown()
    {
        //panel.SetActive(true);

        upgr1.GetComponent<Button>().onClick.RemoveAllListeners();
        upgr2.GetComponent<Button>().onClick.RemoveAllListeners();
        Sell.GetComponent<Button>().onClick.RemoveAllListeners();

        InfoLabel.GetComponent<Text>().text = Name + "\n" + "Покращення " + Upgrade + " го" + " рівня" + "\nРадіус враження " + Radius + "\nШтраф " + Shtraf;
        upgr1.GetComponent<Button>().onClick.AddListener(delegate { Upgr1(); });
        upgr2.GetComponent<Button>().onClick.AddListener(delegate { Upgr2(); });
        Sell.GetComponent<Button>().onClick.AddListener(delegate { SellUpgrade(); });
    }

    void Upgr1()
    {
        if (cam.GetComponent<Economic>().Money >= 1000)
        {
            cam.GetComponent<Economic>().Money -= 1000;
            this.Upgrade = 1;
            InfoLabel.GetComponent<Text>().text = Name + "\n" + "Покращення " + Upgrade + " го" + " рівня" + "\n" + "Радіус враження " + Radius;
        }
    }

    void Upgr2()
    {
        if (cam.GetComponent<Economic>().Money >= 5000)
        {
            this.Profit = 500;
            cam.GetComponent<Economic>().Money -= 5000;
            this.Upgrade = 2;
            InfoLabel.GetComponent<Text>().text = Name + "\n" + "Покращення " + Upgrade + " го" + " рівня" + "\n" + "Радіус враження " + Radius;
        }
    }

    void SellUpgrade()
    {
        
        if (this.Upgrade == 1)
            cam.GetComponent<Economic>().Money += 1000;
        if (this.Upgrade == 2)
            cam.GetComponent<Economic>().Money += 5000;
        this.Upgrade = 0;
        InfoLabel.GetComponent<Text>().text = Name + "\n" + "Покращення " + Upgrade + " го" + " рівня" + "\nРадіус враження " + Radius + "\nШтраф " + Shtraf;
    }
}
