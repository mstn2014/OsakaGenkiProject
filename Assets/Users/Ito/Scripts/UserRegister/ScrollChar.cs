using UnityEngine;
using System.Collections;

public class ScrollChar : MonoBehaviour {

    const int dispCharNum = 7;

    int index = 1;  // 現在の選ばれている文字
    int tempIndex = 1;
    InputMgr m_btnState;
    CharState[] m_charState = new CharState[dispCharNum];
    GameObject[] m_keyborad = new GameObject[dispCharNum];

    [Header("設定ファイル")]
    public UserRegSetting m_userSetting;

	// Use this for initialization
	void Start () {
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_btnState = gs.InputMgr;

        index = tempIndex = m_userSetting.initChar;

        for (int i = 0; i < dispCharNum; i++)
        {
            CreateChar(i,i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        // 右に動かす
        if (m_btnState.YellowButtonTrigger)
        {
            foreach (CharState cs in m_charState)
            {
                cs.MoveLeft();
            }  
        }

        // 左に動かす
        if (m_btnState.GreenButtonTrigger)
        {
            foreach (CharState cs in m_charState)
            {
                cs.MoveRight();
            }
        }
	}

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
