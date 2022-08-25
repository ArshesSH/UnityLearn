using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate
{
    void RotateByAxis(GameObject gameObject, Vector3 refLocalAxis, float speed)
    {
        var locToWorldMat = gameObject.transform.localToWorldMatrix;
        Vector3 refWorldAxis = locToWorldMat * refLocalAxis;

        float rot = speed * Time.deltaTime;
        gameObject.transform.Rotate( refWorldAxis, rot );
    }
}
