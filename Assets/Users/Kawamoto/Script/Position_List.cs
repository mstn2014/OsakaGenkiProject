using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Position_List : MonoBehaviour {

	// リスト
	List<GameObject> PositionList = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		for(int i=0;i<50;++i){
			//string str = i.ToString();
			PositionList.Add(GameObject.Find ("Position_" + i));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public Vector3 GetPos(){
		GameObject obj = PositionList[0];
		PositionList.RemoveAt(0);
		return obj.transform.position;
	}
}
