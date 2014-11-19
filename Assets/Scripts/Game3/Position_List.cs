using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//======================================================
// @brief:着地ポジションクラス
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Position_List : MonoBehaviour {

	// リスト
	List<GameObject> PositionList = new List<GameObject> ();	// 着地ポジション
	int count = 50;												// ポジション数
	
	// Use this for initialization
	void Start () {
		for(int i=0;i<count;++i){
			PositionList.Add(GameObject.Find ("Position_" + i));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//======================================================
	// @brief:ポジションゲット
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:Vector3 pos ポジションがない場合に戻すポジション
	// @return:Vector3    着地ポジション
	//======================================================
	public Vector3 GetPos( Vector3 pos ){
		if (count > 0) {
			GameObject obj = PositionList [0];
			PositionList.RemoveAt (0);
			count--;
			return obj.transform.position;
		} else {
			return pos;
		}
	}
}
