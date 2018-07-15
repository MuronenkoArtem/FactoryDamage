using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void QuitProgram()
    {
        Application.Quit();
    }
    public void LoadPollut()
    {
        SceneManager.LoadScene("PollutionMeth");

    }
    public void LoadSample()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadGrafh()
    {
        SceneManager.LoadScene("Grafics");
    }
}
