using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Game3MobController : MonoBehaviour {

    // アニメーション関連
    Animator m_animator;                // アニメーションクリップ
    float m_speed = 2.0f;               // キャラクターの移動スピード

    // 時間管理関連
    float m_nowTime = 0.0f;             // ステート管理用の時間変数
    float m_stayTime = 2.0f;            // ライトにとどまる時間(設定ファイルに書き出し予定)

    // キャラステート関連
    enum State { Forward, Light, Bye, Parade,DanceReady,Dance };
    State m_state;                      // キャラクタステート
    string[] charType = { "Yellow", "Blue", "Red", "Green" };

    // モブ制御オブジェクト
    Transform m_sayonaraPoint;          // キャラクタがさるときに向かうポイント
    Transform m_deletePoint;            // キャラクタを消すライン
    Transform m_light;                  // ライトのオブジェクト
    string m_lightName = string.Empty;  // ライトの名前
    Game3ObjMgr m_objMgr;               // オブジェマネージャーを取得
    Game3PositionList m_paradePos;      // パレードのポジション
    iTweenPath m_tweenPath;             // パレードの後ろに着くときのパス 
    public bool IsOK { set; get; }             // 押したボタンが正解ならフラグを立てる
    bool m_isPush = false;
    Game3MobController m_otherController;   // 逆サイドのスクリプト
    Game3Balancer m_balancer;
    Game3LightMgr m_lightMgr;           // ライト色変更用
    ScoreMgr m_scoreMgr;                // スコア
	SoundMgr m_sound;          			// サウンド
    NavMeshAgent m_navMesh;             // ナビゲーションメッシュ
    Vector3 m_targetPos;
    Transform m_paradeJoiner;

    UISprite m_waitSprite;              // 残り時間を表示するためのスプライト

    public Transform m_player;          // プレイヤー
    // 共通設定関連
    InputMgr m_input;                   // 入力をとる

    // アクセサ
    public bool IsSelected{set;get;}    // ライトに向かう時にtrueになる

    void SetOKFlg()
    {
        IsOK = true;
    }

    public Game3MobController OtherMobController
    {
        set { m_otherController = value; }
    }

    //======================================================
    // @brief:スタート（各種初期化設定)
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
	void Start() {
        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_input = gs.InputMgr;
		m_sound = gs.SoundMgr;

        // タグをランダムでつける
        this.gameObject.tag = charType[Random.Range(0, 4)];

        m_balancer = GetComponentInParent<Game3Balancer>();
        m_scoreMgr = GameObject.Find("ScoreMgr").GetComponent<ScoreMgr>();

        // モブ制御のためのオブジェクトを取得
        m_paradePos = GameObject.Find("PositionList").GetComponent<Game3PositionList>();
        m_animator = GetComponent<Animator>();
        m_sayonaraPoint = GameObject.Find("SayonaraPoint").transform;
        m_deletePoint = GameObject.Find("Delete_Position_Box").transform;
        m_objMgr = transform.parent.GetComponent<Game3ObjMgr>();
        m_state = State.Forward;
        m_tweenPath = GameObject.Find("Path1").GetComponent<iTweenPath>();
        m_waitSprite = GameObject.Find("RightWaitBar/WaitTime").GetComponent<UISprite>();
        m_navMesh = GetComponent<NavMeshAgent>();
        IsSelected = false;

        m_paradeJoiner = GameObject.Find("ParadeJoiner").transform;

        m_otherController = this;

        transform.parent = GameObject.Find("MoveObj").transform;
	}

    //======================================================
    // @brief:アップデート
    // ステートで挙動を分けている
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
	void Update () {
        switch(m_state){
            case State.Forward:
                // MoveForward();
                LookTarget(m_player);
                if (m_deletePoint.position.x >= this.transform.position.x)
                {
                    m_objMgr.CreateNewMob(this.gameObject);
                    Destroy(this.gameObject);
                }
                break;
            case State.Light:
                if (m_nowTime >= m_balancer.DanceTime)
                {
                    //MoveToTarget(m_sayonaraPoint, 2.0f);
                    Invoke("DestoryMyself", 2.0f);
                    GetComponent<NavMeshAgent>().SetDestination(m_sayonaraPoint.transform.position);
                    m_lightMgr.ChangeColor("White");
                    GameObject.Find(m_lightName.Replace("Light", "") + "WaitBar/WaitTime").GetComponent<UISprite>().fillAmount = 1.0f; 
                    m_balancer.Miss();
					m_sound.PlaySeMiss();
                    m_animator.SetTrigger("IsStand");
                    m_speed = 10.0f;
                    m_animator.SetFloat("speed", m_speed);
                    m_state = State.Bye;
                    break;
                }
                CheckHitLight(1.0f);
                break;
            case State.Parade:
                // iTweenによるパレード参加
                /*m_tweenPath.nodes[0] = m_light.position;
                m_tweenPath.nodes[m_tweenPath.nodeCount - 1] = m_paradePos.Pos;
                iTweenEvent.GetEvent(this.gameObject, "MoveToParade").Play();
                m_state = State.Dance;*/
                m_targetPos = m_paradePos.Pos;
                m_navMesh.SetDestination(m_targetPos);
                m_animator.SetTrigger("IsStand");
                m_animator.SetFloat("speed", 10);
                m_objMgr.CreateNewMob(this.gameObject);
                m_state = State.DanceReady;
                break;
            case State.DanceReady:
                if (Vector3.Distance(m_targetPos, transform.position) <= 0.5f)
                {
                    m_animator.SetFloat("speed", 0);
                    m_animator.SetTrigger("IsPose");
                    OnCompleteTweenPath();
                    m_state = State.Dance;
                }
                break;
        }
	}

    void LateUpdate()
    {
        switch (m_state)
        {
            case State.Forward:
                break;
            case State.Light:
                if (m_isPush && !IsOK )
                {
                    m_nowTime = m_balancer.DanceTime;
                }
                IsOK = false;
                break;
            case State.Parade:
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
        MoveToTarget(target, m_balancer.Speed);
        m_lightName = target.name;
        m_light = target;
        m_objMgr.SelectedMob = this;
        m_lightMgr = GameObject.Find(m_lightName + "_Obj").GetComponent<Game3LightMgr>();
    }

    //======================================================
    // @brief:iTweenの移動が終わると呼び出される関数
    //------------------------------------------------------
    // @author:K.Ito
    // @param:target:targetの方向を向く
    // @return:none
    //======================================================
    void MoveFinished()
    {
        switch (m_state)
        {
            case State.Light:
                m_speed = 0.0f;
                // アニメーターにスピードをセット
                m_animator.SetFloat("speed", m_speed);
                m_animator.SetTrigger("IsPose");
                LookTarget(GameObject.Find("Parade").transform);
                break;
            case State.Bye:
                //m_objMgr.CreateNewMob(this.gameObject);
                //Destroy(this.gameObject);
                break;
        }

    }

    void DestoryMyself()
    {
        m_objMgr.CreateNewMob(this.gameObject);
        Destroy(this.gameObject);
    }

    //======================================================
    // @brief:ライトオブジェクトとの当たり判定処理
    //------------------------------------------------------
    // @author:K.Ito
    // @param:th:この距離以下になるとボタンの判定をとる
    // @return:none
    //======================================================
    public bool CheckHitLight(float th)
    {
        float distance = Vector3.Distance(this.transform.position, m_light.transform.position);
        m_isPush = false;
        if (distance <= th)
        {
            m_nowTime += Time.deltaTime;
            GameObject.Find(m_lightName.Replace("Light","") + "WaitBar/WaitTime").GetComponent<UISprite>().fillAmount = 1.0f - m_nowTime / m_balancer.DanceTime; 
            //m_waitSprite.fillAmount = 1.0f - m_nowTime / m_balancer.DanceTime;
            // ここにボタンの成否処理を書く
            List<string> pushButton = new List<string>();

            // とりあえず同時押しに対応しておく
            if (m_input.YellowButtonTrigger)
            {
                pushButton.Add(charType[0]);
            }
            if (m_input.BlueButtonTrigger)
            {
                pushButton.Add(charType[1]);
            }
            if (m_input.RedButtonTrigger)
            {
                pushButton.Add(charType[2]);
            }
            if (m_input.GreenButtonTrigger)
            {
                pushButton.Add(charType[3]);
            }

            // ボタンを押したらフラグを立てる
            if (pushButton.Count >= 1) m_isPush = true;

            // 押したボタンがタグと一致すればパレードにモブを参加させる
            if (pushButton.Contains(this.gameObject.tag))
            {
                GameObject.Find(m_lightName.Replace("Light", "") + "WaitBar/WaitTime").GetComponent<UISprite>().fillAmount = 1.0f;
                m_scoreMgr.AddScore(1.0f);
                m_state = State.Parade;
                m_speed = 0.0f;
                m_animator.SetFloat("speed", m_speed);
                m_otherController.IsOK = true;
                m_balancer.Success();
                m_lightMgr.ChangeColor("White");
				m_sound.PlaySeSpotcuccess();
                transform.parent = m_paradeJoiner;
                /*Hashtable param = new Hashtable(){
                    {"y",1},
                    {"time",0.1f},
                    {"looptype",iTween.LoopType.loop},
                    {"easetype",iTween.EaseType.linear},
                    {"name",this.gameObject.name},
                };
                iTween.RotateBy(this.gameObject, param);*/
                return true;
            }

            m_lightMgr.ChangeColor(this.tag);
        }
        return false;
    }

    //======================================================
    // @brief:パレードに向かうiTweenPathの処理が終わると呼び出される関数
    //------------------------------------------------------
    // @author:K.Ito
    // @param:th:この距離以下になるとボタンの判定をとる
    // @return:none
    //======================================================
    void OnCompleteTweenPath()
    {
        m_animator.SetInteger("DanceType",Random.Range(0,5));
        LookTarget(GameObject.Find("Game3Player").transform);
        
    }
}
