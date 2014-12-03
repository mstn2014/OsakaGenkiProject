using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultMgr : MonoBehaviour {

    // コンポーネント関連
    SaveData m_save;            // 一時セーブデータ
    InputMgr m_inputMgr;        // インプット
    FadeMgr m_fadeMgr;          // フェード
	SoundMgr m_sound;          	// サウンド
    
    // ローカル変数
    SaveData.eState m_state;    // どのシーンから飛んできたか。どのシーンの結果を返すか判断するフラグ。
    float m_score;              // ゲームのスコア
    float m_maxPoint;           // そのゲームの最大スコア
    ResultSetting.CRank[] m_rank;   // ランクが定義された定数
    ResultSetting m_setting; // 設定ファイル
    bool m_isCompleteEffect;    // 演出が終わったらtrue
    Dictionary<UILabel,float> m_pointLabel = new Dictionary<UILabel,float>();   // ポイントを表示するラベル
    float m_cnt;                // ポイントのカウントアップ変数

    // public
    public GameObject m_one;    // 一つだけのゲームの結果を表示するときに使う
    public GameObject m_all;    // 総合得点を表示する

    void Awake()
    {
        SetNoActive(m_one);
        SetNoActive(m_all);
    }

	// Use this for initialization
	void Start () {
        m_save = Resources.Load<SaveData>("SaveData/SaveData");
        m_state = m_save.gameState;
        m_setting = Resources.Load<ResultSetting>("Setting/ResultSetting");

        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_inputMgr = gs.InputMgr;
        m_fadeMgr = gs.FadeMgr;
		m_sound = gs.SoundMgr;
		m_sound.PlayResult();

        // 一つバージョンか総合得点バージョンかどちらかのフラグを立てる
        if (m_state == SaveData.eState.ALL)
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
            m_pointLabel.Add(GameObject.Find("Point").GetComponent<UILabel>(),m_score);
        }
        else
        {
            // ゲーム１
            GameObject.Find("Game1/Rank").GetComponent<UISprite>().spriteName = CalcRank(SaveData.eState.GAME1);
            m_pointLabel.Add(GameObject.Find("Game1/Point").GetComponent<UILabel>(), m_score); ;

            // ゲーム2
            GameObject.Find("Game2/Rank").GetComponent<UISprite>().spriteName = CalcRank(SaveData.eState.GAME2);
            m_pointLabel.Add(GameObject.Find("Game2/Point").GetComponent<UILabel>(), m_score);
            
            // ゲーム3
            GameObject.Find("Game3/Rank").GetComponent<UISprite>().spriteName = CalcRank(SaveData.eState.GAME3);
            m_pointLabel.Add(GameObject.Find("Game3/Point").GetComponent<UILabel>(), m_score);
            
            // 総合得点
            GameObject.Find("All/Rank").GetComponent<UISprite>().spriteName = CalcRank(SaveData.eState.ALL);
            m_pointLabel.Add(GameObject.Find("All/Point").GetComponent<UILabel>(), m_score);
        }

        // ローカル変数の初期化
        m_isCompleteEffect = false;

        StartCoroutine(CountUp());
	}
	
	// Update is called once per frame
	void Update () {
        // 赤ボタンを押した & 演出がすべて終了したら
        if (m_inputMgr.RedButtonTrigger && m_isCompleteEffect )
        {
            switch (m_save.gameState)
            {
                case SaveData.eState.GAME1:
                    m_fadeMgr.LoadLevel("game2");
                    break;
                case SaveData.eState.GAME2:
                    m_fadeMgr.LoadLevel("game3");
                    break;
                case SaveData.eState.GAME3:
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
            foreach (var point in m_pointLabel)
            {
                // カウントアップ
                if (m_cnt <= point.Value)
                {
                    point.Key.text = m_cnt.ToString() + "pt";
                }
                // カウンターが過ぎたらリストから外す
                else if (m_cnt > point.Value)
                {
                    removeLabel.Add(point.Key);
                }
            }

            foreach(var remove in removeLabel){
                m_pointLabel.Remove(remove);
                // ランクの演出開始
                remove.transform.parent.FindChild("Rank").GetComponent<TweenScale>().enabled = true;
            }

            m_cnt++;
            yield return null;
        }
    }

    string CalcRank(SaveData.eState state)
    {
        switch (state)
        {
            case SaveData.eState.GAME1:
                m_score = m_save.game1Score;
                m_maxPoint = m_setting.game1MaxPoint;
                m_rank = m_setting.game1Rank;
                break;
            case SaveData.eState.GAME2:
                m_score = m_save.game2Score;
                m_maxPoint = m_setting.game2MaxPoint;
                m_rank = m_setting.game2Rank;
                break;
            case SaveData.eState.GAME3:
                m_score = m_save.game3Score;
                m_maxPoint = m_setting.game3MaxPoint;
                m_rank = m_setting.game3Rank;
                break;
            case SaveData.eState.ALL:
                m_score = m_save.game1Score + m_save.game2Score + m_save.game3Score;
                m_maxPoint = m_setting.game1MaxPoint + m_setting.game2MaxPoint + m_setting.game3MaxPoint;
                m_rank = m_setting.game3Rank;
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
        m_isCompleteEffect = true;
    }

    void SetNoActive(GameObject go)
    {
        if (go.activeInHierarchy)
        {
            go.SetActive(false);
        }
    }
}
