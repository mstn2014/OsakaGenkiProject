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

    public void SetNum(int num){
        num = Mathf.Clamp(num,0,100);
    }
}
