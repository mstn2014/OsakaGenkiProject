using UnityEngine;
using System.Collections;

public class Char_sp : MonoBehaviour {

	// セッティング
	public Test_Setting Seting;

	// コンポーネント用
	GameMain 	gamemain;

	// カウント
	float TimeCount = 0;
	float TimeLimit = 5;
	const int hold = 30;	// 移動間隔
	float nowTime = 0;		// 現在の時間
	
	// フラグ
	bool GoFlag = false;
	
	// Use this for initialization
	void Start () {
		// 座標を代入
		Seting.SpotR_pos_x = GameObject.Find ("Spot_R").transform.position.x;
		Seting.SpotR_pos_y = GameObject.Find ("Spot_R").transform.position.y;
		Seting.SpotR_pos_z = GameObject.Find ("Spot_R").transform.position.z;
		// コンポーネントをゲット
		gamemain = GameObject.Find ("Pare").GetComponent<GameMain> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GoFlag) {
			// スポットライトへの移動
			nowTime += Time.deltaTime;
			float xpos = Mathf.Lerp (this.transform.position.x, Seting.SpotR_pos_x, nowTime / hold);
			float ypos = Mathf.Lerp (this.transform.position.y, Seting.SpotR_pos_y, nowTime / hold);
			float zpos = Mathf.Lerp (this.transform.position.z, Seting.SpotR_pos_z, nowTime / hold);
			this.transform.position = new Vector3 (xpos, ypos, zpos);

			// スポットライトの位置まで移動したときの処理
			if (this.transform.position.z >= Seting.SpotR_pos_z - 2 && this.transform.position.z<= Seting.SpotR_pos_z + 2) {
				// 時間計測
				TimeCount += Time.deltaTime;
				if (TimeCount > TimeLimit) 		this.SetFig = false;// リミットを過ぎると移動開始
			}

		} else { // フラグoff時の処理
			// 横移動
			this.transform.position = 
				new Vector3 (this.transform.position.x - Seting.Speed,this.transform.position.y,this.transform.position.z);
		}

		// 画面外まで出た時の処理
		if (this.transform.position.x < Seting.Detline) {
			// スポットライトから画面外に出た時
			if (this.transform.position.z >= Seting.SpotR_pos_z - 2 && this.transform.position.z <= Seting.SpotR_pos_z + 2) {
				gamemain.ModelDeleteOrder(this.gameObject);// モデルの削除＆新しいターゲットの選定
			}else{
				gamemain.ModelDelete(this.gameObject);// モデルの削除
			}
		}
	}

	// フラグ管理
	public bool SetFig{
		set{
			GoFlag = value;
		}

		get{
			return GoFlag;
		}
	}
}
