//======================================================
// @brief:Miss,Safe,Good,Perfectのラベルを表示する.
//------------------------------------------------------
// @author:前田稚隼.
// @param:　m_dispTime 表示する時間.
// @return:　なし.
//======================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DispLabel : MonoBehaviour
{
	//	ラベル関連
	private GameObject m_missLabel;			//	"Miss!!"と書かれたラベル.
	private GameObject m_safeLabel;			//	"Safe!!"と書かれたラベル.
	private GameObject m_goodLabel;			//	"Good!!"と書かれたラベル.
	private GameObject m_perfectLabel;		//	"perfect!!"と書かれたラベル.
	public  GameObject m_dispLabel;			//	表示するラベル.
	public  float	   m_dispTime = 0.3f;	//	表示する時間.
	private float	   m_nowTime;			//  経過時間.

	// Use this for initialization
	void Start () {
		m_missLabel = Resources.Load<GameObject>("LabelMiss");  
		m_safeLabel = Resources.Load<GameObject>("LabelSafe");  
		m_goodLabel = Resources.Load<GameObject>("LabelGood");  
		m_perfectLabel = Resources.Load<GameObject>("LabelPerfect");  
		m_nowTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_nowTime += Time.deltaTime;
		
		if (m_nowTime >= m_dispTime)
			Destroy (m_dispLabel);
	}
	
	//======================================================
	// @brief:一定の位置までボタンが流れてきたら"MISS"を表示.
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　なし.
	// @return:　なし.
	//======================================================
	void OnTriggerEnter2D ()
	{
		CDispLabel("miss");
	}
	
	//======================================================
	// @brief:(MISS,SAFE)などのラベルを表示する.
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　string labelName 表示するラベルの名前.
	// @return:　なし.
	//======================================================
	public void CDispLabel(string labelName)
	{
		switch(labelName)
		{
			case "safe":
				m_dispLabel = Instantiate(m_safeLabel,transform.position,transform.rotation) as GameObject; 
				break;
			
			case "miss":
				m_dispLabel = Instantiate(m_missLabel,transform.position,transform.rotation) as GameObject; 
				break;

			case "good":
				m_dispLabel = Instantiate(m_goodLabel,transform.position,transform.rotation) as GameObject; 
				break;

			case "perfect":
				m_dispLabel = Instantiate(m_perfectLabel,transform.position,transform.rotation) as GameObject; 
				break;
		}
		m_dispLabel.transform.parent = GameObject.Find ("DispLabel").transform;
		m_nowTime = 0.0f;
	}
}