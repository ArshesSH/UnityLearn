using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastEx : MonoBehaviour
{
    [Range( 0, 50 )]
    public float Distance = 10.0f;
    private RaycastHit rayCastHit;
    private RaycastHit[] rayCastHits;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        //ray = new Ray();
        //ray.origin = this.transform.position;
        //ray.direction = this.transform.forward;

        ray = new Ray( transform.position, transform.forward );
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = this.transform.position;
        ray.direction = this.transform.forward;
        //Ray_1();
        //Ray_2();
        //Ray_3();
        //FindByLayer( "Test" );
        //CastInRange();
        //GetDistanceFromFirstCastedObj();
        CastObjectsByRay();

    }

    void CastObjectByRay()
    {
        if (Physics.Raycast( ray, out rayCastHit, Distance ))
        {
            Debug.Log( rayCastHit.collider.gameObject.name );
        }
    }
    void CastObjectsByRay()
    {
        rayCastHits = Physics.RaycastAll( ray, Distance );
        foreach(var rayHit in rayCastHits)
        {
            Debug.Log( rayHit.collider.gameObject.name );
        }
    }

    void CastObjectsBySphere()
    {
        rayCastHits = Physics.SphereCastAll( ray, 0.0f, Distance );
        string objName = "";
        foreach ( RaycastHit hit in rayCastHits )
        {
            objName += hit.collider.name + ",";
        }
        print( objName );
    }

    void FindByTag(string tag)
    {
        rayCastHits = Physics.RaycastAll( ray, Distance );
        foreach ( var rayHit in rayCastHits )
        {
            if( rayHit.collider.CompareTag( tag ) )
            {
                Debug.Log( rayHit.collider.gameObject.name );
            }
        }
    }
    void FindByLayer( string layerName )
    {
        rayCastHits = Physics.RaycastAll( ray, Distance );
        foreach ( var rayHit in rayCastHits )
        {
            if ( rayHit.collider.gameObject.layer == LayerMask.NameToLayer( layerName ) )
            {
                Debug.Log( rayHit.collider.gameObject.name );
            }
        }
    }



    void GetDistanceFromFirstCastedObj()
    {
        if( Physics.Raycast( ray, out rayCastHit, Distance ) )
        {
            Debug.Log( "distatnce : " + rayCastHit.distance.ToString() );
        }
    }


    private void OnDrawGizmos()
    {
        DrawGizmos_1();
        DrawGizmos_2();
    }

    void DrawGizmos_1()
    {
        // Draw ray
        //Debug.DrawRay( ray.origin, ray.direction * Distance, Color.green );

        // Draw origin
        Gizmos.color = new Color32( 0, 0, 255, 255 );
        Gizmos.DrawSphere( ray.origin, 0.1f );

        // Draw Cast Range
        Gizmos.color = new Color32( 255, 255, 0, 255 );
        Gizmos.DrawWireSphere( ray.origin, Distance );


    }
    void DrawGizmos_2()
    {
        if(rayCastHits != null)
        {
            foreach(var rayHit in rayCastHits)
            {
                if(rayHit.collider != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere( rayHit.point, 0.1f );
                    // Draw to point
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine( transform.position, transform.position + transform.forward * rayHit.distance );

                    // Draw Normal
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine( rayHit.point, rayHit.point + rayHit.normal );

                    // Draw Reflect
                    Gizmos.color = Color.magenta;
                    Vector3 reflect = Vector3.Reflect( transform.forward, rayHit.normal );
                    Gizmos.DrawLine( rayHit.point, rayHit.point + reflect );
                }
            }
        }
    }
}
