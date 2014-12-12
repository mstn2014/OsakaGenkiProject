using UnityEngine;
using System.Collections;

public class Game2ModelMotion : MonoBehaviour {

    Animator m_animator;    // モーションコントローラー
    public enum DanceType { STAND = 0, BONODORI, HULA, BELLY, SAMBA, FLAMENCO,POSE,RUN };
    DanceType m_danceState;
    FaceMgr m_face;
	// Use this for initialization
	void Awake () {
        m_animator = GetComponent<Animator>();
        m_danceState = DanceType.STAND;
        m_face = GetComponent<FaceMgr>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeMotion(DanceType type)
    {
        m_danceState = type;
        m_animator.SetInteger("DanceType", (int)m_danceState);
    }
}
