using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMgr : MonoBehaviour {

    InputMgr m_btnState;                    // 入力インスタンス
    Guide m_guide;                          // ガイドさん 
    public Game1Setting m_sceneSetting;     // シーンの設定ファイル

	// Use this for initialization
	void Start () {
        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;

        // ガイドを呼び出す
        m_guide = GameObject.Find("Guide").GetComponent<Guide>();
	}
	
	// Update is called once per frame
	void Update () {
        // 1
	    if (m_btnState.RedButtonTrigger)
        {

        }
        
        // 2
        if (m_btnState.GreenButtonTrigger)
        {

        }

        // 3
        if (m_btnState.BlueButtonTrigger)
        {
            m_guide.End();
        }

        // 4
        if (m_btnState.YellowButtonTrigger)
        {
            m_guide.Begin("Message/game1","");
        }
        
	}
}
