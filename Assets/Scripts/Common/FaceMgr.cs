using UnityEngine;
using System.Collections;

public class FaceMgr : MonoBehaviour {

    [Header("顔のマテリアル")]
    public Material m_face; // 顔のマテリアル

    [Header("顔のテクスチャ")]
    public Texture[] m_normal;
    public Texture[] m_grad;
    public Texture[] m_pass;
    public Texture[] m_sad;
    public Texture[] m_smile;

    public enum eFaceType { NORMAL, PASS, GRAD, SAD, SMILE };
    [Header("顔タイプ")]
    public eFaceType m_faceType;

    [Header("まばたきの時間")]
    public float m_blinkTime;

    float m_changeExpressionTime;

    Texture m_nowTex;               // 現在のテクスチャ 
    int m_index;                    // テクスチャインデックス
    // 時間を管理する変数
    float m_nowExpressionTime;      // 表情を管理
    float m_nowBlinkTime;           // まばたきを管理

    float m_waitBlinkTime;          // まばたきまでの時間 
    bool m_isBlink;                 // まばたきしているとtrue

    // アクセサ
    // 表情を自動で変更するかどうか
    [Header("表情を自動で変更するかどうか")]
    public bool IsAuto;

    [Header("プレイヤー？")]
    public bool IsPlayer;

	// Use this for initialization
	void Start () {
        m_nowTex = m_normal[0];
        m_faceType = eFaceType.NORMAL;
        m_nowBlinkTime = 0.0f;
        m_nowExpressionTime = 0.0f;
        m_index = 0;
        m_waitBlinkTime = Random.Range(1.0f, 3.0f);
        m_changeExpressionTime = Random.Range(3.0f, 8.0f);
        m_isBlink = false;
	}
	
	// Update is called once per frame
	void Update () {
        RandomFace();
	}

    //======================================================
    // @brief:ランダムに表情を決める
    // normal,grad,smileからランダムでチョイスする。
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //=====================================================
    void RandomFace()
    {
        if (!IsAuto)
        {
            m_nowExpressionTime = 0.0f;
            return;
        }

        m_nowExpressionTime += Time.deltaTime;
        if (m_nowExpressionTime >= m_changeExpressionTime && m_index == 0)
        {

            while (m_nowTex == m_face.mainTexture)
            {
                int index = Random.Range(0, 3);
                switch (index)
                {
                    case 0:
                        m_nowTex = m_normal[0];
                        m_faceType = eFaceType.NORMAL;
                        break;
                    case 1:
                        if (!IsPlayer)
                        {
                            m_nowTex = m_pass[0];
                            m_faceType = eFaceType.PASS;
                            break;
                        }
                        m_nowTex = m_grad[0];
                        m_faceType = eFaceType.GRAD;
                        break;
                    case 2:
                        m_nowTex = m_smile[0];
                        m_faceType = eFaceType.SMILE;
                        break;
                }
            }
            m_face.mainTexture = m_nowTex;
            m_nowExpressionTime = 0.0f;
            m_changeExpressionTime = Random.Range(3.0f, 8.0f);
        }
        // まばたき
        if (IsPlayer)
        {
            BlinkEye();
        }
    }

    //======================================================
    // @brief:まばたき
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //=====================================================
    void BlinkEye()
    { 
        if (!m_isBlink)
        {
            m_isBlink = true;
            m_waitBlinkTime = Random.Range(1.0f, 3.0f);
            StartCoroutine(DoBlink());
        }

        m_face.mainTexture = m_nowTex;
    }

    IEnumerator DoBlink()
    {
        while (m_nowBlinkTime <= m_waitBlinkTime)
        {
            m_nowBlinkTime += Time.deltaTime;
            yield return null;
        }

        switch (m_faceType)
        {
            case eFaceType.GRAD:
                m_nowTex = m_grad[1];
                break;
            case eFaceType.SMILE:
                m_nowTex = m_smile[1];
                break;
        }

        m_face.mainTexture = m_nowTex;
        yield return new WaitForSeconds(m_blinkTime);

        switch (m_faceType)
        {
            case eFaceType.GRAD:
                m_nowTex = m_grad[0];
                break;
            case eFaceType.SMILE:
                m_nowTex = m_smile[0];
                break;
        }
        m_face.mainTexture = m_nowTex;
        m_nowBlinkTime = 0.0f;
        m_isBlink = false;
    }

    //======================================================
    // @brief:任意の顔に変更する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:フェイスタイプ
    // @return:none
    //=====================================================
    public void ChangeFace(eFaceType type)
    {
        if (m_faceType == type) return;

        m_faceType = type;

        switch (type)
        {
            case eFaceType.NORMAL:
                m_nowTex = m_normal[0];
                break;
            case eFaceType.PASS:
                m_nowTex = m_pass[0];
                break;
            case eFaceType.GRAD:
                if (m_grad[0] == null) break;
                m_nowTex = m_grad[0];
                break;
            case eFaceType.SAD:
                if (m_sad[0] == null) break;
                m_nowTex = m_sad[0];
                break;
            case eFaceType.SMILE:
                m_nowTex = m_smile[0];
                break;
        }

        m_face.mainTexture = m_nowTex;
        m_index = 0;
        m_nowBlinkTime = 0.0f;
    }
}
