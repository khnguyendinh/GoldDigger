using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pod : MonoBehaviour {

    public enum PodState { 
        ROTATION,// ban đầu
        SHOOT,//khi nhán nút xuống
        REWIND // khi kéo lên
    }
    private PodState podState = PodState.ROTATION;
    private int _angle;
    private float _slowDown;
    #region Serialize
    [SerializeField]
    private int _rotateSpeed = 2;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform point;
    private Animator _mainAnimator;
    public int _dollar,_sumDollar;
    private Vector3 _original;
    private Transform _Rod;
    private bool _flagged;
    private GUIText _score;
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("_flagged " + _flagged);
        if (_flagged)
            return;
        _flagged = true;
        _Rod = collision.transform;
        switch (collision.tag)
        {
            case Config.TAG_BOOM:
                //_Rod.tag = this.tag;
                _Rod.GetComponent<Boom>().Bang(_Rod.position);
                break;
            case Config.TAG_GOLD:
            case Config.TAG_MOUSE:
                _Rod.SetParent(transform);
                _Rod.localPosition = point.localPosition;
                break;
        }
        Debug.Log("Tag "+collision.tag);
        _dollar = _Rod.GetComponent<Rod>().dollar;
     //   _Rod.transform.rotation = Quaternion.Euler(0, 0, 0);
        _slowDown = _Rod.GetComponent<Rod>().slowDown;
        Destroy(_Rod.GetComponent<Rod>());
        podState = PodState.REWIND;
    }
    void Awake() {
        _original = transform.position;
        _score = Camera.main.GetComponent<GUIText>();
        _mainAnimator = transform.root.GetComponent<Animator>();
    }
	void Update () {
        switch (podState)
        {
            case PodState.ROTATION:
                _mainAnimator.Play("Rotation");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    podState = PodState.SHOOT;
                }
                _angle += _rotateSpeed;
                //_flagged = false;
                if (_angle > 70 || _angle < -70)
                {
                    _rotateSpeed *= -1;
                }
                transform.rotation = Quaternion.AngleAxis(_angle,Vector3.forward);
                break;
            case PodState.SHOOT:
                _mainAnimator.Play("Shoot");
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                if(Mathf.Abs(transform.position.x) >14 || transform.position.y < -4)
                {
                    podState = PodState.REWIND;
                }
                break;
            case PodState.REWIND:
                _mainAnimator.Play("Rewind");
                transform.Translate(Vector3.up * (_speed - _slowDown)* Time.deltaTime);
                if (Mathf.Floor(transform.position.x) == Mathf.Floor(_original.x) &&
                    Mathf.Floor(transform.position.y) == Mathf.Floor(_original.y))
                {
                    if(_Rod != null)
                    {
                        _slowDown = 0;
                        _flagged = false;
                        addDollar(_dollar);
                        Destroy(_Rod.gameObject);
                    }
                    transform.position = _original;
                    podState = PodState.ROTATION;
                }
                    break;
        }
	}
    void addDollar(int addDollar)
    {
        _sumDollar += addDollar;
        _score.text = string.Format("$ {0}", _sumDollar);
    }
}
