using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    [Header( "Settings" )]
    public float moveSpeed = 500.0f;
    public float destroyPosX = -600.0f;
    public int maxHP = 3;

    [Header("Status")]
    public bool shouldDestroy = false;
    int curHP;

    void Start()
    {
        curHP = maxHP;
    }

    void Update()
    {
        MoveWolf();
        CheckDestroyCondition();


        if (shouldDestroy)
        {
            Destroy( gameObject );
        }
    }

    void MoveWolf()
    {

        transform.Translate( Time.deltaTime * moveSpeed * -transform.right );
    }

    void CheckDestroyCondition()
    {
        if( transform.position.x <= destroyPosX )
        {
            shouldDestroy = true;
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.gameObject.CompareTag("Player") )
        {
            Debug.Log( "Hit with Player!" );
        }
    }

}
