using UnityEngine;
using System.Collections;

public class Game1TapCount : MonoBehaviour {

    public UISprite m_10;
    public UISprite m_1;

    public Vector3 m_pos1;
    public Vector3 m_pos2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetCount(int num)
    {
        m_1.spriteName = (num % 10).ToString();
        m_10.spriteName = ((num / 10) % 10).ToString();

        if (num < 10)
        {
            m_10.enabled = false;
            m_1.transform.localPosition = m_pos1;
        }
        else
        {
            m_10.enabled = true;
            m_1.transform.localPosition = m_pos2;
        }
    }
}
