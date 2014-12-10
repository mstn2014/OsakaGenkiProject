using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WWWKit;
using MiniJSON;

public class ResultMgr : MonoBehaviour {

    // コンポーネント関連
    SaveMgr m_save;            // 一時セーブデータ
    InputMgr m_inputMgr;        // インプット
    FadeMgr m_fadeMgr;          // フェード
	SoundMgr m_sound;          	// サウンド
    WWWClientManager m_rankServer;  // ランキングサーバー
    
    // ローカル変数
    SaveMgr.eState m_state;    // どのシーンから飛んできたか。どのシーンの結果を返すか判断するフラグ。
    float m_score;              // ゲームのスコア
    float m_maxPoint;           // そのゲームの最大スコア
    ResultSetting.CRank[] m_rank;   // ランクが定義された定数
    ResultSetting m_setting; // 設定ファイル
    bool m_isCompleteEffect;    // 演出が終わったらtrue
    int m_effectMax;            // 演出を行う回数
    int m_effectCnt;            // 演出を行った回数
    Dictionary<UILabel,float> m_pointLabel = new Dictionary<UILabel,float>();   // ポイントを表示するラベル
    List<UISprite> m_effectList = new List<UISprite>(); // エフェクトを追加するオブジェクトのリスト
    public List<UISprite> m_gaugeList = new List<UISprite>();
    float m_cnt;                // ポイントのカウントアップ変数
    int m_depth;                // スプライトのデプス

    // public
    public GameObject m_one;    // 一つだけのゲームの結果を表示するときに使う
    public GameObject m_all;    // 総合得点を表示する
    public UISprite m_gauge;
    public ResultPercent m_percent;
    public List<ResultPercent> m_percentList = new List<ResultPercent>();
    public UISprite m_name;
    public enum eType { OneTime, Sequence };
    public eType m_dispType;

    void Awake()
    {
        SetNoActive(m_one);
        SetNoActive(m_all);
    }

	// Use this for initialization
	void Start () {
        m_rankServer = new WWWClientManager(this);
        m_setting = Resources.Load<ResultSetting>("Setting/ResultSetting");

        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_inputMgr = gs.InputMgr;
        m_fadeMgr = gs.FadeMgr;
		m_sound = gs.SoundMgr;
        m_save = gs.SaveMgr;
        m_state = m_save.gameState;
		m_sound.PlayResult();

        // 一つバージョンか総合得点バージョンかどちらかのフラグを立てる
        if (m_state == SaveMgr.eState.ALL)
        {
            m_all.SetActive(true);
        }
        else if (!m_all.activeInHierarchy)
        {
            m_one.SetActive(true);
        }

        // 一つバージョンと総合得点バージョンの処理を書く
        if (m_one.activeInHierarchy)
        {
            GameObject.Find("Rank").GetComponent<UISprite>().spriteName = CalcRank(m_state);
            m_pointLabel.Add(GameObject.Find("Point").GetComponent<UILabel>(),m_score / m_maxPoint *100 );
        }
        else
        {
            // ゲーム１
            GameObject.Find("Game1/Rank").GetComponent<UISprite>().spriteName = CalcRank(SaveMgr.eState.GAME1);
            m_pointLabel.Add(GameObject.Find("Game1/Point").GetComponent<UILabel>(), m_score / m_setting.game1MaxPoint * 100);

            // ゲーム2
            GameObject.Find("Game2/Rank").GetComponent<UISprite>().spriteName = CalcRank(SaveMgr.eState.GAME2);
            m_pointLabel.Add(GameObject.Find("Game2/Point").GetComponent<UILabel>(), m_score / m_setting.game2MaxPoint * 100);
            
            // ゲーム3
            GameObject.Find("Game3/Rank").GetComponent<UISprite>().spriteName = CalcRank(SaveMgr.eState.GAME3);
            m_pointLabel.Add(GameObject.Find("Game3/Point").GetComponent<UILabel>(), m_score / m_save.game3Max * 100);
            
            // 総合得点
            GameObject.Find("All/Rank").GetComponent<UISprite>().spriteName = CalcRank(SaveMgr.eState.ALL);
            m_pointLabel.Add(GameObject.Find("All/Point").GetComponent<UILabel>(), m_score / (m_setting.game1MaxPoint+m_setting.game2MaxPoint+m_save.game3Max) * 100);
        }


        // ローカル変数の初期化
        m_isCompleteEffect = false;
        m_depth = -5;
        m_effectMax = m_pointLabel.Count;
        m_effectCnt = 0;

        StartCoroutine(CountUp());
	}
	
	// Update is called once per frame
	void Update () {
        // 赤ボタンを押した & 演出がすべて終了したら
        if (m_inputMgr.RedButtonTrigger && m_isCompleteEffect )
        {
            switch (m_save.gameState)
            {
                case SaveMgr.eState.GAME1:
                    m_fadeMgr.LoadLevel("game2");
                    break;
                case SaveMgr.eState.GAME2:
                    m_fadeMgr.LoadLevel("game3");
                    break;
                case SaveMgr.eState.GAME3:
                    m_fadeMgr.LoadLevel("result");
                    m_save.gameState = SaveMgr.eState.ALL;
                    break;
                case SaveMgr.eState.ALL:
                    m_fadeMgr.LoadLevel("bigIvent2");
                    break;
            }
        }

        
	}

    IEnumerator CountUp()
    {
        // フェード中は更新しない
        while (m_fadeMgr.IsFading())
        {
            yield return null;
        }

        while (m_pointLabel.Count != 0)
        {
            List<UILabel> removeLabel = new List<UILabel>();
            // スコアの更新
            //------------------------------------------------
            // 全部一気に更新するバージョン
            //------------------------------------------------
            if (m_dispType == eType.OneTime)
            {
                foreach (var point in m_pointLabel)
                {
                    point.Key.transform.parent.FindChild("Point").GetComponent<TweenScale>().enabled = true;
                    // カウントアップ
                    if (m_cnt <= point.Value)
                    {
                        point.Key.text = m_cnt.ToString();
                    }
                    // カウンターが過ぎたらリストから外す
                    else if (m_cnt > point.Value)
                    {
                        point.Key.text = point.Value.ToString();
                        removeLabel.Add(point.Key);
                    }
                }
            }
            //-----------------------------------------------
            // 全部一気に更新するバージョンここまで
            //-----------------------------------------------

            //-----------------------------------------------
            // 上から順番に更新するバージョン
            //-----------------------------------------------
            else if (m_dispType == eType.Sequence)
            {
                foreach (var point in m_pointLabel)
                {
                    point.Key.transform.parent.FindChild("Point").GetComponent<TweenScale>().enabled = true;
                    // カウントアップ
                    if (m_cnt <= point.Value)
                    {
                        point.Key.text = m_cnt.ToString();
                        m_gauge.fillAmount = m_cnt / 100;
                        m_gaugeList[0].fillAmount = m_cnt / 100;
                        m_percentList[0].SetNum(m_cnt);
                        m_percent.SetNum(m_cnt);
                    }
                    // カウンターが過ぎたらリストから外す
                    else if (m_cnt > point.Value)
                    {
                        point.Key.text = point.Value.ToString("0.0");
                        m_gauge.fillAmount = point.Value / 100;
                        m_gaugeList[0].fillAmount = point.Value / 100;
                        m_percent.SetNum(point.Value);
                        m_percentList[0].SetNum(point.Value);
                        m_percentList.RemoveAt(0);
                        m_gaugeList.RemoveAt(0);
                        removeLabel.Add(point.Key);
                        if (m_depth == -2)
                        {
                            postMessage(point.Value);
                        }
                        m_cnt = 0;
                    }
                    
                    break;
                }
            }

            //-----------------------------------------------
            // 上から順番に更新するバージョンここまで
            //-----------------------------------------------

            foreach(var remove in removeLabel){
                m_pointLabel.Remove(remove);
                Transform temp = remove.transform.parent.FindChild("Rank"); 
                temp.GetComponent<TweenScale>().enabled = true;
                temp.GetComponent<TweenRotation>().enabled = true;
                temp.GetComponent<UISprite>().depth = m_depth;
                m_effectList.Add(temp.GetComponent<UISprite>());
                m_depth++;
            }

            m_cnt += 1.0f;
            yield return null;
        }
    }

    string CalcRank(SaveMgr.eState state)
    {
        switch (state)
        {
            case SaveMgr.eState.GAME1:
                m_score = m_save.game1Score;
                m_maxPoint = m_setting.game1MaxPoint;
                m_rank = m_setting.Rank;
                m_name.spriteName = "result_game1";
                break;
            case SaveMgr.eState.GAME2:
                m_score = m_save.game2Score;
                m_maxPoint = m_setting.game2MaxPoint;
                m_rank = m_setting.Rank;
                m_name.spriteName = "result_game2";
                break;
            case SaveMgr.eState.GAME3:
                m_score = m_save.game3Score;
                m_maxPoint = m_save.game3Max;
                m_rank = m_setting.Rank;
                m_name.spriteName = "result_game3";
                break;
            case SaveMgr.eState.ALL:
                m_score = m_save.game1Score + m_save.game2Score + m_save.game3Score;
                m_maxPoint = m_setting.game1MaxPoint + m_setting.game2MaxPoint + m_save.game3Max;
                m_rank = m_setting.Rank;
                break;
        }

        float point = m_score / m_maxPoint;
        foreach (ResultSetting.CRank rank in m_rank)
        {
            if (point >= rank.value)
            {
                return "Rank" + rank.key;
            }
        }
        return string.Empty;
    }

    void CompleteEffect()
    {
        // 演出がすべて終わったら次のシーンへの遷移を許す
        if (++m_effectCnt == m_effectMax)
        {
            m_isCompleteEffect = true;
        }
        m_effectList[0].transform.FindChild("Eff_Star_1").gameObject.SetActive(true);
        Transform tmpPoint = m_effectList[0].transform.parent.FindChild("Point");
        tmpPoint.GetComponent<TweenScale>().enabled = false;
        if (m_effectMax == 4)
        {
            iTween.ScaleTo(tmpPoint.gameObject, new Vector3(96.0f, 96.0f, 0.0f), 1.0f);
        }
        else
        {
            iTween.ScaleTo(tmpPoint.gameObject, new Vector3(128.0f, 128.0f, 0.0f), 1.0f);
        }

        m_effectList.RemoveAt(0);
    }

    void SetNoActive(GameObject go)
    {
        if (go.activeInHierarchy)
        {
            go.SetActive(false);
        }
    }

    void postMessage(float _score)
    {
        string url = "http://mstn2014-osaka.herokuapp.com/users/regist";
        // ポストするキーはstring型で送る
        Dictionary<string, string> post = new Dictionary<string, string>();

        // ランダムな名前を生成
        string name = m_save.userName;

        post.Add("name", name);
        float score = _score;
        post.Add("score", score.ToString());
        post.Add("costume", Random.Range(0, 5).ToString());
        m_rankServer.POST(url, post, "ReceivePost");
        Debug.Log(name.ToString() + " " + score.ToString() + " " + "POSTリクエストを送信しました。");
    }

    void ReceivePost(WWW www)
    {
        Debug.Log(www.text);
    }
}
