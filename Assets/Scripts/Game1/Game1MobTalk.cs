using UnityEngine;
using System.Collections;

public class Game1MobTalk : MonoBehaviour {

    UISprite m_sprite;
    Camera m_camera;
    public Transform m_target;
	// Use this for initialization
	void Awake() {
        m_sprite = GetComponent<UISprite>();
        
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        transform.parent = GameObject.Find("Panel").transform;
        Vector3 work = m_camera.WorldToViewportPoint(m_target.position);
        transform.localPosition = new Vector3(work.x * 1920 - 1920 / 2, work.y * 1080 - 1080 / 2, work.z);
        transform.localRotation = Quaternion.identity;
        m_sprite.MakePixelPerfect();
        
        m_sprite.enabled = false;
	}

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {

        if (m_target == null)
        {
            m_target = GameObject.Find("DangoPoint").transform;
        }
        Vector3 work = m_camera.WorldToViewportPoint(m_target.position);
        transform.localPosition = new Vector3(work.x * 1920 - 1920 / 2, work.y * 1080 - 1080 / 2 + 100, work.z);
	}
}
