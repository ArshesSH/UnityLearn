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

        // 2022.08.30 texture �ҷ��ͼ� �����ϱ�
        //Texture baseTexture = Resources.Load( "Textures/Sky4" ) as Texture; //Resources ���丮 ���� ��ο��� ȣ�� �� texture�� ����
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