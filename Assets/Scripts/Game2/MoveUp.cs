using UnityEngine;
using System.Collections;

public class MoveUp : MonoBehaviour
{
	private GameObject m_bufGameObj;		//	移動する階層先のオブジェの名前格納用.
	private bool	   m_moveFlg;			//	移動フラグ.
	public  Vector3    m_upSpeed = new Vector3(0.0f, 0.05f, 0.0f); //	上に上昇するスピード.
	public	int		   m_hitNumber;			//	何番目に当たったか（複数のボタンが同時に判定されるのを防ぐ）.

	// Use this for initialization
	void Start () {
		m_bufGameObj = GameObject.Find("DestroyButton");
		m_moveFlg = false;
	}
	
	//======================================================
	// @brief:m_moveFlg（移動フラグ）がONなら上へ移動.
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　なし.
	// @return:　なし.
	//======================================================
	void Update (){
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

	//======================================================
	// @brief:m_moveFlg（移動フラグ）をONに.
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　なし.
	// @return:　なし.
	//======================================================
	public void MoveUpButton()
	{
		m_moveFlg = true;
		transform.parent = m_bufGameObj.transform;	//	回転から外す.
		//	コライダー消す.
	}


	//======================================================
	// @brief:ボタンを消す（タイミングは合っているが、押すボタンを間違えた場合）.
	//------------------------------------------------------
	// @author:前田稚隼.
	// @param:　なし.
	// @return:　なし.
	//======================================================
	public void DestroyButton()
	{
		Destroy (this.gameObject);
	}
}
