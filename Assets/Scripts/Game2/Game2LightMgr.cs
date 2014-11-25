using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game2LightMgr : MonoBehaviour
{

    Material m_material;        // マテリアル

    float m_offsetValue = 1.0f / 6.0f;

    float m_nowTime;
    float m_changeTime;

    Color m_nowColor;
    // ライトの可動域を設定きめる定数
    Vector2 m_xRange;
    Vector2 m_zRange;

    public enum LightType { BACK, RIGHT, LEFT, FRONTRIGHT,FRONTLEFT, FRONTBACK};
    public LightType m_lightType;
    // Use this for initialization
    void Start()
    {
        m_material = this.renderer.material;
        //UVは白色に固定
        m_material.mainTextureOffset = new Vector2(0.83f, 0.0f);
        // 初期の色はランダムで決定
        m_nowColor = RandomColor();
        RandomLight();

        // ライト回転
        SetNewRotate();

        // ライトの種類によって可動域を変更する
        switch (m_lightType)
        {
            case LightType.BACK:
                m_xRange.x = 20.0f;
                m_xRange.y = 70.0f;
                m_zRange.x = -25.0f;
                m_zRange.y = 25.0f;
                break;
            case LightType.RIGHT:
                m_xRange.x = 20.0f;
                m_xRange.y = 70.0f;
                m_zRange.x = -25.0f;
                m_zRange.y = 25.0f;
                break;
            case LightType.LEFT:
                m_xRange.x = 290.0f;
                m_xRange.y = 340.0f;
                m_zRange.x = -25.0f;
                m_zRange.y = 25.0f;
                break;
            case LightType.FRONTRIGHT:
                m_xRange.x = 20.0f;
                m_xRange.y = 70.0f;
                m_zRange.x = 155.0f;
                m_zRange.y = 205.0f;
                break;
            case LightType.FRONTLEFT:
                m_xRange.x = 290.0f;
                m_xRange.y = 340.0f;
                m_zRange.x = 155.0f;
                m_zRange.y = 205.0f;
                break;
            case LightType.FRONTBACK:
                m_xRange.x = 290.0f;
                m_xRange.y = 340.0f;
                m_zRange.x = 155.0f;
                m_zRange.y = 205.0f;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_nowTime += Time.deltaTime;
        if (m_nowTime >= m_changeTime)
        {
            RandomLight();
        }
    }

    void RandomLight()
    {
        SetNewColor();
        m_nowTime = 0.0f;
        m_changeTime = Random.Range(1.0f, 5.0f);
    }

    void SetNewColor()
    {
        m_nowColor = RandomColor();
        Hashtable parameters = new Hashtable(){
            {"color",m_nowColor},
            {"time",m_changeTime},
            {"easetype",iTween.EaseType.linear},
        };
        iTween.ColorTo(this.gameObject, parameters);
    }

    void SetNewRotate()
    {
        Hashtable parameters = new Hashtable(){
            {"x",Random.Range(m_xRange.x,m_xRange.y)},
            {"z",Random.Range(m_zRange.x,m_zRange.y)},
            {"speed",30.0f},
            {"islocal",false},
            {"easetype",iTween.EaseType.linear},
            {"oncomplete", "SetNewRotate"}
        };
        iTween.RotateTo(this.gameObject,parameters);
    }

    Color RandomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}
