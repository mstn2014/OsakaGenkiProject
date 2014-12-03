using UnityEngine;
using System.Collections;

public class ScoreMgr : MonoBehaviour {
    
    float m_score;      // スコア

    public float Score
    {
        get { return m_score; }
    }
    
	// Use this for initialization
	void Start () {
        m_score = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddScore(float score)
    {
        m_score += score;
    }

    public void SubScore(float score)
    {
        m_score -= score;
        if (m_score < 0) m_score = 0.0f;
    }

    public void Reset()
    {
        m_score = 0.0f;
    }
}
