using UnityEngine;
using System.Collections;

public class bigIvent2SE : MonoBehaviour {

    SoundMgr m_sound;
    public bool isPlay = true; 
	// Use this for initialization
	void Start () {
        // 共通設定の呼び出し.
        GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
        m_sound = gs.SoundMgr;
        StartCoroutine(HandClap());
        StartCoroutine(Hanabi());
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator HandClap()
    {
        while (isPlay)
        {
            m_sound.PlaySeHandclapLast();
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        }
    }

    IEnumerator Hanabi()
    {
        while (isPlay)
        {
            m_sound.PlaySeHanabi();
            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
        }
    }
}
