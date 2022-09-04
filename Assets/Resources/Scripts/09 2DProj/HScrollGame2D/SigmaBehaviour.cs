using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigmaBehaviour : MonoBehaviour
{
    public enum State
    {
        Idle,
        Ready,
        Fade,
        Open,
        Damaged
    }

    [Header("Status")]
    public float maxHP = 30.0f;
    public float curHP = 30.0f;
    public float moveSpeed = 100.0f;

    public GameObject spawnPos;

    void Start()
    {
        curHP = maxHP;
    }

    void Update()
    {
        
    }
}
