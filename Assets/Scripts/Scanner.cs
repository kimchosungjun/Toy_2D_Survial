using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0,targetLayer);
        nearestTarget=GetNearest();
    }

    Transform GetNearest()
    {
        Transform result=null;
        float diff= 100;
        for (int i=0; i<targets.Length; i++)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = targets[i].transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);
            if (diff > curDiff)
            {
                diff = curDiff;
                result = targets[i].transform;
            }
        }
        return result;
    }
}
