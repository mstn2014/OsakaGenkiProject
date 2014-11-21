using UnityEngine;
using System.Collections;

public class Event1 : MonoBehaviour {

	private GameObject m_guest;         // 参加者
	//m_guest = Resources.Load<GameObject>("Prefab/Game2/blue");  

	// Use this for initialization
	void Start () {
		m_guest = GameObject.Find("guest1");

	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (1.0f,0.0f,0.0f);
	}
}
