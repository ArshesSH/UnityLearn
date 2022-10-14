using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyFlow : MonoBehaviour
{
    private Renderer render;
    private Material mat;
    float flowSpeed = 0.01f;
    float moveFlowSpeed = 0.1f;
    float offset;

    private void Awake()
    {
        render= GetComponent<Renderer>();
    }

    void Start()
    {
        mat = render.material;

        // 2022.08.30 texture 불러와서 변경하기
        //Texture baseTexture = Resources.Load( "Textures/Sky4" ) as Texture; //Resources 디렉토리 밑의 경로에서 호출 후 texture로 변경
        //mat.SetTexture( "_BaseMap", baseTexture );
        // mat.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        float curFlowSpeed = flowSpeed;

        if(GameManager_FlappyBird.Instance.IsGamePlaying())
        {
            curFlowSpeed = moveFlowSpeed;
        }
        offset += Time.deltaTime * curFlowSpeed;

        mat.SetTextureOffset( "_BaseMap", new Vector2( offset, 0 ) );
    }
}
