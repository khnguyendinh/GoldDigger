using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pod : MonoBehaviour {

    public enum PodState { 
        ROTATION,// ban đầu
        SHOOT,//khi nhán nút xuống
        REWIND // khi kéo lên
    }
    PodState podState = PodState.ROTATION;
    private int _angle;
    private int _rotateSpeed = 2;
    void Awake() { 
    }
	void Update () {
        switch (podState)
        {
            case PodState.ROTATION:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    podState = PodState.SHOOT;
                }
                _angle += _rotateSpeed;
                if (_angle > 80 || _angle < -80)
                {
                    _rotateSpeed *= -1;
                }
                Vector3 forward = new Vector3(0, 0, 1);
                transform.rotation = Quaternion.AngleAxis(_angle,Vector3.forward);
                break;
            case PodState.SHOOT:
                break;
            case PodState.REWIND:
                break;
        }
	}
}
