using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelEntrance : MonoBehaviour
{
    AudioReverbZone reverbZone;


    void Start()
    {
        reverbZone = transform.root.gameObject.GetComponent<AudioReverbZone>();
        if(reverbZone != null)
        {
            print( "Set" );
        }
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter( Collision collision )
    {
        if ( collision.gameObject.CompareTag( "Player" ) )
        {
            if ( reverbZone.enabled == false )
            {
                reverbZone.enabled = true;
            }
            else
            {
                reverbZone.enabled = false;
            }
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.gameObject.CompareTag( "Player" ) )
        {
            if(reverbZone.enabled == false)
            {
                reverbZone.enabled = true;
            }
            else
            {
                reverbZone.enabled = false;
            }
        }
    }
}
