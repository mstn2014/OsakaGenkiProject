using UnityEngine;
using System.Collections;

public class GoParade : MonoBehaviour {

	public Vector3[] path = new Vector3[4];
	Position_List	 p_list;

	// Use this for initialization
	void Start () {
		p_list = GameObject.Find ("Position").GetComponent<Position_List> ();

		// 最初と最後の座標だけ指定
		path = iTweenPath.GetPath ("New Path 1");
		path [0] = this.gameObject.transform.position;
		path [2] = p_list.GetPos ();
		iTween.MoveTo(this.gameObject,iTween.Hash("path",path,"time",0.5f,"easetype",iTween.EaseType.linear));
		iTween.RotateAdd(this.gameObject, iTween.Hash("z", 360, "time", 0.5f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
