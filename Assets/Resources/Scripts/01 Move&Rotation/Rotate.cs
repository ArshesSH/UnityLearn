using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 refLocalAxis;
    [SerializeField]
    private float speed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RotateByAxis()
    {
        var locToWorldMat = transform.localToWorldMatrix;
        Vector3 refWorldAxis = locToWorldMat * refLocalAxis;
        
    }
}
