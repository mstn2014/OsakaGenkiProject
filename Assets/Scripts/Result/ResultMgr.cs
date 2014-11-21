﻿using UnityEngine;
using System.Collections;

public class ResultMgr : MonoBehaviour {

    // コンポーネント関連
    SaveData m_save;            // 一時セーブデータ
    UISprite m_rankSprite;      // ランクを表示するスプライト
    UILabel m_pointLabel;       // 得点を表示するラベル
    
    // ローカル変数
    SaveData.eState m_state;    // どのシーンから飛んできたか。どのシーンの結果を返すか判断するフラグ。
    float m_score;              // ゲームのスコア
    float m_maxPoint;           // そのゲームの最大スコア
    ResultSetting.CRank[] m_rank;   // ランクが定義された定数
    ResultSetting m_setting; // 設定ファイル
   
	// Use this for initialization
	void Start () {
        m_save = Resources.Load<SaveData>("SaveData/SaveData");
        m_state = m_save.gameState;
        m_setting = Resources.Load<ResultSetting>("Setting/ResultSetting");
        m_rankSprite = GameObject.Find("Rank").GetComponent<UISprite>();
        m_pointLabel = GameObject.Find("Point").GetComponent<UILabel>();
        switch (m_state)
        {
            case SaveData.eState.GAME1:
                m_score = m_save.game1Score;
                m_maxPoint = m_setting.game1MaxPoint;
                m_rank = m_setting.game1Rank;
                break;
            case SaveData.eState.GAME2:
                m_score = m_save.game2Score;
                m_maxPoint = m_setting.game2MaxPoint;
                m_rank = m_setting.game2Rank;
                break;
            case SaveData.eState.GAME3:
                m_score = m_save.game3Score;
                m_maxPoint = m_setting.game3MaxPoint;
                m_rank = m_setting.game3Rank;
                break;
        }
        m_rankSprite.spriteName = CalcRank();
        m_pointLabel.text = m_score.ToString() + "pt";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    string CalcRank()
    {
        float point = m_score / m_maxPoint;
        foreach (ResultSetting.CRank rank in m_rank)
        {
            if (point >= rank.value)
            {
                return "Rank" + rank.key;
            }
        }
        return string.Empty;
    }
}
