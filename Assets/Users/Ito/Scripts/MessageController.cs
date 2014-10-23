using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
[RequireComponent(typeof(TypewriterEffect))]
public class MessageController : MonoBehaviour {

    UILabel m_uiLabel;
    TypewriterEffect m_typeEffect;
    string m_text;      // 表示する文字

    //set getアクセサ
    public string Text
    {
        set {
            if (m_typeEffect.IsFinished)
            { 
                m_uiLabel.text = value;
                m_typeEffect.Reset();
            }
        }
        get { return m_uiLabel.text; }
    }

	// Use this for initialization
	void Start () {
        if (this == null)
        {
            Debug.Log("メッセージオブジェクトが登録されていません。");
        }

        m_uiLabel = this.GetComponent<UILabel>();
        m_typeEffect = this.GetComponent<TypewriterEffect>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //======================================================
    // @brief:メッセージを呼び出す
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void Call()
    {
        m_typeEffect.enabled = true;
        // 同時にtrueにすると一瞬文字が表示されるためにコルーチンでm_typeEffectがtrueになるのを待つ
        StartCoroutine(AddTypeEffect());
    }

    IEnumerator AddTypeEffect()
    {
        yield return m_typeEffect.enabled;
        m_uiLabel.enabled = true;
    }

    //======================================================
    // @brief:メッセージを終了させる
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void End()
    {
        m_typeEffect.Reset();

        m_uiLabel.enabled = false;
        m_typeEffect.enabled = false;
    }
}
