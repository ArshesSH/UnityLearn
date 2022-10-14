using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyObstacle : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float destroyPos = -15.0f;
    [SerializeField]
    private float posRange = 4.0f;

    [SerializeField]
    private GameObject topObstacle;
    [SerializeField]
    private GameObject bottomObstacle;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3( 0.0f, Random.Range( -posRange, posRange ), 0.0f );
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager_FlappyBird.Instance.IsGamePlaying())
        {
            transform.Translate( Vector3.left * moveSpeed * Time.deltaTime );

            if ( transform.position.x <= destroyPos )
            {
                Destroy( gameObject );
            }
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if(other.gameObject.CompareTag("FireAttack"))
        {
            
        }
    }

}
