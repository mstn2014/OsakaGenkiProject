//======================================================
// @brief:赤、青、黄色、緑のアイコンの回転をおこなう
//------------------------------------------------------
// @author:前田稚隼
// @param:　m_rotationSpeed 回転スピード
// @return:　なし
//======================================================
using UnityEngine;
using System.Collections;

public class Game2MoveRotation : MonoBehaviour 
{
	public Vector3 m_rotationSpeed = new Vector3(0.0f, 0.0f, 0.5f);
    Camera m_camera;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () 
	{
		// transform.Rotate (m_rotationSpeed);
        Hashtable parameters = new Hashtable(){
            {"time",3.0f},
            {"z",1.0f},
            {"easetype",iTween.EaseType.linear},
            {"looptype",iTween.LoopType.loop},
        };
        iTween.RotateBy(this.gameObject, parameters);
	}
}
