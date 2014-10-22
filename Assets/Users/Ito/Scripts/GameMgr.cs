using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {

    InputMgr m_btnState;            // 入力インスタンス

    WindowMgr m_windowMgr;          // ウィンドウマネージャー

	// Use this for initialization
	void Start () {
        // インプットのプレハブを見つけるor生成
        GameObject go = GameObject.FindGameObjectWithTag("InputMgr");
        if (go == null)
        {
            go = GameObject.Instantiate(Resources.Load("Singleton/InputMgr")) as GameObject;
        }
        m_btnState = go.GetComponent<InputMgr>();

        m_windowMgr = GameObject.Find("WindowMgr").GetComponent<WindowMgr>();
	}
	
	// Update is called once per frame
	void Update () {
        // 1
	    if (m_btnState.RedButtonTrigger)
        {
            Debug.Log("RedButtonが押されました。");
            m_windowMgr.OpenWindow();
        }
        
        // 2
        if (m_btnState.GreenButtonTrigger)
        {
            Debug.Log("GreenButtonが押されました。");
            m_windowMgr.CloseWindow();
        }

        // 3
        if (m_btnState.BlueButtonTrigger)
        {
            string str = "BlueButtonが押されました。";
            Debug.Log(str);
            m_windowMgr.Text = str;
        }

        // 4
        if (m_btnState.YellowButtonTrigger)
        {
            string str = "YellowButtonが押されました。";
            Debug.Log(str);
            m_windowMgr.Text = str;
        }

        
	}
}
