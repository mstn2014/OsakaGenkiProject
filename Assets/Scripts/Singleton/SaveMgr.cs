using UnityEngine;
using System.Collections;

public class SaveMgr : SingletonMonoBehaviourFast<SaveMgr>
{

    [Header("ユーザー名")]
    public string userName = "";
    [Header("スコア")]
    public float game1Score = 0;
    public float game2Score = 0;
    public float game3Score = 0;
    [Header("ユーザーランク")]
    public int userRank = 0;

    public enum eState { GAME1, GAME2, GAME3, ALL };
    [Header("ゲームステート")]
    public eState gameState;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        // Unityではシーンを切り替えるとGameObject等は全部破棄される
        // 引数に指定したGameObjectは破棄されなくなり
        // Scene切替時にそのまま引き継がれます
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
