//======================================================
// @brief:赤、青、黄色、緑のアイコンの動的生成を行う
//------------------------------------------------------
// @author:前田稚隼
// @param:　m_createTime 生成する時間（60で１秒に１回生成)
// @return:　なし
//======================================================

using UnityEngine;
using System.Collections;

public class CreateIcon : MonoBehaviour {

	public float  m_createTime = 3.0f;			//	現状は時間で管理してるが、今後サウンドにあわせて生成する
	public float aliveTime = 3;
	private float nowTime;

	private GameObject m_gameObject;		//	ゲームオブジェクト本体
	private GameObject m_redGameObject;		//	赤いアイコン
	private GameObject m_blueGameObject;	//	青いアイコン
	private GameObject m_greenGameObject;	//	緑のアイコン
	private GameObject m_yellowGameObject;	//	黄色のアイコン

	// Use this for initialization
	void Start () {
		m_blueGameObject = Resources.Load<GameObject>("blue");  
		m_redGameObject = Resources.Load<GameObject>("red");  
		m_greenGameObject = Resources.Load<GameObject>("green");  
		m_yellowGameObject = Resources.Load<GameObject>("yellow"); 
		nowTime = 0.0f;
	}
	
	// Update is called once per fra
	void Update () {

		nowTime += Time.deltaTime;

		if ( nowTime >= m_createTime)
		{
			switch(Random.Range(0, 4))
			{
				case 0:
					m_gameObject = Instantiate(m_blueGameObject,transform.position,transform.rotation) as GameObject; 
					break;

				case 1:
					m_gameObject = Instantiate(m_redGameObject,transform.position,transform.rotation)  as GameObject; 
					break;
	
				case 2:
					m_gameObject = Instantiate(m_greenGameObject,transform.position,transform.rotation)  as GameObject; 
					break;

				case 3: 
					m_gameObject = Instantiate(m_yellowGameObject,transform.position,transform.rotation)  as GameObject; 
					break;
			}

			nowTime = 0.0f;

			m_gameObject.transform.parent = GameObject.Find ("IconManager").transform;
		}

		if (nowTime >= aliveTime){
			Destroy(this.gameObject);
				}
			
	}
}
