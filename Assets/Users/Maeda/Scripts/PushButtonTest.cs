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
	//private GameObject m_button;	//	当たり判定を行うボタン.

	//	カウント関連
	public	int	m_pressKeyCount;	//	ボタンを押した回数.
	public	int	m_redButtonCount;	//	赤ボタンを押した回数.
	public	int	m_blueButtonCount;	//	青ボタンを押した回数.
	public	int	m_yellowButtonCount;//	黄色ボタンを押した回数.
	public	int	m_greenButtonCount;	//	緑ボタンを押した回数.

	//	判定関連
	public  bool m_triggerFlg;		//	何かに当たっているかのフラグ.
	private string m_bufName;		//	当たっているオブジェの名前格納用.

	//	ラベル表示（判定結果）関連
	public	string dispLabel;		//	表示するラベル.
	DispLabel disp;

	// Use this for initialization
	void Start () {
		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;

		m_triggerFlg = false;
		m_pressKeyCount = 0;
		m_redButtonCount = 0;	//	赤ボタンを押した回数.
		m_blueButtonCount = 0;	//	青ボタンを押した回数.
		m_yellowButtonCount = 0;//	黄色ボタンを押した回数.
		m_greenButtonCount = 0;	//	緑ボタンを押した回数.

		dispLabel = "miss";
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
						//Debug.Log("赤いボタンが流れてきました");
						if(m_btnState.RedButtonTrigger == true)
						{
							m_redButtonCount++;
							dispLabel = "safe";
						}else
							dispLabel = "miss";
						break;

					case "blue(Clone)":
						//Debug.Log("青いボタンが流れてきました");
						if(m_btnState.BlueButtonPress == true)
						{
							m_blueButtonCount++;
							dispLabel = "safe";
						}else
							dispLabel = "miss";
						break;

					case "green(Clone)":
						//Debug.Log("緑色ボタンが流れてきました");
						if(m_btnState.GreenButtonPress == true)
						{
							m_greenButtonCount++;
							dispLabel = "safe";
						}else
							dispLabel = "miss";
						break;

					case "yellow(Clone)":
						//Debug.Log("黄色いボタンが流れてきました");
						if(m_btnState.YellowButtonPress == true)
						{
							m_yellowButtonCount++;
							dispLabel = "safe";
						}else
							dispLabel = "miss";
						break;

				}
				//	タイミングは合っているか.
			}
			m_triggerFlg=false;
		}
	}

	void OnTriggerEnter2D (Collider2D button)
	{
		dispLabel = "miss";
		m_triggerFlg = true;
		m_bufName = button.name;
	}
}