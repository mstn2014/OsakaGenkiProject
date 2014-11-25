using UnityEngine;
using System.Collections;

public class Game2Event4 : MonoBehaviour {

	private GameObject m_rightLight;	//	ステージの上ライト右側
	private GameObject m_leftLight;		//	ステージの上ライト左側
	
	// Use this for initialization
	void Start () {
		m_rightLight = GameObject.Find("Event4LightRight");
		m_leftLight = GameObject.Find("Event4LightLeft");
	}
	
	// Update is called once per frame
	void Update () {
		m_rightLight.transform.Rotate (0.0f,1.0f,0.0f);
		m_leftLight.transform.Rotate (0.0f,-1.0f,0.0f);
	}
}
