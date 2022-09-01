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
    [SerializeField]
    private float effectTime = 0.3f;

    public bool showTowerUI = true;

    private Renderer render;
    private Material mat;

    private Renderer minimapCubeRender;
    private Material minimapCubeMat;

    private void Awake()
    {
        render = GetComponentInChildren<Renderer>();

    }
    void Start()
    {
        curHP = maxHP;
        mat = render.material;
        minimapCubeRender = transform.Find("MinimapCube").GetComponent<Renderer>();
        minimapCubeMat = minimapCubeRender.material;
    }
    void Update()
    {
        if(curHP <= 0)
        {
            SPDGameManager.Instance.towerCount--;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.CompareTag(enemyTag) && other.gameObject.CompareTag(damagedTag))
        {
            curHP--;
            StartCoroutine("DamageEffect");
        }
    }

    IEnumerator DamageEffect()
    {
        var oldColor = mat.color;
        var oldCubeColor = minimapCubeMat.color;
        mat.color = Color.red;
        minimapCubeMat.color = Color.red;
        yield return new WaitForSeconds(effectTime);
        mat.color = oldColor;
        minimapCubeMat.color = oldCubeColor;
    }


    private void OnGUI()
    {
        if(showTowerUI)
        {
            GUI.Box(new Rect(350.0f + towerNumber * 100.0f, 0.0f, 100.0f, 30.0f), "Tower" + towerNumber + ": " + curHP);
        }
    }

}
