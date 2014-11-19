using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMain : MonoBehaviour {

	int MaxChar = 4;
	GameObject m_char;
	GameObject[] m_obj = new GameObject[4];
	Vector3[] pos = new Vector3[4];
	Vector3[] sce = new Vector3[4];
	
	string[] material = { "red", "green", "blue", "yerrow" };

	// コンポーネント用
	Char_sp	m_Char_sp;
	Char_List char_list;
	GameObject obj;

	// 乱数範囲
	int min = 1;
	int max = 5;

	// Use this for initialization
	void Start () {
		// プレハブロード
		m_char = Resources.Load<GameObject>("Kawamoto/Char");
		char_list = GameObject.Find("CharmgrSc").GetComponent<Char_List> ();
		// タグ付けしたオブジェクトをまとめて取得
		m_obj = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < 4;i++ )
		{
			pos[i] = m_obj[i].transform.localPosition;
			sce[i] = m_obj[i].transform.localScale;
		}
		// 最初のターゲットを選択
		CharMoveOrder ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	// スポットライトに行くターゲットを選択
	public void CharMoveOrder(){
		int ren;
		obj = null;
		do {
			ren = Random.Range (0, 4);
		} while(m_obj [ren] == null);
		obj = m_obj [ren];
		m_obj [ren] = null;
		NewModelMake ();
		m_Char_sp = obj.GetComponent<Char_sp> ();
		m_Char_sp.SetFig = 1;
	}

	// キャラをランダムで生成する
	public void NewModelMake(){
		for(int i = 0;i<MaxChar;i++)
		{
			if( m_obj[i] == null)
			{
				m_obj[i] = CreatePrefab.InstantiateGameObject(m_char,pos[i],Quaternion.identity,sce[i],GameObject.Find("CharMane"));
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

	//　削除と生成
	public void ModelDelete_Make(GameObject obj){
		Destroy (obj.gameObject);
		NewModelMake ();
	}

	// 削除と選定
	public void DeleteMoveOrder(GameObject obj){
		Destroy (obj.gameObject);
		CharMoveOrder ();
	}

	// リストにオブジェクトを格納
	public void ObjInList(GameObject obj){
		obj.GetComponent<Char_sp> ().enabled = false;	// 移動スクリプトを無効化
		obj.AddComponent<GoParade> ();					// パレ―ドの後ろについてくるスクリプトを接続
		obj.gameObject.rigidbody.detectCollisions = false;
		char_list.Push (obj);							// リストに格納
	}

	// 選択されたオブジェクトがライトに当たったフラグを立てる
	public void ObjHitOn(){
		m_Char_sp.SetFig = 2;
	}

	// 選択されたオブジェクトのフラグチェック
	public int ObjFlagC(){
		return m_Char_sp.SetFig;
	}

	// 選択されたオブジェクトを画面外に飛ばす
	public void SayonaraObj(){
		//Debug.Log(m_Char_sp.SetFig);
		iTween.MoveTo (obj, GameObject.Find ("RetPosition").transform.position, 4.0f);
		obj.gameObject.rigidbody.detectCollisions = false;
	}
}