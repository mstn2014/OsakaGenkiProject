using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//======================================================
// @brief:ゲーム２の状態を管理するクラス
//------------------------------------------------------
// @author:K.Ito
//======================================================
public class Game2Mgr : MonoBehaviour {

    // ゲームステートの宣言
    enum Game2State { WAIT,GUIDE, COUNTDOWN, GAME, END };
    Game2State m_state;

    // 各種マネージャの呼び出し
    InputMgr m_btnState;                    // 入力インスタンス
    FadeMgr m_fadeMgr;                      // フェード
    WindowMgr m_windowMgr;                  // ウィンドウマネージャー
    GuideMgr m_guideMgr;                    // ガイド役のマネージャー
    StartCountDown m_countDown;             // カウントダウン用のクラス
    List<string> m_messageText;             // メッセージデータ
    int m_messageIndex;                     // メッセージのインデックス

    public Game2Setting m_sceneSetting;    // シーンの設定ファイル

    // コルーチン制御用のwaitフラグ
    bool m_waitFlg = false;                         

	// Use this for initialization
	void Start () {
        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;
        m_fadeMgr = gs.FadeMgr;

        // ステートの設定
        m_state = Game2State.WAIT;
        // ウィンドウクラスの呼び出し
        m_windowMgr = GameObject.Find("WindowMgr").GetComponent<WindowMgr>();
        // ガイドを呼び出す
        m_guideMgr = GameObject.Find("GuideMgr").GetComponent<GuideMgr>();
        // カウントダウンを呼び出す
        m_countDown = GameObject.Find("Count").GetComponent<StartCountDown>();
        // テキストを読み込んでおく
        ParseMessageText textParser = new ParseMessageText();
        m_messageText = new List<string>();
        m_messageText = textParser.LoadText(m_sceneSetting.messageTextPath);
        m_messageIndex = 0;
        m_windowMgr.Text = m_messageText[m_messageIndex];
	}
	
	// Update is called once per frame
	void Update () {
        switch (m_state)
        {
            case Game2State.WAIT:
                Wait();
                break;
            case Game2State.GUIDE:
                Guide();
                break;
            case Game2State.COUNTDOWN:
                CountDown();
                break;
            case Game2State.GAME:
                break;
            case Game2State.END:
                break;
        }
	}

    //======================================================
    // @brief:Waitステート：３秒待ってからガイドを呼び出す
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void Wait()
    {
        StartCoroutine(WaitTime());
        m_waitFlg = true;
        m_state = Game2State.GUIDE;
    }

    IEnumerator WaitTime()
    {
        // 3秒待つ
        yield return new WaitForSeconds(3.0f);

        m_guideMgr.CallGuide();
        m_windowMgr.OpenWindow();
        m_waitFlg = false;
    }

    //======================================================
    // @brief:Guideの話のステート
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void Guide()
    {
        if (m_waitFlg) return;

        // メッセージ送り
        if (m_btnState.AnyButtonTrigger && m_windowMgr.IsFinished && (m_messageText.Count > (m_messageIndex+1) ) )
        {
            m_windowMgr.Text = m_messageText[++m_messageIndex];
        }
        //　メッセージが最後まで行った時の処理 
        else if(m_btnState.AnyButtonTrigger && m_windowMgr.IsFinished && (m_messageText.Count == (m_messageIndex+1)) )
        {
            m_windowMgr.CloseWindow();
            m_guideMgr.EndGuide();
            m_state = Game2State.COUNTDOWN;
        }
    }

    //======================================================
    // @brief:カウントダウンのステート
    // ２秒待ってからカウントダウンを開始する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void CountDown()
    {
        StartCoroutine(Count());
        m_waitFlg = true;
        m_state = Game2State.GAME;
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(2.0f);

        m_countDown.Begin();
        m_waitFlg = false;
    }
}
