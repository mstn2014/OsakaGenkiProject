using UnityEngine;
using System.Collections;

public class TitleLogo : MonoBehaviour {
    // コンポーネント関連
    UISprite m_sprite;

    // 設定ファイル
    TitleSetting m_setting;

	// Use this for initialization
	void Start () {
        // コンポーネントの取得
        m_sprite = GetComponent<UISprite>();

        // 設定ファイルの読み込み
        m_setting = Resources.Load<TitleSetting>("Setting/TitleSetting");

        BeginRotaion();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void FadeOut()
    {
        BeginAlpha();
        BeginScale();
        //iTween.Stop("RotateTo");
    }

    
    void BeginScale()
    {
        // スケールのtween
        Hashtable parameters = new Hashtable();
        parameters.Add("x", transform.localScale.x * m_setting.scale );
        parameters.Add("y", transform.localScale.y * m_setting.scale );
        parameters.Add("islocal", true);
        parameters.Add("easetype", iTween.EaseType.easeInOutQuad);
        parameters.Add("time", m_setting.disappearSpeed);
        iTween.ScaleTo(this.gameObject, parameters);
    }

    void BeginAlpha()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1.0f, "to", 0.0f, "time", m_setting.disappearSpeed, "onupdate", "UpdateHandler"));
    }
    void UpdateHandler(float value)
    {
        m_sprite.color = new Color(m_sprite.color.r,m_sprite.color.g,m_sprite.color.b,value);
    }

    void BeginRotaion()
    {
        Hashtable parameters = new Hashtable();
        parameters.Add("y", 180); 
        parameters.Add("time", m_setting.rotateTime);
        parameters.Add("easetype", iTween.EaseType.easeInOutQuad);
        parameters.Add("looptype", iTween.LoopType.pingPong);
        iTween.RotateTo(this.gameObject, parameters);
    }
}
