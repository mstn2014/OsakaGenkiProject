using UnityEngine;
using System.Collections;

public class Game1MobTalk : MonoBehaviour {

    Camera m_camera;
    Texture m_dango;    // ダンゴーテクスチャ
	// Use this for initialization
	void Awake() {
        
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        transform.LookAt( m_camera.transform.position - transform.position );
        m_dango = Resources.Load<Texture>("Prefab/Game1/game1_ui");
        /*Vector3 work = m_camera.WorldToViewportPoint(transform.position);
        transform.localPosition = new Vector3(work.x * Screen.width, work.y * Screen.height, work.z);
        transform.localRotation = Quaternion.identity;*/
	}

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
        //Vector3 work = m_camera.WorldToViewportPoint(m_target.position);
        //transform.localPosition = new Vector3(work.x * 1920, work.y * 1080 + 100, work.z);
        // transform.LookAt( -m_camera.transform.position + transform.parent.position );
	}

    void OnGUI()
    {
        Vector3 work = m_camera.WorldToViewportPoint(transform.parent.position);
        work = new Vector3(work.x * Screen.width, work.y * Screen.height, work.z);
        GUI.DrawTexture(new Rect(work.x,work.y,256 * work.z / 1000,256*work.z / 1000), m_dango);
    }
}
