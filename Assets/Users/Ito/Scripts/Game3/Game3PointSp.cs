using UnityEngine;
using System.Collections;

public class Game3PointSp : MonoBehaviour {

    iTweenPath m_path;
    iTweenEvent m_tween;
	// Use this for initialization
	void Start () {
        m_path = GameObject.Find("mapPath").GetComponent<iTweenPath>();
        this.transform.position = m_path.nodes[0];
        m_tween = iTweenEvent.GetEvent(this.gameObject, "MoveToMapPath");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
