using UnityEngine;
using System.Collections;

//======================================================
// @brief:指導役をコントロールするクラス
// Tweenの位置はTween Postionから設定してください。
//------------------------------------------------------
// @author:K.Ito
//======================================================

[RequireComponent(typeof(TweenPosition))]
public class GuideController : MonoBehaviour {
    string className;           // エラーで現在のクラス名を返す
    TweenPosition m_tweenPostion;   // TweenPositionを格納しておく
    bool m_isTo;                // 移動先にいるか
    bool m_isFrom;              // 移動元にいるか
    bool m_isMove;              // 動いている最中か

	// Use this for initialization
	void Awake () {
	    m_tweenPostion = this.GetComponent<TweenPosition>();
        // イベントレシーバーにこのオブジェクトを登録
        m_tweenPostion.eventReceiver = this.gameObject;
        // tweenが終わった時に呼び出す関数を指定
        m_tweenPostion.callWhenFinished = "Finished";

        this.transform.position = m_tweenPostion.from;
        m_isMove = false;
        m_isFrom = true;
        m_isTo = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //======================================================
    // @brief:ガイド役を呼び出す
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void Call()
    {
        MoveToPosition(true);
    }

    //======================================================
    // @brief:ガイド役を帰らせる
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void End()
    {
        MoveToPosition(false);
    }

    //======================================================
    // @brief:移動処理
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //=====================================================
    void MoveToPosition(bool flg)
    {
        if (!(m_isFrom | m_isTo)) return;

        m_isFrom = m_isTo = false;
        m_isMove = true;
        m_tweenPostion.enabled = true;

        m_tweenPostion.Play(flg);
        m_isMove = flg;
    }

    //======================================================
    // @brief:移動が終わると呼び出される
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    void Finished()
    {
        m_tweenPostion.enabled = false;

        m_isTo = m_isMove;
        m_isFrom = !m_isMove;
    }
}
