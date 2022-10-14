using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpartaControllerAI : SpartaPlayerController
{
    // Start is called before the first frame update
    void Start()
    {
        spartanKingAnim = gameObject.GetComponentInChildren<Animation>();
        spartanKingAnim.wrapMode = WrapMode.Loop;
        spartanKingAnim.Play( "idle" );

        weaponCollider = weapon.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
    }
}
