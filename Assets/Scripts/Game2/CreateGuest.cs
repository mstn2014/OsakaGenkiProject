using UnityEngine;
using System.Collections;

public class CreateGuest : MonoBehaviour {

	//	参加者
	private GameObject m_guest;   
	private int m_createGuestVal;	//	生成した参加者の数
	private float m_setPositionX;	//	生成する位置X	
	private float m_setPositionZ;	//	生成する位置Z

	public Game2Setting m_sceneSetting;    // シーンの設定ファイル

	// Use this for initialization
	void Start () {
		m_guest = Resources.Load<GameObject>("Prefab/Game2/guest");  //	プレハブ参加者読み込み.
		m_createGuestVal = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//======================================================
	// @brief:引数の数だけ参加者を増加する、ただし表示限界人数以上は生成しない
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　int val 参加者の数.
	// @return:　なし.
	//======================================================
	public void IncreaseGuest(int val)
	{	
		//	参加者生成
		for (int i=0; i<val; i++) 
		{
			if (m_createGuestVal != m_sceneSetting.GuestPosition.Length )
			{
				m_guest = Instantiate (m_guest, transform.position, transform.rotation) as GameObject;
				m_guest.transform.parent = this.transform;

				m_guest.transform.position = m_sceneSetting.GuestPosition[m_createGuestVal];

				m_guest.name = "guest"+m_createGuestVal;
				m_createGuestVal++;
			}
		}
	}
}
