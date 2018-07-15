using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGrafic : MonoBehaviour {

    public void LoadGrafic()
    {
        SceneManager.LoadScene("PollutionMeth");
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
