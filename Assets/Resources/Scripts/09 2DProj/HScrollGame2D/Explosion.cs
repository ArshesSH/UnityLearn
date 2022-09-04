using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifeTime = 1.0f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        
    }
}
