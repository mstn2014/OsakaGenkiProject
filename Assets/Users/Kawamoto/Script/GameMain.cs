using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

	GameObject RedChar_clone;
	GameObject BlueChar_clone;
	GameObject GreenChar_clone;
	GameObject YerrowChar_clone;
	

	// Use this for initialization
	void Start () {

		// クローンロード
		RedChar_clone = Resources.Load<GameObject> ("Kawamoto/RedChar");
		BlueChar_clone = Resources.Load<GameObject> ("Kawamoto/BlueChar");
		GreenChar_clone = Resources.Load<GameObject> ("Kawamoto/GreenChar");
		YerrowChar_clone = Resources.Load<GameObject> ("Kawamoto/YerrowChar");

		// 最初のターゲットを選択
		//CharMoveOrder ();
		GameObject.Find("RedChar").SendMessage("OnGo");
	}
	
	// Update is called once per frame
	void Update () {

	}

	// スポットライトに行くターゲットを選択
	public void CharMoveOrder(){
		// コード(1～4の乱数)
		int Cord = Random.Range (1,5);

		switch (Cord) {
			case 1:
				//GameObject.Find("RedChar").SendMessage("OnGo");
				GameObject.Find("RedChar(Clone)").SendMessage("OnGo");
				break;

			case 2:
				//GameObject.Find("RedChar").SendMessage("OnGo");
				GameObject.Find("RedChar(Clone)").SendMessage("OnGo");
				//GameObject.Find("BlueChar(Clone)").SendMessage("OnGo");
				break;

			case 3:
				//GameObject.Find("RedChar").SendMessage("OnGo");
				GameObject.Find("RedChar(Clone)").SendMessage("OnGo");
				//GameObject.Find("GreenChar(Clone)").SendMessage("OnGo");
				break;

			case 4:
				//GameObject.Find("RedChar").SendMessage("OnGo");
				GameObject.Find("RedChar(Clone)").SendMessage("OnGo");
				//GameObject.Find("YerrowChar(Clone)").SendMessage("OnGo");
				break;
		}
	}

	// キャラをランダムで生成する
	public void NewModelMake(){
		// コード(1～4の乱数)
		int Cord = Random.Range (1,5);
		int Lng = Random.Range (1,5);

		switch (Cord) {
			case 1:
			// 新たなモデルを生成
				RedChar_clone = Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
				break;
				
			case 2:
			// 新たなモデルを生成
				RedChar_clone = Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
			//Instantiate (BlueChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity);	// 新たなモデルを生成
				break;
				
			case 3:
			// 新たなモデルを生成
				RedChar_clone = Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
			//Instantiate (GreenChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity);	// 新たなモデルを生成
				break;
				
			case 4:
			// 新たなモデルを生成
				RedChar_clone = Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
			//Instantiate (YerrowChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity);// 新たなモデルを生成
				break;
		}
	}

	//　削除開始
	public void ModelDelete(GameObject obj){ Destroy (obj.gameObject); }

	// 削除と選定
	public void ModelDeleteOrder(GameObject obj){
		Destroy (obj.gameObject);
		CharMoveOrder ();
	}
}