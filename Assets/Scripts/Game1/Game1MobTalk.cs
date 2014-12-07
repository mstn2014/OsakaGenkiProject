using UnityEngine;
using System.Collections;

public class Game1MobTalk : MonoBehaviour {

    Camera m_camera;
    SpriteRenderer m_renderer;  // スプライトのレンダラー
	// Use this for initialization
	void Awake() {
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_renderer = GetComponent<SpriteRenderer>();
	}

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 work = new Vector3(-m_camera.transform.position.x + transform.position.x, -m_camera.transform.position.y + transform.position.y, -m_camera.transform.position.z + transform.position.z);

        transform.LookAt( work );
	}
}
