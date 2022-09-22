using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float destroyTime = 10.0f;
    public string groundTag = "Obstacle";

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("DestroyObj");
    }
    void Update()
    {
    }
    private void FixedUpdate()
    {
        rb.MovePosition( transform.position + Time.deltaTime * moveSpeed * transform.forward);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

}
