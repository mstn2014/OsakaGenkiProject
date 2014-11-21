using UnityEngine;
using System.Collections;

public class CheckHitPlayer : MonoBehaviour {

    GameObject m_light_r, m_light_l;    // 左右のライトをとる

	// Use this for initialization
	void Start () {
        m_light_l = this.transform.FindChild("Light_L").gameObject;
        m_light_r = this.transform.FindChild("Light_R").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
