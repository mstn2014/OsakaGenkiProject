using UnityEngine;
using System.Collections;

public class TimeFrame : MonoBehaviour {
	private UISprite	m_frame;
	private CountDown	m_timer;

	private float m_timelimit;
	// Use this for initialization
	void Start () {
		GameObject countDown = GameObject.Find("Timer");
		m_timer = countDown.GetComponent<CountDown>();

		m_frame = GetComponent<UISprite> ();
	}
	
	// Update is called once per frame
	void Update () {
		float time = m_timer.IsTimer;

		m_frame.fillAmount = (3.0f - time) / 3.0f;
	}
}
