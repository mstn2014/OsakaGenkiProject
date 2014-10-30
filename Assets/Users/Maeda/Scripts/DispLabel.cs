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

public class DispLabel : MonoBehaviour {

	//	共通関連
	FadeMgr m_fadeMgr;                      // フェード
	InputMgr m_btnState; 					// 入力インスタンス
	private GameObject   m_dispLabel;		// 表示するラベル
	public  Game2Setting m_sceneSetting;    // シーンの設定ファイル

	//	ラベル関連
	private GameObject m_safeLabel;			//	"Safe!!"と書かれたラベル
	private bool	   m_dispFlg;			//	表示フラグ
	public  float	   m_dispTime = 3.0f;	//	表示する時間
	private float	   m_nowTime;			//  時間

	//	判定関連
	private GameObject m_button;	//	当たり判定を行うボタン


	// Use this for initialization
	void Start () {

		// 共通設定の呼び出し
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;
		m_fadeMgr = gs.FadeMgr;

		//	ラベル関連
		m_safeLabel = Resources.Load<GameObject>("LabelSafe");  
		m_dispFlg = false;
		m_nowTime = 0.0f;

		//	判定関連
		//Button = GameObject.Find("TargetEnemy");
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_nowTime += Time.deltaTime;

		if (m_nowTime >= m_dispTime)
				Destroy (m_dispLabel);

		//if (m_btnState.RedButtonTrigger)
			//Debug.Log("RedButtonが押されました。");
	}
	

	void OnTriggerEnter2D (Collider2D button)
	{
	
		if (m_btnState.RedButtonTrigger)
		{
			if(button.gameObject == Resources.Load<GameObject>("red") )
			{
				m_dispLabel = Instantiate(m_safeLabel,transform.position,transform.rotation) as GameObject; 
				m_dispLabel.transform.parent = GameObject.Find ("DispMiss").transform;
				m_nowTime = 0.0f;
				Debug.Log("RedButtonが押されました。");
			}
		}
	}
}
