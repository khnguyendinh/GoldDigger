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
        panelPause.SetActive(false);
    }
}
