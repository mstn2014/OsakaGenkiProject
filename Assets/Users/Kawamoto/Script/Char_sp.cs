using UnityEngine;
using System.Collections;

public class Char_sp : MonoBehaviour {

	// セッティング
	public Test_Setting Seting;

	// コンポーネント用
	GameMain 	gamemain;
	CharSpeedMgr	charspeed_mgr;

	// カウント
	float TimeCount = 0;
	float TimeLimit = 3;
	float nowTime = 0;		// 現在の時間
	float Detline;			// 死亡ライン
	float D_time = 1.0f;
	
	// フラグ
	bool GoFlag = false;		// ライト方向に行くかどうか
	bool SerectFlag = false;	// スポットライトに選ばれたかどうか
	bool HitFlag = false;		// 当たったかどうか


	// Use this for initialization
	void Start () {
		// 座標を代入
		Seting.L_L_Position = GameObject.Find ("Spot_L").transform.position;
		// コンポーネントをゲット
		gamemain = GameObject.Find ("Pare").GetComponent<GameMain> ();
		charspeed_mgr = GameObject.Find ("Speed_Mgr").GetComponent<CharSpeedMgr> ();
		// 削除するライン
		Detline = GameObject.Find ("DeleteLine").transform.position.x;

		// ダンススピード調整
		TimeLimit = TimeLimit - charspeed_mgr.GetDansSpeed ();
		D_time = D_time - charspeed_mgr.GetSpeed ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GoFlag) {
			// スポットライトの位置まで移動したときの処理
			if (this.transform.position.z >= Seting.L_L_Position.z - 2 && this.transform.position.z <= Seting.L_L_Position.z + 2) {
				// 時間計測
				TimeCount += Time.deltaTime;
				if (TimeCount > TimeLimit) {
						iTween.MoveTo (this.gameObject, GameObject.Find ("RetPosition").transform.position, 4.0f);
						this.gameObject.rigidbody.detectCollisions = false;		// あたり判定を無効化
				}
			}
		} else { // GoFlag off時の処理
			// 横移動
			this.transform.position = 
				new Vector3 (this.transform.position.x - Seting.Speed, this.transform.position.y, this.transform.position.z);
		}


		// 画面外まで出た時の処理
		if (this.transform.position.z > Detline) {
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
				iTween.MoveTo(this.gameObject, iTween.Hash( "position", GameObject.Find("Spot_L").transform.position, "time",D_time ,"easetype",iTween.EaseType.linear) );
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
