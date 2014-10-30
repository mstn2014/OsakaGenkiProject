using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingBoard : MonoBehaviour {

    ConnectServer m_server;         // サーバーとの通信をするクラス
    List<UILabel> m_rankText;       // 順位を表示する
    List<UILabel> m_nameText;       // 名前を表示する
    List<UILabel> m_scoreText;      //　スコアを表示する 

    [SerializeField]
    [Header("ランキング設定ファイル")]
    RankingSetting m_rankSetting;
    [SerializeField]
    [Header("ランキングを表示するオブジェクト")]
    List<GameObject> m_rank;

	// Use this for initialization
	void Start () {
        // サーバーからランキングデータの取得
        m_server = GetComponent<ConnectServer>();
        m_server.getRanking();



        // ランキングオブジェクトからUILabelを取り出しておく
        foreach (GameObject go in m_rank)
        {
            m_rankText.Add(go.transform.FindChild("Rank").GetComponent<UILabel>());
            m_nameText.Add(go.transform.FindChild("Name").GetComponent<UILabel>());
            m_scoreText.Add(go.transform.FindChild("Score").GetComponent<UILabel>());
        }

        // 取得したデータを表示する
        for (int i = 0; i < m_rank.Count; i++)
        {
            m_rankText[i].text = m_server.Data[i].rank.ToString();
            m_nameText[i].text = m_server.Data[i].name.ToString();
            m_scoreText[i].text = m_server.Data[i].score.ToString();
        }

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
