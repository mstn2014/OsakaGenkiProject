//======================================================
// @brief:赤、青、黄色、緑のアイコンの動的生成を行う
//------------------------------------------------------
// @author:前田稚隼
// @param:　なし
// @return:　なし
//======================================================

using UnityEngine;
using System.Collections;

public class CreateIcon : MonoBehaviour {

	public int m_createTime;			//	現状は時間で管理してるが、今後サウンドにあわせて生成する
	private GameObject m_gameObject;	//	オブジェクトの生成用

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per fra
	void Update () {
		if (Time.frameCount % m_createTime == 0)
		{

			//m_gameObject = Resources.Load<GameObject>("red");  
			//m_gameObject = Instantiate(m_gameObject,transform.position,transform.rotation)  as GameObject; 
			
			switch(Random.Range(0, 4))
			{
				case 0:
					m_gameObject = Resources.Load<GameObject>("blue");  
					m_gameObject = Instantiate(m_gameObject,transform.position,transform.rotation) as GameObject; 
					break;

				case 1:
					m_gameObject = Resources.Load<GameObject>("red");  
					m_gameObject = Instantiate(m_gameObject,transform.position,transform.rotation)  as GameObject; 
					break;
	
				case 2:
					m_gameObject = Resources.Load<GameObject>("green");  
					m_gameObject = Instantiate(m_gameObject,transform.position,transform.rotation)  as GameObject; 
					break;

				case 3:
					m_gameObject = Resources.Load<GameObject>("yellow");  
					m_gameObject = Instantiate(m_gameObject,transform.position,transform.rotation)  as GameObject; 
					break;
			}

			m_gameObject.transform.parent = GameObject.Find ("Panel").transform;
		}

		if (Time.frameCount % 240 == 0) {
						//Destroy (gameObject,5f);
				}
			
	}
}
