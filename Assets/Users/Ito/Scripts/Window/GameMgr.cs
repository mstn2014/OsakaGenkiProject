﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMgr : MonoBehaviour {

    InputMgr m_btnState;                    // 入力インスタンス
    FadeMgr m_fadeMgr;                      // フェード
    WindowMgr m_windowMgr;                  // ウィンドウマネージャー
    GuideMgr m_guideMgr;                    // ガイド役のマネージャー
    List<string> m_messageText;             // メッセージデータ 
    public Game1Setting m_sceneSetting;    // シーンの設定ファイル

	// Use this for initialization
	void Start () {
        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;
        m_fadeMgr = gs.FadeMgr;

        // ウィンドウクラスの呼び出し
        m_windowMgr = GameObject.Find("WindowMgr").GetComponent<WindowMgr>();

        // ガイドを呼び出す
        m_guideMgr = GameObject.Find("GuideMgr").GetComponent<GuideMgr>();

        // テキストを読み込んでおく
        ParseMessageText textParser = new ParseMessageText();
        m_messageText = new List<string>();
        m_messageText = textParser.LoadText(m_sceneSetting.messageTextPath);
	}
	
	// Update is called once per frame
	void Update () {
        // 1
	    if (m_btnState.RedButtonTrigger)
        {
            Debug.Log("RedButtonが押されました。");
            m_windowMgr.OpenWindow();
            m_guideMgr.CallGuide();
        }
        
        // 2
        if (m_btnState.GreenButtonTrigger)
        {
            Debug.Log("GreenButtonが押されました。");
            m_windowMgr.CloseWindow();
            m_guideMgr.EndGuide();
        }

        // 3
        if (m_btnState.BlueButtonTrigger)
        {
            string str = "BlueButtonが押されました。";
            Debug.Log(str);
            m_windowMgr.Text = m_messageText[0];
        }

        // 4
        if (m_btnState.YellowButtonTrigger)
        {
            string str = "YellowButtonが押されました。";
            Debug.Log(str);
            m_windowMgr.Text = m_messageText[1];
            m_fadeMgr.LoadLevel("WindowSample", 1);
        }
        
	}
}
