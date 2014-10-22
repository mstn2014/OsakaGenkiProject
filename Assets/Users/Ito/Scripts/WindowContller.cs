using UnityEngine;
using System.Collections;

public class WindowContller : MonoBehaviour
{
    InputMgr m_btnState;        // 入力インスタンス
    GameObject m_windowObj;     // プレハブから生成した実体
    string className;           // エラーで現在のクラス名を返す
    TweenScale m_tweenScale;    // トゥウィーンの設定
    Vector3 m_tempScale;        // スケールを保存する
    bool m_isBig;               // 大きい状態か
    bool m_isSmall;             // 小さい状態か
    bool m_isToBig;             // 大きくする操作してるかどうか

    // public
    [Header("ウィンドウオブジェクト登録")]
    public GameObject m_talkWindow;

    //set getプロパティ
    public bool IsBig
    {
        get{return m_isBig;}
    }

    public bool IsSmall
    {
        get{return m_isSmall;}
    }

	// Use this for initialization
	void Start () {
        // クラス名の取得
        className = this.GetType().FullName;

        // TweenScaleの設定
        if (m_talkWindow == null)
        {
            Debug.Log("ウィンドウプレハブが登録されていません！インスペクター上で設定してください。");
        }

        // サイズの保存
        m_tempScale = m_talkWindow.transform.localScale;
        m_talkWindow.transform.localScale = new Vector3(0, 0, 1);

        // tweenScaleの設定
        m_tweenScale = m_talkWindow.GetComponent<TweenScale>();
        m_tweenScale.eventReceiver = this.gameObject;
        m_tweenScale.from = new Vector3(0, 0, 1);
        m_tweenScale.to = m_tempScale;

        m_isBig = false;
        m_isSmall = true;
        m_isToBig = false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    //======================================================
    // @brief:TalkWindowを呼び出す
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void Call()
    {
        Scale(true);
    }

    //======================================================
    // @brief:TalkWindowを終了させる
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void End()
    {
        Scale(false);
    }

    //======================================================
    // @brief:枠のスケーリングをさせる
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void Scale(bool isBig)
    {
        if ( !( m_isBig | m_isSmall) ) return;

        m_isBig = false;
        m_isSmall = false;
        m_tweenScale.enabled = true;
        m_tweenScale.Play(isBig);
        m_isToBig = isBig;
    }

    //======================================================
    // @brief:スケールが終わると呼び出される
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void Finished()
    {
        m_tweenScale.enabled = false;

        m_isBig = m_isToBig;
        m_isSmall = !m_isToBig;
    }
}
