using UnityEngine;
using System.Collections;

public class ResultPercent : MonoBehaviour {

    public UISprite m_100;
    public UISprite m_10;
    public UISprite m_1;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetNum(float num){
        num = Mathf.Clamp(num,0.0f,100.0f);
        m_100.spriteName = ((int)(num / 100) % 10).ToString();
        m_10.spriteName = ((int)(num / 10) % 10).ToString();
        m_1.spriteName = ((int)(num) % 10).ToString();

        m_100.MakePixelPerfect();
        m_10.MakePixelPerfect();
        m_1.MakePixelPerfect();

        if (num == 100)
        {
            m_100.enabled = true;
        }
        else
        {
            m_100.enabled = false;
        }
    }
}
