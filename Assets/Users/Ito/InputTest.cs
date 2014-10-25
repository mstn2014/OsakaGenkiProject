using UnityEngine;
using System.Collections;

public class InputTest : MonoBehaviour {

    InputMgr m_btnState;
    int cnt = 0;
	// Use this for initialization
	void Start () {
        // Fade 生成or見つける
        GameObject go = GameObject.FindGameObjectWithTag("InputMgr");
        if (go == null)
        {
            go = GameObject.Instantiate(Resources.Load("Singleton/InputMgr")) as GameObject;
        }
        m_btnState = go.GetComponent<InputMgr>();
	}
	
	// Update is called once per frame6
	void Update () {
        if (m_btnState == null)
        {
            Debug.Log("m_btnStateがぬるぽです。");
            return;
        }

        if (m_btnState.RedButtonTrigger)
        {
            Debug.Log("押した回数:" + (++cnt).ToString() );
        }
	}
}
