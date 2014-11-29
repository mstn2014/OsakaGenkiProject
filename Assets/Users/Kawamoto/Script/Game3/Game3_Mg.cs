using UnityEngine;
using System.Collections;
//======================================================
// @brief:ゲームメインクラス(奥列)
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Game3_Mg : MonoBehaviour {

	int MaxChar = 4;		// キャラ数
	GameObject m_char;
	GameObject[] m_obj = new GameObject[4];
	Vector3[] pos = new Vector3[4];
	Vector3[] sce = new Vector3[4];
	bool      m_start = true;
	
	string[] material = { "red", "green", "blue", "yerrow" };
	
	// コンポーネント用
	Char_Move m_Char_sp;
	Char_List char_list;
	GameObject obj;		// 抽選に当選したキャラ
	
	// 乱数範囲
	int min = 1;
	int max = 5;
	
	// Use this for initialization
	void Start () {
		// プレハブロード
		m_char = Resources.Load<GameObject>("Kawamoto/Char");
		char_list = GameObject.Find("List").GetComponent<Char_List> ();
		// タグ付けしたオブジェクトをまとめて取得
		m_obj = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < 4;i++ )
		{
			pos[i] = m_obj[i].transform.localPosition;
			sce[i] = m_obj[i].transform.localScale;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (m_start) {
			CharMoveOrder ();
			m_start = false;
		}
	}

	//======================================================
	// @brief:スポットライトに行くターゲットを選択
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	public void CharMoveOrder(){
		int ren;
		obj = null;
		do {
			ren = Random.Range (0, 4);
		} while(m_obj [ren] == null);
		obj = m_obj [ren];
		m_obj [ren] = null;
		NewModelMake ();
		m_Char_sp = obj.GetComponent<Char_Move>();
		m_Char_sp.SetFig = 1;
	}

	//======================================================
	// @brief:キャラをランダムで生成する
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	public void NewModelMake(){
		for(int i = 0;i<MaxChar;i++)
		{
			if( m_obj[i] == null)
			{
				m_obj[i] = CreatePrefab.InstantiateGameObject(m_char,pos[i],Quaternion.identity,sce[i],GameObject.Find("Main_Char"));
				string m_cl = material[Random.Range (0, 4)];
				if(m_cl == "blue"){
					m_obj[i].renderer.material.color = Color.blue;
					m_obj[i].renderer.material.name = "blue";
				}
				
				if(m_cl == "yerrow"){
					m_obj[i].renderer.material.color = Color.yellow;
					m_obj[i].renderer.material.name = "yerrow";
				}
				
				if(m_cl == "green"){
					m_obj[i].renderer.material.color = Color.green;
					m_obj[i].renderer.material.name = "green";
				}
				
				if(m_cl == "red"){
					m_obj[i].renderer.material.color = Color.red;
					m_obj[i].renderer.material.name = "red";
				}
			}
		}
	}

	//======================================================
	// @brief:削除と生成
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:GameObject obj 削除するオブジェクト
	// @return:なし
	//======================================================
	public void ModelDelete_Make(GameObject obj){
		Destroy (obj.gameObject);
		NewModelMake ();
	}

	//======================================================
	// @brief:削除と選定
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:GameObject obj 削除するオブジェクト
	// @return:なし
	//======================================================
	public void DeleteMoveOrder(GameObject obj){
		Destroy (obj.gameObject);
		CharMoveOrder ();
	}

	//======================================================
	// @brief:リストにオブジェクトを格納
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:GameObject obj 格納するオブジェクト
	// @return:なし
	//======================================================
	public void ObjInList(GameObject obj){
		obj.GetComponent<Char_Move> ().enabled = false;	// 移動スクリプトを無効化
		obj.AddComponent<Go_Parade> ();					// パレ―ドの後ろについてくるスクリプトを接続
		obj.gameObject.rigidbody.detectCollisions = false;
		char_list.Push (obj);							// リストに格納
	}

	//======================================================
	// @brief:選択されたオブジェクトがライトに当たったフラグを立てる
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	public void ObjHitOn(){
		m_Char_sp.SetFig = 2;
	}

	//======================================================
	// @brief:選択されたオブジェクトのフラグチェック
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	public int ObjFlagC(){
		return m_Char_sp.SetFig;
	}

	//======================================================
	// @brief:選択されたオブジェクトを画面外に飛ばす
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:なし
	// @return:なし
	//======================================================
	public void SayonaraObj(){
		//Debug.Log(m_Char_sp.SetFig);
		iTween.MoveTo (obj, GameObject.Find ("Delete_Position").transform.position, 4.0f);
		obj.gameObject.rigidbody.detectCollisions = false;
	}
}
