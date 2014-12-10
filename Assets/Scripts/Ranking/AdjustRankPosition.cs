using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AdjustRankPosition : MonoBehaviour
{
    public float m_pitch;       // 縦のピッチサイズs

    Transform[] m_children;       // 子のオブジェクト    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnValidate()
    {
        m_children = GetComponentsInChildren<Transform>();
        List<Transform> m_rank = new List<Transform>();
        foreach (Transform t in m_children)
        {
            if( t.name.Contains("1") 
                || t.name.Contains("2")
                || t.name.Contains("3")
                || t.name.Contains("4")
                || t.name.Contains("5")
                || t.name.Contains("6")
                || t.name.Contains("7")
                || t.name.Contains("8")
                || t.name.Contains("9"))
            {
                m_rank.Add(t);
            }
        }

        for(int i=1;i<m_rank.Count;i++)
        {
            m_rank[i].localPosition = new Vector3( m_rank[i].localPosition.x,  m_rank[i - 1].localPosition.y - m_pitch ,m_rank[i].localPosition.x );
        }
    }
}
