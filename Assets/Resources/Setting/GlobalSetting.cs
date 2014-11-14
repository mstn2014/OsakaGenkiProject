using UnityEngine;
using System.Collections;

public class GlobalSetting : ScriptableObject
{
    [Header("マネージャークラス")]
    public string inputMgrPath;
    public string fadeMgrPath;
    [Header("FPS")]
    public int fps = 60;

    GlobalSetting()
    {
        Application.targetFrameRate = fps;
    }
    
    // 入力クラスを取得
    public InputMgr InputMgr{
        get{
            InputMgr inputMgr;
            GameObject go = GameObject.FindGameObjectWithTag("InputMgr");
            if (go == null)
            {
                go = GameObject.Instantiate(Resources.Load(inputMgrPath)) as GameObject;
            }
            inputMgr = go.GetComponent<InputMgr>();
            return inputMgr;
        }
    }

    // フェードクラスを取得
    public FadeMgr FadeMgr
    {
        get
        {
            FadeMgr fadeMgr;
            GameObject go = GameObject.FindGameObjectWithTag("FadeMgr");
            if (go == null)
            {
                go = GameObject.Instantiate(Resources.Load(fadeMgrPath)) as GameObject;
            }
            fadeMgr = go.GetComponent<FadeMgr>();
            return fadeMgr;
        }
    }
}
