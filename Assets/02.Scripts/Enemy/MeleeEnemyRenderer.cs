using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyRenderer : AgentRenderer
{
    public bool _isReversal = false;
    public override void ChangeFace(Vector2 pointerInput)
    {

        Vector3 dir = (Vector3)pointerInput - transform.position;
        Vector3 result = Vector3.Cross(Vector2.up, dir);
        if (!_isReversal)
        {
            if (result.z < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (result.z > 0)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
        else if(_isReversal)
        {
            if (result.z > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (result.z < 0)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}
