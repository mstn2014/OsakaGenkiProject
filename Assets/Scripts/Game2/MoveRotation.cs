//======================================================
// @brief:赤、青、黄色、緑のアイコンの回転をおこなう
//------------------------------------------------------
// @author:前田稚隼
// @param:　m_rotationSpeed 回転スピード
// @return:　なし
//======================================================
using UnityEngine;
using System.Collections;

public class MoveRotation : MonoBehaviour 
{
	public Vector3 m_rotationSpeed = new Vector3(0.0f, 0.0f, 0.5f);

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (m_rotationSpeed);
	}
}
