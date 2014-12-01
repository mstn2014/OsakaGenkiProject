using UnityEngine;
using System.Collections;

public class Game3Balancer : MonoBehaviour {

    // private
    float m_waitLength;                      // 抽選までの時間の乱数の間隔
    float m_waitRandom;                      // 乱数を保持
    float m_waitTime;                        // 抽選までの時間
    int m_changeDiffCount;                  // 難易度を変える成功回数
    int m_successCon;                       // 連続で成功した回数をカウント

    // public 
    public float CreateTime
    {
        get
        {
            return m_waitTime + Random.Range(0.0f, m_waitLength);
        }
    }
    
    public float Speed { set; get; }          // キャラクタの移動スピード
    public float DanceTime { set; get; }      // ダンスの時間
    public float Difficulty { set; get; }      // 難易度調整のための値

	// Use this for initialization
	void Start () {
	    m_waitLength = 2.0f;
        m_changeDiffCount = 3;
        m_successCon = 0;
        m_waitTime = 5.0f;

        Speed = 2.0f;
        DanceTime = 3.0f;
        Difficulty = 0.5f;
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
        }
    }

    public void Miss()
    {
        DownDifficulty();
        m_successCon = 0;
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
        m_waitTime = Mathf.Clamp(m_waitTime, 0.0f, 5.0f);
        Speed = Mathf.Clamp(Speed, 0.5f, 2.0f);
        DanceTime = Mathf.Clamp(DanceTime, 0.1f, 3.0f);
    }
}
