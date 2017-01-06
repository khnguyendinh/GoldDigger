using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : Rod {
    public float start;
    public float end;
    [SerializeField]
    public int _speed;
    private void Start()
    {

    }
    private void Update()
    {
        if(check(start)|| check(end))
        {
            _speed *= -1;
        }
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
    private bool check(float point)
    {
        return Mathf.Abs(point) < Mathf.Abs(transform.position.x);
    }
}
