using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private int maxHP = 10;
    private int curHP;
    [SerializeField]
    private string enemyTag = "Enemy";
    [SerializeField]
    private string damagedTag = "Weapon";
    [SerializeField]
    private int towerNumber = 0;

    void Start()
    {
        curHP = maxHP;
    }
    void Update()
    {
        if(curHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.CompareTag(enemyTag) && other.gameObject.CompareTag(damagedTag))
        {
            curHP--;
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(150.0f + towerNumber * 50.0f, 400.0f, 50.0f, 30.0f), "HP:" + maxHP );
    }

}
