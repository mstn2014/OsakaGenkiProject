using UnityEngine;
using System.Collections;

//======================================================
// @brief:スクロールする文字を制御するクラス
//------------------------------------------------------
// @author:K.Ito
// @param:none
// @return:none
//======================================================
public class ScrollChar : MonoBehaviour {

    const int dispCharNum = 7;      // 画面に表示する最大文字数
    float m_triggerTime;            // 決定ボタンを押してからの時間
    int index = 1;                  // 現在の選ばれている文字
    InputMgr m_btnState;            // ボタン
    FadeMgr m_fadeMgr;              // シーン遷移
	SoundMgr m_sound;          		// サウンド
    CharState[] m_charState = new CharState[dispCharNum];   // 各文字を制御するスクリプト
    GameObject[] m_keyborad = new GameObject[dispCharNum];  // 各文字のプレハブ

    [Header("設定ファイル")]
    [SerializeField]
    UserRegSetting m_userSetting;
    [Header("名前を入力するオブジェクト")]
    [SerializeField]
    GameObject m_inputName;
    [Header("セーブデータ")]
    SaveMgr m_saveData;

	
	void Start () {
        // 入力クラスの取得
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;
        m_fadeMgr = gs.FadeMgr;
		m_sound = gs.SoundMgr;
        m_saveData = gs.SaveMgr;
        
        // 設定ファイルから初期文字位置を取り出す
        index = m_userSetting.initChar;

        // 各文字を生成する
        for (int i = 0; i < dispCharNum; i++)
        {
            CreateChar(i,i);
        }
	}
	
	
	void Update () {
        // 右に動かす
        if (m_btnState.YellowButtonTrigger)
        {
            foreach (CharState cs in m_charState)
            {
				m_sound.PlaySeMoveCursor();
                cs.MoveLeft();
            }  
        }

        // 左に動かす
        if (m_btnState.GreenButtonTrigger)
        {
            foreach (CharState cs in m_charState)
            {
				m_sound.PlaySeMoveCursor();
                cs.MoveRight();
            }
        }

        // Redボタンで決定の有効をとる
        if (m_btnState.RedButtonTrigger)
        {
			m_sound.PlaySeReturn();
            m_triggerTime = 0;
        }
        else if (m_btnState.RedButtonPress)
        {
            m_triggerTime += Time.deltaTime;
            string str = m_inputName.GetComponentInChildren<UILabel>().text;
            if (m_triggerTime >= m_userSetting.returnTime && str.Length > 0)
            {
                m_saveData.userName = str;
                m_fadeMgr.LoadLevel("game1");
            }
        }

        // Redボタンで文字入力
        if (m_btnState.RedButtonRelease)
        {
            // ボタンを離すとリセット
            m_triggerTime = 0;

            foreach (CharState cs in m_charState)
            {
                if (cs.Pos == 3)
                {
                    if (m_inputName.GetComponentInChildren<UILabel>().text.Length < m_userSetting.nameLength)
                    {
                        m_inputName.GetComponentInChildren<UILabel>().text += cs.Text;
                    }
                }
            } 
        }

        // Blueボタンで文字削除
        if (m_btnState.BlueButtonTrigger)
        {
			m_sound.PlaySeCansel();
            string str = m_inputName.GetComponentInChildren<UILabel>().text;
            if (str.Length > 0)
            {
                m_inputName.GetComponentInChildren<UILabel>().text = str.Substring(0, str.Length - 1);
            }
        }
	}

    //======================================================
    // @brief:スクロールする文字を生成する
    //------------------------------------------------------
    // @author:K.Ito
    // @param:none
    // @return:none
    //======================================================
    public void CreateChar(int arry,int param)
    {
        // 文字を生成
        m_keyborad[arry] = Instantiate(Resources.Load<GameObject>(m_userSetting.prefabPath), m_userSetting.position[param], Quaternion.identity) as GameObject;
        m_keyborad[arry].transform.parent = this.gameObject.transform;

        // 文字のパラメータを設定
        m_keyborad[arry].transform.localPosition = m_userSetting.position[param];
        m_keyborad[arry].transform.localScale = m_userSetting.scale[param];

        // 文字の制御をするスクリプトを取得
        m_charState[arry] = m_keyborad[arry].GetComponent<CharState>();
        m_charState[arry].Pos = param;

        // 一周したときの処理
        m_charState[arry].Index = (index + (arry - (int)(dispCharNum / 2)));
    }
}
