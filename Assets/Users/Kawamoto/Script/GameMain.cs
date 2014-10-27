using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour {

	GameObject m_char;
	GameObject[] m_obj = new GameObject[4];
	Vector3[] pos = new Vector3[4];
	Vector3[] sce = new Vector3[4];
	
	string[] material = { "red", "green", "blue", "yellow" };

	// コンポーネント用
	Char_sp	m_Char_sp;

	// 乱数範囲
	int min = 1;
	int max = 5;

	// Use this for initialization
	void Start () {
		// プレハブロード
		m_char = Resources.Load<GameObject>("Kawamoto/Char");
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
		m_Char_sp = m_obj [Random.Range (0, 4)].GetComponent<Char_sp> ();
		m_Char_sp.SetFig = true;
	}

	// キャラをランダムで生成する
	public void NewModelMake(){
		for(int i = 0;i<4;i++)
		{
			if( m_obj[i] == null)
			{
				m_obj[i] = CreatePrefab.InstantiateGameObject(m_char,pos[i],Quaternion.identity,sce[i],GameObject.Find("CharMane"));
				string m_cl = material[Random.Range (0, 4)];
				if(m_cl == "blue") 		m_obj[i].renderer.material.color = Color.blue;
				if(m_cl == "yellow") 	m_obj[i].renderer.material.color = Color.yellow;
				if(m_cl == "green") 	m_obj[i].renderer.material.color = Color.green;
				if(m_cl == "red") 		m_obj[i].renderer.material.color = Color.red;
			}
		}
	}

	//　削除開始
	public void ModelDelete(GameObject obj){
		Destroy (obj.gameObject); 
		NewModelMake ();
		//Invoke ("NewModelMake",0.5f);
	}

	// 削除と選定
	public void ModelDeleteOrder(GameObject obj){
		Destroy (obj.gameObject);
		NewModelMake ();
		//Invoke ("NewModelMake",0.5f);
		CharMoveOrder ();
	}
}