using UnityEngine;
using System.Collections;

public class ScoreMgr : MonoBehaviour {
    
    float m_score;      // スコア
    UILabel m_string;   // スコアの表示

    public float Score
    {
        get { return m_score; }
    }
    
	// Use this for initialization
	void Start () {
        m_string = GetComponent<UILabel>();
        m_score = 0.0f;
        SetText();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetText()
    {
        m_string.text = ((int)m_score).ToString();
    }

    public void AddScore(float score)
    {
        m_score += score;
        SetText();
    }

    public void SubScore(float score)
    {
        m_score -= score;
        if (m_score < 0) m_score = 0.0f;
        SetText();
    }

    public void Reset()
    {
        m_score = 0.0f;
    }
}
