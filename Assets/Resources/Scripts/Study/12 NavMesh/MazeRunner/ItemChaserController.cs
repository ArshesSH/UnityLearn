using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class ItemChaserController : EnemyController
{
    #region Public Fields
    #endregion


    #region Private Fields
    #endregion


    #region MonoBehaviour Callback
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    #endregion

    protected override void FindTargetPosition()
    {
        GameObject item = GameManager_MazeRunner.Instance.Item;
        if (item != null)
        {
            targetPos = item.transform.position;
        }
    }


    #region Private Methods

    #endregion

}
