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

	//	共通設定.
	FadeMgr m_fadeMgr;              // フェード.
	InputMgr m_btnState; 			// 入力インスタンス.

    // Game2設定.
    Game2Setting Setting;          // ゲーム設定ファイル

	//	カウント関連.
	private	int	m_pressKeyCount;	//	ボタンを押した回数.
	private int m_missCount;		//	ミスの回数.
	private int m_safeCount;		//	セーフの回数.

	//	判定関連.
	private  bool   m_triggerFlg;	//	何かに当たっているかのフラグ.
	private  string m_bufName;		//	当たっているオブジェの名前格納用.

	//	ラベル表示用.
	LabelMgr m_dispClass;	
	private GameObject m_dispBuf;	//	DispLabelクラス代入用.

	//	ボタン上昇移動.
	MoveUp m_moveUpClass;
	private GameObject m_moveBuf;	//	MoveUpクラス代入用.

	//	セーフ、グッド、パーフェクトの判定.
	private Vector3 m_buttonPosition;	//	ボタンの座標.

    // スコア関連
    private ScoreMgr m_score;     // スコアオブジェクト

	// Use this for initialization
	void Start () {
		// 共通設定の呼び出し.
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_btnState = gs.InputMgr;

        // 設定ファイルの読み込み
        Setting = Resources.Load<Game2Setting>("Setting/Game2Setting");

		//	ラベル表示用.
		m_dispBuf = GameObject.Find ("DispLabel");
		m_dispClass = m_dispBuf.GetComponent<LabelMgr>();

        // スコア表示用
        m_score = GameObject.Find("ScoreMgr").GetComponent<ScoreMgr>();

		m_triggerFlg = false;
		m_pressKeyCount = 0;
		m_missCount = 0;
		m_safeCount = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//	ボタンが押されたか.
		if (true == m_btnState.AnyButtonTrigger) 
		{
			m_pressKeyCount++;	//	ボタン押下回数+1.
			if(m_triggerFlg == true)	//	何かに当たっているか.
			{
				m_triggerFlg=false;
				//	同じ色か判定（違う色ならMiss）.
				switch(m_bufName)
				{
					case "Red":
						if(m_btnState.RedButtonTrigger == true)
						{
							CheckButtonTiming();
							m_moveUpClass.MoveUpButton();
							m_safeCount++;
						}
						else
						{
							m_dispClass.Call("miss");
							m_moveUpClass.DestroyButton();
							m_missCount++;
						}
						break;

					case "Blue":
						if(m_btnState.BlueButtonPress == true)
						{
							CheckButtonTiming();
							m_moveUpClass.MoveUpButton();
							m_safeCount++;
						}
						else
						{
							m_dispClass.Call("miss");
							m_moveUpClass.DestroyButton();
							m_missCount++;	
						}
						break;

					case "Green":
						if(m_btnState.GreenButtonPress == true)
						{
							CheckButtonTiming();
							m_moveUpClass.MoveUpButton();
							m_safeCount++;
						}
						else
						{
							m_dispClass.Call("miss");
							m_moveUpClass.DestroyButton();
							m_missCount++;
						}
						break;

					case "Yellow":
						if(m_btnState.YellowButtonPress == true)
						{
							CheckButtonTiming();
							m_moveUpClass.MoveUpButton();
							m_safeCount++;
						}
						else
						{
							m_dispClass.Call("miss");
							m_moveUpClass.DestroyButton();
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
		m_bufName = button.tag;							//	ボタンの種類取得.
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
	private void CheckButtonTiming()
	{
		float distance;

		distance = Vector3.Distance (this.transform.position , m_buttonPosition) * Setting.playDistance;

        if (distance <= Setting.perfectRange)
        {
            m_dispClass.Call("perfect");
            m_score.AddScore(Setting.perfectPoint);
        }
        else if (distance <= Setting.goodRange)
        {
            m_dispClass.Call("good");
            m_score.AddScore(Setting.goodPoint);
        }
        else
        {
            m_dispClass.Call("safe");
            m_score.AddScore(Setting.safePoint);
        }
	}
}