using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NameDisplay : MonoBehaviour {

    List<UISprite> m_charList = new List<UISprite>();   // 名前のスプライトのリスト

    [Header("設定ファイル")]
    public UserRegSetting m_setting;                    // 設定ファイル

    [Header("文字プレハブ")]
    public GameObject m_char;                           // 文字フレハブ

    [Header("文字の間隔")]
    public float m_charOffset;

    // アクセサ
    public string Name{
        get
        {
            string str = string.Empty;
            foreach(UISprite us in m_charList){
                str += us.spriteName;
            }
            return str;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //======================================================
    // @brief:入力された文字を整列させる
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //=====================================================
    void AdjustCharList()
    {
        if( m_charList.Count <= 0) return;

        // 文字の幅を取得
        float charWidth = m_charList[0].transform.localScale.x;

        // センタリングのオフセット
        float charCenterOffset = charWidth * m_charList.Count / 2.0f - charWidth/2.0f;
        // 文字間隔のオフセット
        float charLenghtOffset = m_charOffset * m_charList.Count / 2.0f - m_charOffset/2.0f;

        for(int i=0;i<m_charList.Count;i++)
        {
            if( i == 0)
            {
                m_charList[i].transform.localPosition = new Vector3(-charCenterOffset-charLenghtOffset,m_charList[i].transform.localPosition.y,m_charList[i].transform.localPosition.z);
            }
            else
            {
                m_charList[i].transform.localPosition = new Vector3(m_charList[i-1].transform.localPosition.x + charWidth + m_charOffset,m_charList[i].transform.localPosition.y,m_charList[i].transform.localPosition.z);
            }
        }
    }

    //======================================================
    // @brief:入力された文字をリストに追加する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:入力された文字
    // @return:none
    //=====================================================
    public void AddName(string str)
    {
        if (m_charList.Count < m_setting.nameLength)
        {
            // 文字プレハブを生成
            GameObject go = Instantiate(m_char) as GameObject;
            Vector3 tmpScale = go.transform.localScale;
            Vector3 tmpPos = go.transform.localPosition;
            go.transform.parent = transform;
            go.transform.localScale = tmpScale;
            go.transform.localPosition = tmpPos;
            go.name = "Name" + m_charList.Count;
            UISprite us = go.GetComponent<UISprite>();
            us.spriteName = str;
            // 文字リストに追加
            m_charList.Add(us);
            // 文字のセンタリング
            AdjustCharList();
        }
    }

    //======================================================
    // @brief:文字を削除する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //=====================================================
    public void DeleteName()
    {
        if (m_charList.Count <= 0) return;

        // リストの末尾から削除する
        Destroy( m_charList[m_charList.Count - 1].gameObject );
        m_charList.RemoveAt(m_charList.Count-1);
        // 文字のセンタリングs
        AdjustCharList();
    }
}
