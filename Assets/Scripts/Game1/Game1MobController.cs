using UnityEngine;
using System.Collections;

public class Game1MobController : MonoBehaviour {

    Animator m_animator;        // モブのアニメーター
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
        if (angle > 180)
        {
            angle = 180 - angle;
        }
        Hashtable parameters = new Hashtable(){
            {"y",angle},
            {"time",0.5f},
            {"easetype",iTween.EaseType.linear}
        };
        iTween.RotateTo(this.gameObject, parameters);
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
