using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour {

	//入力
	InputMgr input_bt;
	GameObject go;

	GameObject RedChar_clone;

	// Use this for initialization
	void Start () {

		// 入力クラス処理
		go = GameObject.FindGameObjectWithTag("InputMgr");
		if (go == null) {
			go = GameObject.Instantiate (Resources.Load ("Singleton/InputMgr")) as GameObject;
		}
		input_bt = go.GetComponent<InputMgr> ();

		// クローンロード
		RedChar_clone = Resources.Load<GameObject> ("Kawamoto/RedChar");
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void OnTriggerStay(Collider other)
	{
		//Debug.Log ("よばれた！");
		if (other.gameObject.name == "RedChar(Clone)" || other.gameObject.name == "RedChar") {
			if (input_bt.RedButtonTrigger){ 
				Destroy (other.gameObject);	// 対応するボタンを押せば衝突オブジェを消す
				Instantiate (RedChar_clone, new Vector3 (20, 0, 5), Quaternion.identity);	// 新たなモデルを生成
				Debug.Log (RedChar_clone.name);
				GameMain.CharMoveOrder();	// 新しいターゲットを選定
			}
		}
	}
}
