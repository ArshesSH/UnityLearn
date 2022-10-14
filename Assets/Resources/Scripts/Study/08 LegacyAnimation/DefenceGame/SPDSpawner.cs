using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    private float spawnTime = 1.0f;

    IEnumerator Start()
    {
        while (true)
        {
            spawnTime = SPDGameManager.Instance.genTime;
            //if (GameManager_FlappyBird.Instance.IsGamePlaying())
            {
                GameObject obj = null;
                if (spawnObject != null)
                {
                    obj = Instantiate(spawnObject, transform.position, transform.rotation);
                }
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
