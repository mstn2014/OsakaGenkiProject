using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using WWWKit;
using MiniJSON;

//========================================================================
// ランキングサーバーと通信するクラス
//========================================================================
public class ConnectServer : MonoBehaviour {
    WWWClientManager cm;        // WWWラッパークラス

    public class RankingData    // ランキングデータを格納するクラス
    {
        public int rank { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int score { get; set; }

        // コンストラクタ
        public RankingData(int _rank, int _id, string _name, int _score)
        {
            rank = _rank;
            id = _id;
            name = _name;
            score = _score;
        }
    }

    List<RankingData> m_data;      // ランキングデータの実体

    [SerializeField]
    [Header("ランキング設定ファイル")]
    RankingSetting m_rankSetteing;

    // get setアクセサ
    public List<RankingData> Data
    {
        get { return m_data; }
    }

	// Use this for initialization
	void Start () {
        cm = new WWWClientManager(this);
        m_data = new List<RankingData>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    //======================================================
    // @brief:ランキングサーバーにユーザーを登録する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:name ユーザー名
    // @param:score ユーザーの獲得スコア
    // @return:none
    //======================================================
    public void postRanking(string name,int score)
    {
        // ポストするキーはstring型で送る
        Dictionary<string, string> post = new Dictionary<string, string>();
        post.Add("name", name);
        post.Add("score", score.ToString());
        cm.POST(m_rankSetteing.postURL, post, "ReceivePost");
    }

    void ReceivePost(WWW www)
    {
        Debug.Log(www.text);
    }

    //======================================================
    // @brief:ランキングサーバーからランキングを取得する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:name ユーザー名
    // @param:score ユーザーの獲得スコア
    // @return:none
    //======================================================
    public void getRanking()
    {
        cm.GET(m_rankSetteing.getURL, "ReceiveGet","ReceiveGetError");
    }

    void ReceiveGet(WWW www)
    {
        string json = www.text;
        var scoreInfo = Json.Deserialize(json) as Dictionary<string, object>;
        int i = 1;
        foreach (object ob in scoreInfo)
        {
            Dictionary<string, object> num = (Dictionary<string, object>)scoreInfo[i.ToString()];
            long id = (long)num["id"];
            string name = (string)num["name"];
            long score = (long)num["score"];

            m_data.Add(new RankingData((int)i, (int)id, name, (int)score));

            i++;
        }
    }

    void ReceiveGetError()
    {
        Debug.Log("ランキングの取得に失敗しました。");
    }
}
