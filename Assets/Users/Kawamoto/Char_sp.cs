using UnityEngine;
using System.Collections;

public class Char_sp : MonoBehaviour {

	// 移動関係
	float nowTime = 0;		// 現在の時間
	const int hold = 30;	// 移動間隔
	float SpotR_pos_x;		// スポットライトx座標
	float SpotR_pos_y;		// スポットライトy座標
	float SpotR_pos_z;		// スポットライトz座標
	float Speed = 0.1f;		// 横移動スピード
	float Detline = -20;	// 死亡ライン

	// カウント
	float TimeCount = 0;
	float TimeLimit = 5;
	
	// フラグ
	bool GoFlag = false;

	// 乱数関係
	float mim = 1;
	float max = 4;

	GameObject RedChar_clone;

	// Use this for initialization
	void Start () {
		// 座標を代入
		SpotR_pos_x = GameObject.Find ("Spot_R").transform.position.x;
		SpotR_pos_y = GameObject.Find ("Spot_R").transform.position.y;
		SpotR_pos_z = GameObject.Find ("Spot_R").transform.position.z;

		// クローンロード
		RedChar_clone = Resources.Load<GameObject> ("Kawamoto/RedChar");
	}
	
	// Update is called once per frame
	void Update () {

		if (GoFlag) {
			// スポットライトへの移動
			nowTime += Time.deltaTime;
			float xpos = Mathf.Lerp (this.transform.position.x, SpotR_pos_x, nowTime / hold);
			float ypos = Mathf.Lerp (this.transform.position.y, SpotR_pos_y, nowTime / hold);
			float zpos = Mathf.Lerp (this.transform.position.z, SpotR_pos_z, nowTime / hold);
			this.transform.position = new Vector3 (xpos, ypos, zpos);

			// スポットライトの位置まで移動したときの処理
			if (this.transform.position.z >= SpotR_pos_z - 2 && this.transform.position.z<= SpotR_pos_z + 2) {
				// 時間計測
				TimeCount += Time.deltaTime;
				if (TimeCount > TimeLimit) {
						this.SendMessage ("OffGo");// リミットを過ぎると移動開始
				}
			}
		} else {
			// 横移動
			this.transform.position = new Vector3 (this.transform.position.x - Speed,
			                                       this.transform.position.y,
			                                       this.transform.position.z);
		}


		if (this.transform.position.x < Detline) {
			float Cord = Random.Range (mim,max);
			Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Cord), Quaternion.identity);
			if (this.transform.position.z >= SpotR_pos_z - 2 && this.transform.position.z <= SpotR_pos_z + 2) {
				Debug.Log (RedChar_clone.name);
				GameMain.ModelDeleteOrder(this.gameObject);
			}
			GameMain.ModelDelete(this.gameObject);
		}
	}

	void OnGo(){ GoFlag = true; }
	void OffGo(){ GoFlag = false; }
	/*
	// カメラから見えなくなれば消す
	void OnBecameInvisible(){
		float Cord = Random.Range (mim,max);
		Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Cord), Quaternion.identity);
		if (this.transform.position.z >= SpotR_pos_z - 2 && this.transform.position.z <= SpotR_pos_z + 2) {
			Debug.Log (RedChar_clone.name);
			GameMain.ModelDeleteOrder(this.gameObject);
		}
		GameMain.ModelDelete(this.gameObject);
	}*/

}
