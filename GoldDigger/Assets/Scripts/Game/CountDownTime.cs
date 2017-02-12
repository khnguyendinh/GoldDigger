using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownTime : MonoBehaviour {
    private float secondsLeft = 10f;
    public Text _timeLeft;
    public Text _score;
    public GameObject dialogWin;
    public GameObject dialogLose;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        secondsLeft -= Time.deltaTime;
        if (secondsLeft > 0)
            _timeLeft.text = (int)secondsLeft+"";
        else
        {
            _timeLeft.text = (int)0+"";
            if (!GameManager.Instance.Paused)
            {
                Debug.Log("countdown");
                GameManager.Instance.pauseGame();
                if (GameManager.Instance.Score > 0)
                {
                    _score.text = "$ " + GameManager.Instance.Score;
                    dialogWin.SetActive(true);
                }
                else
                {
                    dialogLose.SetActive(true);
                }
            }
        }
	}
    
}
