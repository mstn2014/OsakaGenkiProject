using UnityEngine;
using System.Collections;

public class CreatePrefab : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
	
	}

	//======================================================
	// @brief:(生成したいプレハブ・座標・角度・拡縮・親)を入れることができます.
	//------------------------------------------------------
	// @author:新垣.
	// @param:引数名　clone:作りたいGameObject(public定義のあとInspectorでprefabを設定すること).
	//				 potion:座標		angle:角度	scale:拡縮		(オプション引数使いたい).
	//				 parent:生成したい場所(親).
	// @return:戻り値　生成したGameObject.
	//======================================================
	public static GameObject InstantiateGameObject
		(GameObject clone, Vector3 position, Quaternion angle,
		 Vector3 scale,GameObject parent=null){
		GameObject gameObj = Instantiate(clone) as GameObject;
		Transform tran = gameObj.transform;
		// 親の設定.

		if (parent != null) {
			tran.parent = parent.transform;
		}
		// Transform初期化.
		tran.localPosition = position;
		tran.localRotation = angle;
		tran.localScale = scale;

		return gameObj;
	}
}
