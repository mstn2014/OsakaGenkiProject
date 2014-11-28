using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game3PositionList : MonoBehaviour {

    List<Vector3> m_paradePos = new List<Vector3>();
    Vector3 m_deletePos;

    public bool hideChild = false;
	// Use this for initialization
    void Awake()
    {
        // 子オブジェクトを非表示にする
        Renderer[] ren = GetComponentsInChildren<MeshRenderer>();
        foreach (Renderer r in ren)
        {
            r.enabled = false;
        }
    }

	void Start () {
        for (int i = 0; i < 50; i++)
        {
            m_paradePos.Add(GameObject.Find("Position_" + i.ToString()).transform.position);
        }
        m_deletePos = GameObject.Find("Delete_Position_Box").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 Pos
    {
        get
        {
            if (m_paradePos.Count == 0) return m_deletePos;

            Vector3 workVec = m_paradePos[0];
            m_paradePos.RemoveAt(0);
            return workVec;
        }
    }

    void OnValidate()
    {
        // 子オブジェクトを非表示にする
        Renderer[] ren = GetComponentsInChildren<MeshRenderer>();
        foreach (Renderer r in ren)
        {
            r.enabled = !hideChild;
        }
    }
}
