using UnityEngine;
using System.Collections;

public class Move_Green : MonoBehaviour {

	// 移動関係
	float nowTime = 0;		// 現在の時間
	const int hold = 30;	// 移動間隔
	float SpotR_pos_x;		// スポットライトx座標
	float SpotR_pos_y;		// スポットライトy座標
	float SpotR_pos_z;		// スポットライトz座標
	float Speed = 0.1f;		// 横移動スピード

	// カウント
	float TimeCount = 0;
	float TimeLimit = 5;
	
	// フラグ
	bool GoFlag = false;
	
	GameObject GreenChar_clone;
	
	// Use this for initialization
	void Start () {
		// 座標を代入
		SpotR_pos_x = GameObject.Find ("Spot_R").transform.position.x;
		SpotR_pos_y = GameObject.Find ("Spot_R").transform.position.y;
		SpotR_pos_z = GameObject.Find ("Spot_R").transform.position.z;
		
		// クローンロード
		GreenChar_clone = Resources.Load<GameObject> ("Kawamoto/GreenChar");
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
		
		//Debug.Log (this.transform.position.x);
		if (this.transform.position.x < -20) {
			float Cord = Random.Range (1,4);
			Instantiate (GreenChar_clone, new Vector3 (20, 0, 5+Cord), Quaternion.identity);

			if (this.transform.position.z >= SpotR_pos_z - 2 && this.transform.position.z <= SpotR_pos_z + 2) {
				GameObject.Find ("Pare").SendMessage ("CharMoveOrder");
			}
			Destroy (this.gameObject);
		}
	}
	
	void OnGo(){ GoFlag = true; }
	void OffGo(){ GoFlag = false; }
	
}