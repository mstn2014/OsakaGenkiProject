using UnityEngine;
using System.Collections;
//======================================================
// @brief:触れたオブジェクトを削除するクラス
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Char_Delete : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//======================================================
	// @brief:オブジェクトが触れたら消す(あたり判定を感知すると自動呼出)
	//------------------------------------------------------
	// @author:A,Kawamoto
	// @param:Collider other  当たったオブジェクト(自動で入る)
	// @return:なし
	//======================================================
	private void OnTriggerStay(Collider other)
	{
		Destroy(other.gameObject);
	}
}
