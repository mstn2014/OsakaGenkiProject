using UnityEngine;
using System.Collections;

public class Event1 : MonoBehaviour {

	private GameObject m_guest;         // 参加者

	// Use this for initialization
	void Start () {
		m_guest = GameObject.Find("guest1");
		iTween.MoveTo(m_guest,iTween.Hash("path",iTweenPath.GetPath("MovePath"),"time",3,"easetype",iTween.EaseType.easeOutSine));
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (1.0f,0.0f,0.0f);
	}
}
