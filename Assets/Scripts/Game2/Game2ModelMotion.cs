using UnityEngine;
using System.Collections;

public class Game2ModelMotion : MonoBehaviour {

    Animator m_animator;    // モーションコントローラー
    public enum DanceType { STAND = 0, BONODORI, HULA, BELLY, SAMBA, FLAMENCO,POSE,RUN };
    DanceType m_danceState;
	// Use this for initialization
	void Awake () {
        m_animator = GetComponent<Animator>();
        m_danceState = DanceType.STAND;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeMotion(DanceType type)
    {
        m_danceState = type;
        m_animator.SetInteger("DanceType", (int)m_danceState);
        GetComponent<FaceMgr>().ChangeFace(FaceMgr.eFaceType.SMILE);
    }
}
