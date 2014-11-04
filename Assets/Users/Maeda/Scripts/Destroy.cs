//======================================================
// @brief:ボタンの削除を行う
//------------------------------------------------------
// @author:前田稚隼
// @param:　なし
// @return:　なし
//======================================================

using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter2D (Collider2D button)
	{
		Destroy (button.gameObject);
	}
}
