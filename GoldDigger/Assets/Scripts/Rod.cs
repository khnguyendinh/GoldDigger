using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    [SerializeField]
    private string _tag;
    [SerializeField]
    public int slowDown;
    [SerializeField]
    public int dollar;

    private void Awake()
    {
        this.tag = _tag;
    }

    void Update()
    {
      //  transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
