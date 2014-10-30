using UnityEngine;
using System.Collections;

public class GoParade : MonoBehaviour {

	public Vector3[] path = new Vector3[4];

	// Use this for initialization
	void Start () {
		path [0] = this.gameObject.transform.position;
		path [1].Set (this.gameObject.transform.position.x - 5 ,this.gameObject.transform.position.y + 5,this.gameObject.transform.position.z);
		path [2].Set (this.gameObject.transform.position.x - 15 ,this.gameObject.transform.position.y + 7,this.gameObject.transform.position.z);
		path [3] = GameObject.Find ("OnPosition").transform.position;
		iTween.MoveTo(this.gameObject,iTween.Hash("path",path,"time",0.5f,"easetype",iTween.EaseType.linear));
		iTween.RotateTo(this.gameObject, iTween.Hash("z", 180, "time", 0.5f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
