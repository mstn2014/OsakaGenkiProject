using UnityEngine;
using System.Collections;

public class CreateGuest : MonoBehaviour {

	//	参加者
	private GameObject m_guest;   

	// Use this for initialization
	void Start () {
		m_guest = Resources.Load<GameObject>("Prefab/Game2/guest");  //	プレハブ参加者読み込み.
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseGuest(int val)
	{	
		//	参加者召喚
		for (int i=0; i<=val; i++) {
			m_guest = Instantiate (m_guest, transform.position, transform.rotation) as GameObject;
			m_guest.transform.parent = this.transform;
		}
	}
}
