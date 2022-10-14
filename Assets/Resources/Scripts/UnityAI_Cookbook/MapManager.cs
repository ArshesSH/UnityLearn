using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    /*--- Public Fields ---*/


    /*--- Protected Fields ---*/


    /*--- Private Fields ---*/


    /*--- MonoBehaviour Callbacks ---*/
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("NavMeshAreas");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("DynamicNavMesh");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("NavMeshLink");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("NvMeshObstacles");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene("NvMeshPatrol");
        }
    }


	/*--- Public Methods ---*/


	/*--- Protected Methods ---*/


	/*--- Private Methods ---*/
}