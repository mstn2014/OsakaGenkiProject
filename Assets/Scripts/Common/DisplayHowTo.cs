using UnityEngine;
using System.Collections;

public class DisplayHowTo : MonoBehaviour {

    public GameObject m_button; // 赤ボタンのゲームオブジェクト

    TweenScale m_scale;         // Tweenスケールのコンポーネント
	// Use this for initialization
	void Start () {
        m_scale = GetComponentInChildren<TweenScale>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play(){
        m_scale.Play(true);
    }

    public void End()
    {
        m_scale.Play(false);
        m_button.SetActive(false);
    }

    void OnComplete()
    {
        if (m_scale.direction == AnimationOrTween.Direction.Forward)
        {
            m_button.SetActive(true);
        }
    }
}
