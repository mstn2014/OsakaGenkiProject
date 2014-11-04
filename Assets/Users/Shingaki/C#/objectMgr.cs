using UnityEngine;
using System.Collections;

public class ObjectMgr : MonoBehaviour {
	
	[Header("Object")]
	public GameObject m_objParent;	// GameObjectの一番上.
	public GameObject m_gallery;	// ギャラリー.
	private GameObject m_panel;		// NGUIの親.
	
	// Use this for initialization
	void Start () {
		m_panel = GameObject.Find ("Panel");
		//m_objParent = Instantiate (m_objParent);
		// ギャラリーを人数作る.
		// ToDo ランダムでキャラのポジション(中心からの距離を重みでばらけるように).
		//GameObject 
		// 
		for(int i=0; i<10; i++){
			//Instantiate();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
