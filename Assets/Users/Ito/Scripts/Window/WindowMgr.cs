using UnityEngine;
using System.Collections;

public class WindowMgr : MonoBehaviour {

    WindowController m_talkWindowContller;
    MessageController m_messageContller;

    // set getアクセサ
    public string Text
    {
        set { m_messageContller.Text = value; }
        get { return m_messageContller.Text; }
    }

    public bool IsFinished
    {
        get { return m_messageContller.IsFinished & (m_talkWindowContller.IsBig || m_talkWindowContller.IsSmall); }
    }

    void Awake()
    {
        // ウィンドウコントローラーの呼び出し
        m_talkWindowContller = this.GetComponentInChildren<WindowController>();
        // メッセージコントローラーの呼び出し
        m_messageContller = this.GetComponentInChildren<MessageController>();
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
    
	}

    //======================================================
    // @brief:メッセージウィンドウを生成する
    // 枠が拡大しきってからメッセージを呼び出す
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void OpenWindow()
    {
        // 枠の呼び出し
        m_talkWindowContller.Call();
        // メッセージの呼び出し
        StartCoroutine(CallMessage());
    }

    IEnumerator CallMessage()
    {
        while (!m_talkWindowContller.IsBig)
        {
            yield return null;
        }
        m_messageContller.Call();
    }

    //======================================================
    // @brief:メッセージウィンドウを閉じる
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void CloseWindow()
    {
        m_talkWindowContller.End();
        m_messageContller.End();
    }
}
