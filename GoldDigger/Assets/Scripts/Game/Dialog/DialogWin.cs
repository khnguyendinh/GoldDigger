using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogWin : MonoBehaviour {
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
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }
}
