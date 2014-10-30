//======================================================
// @brief:押されたボタンと流れてきたボタンの比較を行う.
//------------------------------------------------------
// @author:前田稚隼.
// @param:　なし.
// @return:　表示するラベル.
//======================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushButtonTest : MonoBehaviour {

	FadeMgr m_fadeMgr;              // フェード.
	InputMgr m_btnState; 			// 入力インスタンス.
	private GameObject m_button;	//	当たり判定を行うボタン.
	public	int	m_pressKeyCount;	//	ボタンを押した回数.
	public  bool m_triggerFlg;		//	何かに当たっているかのフラグ.
	private GameObject m_redGameObject;		//	赤いボタン.
	private GameObject m_blueGameObject;	//	青いボタン.
	private GameObject m_greenGameObject;	//	緑のボタン.
	private GameObject m_yellowGameObject;	//	黄色のボタン.
	private GameObject m_buttonBuf;			//	ボタンのバッファ.
	private string m_bufName;

	// Use this for initialization
	void Start () {
		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;

		//	ボタン情報代入.
		m_blueGameObject = Resources.Load<GameObject>("blue");  
		m_redGameObject = Resources.Load<GameObject>("red");  
		m_greenGameObject = Resources.Load<GameObject>("green");  
		m_yellowGameObject = Resources.Load<GameObject>("yellow"); 

		m_triggerFlg = false;
		m_pressKeyCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//	ボタンが押されたか.
		if (true == m_btnState.AnyButtonTrigger()) 
		{
			m_pressKeyCount++;	//	ボタン押下回数+1.
			if(m_triggerFlg == true)	//	何かに当たっているか.
			{
				//if(true == Collision2D.Equals (m_redGameObject,m_buttonBuf))  //	同じ色か.
				if("red(Clone)" == m_bufName)
						Debug.Log("赤いボタンです");
		
				//	違う色ならMiss.
				//	タイミングは合っているか.
				m_triggerFlg=false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D button)
	{
		m_triggerFlg = true;
		m_bufName = button.name;
	}
}