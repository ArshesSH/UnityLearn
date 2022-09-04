using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXBullet : MonoBehaviour
{
    public int Damage = 1;
    public float MoveSpeed = 700.0f;
    public float destroyTime = 3.0f;
    float speed;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (RockManGameManager.Instance.IsPlayerFacingRight())
        {
            speed = MoveSpeed;
        }
        else
        {
            speed = -MoveSpeed;
            spriteRenderer.flipX = true;
        }
        StartCoroutine("DestroyTimer");
    }

    void Update()
    {
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + speed * Time.deltaTime * Vector2.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            HitAndDestroy();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            ImmuneAndDestroy();
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }

    void HitAndDestroy()
    {
        Destroy(gameObject);
    }

    void ImmuneAndDestroy()
    {
        Destroy(gameObject);
    }

}
