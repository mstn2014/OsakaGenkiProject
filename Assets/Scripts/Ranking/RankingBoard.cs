using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingBoard : MonoBehaviour {

    ConnectServer m_server;         // サーバーとの通信をするクラス
    List<UILabel> m_rankText = new List<UILabel>();       // 順位を表示する
    List<UILabel> m_nameText = new List<UILabel>();       // 名前を表示する
    List<UILabel> m_scoreText = new List<UILabel>();      //　スコアを表示する 
    SaveMgr m_saveData;             // セーブデータ

    [SerializeField][Header("ランキング設定ファイル")]
    RankingSetting m_rankSetting;
    [SerializeField][Header("ランキングを表示するオブジェクト")]
    List<GameObject> m_rank;
    

    void Awake()
    {
        // 共通設定の呼び出し.
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_saveData = gs.SaveMgr;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(GetServerInfo());
	}

    IEnumerator GetServerInfo()
    {
        // サーバーからランキングデータの取得
        m_server = GetComponent<ConnectServer>();
        m_server.getRanking();

        while(!m_server.IsGet){
            yield return null;
        }

        // ランキングオブジェクトからUILabelを取り出しておく
        foreach (GameObject go in m_rank)
        {
            m_rankText.Add(go.transform.FindChild("Rank").gameObject.GetComponent<UILabel>());
            m_nameText.Add(go.transform.FindChild("Name").gameObject.GetComponent<UILabel>());
            m_scoreText.Add(go.transform.FindChild("Score").gameObject.GetComponent<UILabel>());
        }

        // 取得したデータを表示する
        for (int i = 0; i < m_rankSetting.dispRank; i++)
        {
            if (i > (m_rankSetting.dispRank - 1)) break;
            if (i > (m_server.Data.Count - 1)) break;

            m_rankText[i].text = m_server.Data[i].rank.ToString();
            m_nameText[i].text = m_server.Data[i].name.ToString();
            m_scoreText[i].text = m_server.Data[i].score.ToString() + "pt";
        }

        if (m_saveData.userRank <= 9)
        {
            for (int i = 5; i < m_rank.Count; i++)
            {
                if (i > (m_server.Data.Count - 1)) break;
                m_rankText[i].text = m_server.Data[i].rank.ToString();
                m_nameText[i].text = m_server.Data[i].name.ToString();
                m_scoreText[i].text = m_server.Data[i].score.ToString() + "pt";
            }
            m_rankText[m_saveData.userRank-1].color = Color.red;
            m_nameText[m_saveData.userRank-1].color = Color.red;
            m_scoreText[m_saveData.userRank-1].color = Color.red;
        }
        else if(m_server.Data.Count != m_saveData.userRank)
        {
            m_rankText[5].text = "：";
            m_nameText[5].text = "：";
            m_scoreText[5].text = "：";
            for (int i = 6; i < m_rank.Count; i++)
            {
                if (i > (m_server.Data.Count - 1)) break;
                m_rankText[i].text = m_server.Data[m_saveData.userRank - (8 - i)].rank.ToString();
                m_nameText[i].text = m_server.Data[m_saveData.userRank - (8 - i)].name.ToString();
                m_scoreText[i].text = m_server.Data[m_saveData.userRank - (8 - i)].score.ToString() + "pt";

                if (i == 7)
                {
                    m_rankText[i].color = Color.red;
                    m_nameText[i].color = Color.red;
                    m_scoreText[i].color = Color.red;
                }
            }
        }
        else
        {
            m_rankText[5].text = "：";
            m_nameText[5].text = "：";
            m_scoreText[5].text = "：";
            for (int i = 6; i < m_rank.Count; i++)
            {
                if (i > (m_server.Data.Count - 1)) break;
                m_rankText[i].text = m_server.Data[m_saveData.userRank - (9 - i)].rank.ToString();
                m_nameText[i].text = m_server.Data[m_saveData.userRank - (9 - i)].name.ToString();
                m_scoreText[i].text = m_server.Data[m_saveData.userRank - (9 - i)].score.ToString() + "pt";

                if (i == 8)
                {
                    m_rankText[i].color = Color.red;
                    m_nameText[i].color = Color.red;
                    m_scoreText[i].color = Color.red;
                }
            }
        }

        m_server.IsGet = false;
    }
	
	// Update is called once per frame
	void Update () {
	}
}
