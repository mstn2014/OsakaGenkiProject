using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

	private float	m_timer;
	private bool	m_paused;
	
	private Question	m_quest;
	//private TimeFrame	m_timeFrame;
	private UILabel		m_text;

	// Game1共通設定.
	private Game1_Setting GAME1;

	// get プロパティ.
	public bool IsPaused{
		get{return m_paused;}
	}
	public float IsTimer{
		get{return m_timer;}
	}
	public float IsTimeLimit {
		get{return GAME1.Round_TimeLimit[m_quest.IsNowRound-1];}
	}

	// Use this for initialization
	void Start () {
		// Game1共通設定.
		GAME1 = Resources.Load<Game1_Setting>("Setting/Game1_Setting");

		m_quest = GetComponent<Question>();
		m_text = GameObject.Find ("Timer").gameObject.GetComponent ("UILabel") as UILabel;

		m_paused = true;
	}
	
	// Update is called once per frame
	void Update () {
		// 時間表示(デバッグ用).
		m_text.text = m_timer.ToString("f1");
		if (m_paused)	return;
		m_timer -= Time.deltaTime;
		if (m_timer <= 0.0f) {
			ResetTimer();

			// 何かの処理.
		}
	}

	public void ResetTimer(){		
		m_timer = GAME1.Round_TimeLimit[m_quest.IsNowRound-1];
		m_paused = true;
	}

	public void StartTimer(){
		m_paused = false;
	}
}
