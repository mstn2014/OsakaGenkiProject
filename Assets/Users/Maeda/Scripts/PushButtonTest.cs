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

	//	セーフ、グッド、パーフェクトの判定.
	private Vector3 m_buttonPosition;	//	ボタンの座標.
	public	float	m_safeRange = 0.3f;	//	セーフの範囲.
	public	float	m_goodRange = 0.2f;	//	グッドの範囲.
	public	float	m_perfectRange = 0.1f;	//	パーフェクトの範囲.

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
				m_triggerFlg=false;
				//	同じ色か判定（違う色ならMiss）.
				switch(m_bufName)
				{
					case "red(Clone)":
						if(m_btnState.RedButtonTrigger == true)
						{
							CheckButaneTiming();
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
							CheckButaneTiming();
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
							CheckButaneTiming();
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
							CheckButaneTiming();
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
			}
		}
	}

	void OnTriggerStay2D (Collider2D button)
	{
		m_triggerFlg = true;							//	当たってるフラグON.
		m_bufName = button.name;						//	ボタンの種類取得.
		m_moveUpClass = button.GetComponent<MoveUp>();	//	ボタンのクラスの関数取得.
		m_buttonPosition = button.transform.position;	//	ボタンの座標取得.
	}


	//======================================================
	// @brief:(MISS,SAFE)などの判定を行う.
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　なし.
	// @return:　なし.
	//======================================================
	private void CheckButaneTiming()
	{
		float kyori;

		kyori = Vector3.Distance (this.transform.position , m_buttonPosition) * 10.0f;
	
		if (kyori <= m_perfectRange)
			m_dispClass.CDispLabel ("perfect");
		else if (kyori <= m_goodRange)
			m_dispClass.CDispLabel ("good");
		else if (kyori <= m_safeRange)
			m_dispClass.CDispLabel ("safe");
		else
			m_dispClass.CDispLabel ("miss");
	
		//Debug.Log("kyori : " + kyori);
	}
}