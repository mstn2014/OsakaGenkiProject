using UnityEngine;
using System.Collections;

public class Game3Balancer : MonoBehaviour {

    // private
    float m_waitLength;                      // 抽選までの時間の乱数の間隔
    float m_waitTime;                        // 抽選までの時間
    int m_changeDiffCount;                  // 難易度を変える成功回数
    int m_successCon;                       // 連続で成功した回数をカウント
    int m_appearNum;                        // 出現

	SoundMgr m_sound;          				// サウンド
    
    // public
    public ParticleSystem m_spdUpEff;       // スピードアップのエフェクト
    public ParticleSystem m_spdDwnEff;      // スピードダウンのエフェクト

    // public 
    public float CreateTime
    {
        get
        {
            return m_waitTime + Random.Range(0.0f, m_waitLength);
        }
    }

    public int AppearNum
    {
        get
        {
            return m_appearNum;
        }
    }
    
    public float Speed { set; get; }          // キャラクタの移動スピード
    public float DanceTime { set; get; }      // ダンスの時間
    public float Difficulty { set; get; }      // 難易度調整のための値

	// Use this for initialization
	void Start () {
	    m_waitLength = 1.0f;
        m_changeDiffCount = 3;
        m_successCon = 0;
        m_waitTime = 2.0f;

        Speed = 2.0f;
        DanceTime = 2.0f;
        Difficulty = 0.1f;
        m_appearNum = 0;

		// 共通設定の呼び出し
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_sound = gs.SoundMgr;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Success()
    {
        if (++m_successCon >= m_changeDiffCount)
        {
            UpDifficulty();
            m_successCon = 0;
            m_spdUpEff.Play();
			m_sound.PlaySePerfect();
        }
        m_appearNum++;
    }

    public void Miss()
    {
        DownDifficulty();
        m_successCon = 0;
        m_appearNum++;
        m_spdDwnEff.Play();
    }

    void UpDifficulty()
    {
        m_waitTime -= Difficulty;
        Speed -= Difficulty;
        DanceTime -= Difficulty;
        ClampValue();
    }

    void DownDifficulty()
    {
        m_waitTime += Difficulty;
        Speed += Difficulty;
        DanceTime += Difficulty;
        ClampValue();
    }

    void ClampValue()
    {
        m_waitTime = Mathf.Clamp(m_waitTime, 0.0f, 2.0f);
        Speed = Mathf.Clamp(Speed, 0.5f, 2.0f);
        DanceTime = Mathf.Clamp(DanceTime, 0.5f, 2.0f);
    }
}
