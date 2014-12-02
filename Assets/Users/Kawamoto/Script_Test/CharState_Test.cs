using UnityEngine;
using System.Collections;
//===================================================
// 一文字の管理クラス
//===================================================
public class CharState_Test : MonoBehaviour {

	// private
	UILabel m_text;         // Text表示のためのUILabel
	int     m_pos;          // 現在の位置情報
	bool    m_effectFlg;    // Tweenで移動するためのフラグ
	int     m_stringPos;    // UserRegSettingのuserCharの何番目の文字を表示しているか
	bool    m_IsMoveRight;  // 右に動いたことを保存するフラグ
	bool    m_IsMoveLeft;   // 左に動いたことを保存するフラグ
	
	// public
	[Header("設定ファイル")]
	public UserRegSetting m_userSetting;
	
	public string Text
	{
		get { return m_text.text; }
	}
	
	public int Pos
	{
		get { return m_pos; }
		set { m_pos = value; }
	}
	
	// set getアクセサ
	public int Index
	{
		set
		{
			// 範囲チェック
			int length = m_userSetting.userChar.Length;
			if (value >= length )
			{
				m_stringPos = value - length;
			}
			else if (value < 0)
			{
				m_stringPos = value + length;
			}
			else
			{
				m_stringPos = value;
			}
			SetText();
		}
	}
	
	void Start () {
		if (m_text == null) m_text = GetComponent<UILabel>();
	}   
	
	// Update is called once per frame
	void Update () {
		Effect();
	}
	
	//======================================================
	// @brief:Tweenアニメーションを制御する
	//------------------------------------------------------
	// @author:K.Ito
	// @param:none
	// @return:none
	//======================================================
	void Effect()
	{
		if (m_effectFlg)
		{
			StartTween(m_pos);
			m_effectFlg = false;
		}
	}
	
	//======================================================
	// @brief:Tweenアニメーションを開始する
	//------------------------------------------------------
	// @author:K.Ito
	// @param:none
	// @return:none
	//======================================================
	void StartTween(int index)
	{
		// 位置のtween
		Hashtable parameters = new Hashtable();
		parameters.Add("x", m_userSetting.position[index].x);
		parameters.Add("y", m_userSetting.position[index].y);
		parameters.Add("islocal", true);
		parameters.Add("easetype", iTween.EaseType.easeOutCirc);
		parameters.Add("time", m_userSetting.duration);
		parameters.Add("oncomplete", "Finished");
		iTween.MoveTo(this.gameObject, parameters);
		
		// スケールのtween
		parameters.Clear();
		parameters.Add("x", m_userSetting.scale[index].x);
		parameters.Add("y", m_userSetting.scale[index].y);
		parameters.Add("islocal", true);
		parameters.Add("easetype", iTween.EaseType.easeOutCirc);
		parameters.Add("time", m_userSetting.duration);
		iTween.ScaleTo(this.gameObject, parameters);
	}
	
	//======================================================
	// @brief:Tweenアニメーションが終わると呼び出される。
	//------------------------------------------------------
	// @author:K.Ito
	// @param:none
	// @return:none
	//======================================================
	void Finished()
	{
		
	}
	
	//======================================================
	// @brief:UILabelに文字をセットする
	//------------------------------------------------------
	// @author:K.Ito
	// @param:none
	// @return:none
	//======================================================
	void SetText()
	{
		if (m_text == null) m_text = GetComponent<UILabel>();
		m_text.text = m_userSetting.userChar[m_stringPos].ToString();
	}
	
	//======================================================
	// @brief:文字を右に移動させる
	//------------------------------------------------------
	// @author:K.Ito
	// @param:none
	// @return:none
	//======================================================
	public void MoveRight()
	{
		m_pos++;
		if (m_pos >= 7)
		{
			m_pos = 0;
			Index = m_stringPos - 7;
		}
		m_effectFlg = true;
		m_IsMoveRight = true;
		m_IsMoveLeft = false;
	}
	
	//======================================================
	// @brief:文字を左に移動させる
	//------------------------------------------------------
	// @author:K.Ito
	// @param:none
	// @return:none
	//======================================================
	public void MoveLeft()
	{
		m_pos--;
		if (m_pos < 0)
		{
			m_pos = 6;
			Index = m_stringPos + 7;
		}
		m_effectFlg = true;
		m_IsMoveRight = false;
		m_IsMoveLeft = true;
	}
}
