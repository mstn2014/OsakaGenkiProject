using UnityEngine;
using System.Collections;

public class Game1CameraMove : MonoBehaviour {
    [Header("カメラの初期位置")]
    public iTweenPath m_path;

    [Header("カメラの注視点")]
    public Transform m_target;
    public Transform[] m_point;

    public bool IsMove{set;get;}

	// Use this for initialization
	void Start () {
        //transform.position = m_point[0].position;
        //transform.rotation = Quaternion.LookRotation(m_target.position - transform.position);
        transform.position = m_path.nodes[0];
        IsMove = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Move(int no)
    {
        IsMove = true;
        Hashtable parameters = new Hashtable(){
            {"time",0.5f},
            {"easetype",iTween.EaseType.linear},
            {"position",m_point[no]},
            {"looktarget",m_target},
            {"oncomplete","MoveComplete"},
        };

        iTween.MoveTo(this.gameObject, parameters);
    }

    void MoveComplete()
    {
        IsMove = false;
    }

    void InitCamera()
    {
        iTween.LookTo(this.gameObject, m_target.position, 2.0f);
    }
}
