﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//======================================================
// @brief:ゲーム２の状態を管理するクラス
//------------------------------------------------------
// @author:K.Ito
//======================================================
public class Game2StateMgr : MonoBehaviour {

    // ゲームステートの宣言
    enum Game2State { WAIT,GUIDE, COUNTDOWN,GAMEREADY, GAME, END };
    Game2State m_state;

    // 各種マネージャの呼び出し
    InputMgr m_btnState;                    // 入力インスタンス
    FadeMgr m_fadeMgr;                      // フェード
    WindowMgr m_windowMgr;                  // ウィンドウマネージャー
    GuideMgr m_guideMgr;                    // ガイド役のマネージャー
    ScoreMgr m_scoreMgr;                    // スコアマネージャ
    StartCountDown m_countDown;             // カウントダウン用のクラス
    CreateButton m_createButton;            // ボタンを生成するスクリプト(盛り上がりイベントのスクリプトを
    List<string> m_messageText;             // メッセージデータ
    int m_messageIndex;                     // メッセージのインデックス

    // コンポーネント関連
    public GameObject m_frame;              // フレームとリングのオブジェクト
    public GameObject m_extra;              // そのたのゲーム関連オブジェクト
	public GameObject m_buf;
	private GameObject m_Event1;			//	盛りあがりイベント1
	private GameObject m_Event2;			//	盛りあがりイベント2
	private GameObject m_Event3;			//	盛りあがりイベント3
	private GameObject m_Event4;			//	盛りあがりイベント4
	private GameObject m_Event5;			//	盛りあがりイベント5

    public Game2Setting m_sceneSetting;    // シーンの設定ファイル

	CreateGuest m_guest;	//	参加者増加用
	private GameObject m_guestbuf; //	CreateGuestクラス代入用.

    // コルーチン制御用のwaitフラグ
    bool m_waitFlg = false;                         

	// Use this for initialization
	void Start () {
        // 共通設定の呼び出し
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;
        m_fadeMgr = gs.FadeMgr;
        // ボタン生成を呼び出す
        m_createButton = m_extra.transform.FindChild("ButtonMgr").GetComponent<CreateButton>();
        // ステートの設定
        m_state = Game2State.WAIT;
        // スコアマネージャの呼び出し
        m_scoreMgr = m_frame.transform.FindChild("ScoreMgr").GetComponent<ScoreMgr>();
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

		//　イベント関連読み込み
		m_Event1 = GameObject.Find("Event1");
		m_Event1.gameObject.SetActive(false);

		m_Event2 = GameObject.Find("Event2");
		m_Event2.gameObject.SetActive(false);

		m_Event3 = GameObject.Find("Event3");
		m_Event3.gameObject.SetActive(false);

		m_Event4 = GameObject.Find("Event4");
		m_Event4.gameObject.SetActive(false);

		m_Event5 = GameObject.Find("Event5");
		m_Event5.gameObject.SetActive(false);

		//	参加者増加用.
		m_guestbuf = GameObject.Find ("CreateGuest");
		m_guest = m_guestbuf.GetComponent<CreateGuest>();
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
            case Game2State.GAMEREADY:
                GameReady();
                break;
            case Game2State.GAME:
                Game();
                break;
            case Game2State.END:
                End();
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
        m_frame.SetActive(true);
        m_waitFlg = true;
        m_state = Game2State.GAMEREADY;
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(2.0f);

        m_countDown.Begin();
        m_waitFlg = false;
    }

    //======================================================
    // @brief:ゲームの初期化のステート
    // カウントダウンが終わったらゲームを開始する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void GameReady()
    {
        if (m_countDown.IsFinished)
        {
            m_extra.SetActive(true);
            m_state = Game2State.GAME;
        }
    }

    //======================================================
    // @brief:ゲームステート
    // 盛り上がりイベントと終わりを処理する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void Game()
    {
        if (m_createButton.WaitFlg && m_createButton.IsFinished && !m_waitFlg)
        {
            // ゲーム終了時の処理を書く
            StartCoroutine(RankIvent());
            m_waitFlg = true;
            m_state = Game2State.END;
        }
        else if (m_createButton.WaitFlg && !m_waitFlg )
        {
			// ToDo：ここに盛り上がりイベントを書く。
			StartCoroutine( LivelyIvent() );
            m_waitFlg = true;
        }

    }

    IEnumerator LivelyIvent()
    {
		m_frame.SetActive(false);	//	フレーム非表示
        yield return new WaitForSeconds(1.5f);	//	3秒末

        GameObject.Find("DebugLog").GetComponent<UILabel>().text = "盛り上がりイベント発生中！！";
        
		if(m_scoreMgr.Score >= m_sceneSetting.Event1Score){
			m_Event1.gameObject.SetActive(true);
			m_guest.IncreaseGuest(m_sceneSetting.Event1Guest);	//	引数の数だけ参加増加.
		}

		if(m_scoreMgr.Score >= m_sceneSetting.Event2Score){
			m_Event2.gameObject.SetActive(true);
			m_guest.IncreaseGuest(m_sceneSetting.Event2Guest);	//	引数の数だけ参加増加.
		}
		
		if(m_scoreMgr.Score >= m_sceneSetting.Event3Score){
			m_Event3.gameObject.SetActive(true);
			m_guest.IncreaseGuest(m_sceneSetting.Event3Guest);	//	引数の数だけ参加増加.
		}
		
		if(m_scoreMgr.Score >= m_sceneSetting.Event4Score){
			m_Event4.gameObject.SetActive(true);
			m_guest.IncreaseGuest(m_sceneSetting.Event4Guest);	//	引数の数だけ参加増加.
		}
		
		if(m_scoreMgr.Score >= m_sceneSetting.Event5Score){
			m_Event5.gameObject.SetActive(true);
			m_guest.IncreaseGuest(m_sceneSetting.Event5Guest);	//	引数の数だけ参加増加.
		}

		//	ToDo：参加者あつまる.

		yield return new WaitForSeconds(5.0f);
        GameObject.Find("DebugLog").GetComponent<UILabel>().text = "";
        m_createButton.WaitFlg = false;
        m_waitFlg = false;
		m_frame.SetActive(true); // フレーム表示
    }

    IEnumerator RankIvent()
    {
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("DebugLog").GetComponent<UILabel>().text = "Rank:" + CalcRank() + "    Score:" + m_scoreMgr.Score.ToString(); 
        m_waitFlg = false;
    }

    string CalcRank()
    {
        float point = m_scoreMgr.Score / m_sceneSetting.maxPoint;
        foreach (Game2Setting.CRank rank in m_sceneSetting.rank)
        {
            if (point >= rank.value)
            {
                return rank.key;
            }
        }
        return string.Empty;
    }

    //======================================================
    // @brief:結果ステート
    // 得点からランクを表示する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void End()
    {

    }



    
}
