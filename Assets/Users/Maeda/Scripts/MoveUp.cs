using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour
{
	PushButtonTest 	   m_getDispLabel;		//	表示するラベル（判定結果).
	private GameObject m_bufLabel;			//	貰ってくる判定ラベル格納用
	private GameObject m_bufGameObj;		//	移動する階層先のオブジェの名前格納用
	private bool	   m_moveFlg;			//	移動フラグ
	public  Vector3    m_upSpeed = new Vector3(0.0f, 0.05f, 0.0f); //	上に上昇するスピード
	//public  float	   m_moveVal;			//	移動量

	// Use this for initialization
	void Start () {
		m_bufLabel = GameObject.Find ("ring");
		m_bufGameObj = GameObject.Find("DestroyButton") as GameObject;
		m_moveFlg = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_moveFlg == true)
		{
			transform.position += m_upSpeed;

			// 画面外で消去
			Vector3 view_pos = Camera.main.WorldToViewportPoint(transform.position);
			if( view_pos.x < -0.5f || view_pos.x > 1.5f ||
			    view_pos.y < -0.5f || view_pos.y > 1.5f ){
				Destroy( this.gameObject );
			}
		}
	}

	void OnTriggerEnter2D (Collider2D button)
	{
		//	判定結果をもらう.
		m_getDispLabel = m_bufLabel.GetComponent<PushButtonTest>();

		if (m_getDispLabel.dispLabel == "safe")
		{
			m_moveFlg = true;
			transform.parent = m_bufGameObj.transform;	//	回転から外す
		}
	}
}
