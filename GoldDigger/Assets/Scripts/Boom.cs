using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Rod {
    public void Bang(Vector3 pos, bool flag =  false)
    {
        var hits = Physics2D.CircleCastAll(pos, 2, Vector2.zero);
        foreach(var hit in hits)
        {
            switch (hit.transform.tag)
            {
                case Config.TAG_GOLD:
                    Destroy(hit.transform.gameObject);
                    break;
                case Config.TAG_BOOM:
                    hit.transform.tag = Config.TAG_GOLD;
                    hit.transform.GetComponent<Boom>().Bang(hit.point, true);
                    break;
            }
        }
        //if (flag)
        //{
        //    Destroy(gameObject);
        //}
    }
}
