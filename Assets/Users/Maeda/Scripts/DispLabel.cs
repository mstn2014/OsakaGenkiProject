//======================================================
// @brief:Miss,Safe,Good,Perfectのラベルを表示する
//------------------------------------------------------
// @author:前田稚隼
// @param:　m_dispTime 表示する時間
// @return:　なし
//======================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DispLabel : MonoBehaviour
{
	//	ラベル関連
	private GameObject m_missLabel;			//	"Miss!!"と書かれたラベル.
	private GameObject m_safeLabel;			//	"Safe!!"と書かれたラベル.
	private GameObject m_dispLabel;			//	表示するラベル.
	public  float	   m_dispTime = 0.3f;	//	表示する時間.
	private float	   m_nowTime;			//  時間.
	PushButtonTest 	   m_getDispLabel;		//	表示するラベル（判定結果).
	private GameObject m_buf;

	// Use this for initialization
	void Start () {
		m_missLabel = Resources.Load<GameObject>("LabelMiss");  
		m_safeLabel = Resources.Load<GameObject>("LabelSafe");  
		m_nowTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_nowTime += Time.deltaTime;
		
		if (m_nowTime >= m_dispTime)
			Destroy (m_dispLabel);
	}
	
	
	void OnTriggerEnter2D (Collider2D button)
	{
		//	判定結果をもらう
		m_buf = GameObject.Find ("ring");
		m_getDispLabel = m_buf.GetComponent<PushButtonTest>();

		m_dispLabel = Instantiate(m_safeLabel,transform.position,transform.rotation) as GameObject; 
		switch(m_getDispLabel.dispLabel)
		{
			case "safe":
				m_dispLabel = Instantiate(m_safeLabel,transform.position,transform.rotation) as GameObject; 
				break;

			case "miss":
				m_dispLabel = Instantiate(m_missLabel,transform.position,transform.rotation) as GameObject; 
				break;
		}
		m_dispLabel.transform.parent = GameObject.Find ("DispLabel").transform;
		m_nowTime = 0.0f;
	}
}