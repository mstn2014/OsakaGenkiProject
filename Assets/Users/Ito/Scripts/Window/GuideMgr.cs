using UnityEngine;
using System.Collections;

public class GuideMgr : MonoBehaviour {

    GuideController m_guideController;  // ガイドコントローラ

	// Use this for initialization
	void Awake () {
        m_guideController = this.GetComponentInChildren<GuideController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CallGuide()
    {
        m_guideController.Call();
    }

    public void EndGuide()
    {
        m_guideController.End();
    }
}
