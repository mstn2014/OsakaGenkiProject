using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	//入力
	InputMgr input_bt;
	GameObject go;

	// コンポーネント用
	GameMain 	gamemain;

	// Use this for initialization
	void Start () {
		// 共通設定の呼び出し
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		input_bt = gs.InputMgr;

		// コンポーネントをゲット
		gamemain = GameObject.Find ("Pare").GetComponent<GameMain> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// ボタンを押したときの処理
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "RedChar(Clone)" || other.gameObject.name == "RedChar") {
			if (input_bt.RedButtonTrigger){ 
				gamemain.ModelDeleteOrder(other.gameObject);// モデルの削除＆新しいターゲットの選定
			}
		}

		if (other.gameObject.name == "BlueChar(Clone)" || other.gameObject.name == "BlueChar") {
			if (input_bt.BlueButtonTrigger){ 
				gamemain.ModelDeleteOrder(other.gameObject);// モデルの削除＆新しいターゲットの選定
			}
		}

		if (other.gameObject.name == "GreenChar(Clone)" || other.gameObject.name == "GreenChar") {
			if (input_bt.GreenButtonTrigger){ 
				gamemain.ModelDeleteOrder(other.gameObject);// モデルの削除＆新しいターゲットの選定
			}
		}

		if (other.gameObject.name == "YerrowChar(Clone)" || other.gameObject.name == "YerrowChar") {
			if (input_bt.YellowButtonTrigger){ 
				gamemain.ModelDeleteOrder(other.gameObject);// モデルの削除＆新しいターゲットの選定
			}
		}
	}
}
