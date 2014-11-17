using UnityEngine;
using System.Collections;

public class TimeFrame : MonoBehaviour {
	enum TimeCount{Count0, Count1, Count2, Count3, Count4, Total};
	private TimeCount	m_count;
	private UISprite[]	m_timeCount;	// フレーム.
	private int[]		m_countRatio;	// コウントの比率.
	private int			m_revisionTime;	// 分母補正用.
	private UISprite	m_frame;
	private CountDown	m_timer;

	private float m_timelimit;
	// Use this for initialization
	void Start () {
		GameObject countDown = GameObject.Find("Timer");
		m_timer = countDown.GetComponent<CountDown>();

		m_timeCount = new UISprite[(int)TimeCount.Total];
		m_timeCount [(int)TimeCount.Count0] = GameObject.Find ("TimeCount_0").GetComponent<UISprite>();
		m_timeCount [(int)TimeCount.Count1] = GameObject.Find ("TimeCount_1").GetComponent<UISprite>();
		m_timeCount [(int)TimeCount.Count2] = GameObject.Find ("TimeCount_2").GetComponent<UISprite>();
		m_timeCount [(int)TimeCount.Count3] = GameObject.Find ("TimeCount_3").GetComponent<UISprite>();
		m_timeCount [(int)TimeCount.Count4] = GameObject.Find ("TimeCount_4").GetComponent<UISprite>();
		m_countRatio = new int[(int)TimeCount.Total];
		m_countRatio [(int)TimeCount.Count0] = 8;
		m_countRatio [(int)TimeCount.Count1] = 9;
		m_countRatio [(int)TimeCount.Count2] = 16;
		m_countRatio [(int)TimeCount.Count3] = 9;
		m_countRatio [(int)TimeCount.Count4] = 8;
		InitTimeCount ();

		m_frame = GetComponent<UISprite> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_timer.IsPaused != true) {
			if(m_count == TimeCount.Total){
				return;
			}
			float timelimit = m_timer.IsTimeLimit;
			float time = m_timer.IsTimer;

			// timeが1/50進んだら1/比率(8,9,16)fillAmouthを進ませる.
			m_timeCount[(int)m_count].fillAmount = ((timelimit-time)*timelimit/50) / m_countRatio[(int)m_count];

			// ToDo 2つ目以降の進んだ分の分母を０に戻す作業.

			// if(m_timeCount[m_count].fillAmount == 1){
				// m_revisionTime += m_countRatio[m_count];
				// m_count++;
			//}
		}
	}

	private void InitTimeCount(){
		for (int i=0; i<(int)TimeCount.Total; i++) {
			m_timeCount[i].fillAmount = 0;	
		}
		m_count = TimeCount.Count0;
		m_revisionTime = 0;

	}
}
