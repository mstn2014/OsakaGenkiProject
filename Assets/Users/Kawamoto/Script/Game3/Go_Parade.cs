using UnityEngine;
using System.Collections;
//======================================================
// @brief:キャラを隊列に組み込むクラス
//------------------------------------------------------
// @author:A,Kawamoto
//======================================================
public class Go_Parade : MonoBehaviour {

	public Vector3[] path = new Vector3[4];		// モーションパス
	Position_List	 p_list;					// ポジションリスト
	
	// Use this for initialization
	void Start () {
		p_list = GameObject.Find ("List").GetComponent<Position_List> ();
		
		// 最初と最後の座標だけ指定
		path = iTweenPath.GetPath ("New Path 1");									// モーションパスをゲット
		path [0] = this.gameObject.transform.position;								// 開始位置を現在位置に設定
		path [2] = p_list.GetPos (GameObject.Find ("Delete_Position_Box").transform.position);	// 終点をポジションリストからゲット
		iTween.MoveTo(this.gameObject,iTween.Hash("path",path,"time",0.5f,"easetype",iTween.EaseType.linear));
		iTween.RotateAdd(this.gameObject, iTween.Hash("z", 360, "time", 0.5f));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
