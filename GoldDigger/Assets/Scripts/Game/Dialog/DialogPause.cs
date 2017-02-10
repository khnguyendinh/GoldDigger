using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPause : MonoBehaviour {
    public GameObject panelPause;
    public void quitGame()
    {
        Application.Quit();
    }
    public void ContinueGame(){
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }
}
