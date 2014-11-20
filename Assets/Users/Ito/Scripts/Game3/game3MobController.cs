using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class game3MobController : MonoBehaviour {

    Animator m_animator;                    // アニメーションクリップ
    float m_speed = 2.0f;             // キャラクターの移動スピード

    float m_nowTime = 0.0f;
    float m_stayTime = 2.0f;

    enum State { Forward, Light, Bye, Parade,Dance };
    State m_state;                      // キャラクラステート

    Transform m_sayonaraPoint;
    Transform m_deletePoint;
    Transform m_light;

    string m_lightName = string.Empty;

    Game3ObjMgr m_objMgr;               // オブジェマネージャーを取得
    Game3PositionList m_paradePos;      // パレードのポジション

    iTweenPath m_tweenPath;             // パレードの後ろに着くときのパス 

    InputMgr m_input;

    // アクセサ
    public bool IsSelected{set;get;}               // ライトに向かう時にtrueになる

	// Use this for initialization
	void Start () {
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_paradePos = GameObject.Find("PositionList").GetComponent<Game3PositionList>();
        m_input = gs.InputMgr;
        m_animator = GetComponent<Animator>();
        m_sayonaraPoint = GameObject.Find("SayonaraPoint").transform;
        m_deletePoint = GameObject.Find("Delete_Position_Box").transform;
        m_objMgr = transform.parent.GetComponent<Game3ObjMgr>();
        m_state = State.Forward;
        m_tweenPath = GameObject.Find("Path1").GetComponent<iTweenPath>(); 
        IsSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
        switch(m_state){
            case State.Forward:
                MoveForward();
                break;
            case State.Light:
                if (m_nowTime >= m_stayTime)
                {
                    MoveToTarget(m_sayonaraPoint, 2.0f);
                    m_state = State.Bye;
                }
                break;
            case State.Parade:
                m_tweenPath.nodes[0] = m_light.position;
                m_tweenPath.nodes[m_tweenPath.nodeCount - 1] = m_paradePos.Pos;
                iTweenEvent.GetEvent(this.gameObject, "MoveToParade").Play();
                m_state = State.Dance;
                break;
        }
	}

    //======================================================
    // @brief:キャラクターの向いている方向に進む
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void MoveForward()
    {
        // 前方に移動(可変フレームなのでTime.deltaTimeをかける)
        transform.Translate(m_speed * Vector3.forward * Time.deltaTime);
        // アニメーターにスピードをセット
        m_animator.SetFloat("speed", m_speed);

        if (m_deletePoint.position.x >= this.transform.position.x)
        {
            m_objMgr.CreateNewMob(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    //======================================================
    // @brief:キャラクタの向きを変える
    //------------------------------------------------------
    // @author:K.Ito
    // @param:target:targetの方向を向く
    // @return:none
    //======================================================
    void LookTarget(Transform target)
    { 
        // ターゲットの方向を向く
        transform.rotation = Quaternion.LookRotation(target.position - transform.position); 
    }

    //======================================================
    // @brief:ターゲットに向かう
    //------------------------------------------------------
    // @author:K.Ito
    // @param:target:targetの方向を向く
    // @return:none
    //======================================================
    public void MoveToTarget(Transform target,float duration)
    {
        // ターゲットの方向を向く
        LookTarget(target);
        // 決められた秒数でターゲットに向かう
        Hashtable parameters = new Hashtable(){
            {"x",target.position.x},
            {"y",target.position.y},
            {"z",target.position.z},
            {"islocal",false},
            {"easetype",iTween.EaseType.linear},
            {"time",duration},
            {"oncomplete", "MoveFinished"},
        };
        iTween.MoveTo(this.gameObject, parameters);

        float distance = Vector3.Distance(target.position, transform.position);

        m_speed = distance / duration;
        // アニメーターにスピードをセット
        m_animator.SetFloat("speed", m_speed);
    }

    void MoveFinished()
    {
        switch (m_state)
        {
            case State.Light:
                m_speed = 0.0f;
                // アニメーターにスピードをセット
                m_animator.SetFloat("speed", m_speed);
                LookTarget(GameObject.Find("Parade").transform);
                break;
            case State.Bye:
                m_objMgr.CreateNewMob(this.gameObject);
                Destroy(this.gameObject);
                break;
        }

    }

    //======================================================
    // @brief:ライトに向かう
    //------------------------------------------------------
    // @author:K.Ito
    // @param:target:targetの方向を向く
    // @return:none
    //======================================================
    public void MoveToLight(Transform target)
    {
        if (m_state == State.Light) return;

        IsSelected = true;
        m_state = State.Light;
        MoveToTarget(target, 2.0f);
        m_lightName = target.name;
        m_light = target;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.transform.name == m_lightName)
        {
            m_nowTime += Time.deltaTime;
            
            if (m_input.YellowButtonTrigger)
            {
                m_state = State.Parade;
                m_speed = 0.0f;
                // アニメーターにスピードをセット
                m_animator.SetFloat("speed", m_speed);
            }
        }
    }

    void OnCompleteTweenPath()
    {
        LookTarget(GameObject.Find("Parade").transform);
        m_objMgr.CreateNewMob(this.gameObject);
    }
}
