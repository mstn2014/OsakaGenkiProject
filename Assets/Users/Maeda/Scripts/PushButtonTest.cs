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

	//	共通設定
	FadeMgr m_fadeMgr;              // フェード.
	InputMgr m_btnState; 			// 入力インスタンス.

	//	カウント関連
	public	int	m_pressKeyCount;	//	ボタンを押した回数.
	private int m_missCount;		//	ミスの回数.
	private int m_safeCount;		//	セーフの回数.

	//	判定関連
	private  bool   m_triggerFlg;	//	何かに当たっているかのフラグ.
	private  string m_bufName;		//	当たっているオブジェの名前格納用.

	//	判定結果関連.
	public	string m_sendMessage;	//	判定結果（miss,sefeなど).

	//	ラベル表示用.
	DispLabel m_dispClass;	
	private GameObject m_dispBuf;	//	DispLabelクラス代入用.

	//	ボタン上昇移動.
	MoveUp m_moveUpClass;
	private GameObject m_moveBuf;	//	MoveUpクラス代入用.

	// Use this for initialization
	void Start () {
		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;

		//	ラベル表示用.
		m_dispBuf = GameObject.Find ("DispLabel");
		m_dispClass = m_dispBuf.GetComponent<DispLabel>();

		//	ボタン上昇移動.
		//m_moveBuf = GameObject.Find ("DispLabel");
		//m_moveUpClass = m_moveBuf.GetComponent<MoveUp>();

		m_triggerFlg = false;
		m_pressKeyCount = 0;
		m_missCount = 0;
		m_safeCount = 0;

		m_sendMessage = "miss";
	}
	
	// Update is called once per frame
	void Update ()
	{
		//	ボタンが押されたか.
		if (true == m_btnState.AnyButtonTrigger()) 
		{
			m_pressKeyCount++;	//	ボタン押下回数+1.
			if(m_triggerFlg == true)	//	何かに当たっているか.
			{
				m_sendMessage = "miss";
				//	同じ色か判定（違う色ならMiss）.
				switch(m_bufName)
				{
					case "red(Clone)":
						if(m_btnState.RedButtonTrigger == true)
						{
							m_dispClass.CDispLabel("safe");
							m_moveUpClass.CMoveUpButton();
						}
						else
						{
							m_dispClass.CDispLabel("miss");
							m_moveUpClass.CDestroyButton();
						}
						break;

					case "blue(Clone)":
						if(m_btnState.BlueButtonPress == true)
						{
							m_dispClass.CDispLabel("safe");
							m_moveUpClass.CMoveUpButton();
						}
						else
						{
							m_dispClass.CDispLabel("miss");
							m_moveUpClass.CDestroyButton();
						}
						break;

					case "green(Clone)":
						if(m_btnState.GreenButtonPress == true)
						{
							m_dispClass.CDispLabel("safe");
							m_moveUpClass.CMoveUpButton();
						}
						else
						{
							m_dispClass.CDispLabel("miss");
							m_moveUpClass.CDestroyButton();
						}
						break;

					case "yellow(Clone)":
						if(m_btnState.YellowButtonPress == true)
						{
							m_dispClass.CDispLabel("safe");
							m_moveUpClass.CMoveUpButton();
						}
						else
						{
							m_dispClass.CDispLabel("miss");
							m_moveUpClass.CDestroyButton();
						}
						break;

				}
				//Debug.Log("PushButtonTest");
				//	タイミングは合っているか.
				m_triggerFlg=false;
				if(m_sendMessage == "safe")
					m_safeCount++;

				if(m_sendMessage == "miss")
					m_missCount++;
			}
			//Debug.Log(m_sendMessage+1);
		}
	}

	void OnTriggerEnter2D (Collider2D button)
	{
		m_sendMessage = "miss";
		m_triggerFlg = true;
		m_bufName = button.name;
		m_moveUpClass = button.GetComponent<MoveUp>();
	}
}