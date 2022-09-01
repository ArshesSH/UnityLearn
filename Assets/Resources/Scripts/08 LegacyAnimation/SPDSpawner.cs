using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPDSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private float spawnTime = 1.0f;

    IEnumerator Start()
    {
        while (true)
        {
            //if (GameManager_FlappyBird.Instance.IsGamePlaying())
            {
                GameObject obj = null;
                if (spawnObject != null)
                {
                    // 부모에 오브젝트 붙이기
                    obj = Instantiate(spawnObject, transform.position, transform.rotation);
                }
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
