using UnityEngine;
using System.Collections;

public class Game2MobController : MonoBehaviour {
    // プライベート変数
    int m_no;                   // ゲストNo.
    Transform m_Player;         // プレイヤーの位置
    Game2ModelMotion m_motion;  // モーションの管理クラス
    Game2CreateButton m_buttonMgr;   // ボタンのマネージャー

	// Use this for initialization
	void Awake () {
        m_Player = GameObject.Find("Game2Player").transform;
        m_motion = this.GetComponent<Game2ModelMotion>();
        m_buttonMgr = GameObject.Find("ButtonMgr").GetComponent<Game2CreateButton>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void JoinDance(int mobNo,Vector3 from,Vector3 to)
    {
        // モブNoをセット
        m_no = mobNo;

        // 初期座標をセット
        transform.position = from;

        // 進行方向を向く
        transform.rotation = Quaternion.LookRotation(to - from);

        // モーションをセット
        m_motion.ChangeMotion(Game2ModelMotion.DanceType.RUN);

        // 移動
        Hashtable parameters = new Hashtable(){
            {"x",to.x},
            {"y",to.y},
            {"z",to.z},
            {"islocal",false},
            {"easetype",iTween.EaseType.linear},
            {"time",3.0f},
            {"oncomplete", "MoveFinished"},
        };

        iTween.MoveTo(this.gameObject,parameters);
    }

    void MoveFinished ()
    {
        // ターゲットの方向を向く
        transform.rotation = Quaternion.LookRotation(m_Player.position - transform.position);
        m_motion.ChangeMotion(Game2ModelMotion.DanceType.POSE);

    }
}
