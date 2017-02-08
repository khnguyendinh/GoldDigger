using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownTime : MonoBehaviour {
    private float secondsLeft = 10f;
    public Text _timeLeft;
    public GameObject pod;
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
            Time.timeScale = 0;
            Pod other = (Pod)pod.GetComponent(typeof(Pod));
            if (other._dollar > 0)
            {
                dialogWin.SetActive(true);
            }
            else
            {
                dialogLose.SetActive(true);
            }
        }
	}
    
}
