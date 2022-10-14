using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    public float spawnTime = 1.0f;

    IEnumerator Start()
    {
        while ( true )
        {
            GameObject obj = null;
            if ( spawnObject != null )
            {
                obj = Instantiate( spawnObject, transform.position, transform.rotation );
            }

            yield return new WaitForSeconds( spawnTime );
        }
    }
}
