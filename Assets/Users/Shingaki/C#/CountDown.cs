using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

	[Header("TIME")]
	public 	float	m_startTime = 20.0f;	// ToDo:最終的には消す
	private float[] m_timelimit;			// 制限時間格納用.
	private float	m_timer;
	private float	m_timelimit_test;			// 制限時間
	private bool	m_paused;

	private GameObject	m_lavel;
	private UILabel		m_text;
	private Question	m_quest;

	// Game1共通設定
	private Game1_Setting GAME1;

	// get プロパティ.
	public bool IsPaused{
		get{return m_paused;}
	}
	public float IsTimer{
		get{return m_timer;}
	}
	/*
	public float IsTimeLimit{
		get{return m_timelimit_test;}
	}4*/
	// Use this for initialization
	void Start () {
		// Game1共通設定
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");

		// ラウンドごとの時間格納.
		m_timelimit = new float[GAME1.MaxQuestNum - (GAME1.MinQuestNum-1)];
		SetTimelimit();

		m_paused = true;
		GameObject obj = GameObject.Find ("GameMain") as GameObject;
		m_quest = obj.GetComponent<Question>();
		m_lavel = GameObject.Find ("Timer") as GameObject;
		m_text = m_lavel.GetComponent ("UILabel") as UILabel;
		ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
		// 時間表示.
		m_text.text = m_timer.ToString("f1");
		if (m_paused)	return;
		m_timer -= Time.deltaTime;
		if (m_timer <= 0.0f) {
			ResetTimer();

			// 何かの処理.
		}
	}

	public void ResetTimer(){		
		m_timer = m_timelimit[m_quest.IsNowRound-1];
		m_paused = true;
	}

	public void StartTimer(){
		m_paused = false;
	}

	private void SetTimelimit(){
		m_timelimit [0] = GAME1.Round1_TimeLimit;
		m_timelimit [1] = GAME1.Round2_TimeLimit;
		m_timelimit [2] = GAME1.Round3_TimeLimit;
		m_timelimit [3] = GAME1.Round4_TimeLimit;
		m_timelimit [4] = GAME1.Round5_TimeLimit;
		m_timelimit [5] = GAME1.Round6_TimeLimit;
		m_timelimit [6] = GAME1.Round7_TimeLimit;
		m_timelimit [7] = GAME1.Round8_TimeLimit;
	}
}
