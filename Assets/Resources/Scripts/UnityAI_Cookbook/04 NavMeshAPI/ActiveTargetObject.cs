using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTargetObject : MonoBehaviour
{
	/*--- Public Fields ---*/
	public GameObject target;

	/*--- MonoBehaviour Callbacks ---*/
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Q))
        {
			target.SetActive( !target.activeInHierarchy );
        }
	}

}