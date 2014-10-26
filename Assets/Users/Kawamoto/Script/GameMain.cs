using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

	GameObject RedChar_clone;
	GameObject BlueChar_clone;
	GameObject GreenChar_clone;
	GameObject YerrowChar_clone;

	// コンポーネント用
	Char_sp	m_Char_sp;

	// 乱数範囲
	int min = 1;
	int max = 5;

	// Use this for initialization
	void Start () {

			// クローンロード
			RedChar_clone = Resources.Load<GameObject> ("Kawamoto/RedChar");
			BlueChar_clone = Resources.Load<GameObject> ("Kawamoto/BlueChar");
			GreenChar_clone = Resources.Load<GameObject> ("Kawamoto/GreenChar");
			YerrowChar_clone = Resources.Load<GameObject> ("Kawamoto/YerrowChar");

			// 最初のターゲットを選択
			//CharMoveOrder ();
			m_Char_sp = GameObject.Find ("RedChar").GetComponent<Char_sp> ();
			m_Char_sp.SetFig = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// スポットライトに行くターゲットを選択
	public void CharMoveOrder(){
		// コード(1～4の乱数)
		int Cord = Random.Range (min,max);

		switch (Cord) {
			case 1:
				m_Char_sp = GameObject.Find("RedChar(Clone)").GetComponent<Char_sp>();
				m_Char_sp.SetFig = true;
				break;

			case 2:
				m_Char_sp = GameObject.Find("RedChar(Clone)").GetComponent<Char_sp>();
				//m_Char_sp = GameObject.Find("BlueChar(Clone)").GetComponent<Char_sp>();
				m_Char_sp.SetFig = true;
				break;

			case 3:
				m_Char_sp = GameObject.Find("RedChar(Clone)").GetComponent<Char_sp>();
				//m_Char_sp = GameObject.Find("GreenChar(Clone)").GetComponent<Char_sp>();
				m_Char_sp.SetFig = true;
				break;

			case 4:
				m_Char_sp = GameObject.Find("RedChar(Clone)").GetComponent<Char_sp>();
				//m_Char_sp = GameObject.Find("YerrowChar(Clone)").GetComponent<Char_sp>();
				m_Char_sp.SetFig = true;
				break;
		}
	}

	// キャラをランダムで生成する
	public void NewModelMake(){
		// コード(1～4の乱数)
		int Cord = Random.Range (min,max);
		int Lng = Random.Range (1,5);

		switch (Cord) {
			case 1:
			// 新たなモデルを生成
				RedChar_clone = Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
				break;
				
			case 2:
			// 新たなモデルを生成
				RedChar_clone = Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
				//BlueChar_clone = Instantiate (BlueChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
				break;
				
			case 3:
			// 新たなモデルを生成
				RedChar_clone = Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
				//GreenChar_clone = Instantiate (GreenChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
				break;
				
			case 4:
			// 新たなモデルを生成
				RedChar_clone = Instantiate (RedChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
				//YerrowChar_clone = Instantiate (YerrowChar_clone, new Vector3 (20, 0, 5+Lng), Quaternion.identity) as GameObject;
				break;
		}
	}

	//　削除開始
	public void ModelDelete(GameObject obj){
		Destroy (obj.gameObject); 
		NewModelMake ();
	}

	// 削除と選定
	public void ModelDeleteOrder(GameObject obj){
		Destroy (obj.gameObject);
		NewModelMake ();
		CharMoveOrder ();
	}
}