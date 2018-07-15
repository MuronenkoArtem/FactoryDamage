using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : Build {

    private void Start()
    {
        Name = "Дерево";
        Upgrade = 0;
        Profit = 0;
        TypeBuild = "Очищення";
        Shtraf = 0;
        Radius = 1;

        cam = GameObject.Find("Main Camera");
        cam.GetComponent<Economic>().Money -= 20;
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

        InfoLabel.GetComponent<Text>().text = Name + "\nРадіус очищення " + Radius;
       
        Sell.GetComponent<Button>().onClick.AddListener(delegate { SellTree(); });
    }
   
    void SellTree()
    {
        cam.GetComponent<Economic>().Money += 20;
        Destroy(this.gameObject);       
    }
}
