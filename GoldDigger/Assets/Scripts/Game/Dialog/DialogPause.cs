using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogPause : MonoBehaviour {
    public GameObject panelPause;
    public GameObject panelWin;
    public void quitGame()
    {
        Application.Quit();
    }
    public void ContinueGame(){
        panelPause.SetActive(false);
        panelWin.SetActive(false);
    }
    public void pauseGame()
    {
        panelPause.SetActive(true);
    }
}
