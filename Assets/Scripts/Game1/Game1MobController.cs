using UnityEngine;
using System.Collections;

public class Game1MobController : MonoBehaviour {

    Animator m_animator;        // モブのアニメーター
    public GameObject m_excl;          // !マークのエフェクト
    public Transform m_player;  // プレイヤーの

	// Use this for initialization
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        transform.forward = Vector3.back;
    }

	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LookTarget(Transform target)
    {
        Vector3 workVec = target.transform.position - this.transform.position;
        // 一気に向くやつ
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 5.0f);

        // iTweenで徐々に向くやつ
        float angle = Vector3.Angle(transform.forward,workVec);
        Hashtable parameters = new Hashtable(){
            {"y",angle},
            {"time",0.5f},
            {"islocal",false},
            {"easetype",iTween.EaseType.linear}
        };
        //iTween.RotateTo(this.gameObject, parameters);
        workVec.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(workVec);

        // !マークエフェクト
        m_excl.SetActive(true);
    }

    public void LookPlayer()
    {
        LookTarget(m_player);
    }

    public void DoPose()
    {
        m_animator.SetBool("IsPose", true);
    }
}
