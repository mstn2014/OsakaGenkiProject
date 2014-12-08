using UnityEngine;
using System.Collections;

public class Game1MobMgr : MonoBehaviour {

    Game1MobController[] m_mobController;   // モブのコントローラー
    public Transform m_player;              // プレイヤー
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void GetController()
    {
        m_mobController = GetComponentsInChildren<Game1MobController>();
    }

    public void LookPlayer(float radius)
    {
        GetController();
        foreach (Game1MobController mc in m_mobController)
        {
            if (Vector2.Distance(new Vector2(m_player.position.x, m_player.position.z), new Vector2(mc.transform.position.x, mc.transform.position.z)) <= radius)
            {
                mc.LookTarget(m_player);
                mc.StopWalk();
            }
        }
    }

    public void StopWalk()
    {
        GetController();
        foreach (Game1MobController mc in m_mobController)
        {
            mc.StopWalk();
        }
    }
}
