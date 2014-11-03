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

		m_triggerFlg = false;
		m_pressKeyCount = 0;
		m_missCount = 0;
		m_safeCount = 0;
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
				//	同じ色か判定（違う色ならMiss）.
				switch(m_bufName)
				{
					case "red(Clone)":
						if(m_btnState.RedButtonTrigger == true)
						{
							m_dispClass.CDispLabel("safe");
							m_moveUpClass.CMoveUpButton();
							m_safeCount++;
						}
						else
						{
							m_dispClass.CDispLabel("miss");
							m_moveUpClass.CDestroyButton();
							m_missCount++;
						}
						break;

					case "blue(Clone)":
						if(m_btnState.BlueButtonPress == true)
						{
							m_dispClass.CDispLabel("safe");
							m_moveUpClass.CMoveUpButton();
							m_safeCount++;
						}
						else
						{
							m_dispClass.CDispLabel("miss");
							m_moveUpClass.CDestroyButton();
							m_missCount++;	
						}
						break;

					case "green(Clone)":
						if(m_btnState.GreenButtonPress == true)
						{
							m_dispClass.CDispLabel("safe");
							m_moveUpClass.CMoveUpButton();
							m_safeCount++;
						}
						else
						{
							m_dispClass.CDispLabel("miss");
							m_moveUpClass.CDestroyButton();
							m_missCount++;
						}
						break;

					case "yellow(Clone)":
						if(m_btnState.YellowButtonPress == true)
						{
							m_dispClass.CDispLabel("safe");
							m_moveUpClass.CMoveUpButton();
							m_safeCount++;
						}
						else
						{
							m_dispClass.CDispLabel("miss");
							m_moveUpClass.CDestroyButton();
							m_missCount++;
						}
						break;

				}
				//	タイミングは合っているか ToDo.
				m_triggerFlg=false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D button)
	{
		m_triggerFlg = true;
		m_bufName = button.name;
		m_moveUpClass = button.GetComponent<MoveUp>();
	}
}