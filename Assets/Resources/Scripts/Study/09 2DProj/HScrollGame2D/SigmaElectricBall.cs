using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigmaElectricBall : MonoBehaviour
{
    public enum State
    {
        Wait,
        Move,
        ReadyForDestroy
    }

    public float damage = 5.0f;
    public float moveSpeed = 600.0f;
    public float waitTime = 2.0f;
    public float destroyTime = 2.0f;

    float time = 0.0f;
    Vector2 direction;

    GameObject playerX;
    State curState = State.Wait;

    void Start()
    {
        playerX = RockManGameManager.Instance.PlayerX;
    }

    void Update()
    {
        switch (curState)
        {
            case State.Wait:
            {
                time += Time.deltaTime;
                if (time >= waitTime)
                {
                    direction = (playerX.transform.position - transform.position).normalized;
                    curState = State.Move;
                }
            }
            break;
            case State.Move:
            {
                transform.Translate(moveSpeed * Time.deltaTime * direction);
            }
            break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            curState = State.ReadyForDestroy;
            Destroy(gameObject, destroyTime);
        }
    }
}
