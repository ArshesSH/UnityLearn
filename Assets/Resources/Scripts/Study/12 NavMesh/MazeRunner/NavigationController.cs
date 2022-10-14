using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public float speed = 1.0f;
    Vector3 direction;
    void Start()
    {
        
    }

    void Update()
    {
        GameObject item = GameManager_MazeRunner.Instance.Item;
        if(item != null)
        {
            transform.LookAt(new Vector3(item.transform.position.x, transform.position.y, item.transform.position.z));
        }
    }
}
