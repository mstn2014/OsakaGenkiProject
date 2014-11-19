using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//======================================================
// @brief:隊列リストクラス
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Char_List : MonoBehaviour {

	// リスト
	List<GameObject> CharList = new List<GameObject> ();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//======================================================
	// @brief:プッシュ
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:GameObject obj  リストに格納するゲームオブジェクト
	// @return:なし
	//======================================================
	public void Push(GameObject obj){
		CharList.Add (obj);
	}
}
