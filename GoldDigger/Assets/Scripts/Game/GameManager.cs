using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    private bool paused;
    private int score;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public bool Paused
    {
        get { return paused; }
    }

    public static GameManager Instance
    {
        get {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return GameManager.instance; 
        }
    }
    public void pauseGame()
    {
        Debug.Log("pause "+paused);
        paused = !paused;
    }
   


}
