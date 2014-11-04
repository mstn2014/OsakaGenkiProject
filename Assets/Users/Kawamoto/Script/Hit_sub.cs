using UnityEngine;
using System.Collections;

public class Hit_sub : MonoBehaviour {

	//入力
	//InputMgr input_bt;
	InputMg	   input_bt;
	GameObject go;
	
	// コンポーネント用
	GameMain 		gamemain;
	GameMain_sub 	gamemain_sub;
	Hit				hit;

	// ヒットしている色
	int HitNum = 0;
	
	// Use this for initialization
	void Start () {
		
		// コンポーネントをゲット
		gamemain = GameObject.Find ("Pare").GetComponent<GameMain> ();
		gamemain_sub = GameObject.Find ("Pare").GetComponent<GameMain_sub> ();
		hit = GameObject.Find ("Spot_L").GetComponent<Hit> ();
		input_bt = GameObject.Find ("InputMane").GetComponent<InputMg> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		// 当たっていないときにボタンを押すと飛ぶ
		if (input_bt.AnyTrigger()) {
			if (gamemain_sub.ObjFlagC () == 0) {
				gamemain_sub.SayonaraObj();
			}
		}
	}
	
	// ボタンを押したときの処理
	private void OnTriggerStay(Collider other)
	{
		//Debug.Log(other.gameObject.renderer.material.name);
		//フラグを立てる
		gamemain_sub.ObjHitOn ();
		if (other.gameObject.renderer.material.name == "Red (Instance)" || 
			other.gameObject.renderer.material.name == "red") {
			HitNum = 1;
			if (input_bt.RedTrigger()) { 
				gamemain_sub.ObjInList (other.gameObject);// リストに格納
				gamemain_sub.CharMoveOrder ();// 新しいターゲットの選定
			}
		} else { 
			if (input_bt.RedTrigger()) { 
				if (gamemain.ObjFlagC () == 2 && hit.GetNum () != 1) {
					iTween.MoveTo (other.gameObject, GameObject.Find ("RetPosition").transform.position, 4.0f);
				}
			}
		}

		if (other.gameObject.renderer.material.name == "Green (Instance)" ||
			other.gameObject.renderer.material.name == "green") {
			HitNum = 2;
			if (input_bt.GreenTrigger()) { 
				gamemain_sub.ObjInList (other.gameObject);// リストに格納
				gamemain_sub.CharMoveOrder ();// 新しいターゲットの選定
			}
		} else {
			if (input_bt.GreenTrigger()) { 
				if (gamemain.ObjFlagC () == 2 && hit.GetNum() != 2 ) {
					iTween.MoveTo (other.gameObject, GameObject.Find ("RetPosition").transform.position, 4.0f);
				}
			}
		}

		if (other.gameObject.renderer.material.name == "Blue (Instance)" ||
			other.gameObject.renderer.material.name == "blue") {
			HitNum = 3;
			if (input_bt.BlueTrigger()) { 
				gamemain_sub.ObjInList (other.gameObject);// リストに格納
				gamemain_sub.CharMoveOrder ();// 新しいターゲットの選定
			}
		} else {
			if (input_bt.BlueTrigger()) { 
				if (gamemain.ObjFlagC () == 2 && hit.GetNum() != 3 ) {
					iTween.MoveTo (other.gameObject, GameObject.Find ("RetPosition").transform.position, 4.0f);
				}
			}
		}

		if (other.gameObject.renderer.material.name == "Yerrow (Instance)" ||
			other.gameObject.renderer.material.name == "yerrow") {
			HitNum = 4;
			if (input_bt.YellowTrigger()) { 
				gamemain_sub.ObjInList (other.gameObject);// リストに格納
				gamemain_sub.CharMoveOrder ();// 新しいターゲットの選定
			}
		} else {
			if (input_bt.YellowTrigger()) { 
				if (gamemain.ObjFlagC () == 2 && hit.GetNum() != 4 ) {
					iTween.MoveTo (other.gameObject, GameObject.Find ("RetPosition").transform.position, 4.0f);
				}
			}
		}
	}

	// 何色に当たっているか
	public int GetNum(){
		return HitNum;
	}
}


