using UnityEngine;
using System.Collections;

public class CreateGuest : MonoBehaviour {

	//	参加者
	private GameObject m_guest;   
	private int m_createGuestVal;	//	生成した参加者の数
	private float m_setPositionX;	//	生成する位置X	
	private float m_setPositionZ;	//	生成する位置Z

	// Use this for initialization
	void Start () {
		m_guest = Resources.Load<GameObject>("Prefab/Game2/guest");  //	プレハブ参加者読み込み.
		m_createGuestVal = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//======================================================
	// @brief:引数の数だけ参加者を増加する
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　int val 参加者の数.
	// @return:　なし.
	//======================================================
	public void IncreaseGuest(int val)
	{	
		//	参加者召喚
		for (int i=0; i<=val; i++) {
			m_guest = Instantiate (m_guest, transform.position, transform.rotation) as GameObject;
			m_guest.transform.parent = this.transform;

			//	ランダムな位置に配置
			m_setPositionX = Random.Range(-5, 6);
			m_setPositionZ = Random.Range(-2, 9);
			m_guest.transform.position = new Vector3(m_setPositionX, 0.0f, m_setPositionZ);
			iTween.MoveTo(gameObject,iTween.Hash("path",iTweenPath.GetPath("MovePath"),"time",3,"easetype",iTween.EaseType.easeOutSine));

			m_guest.name = "guest"+m_createGuestVal;
			m_createGuestVal++;
		}
	}
}
