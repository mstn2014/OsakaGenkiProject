using UnityEngine;
using System.Collections;

public class Game1CameraMove : MonoBehaviour {

    [Header("カメラの注視点")]
    public Transform m_target;
    public Transform[] m_point;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move(int no)
    {
        Hashtable parameters = new Hashtable(){
            {"time",0.5f},
            {"easetype",iTween.EaseType.linear},
            {"position",m_point[no]},
            {"looktarget",m_target},
        };

        iTween.MoveTo(this.gameObject, parameters);
    }
}
