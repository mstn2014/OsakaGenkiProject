using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game2Gauge : MonoBehaviour {

    public ResultSetting m_resultSetting;   // リザルトの設定ファイル
    public Game2Setting m_gameSetting;      // ゲーム２の設定ファイル
    public ScoreMgr m_scoreMgr;
    public UISprite m_gauge;
    public UISprite m_lv;                   // レベルのスプライト
    public TweenScale m_tweenScale;         // レベル文字の演出
    public TweenRotation m_tweenRotate;     // レベル文字の演出

    float m_tmpScore;

    int state;                      // 盛り上がりイベントのステート
    int eventScore;                 // 盛り上がりイベントが入るスコア
    List<int> constEventScore = new List<int>();            // イベントスコアの閾値

	// Use this for initialization
	void Start () {
        m_gauge.fillAmount = 0.0f;
        state = 1;
        eventScore = m_gameSetting.Event1Score;
        m_tmpScore = m_scoreMgr.Score;
        constEventScore.Add(m_gameSetting.Event1Score);
        constEventScore.Add(m_gameSetting.Event2Score);
        constEventScore.Add(m_gameSetting.Event3Score);
        constEventScore.Add(m_gameSetting.Event4Score);
        constEventScore.Add(m_gameSetting.Event5Score);
	}
	
	// Update is called once per frame
	void Update () {

        float fill = m_scoreMgr.Score / eventScore;

        if( fill >= 1.0f && state < constEventScore.Count){
            eventScore = constEventScore[state++];
            fill = m_scoreMgr.Score / eventScore;

            // ここにスプライトを変える処理を書く
            m_lv.spriteName = "game2_gauge_lv" + state.ToString();
            m_tweenRotate.Reset();
            m_tweenRotate.Play(true);
        }
        else if (state == constEventScore.Count)
        {
            fill = 1.0f;
            state++;
            m_lv.spriteName = "game2_gauge_lv" + state.ToString();
            m_lv.MakePixelPerfect();       
        }
        fill = Mathf.Clamp(fill, 0.0f, 1.0f);
        m_gauge.fillAmount = fill;
        m_gauge.fillAmount *= (1.0f / constEventScore.Count * state);

        // ドクンの演出
        if (m_tmpScore != m_scoreMgr.Score)
        {
            m_lv.MakePixelPerfect();
            m_tweenScale.to = m_lv.transform.localScale;
            m_tweenScale.from = m_lv.transform.localScale * 2.0f;
            m_tweenScale.Reset();
            m_tweenScale.Play(true);
        }

        m_tmpScore = m_scoreMgr.Score;
	}
}
