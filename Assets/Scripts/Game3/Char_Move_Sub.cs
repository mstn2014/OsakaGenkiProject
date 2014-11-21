using UnityEngine;
using System.Collections;
//======================================================
// @brief:キャラクラス(手前)
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Char_Move_Sub : MonoBehaviour {

	// セッティング
	public Game3Setting Seting;
	
	// コンポーネント用
	Game3_Mg_Sub 	gamemain;
	Char_SpeedMgr	charspeed_mgr;

	Vector3	L_R_Position;		// スポットライトポジション
	
	// カウント
	float TimeCount = 0;
	float nowTime = 0;		// 現在の時間
	float Detline;			// 死亡ライン
	float m_DanceLimit = 0;		// ダンスリミット
	float m_SpotMoveTime = 0;	// 移動リミット
	
	// フラグ
	bool GoFlag = false;		// ライト方向に行くかどうか
	bool SerectFlag = false;	// スポットライトに選ばれたかどうか
	bool HitFlag = false;		// 当たったかどうか
	
	
	// Use this for initialization
	void Start () {
		// 座標を代入
		L_R_Position = GameObject.Find ("Light_R").transform.position;
		// コンポーネントをゲット
		gamemain = GameObject.Find ("Parade").GetComponent<Game3_Mg_Sub> ();
		charspeed_mgr = GameObject.Find ("Speed_Manager").GetComponent<Char_SpeedMgr> ();
		// 削除するライン
		Detline = GameObject.Find ("Delete_Line").transform.position.x;
		// ダンススピード調整
		m_DanceLimit = Seting.DanceLimit - charspeed_mgr.GetDanceSpeed ();
		m_SpotMoveTime = Seting.SpotMoveTime - charspeed_mgr.GetSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (GoFlag) {
			// スポットライトの位置まで移動したときの処理
			if (this.transform.position.z >= L_R_Position.z - 2 && this.transform.position.z <= L_R_Position.z + 2) {
				// 時間計測
				TimeCount += Time.deltaTime;
				if (TimeCount > m_DanceLimit) {
					iTween.MoveTo (this.gameObject, GameObject.Find ("Delete_Position").transform.position, Seting.DeleteMoveTime);
					this.gameObject.rigidbody.detectCollisions = false;		// あたり判定を無効化
				}
			}
		} else { // GoFlag off時の処理
			// 横移動
			this.transform.position = 
				new Vector3 (this.transform.position.x - Seting.Speed, this.transform.position.y, this.transform.position.z);
		}

		
		// 画面外まで出た時の処理
		if (this.transform.position.x < Detline) {
			// スポットライトから画面外に出た時（選ばれていなければそのまま削除）
			if (SerectFlag) {
				gamemain.DeleteMoveOrder (this.gameObject);// モデルの削除と選定
			} else {
				gamemain.ModelDelete_Make (this.gameObject);// モデルの削除と生成
			}
		}
		
	}
	
	// フラグ管理
	public int SetFig{
		set{
			if(value == 1){
				GoFlag = true;
				SerectFlag = true;
				iTween.MoveTo(this.gameObject, iTween.Hash( "position", GameObject.Find("Light_R").transform.position, "time",m_SpotMoveTime ,"easetype",iTween.EaseType.linear) );
			}
			
			if(value == 2){
				HitFlag = true;
			}
		}
		
		get{
			if(HitFlag == true)	return 2;
			else                return 0;
		}
	}
}
