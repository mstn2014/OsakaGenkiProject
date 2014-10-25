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


	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "RedChar(Clone)" || other.gameObject.name == "RedChar") {
			if (input_bt.RedButtonTrigger){ 
				gamemain.NewModelMake();// モデルを生成
				gamemain.ModelDeleteOrder(other.gameObject);// モデルの削除＆新しいターゲットの選定
			}
		}
	}
}
