using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

	[Header("TIME")]
	public 	float	m_startTime = 20.0f;	// seconds
	private float	m_timer;
	private bool	m_paused;

	private GameObject	m_lavel;
	private UILabel		m_text;

	// get プロパティ.
	public bool IsPaused{
		get{return m_paused;}
	}
	// Use this for initialization
	void Start () {
		m_paused = true;
		m_lavel = GameObject.Find ("Timer") as GameObject;
		m_text = m_lavel.GetComponent ("UILabel") as UILabel;
		ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
		// 時間表示.
		m_text.text = m_timer.ToString("f0");
		if (m_paused)	return;
		m_timer -= Time.deltaTime;
		if (m_timer <= 0.0f) {
			ResetTimer();

			// 何かの処理.
		}
	}

	public void ResetTimer(){
		m_timer = m_startTime;
		m_paused = true;
	}

	public void StartTimer(){
		m_paused = false;
	}
}
