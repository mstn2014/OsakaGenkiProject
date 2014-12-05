using UnityEngine;
using System.Collections;

public class Game3MoveObj : MonoBehaviour {

    bool m_isMove;
    public bool IsMove
    {
        set
        {
            if (!m_isMove)
            {
                iTweenEvent.GetEvent(gameObject, "MapMove").Play();
                m_isMove = true;
            }
        }
        get
        {
            return m_isMove;
        }
    }
	// Use this for initialization
	void Start () {
        m_isMove = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
