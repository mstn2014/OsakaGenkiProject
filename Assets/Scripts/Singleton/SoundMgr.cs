using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SoundMgr : SingletonMonoBehaviourFast<SoundMgr>
{
    const string bgmPath = "Sound/BGM/";
    const string sePath = "Sound/SE/";

    // BGM
    AudioClip bgm_title;            // タイトル
    AudioClip bgm_smallIvent;       // 小イベント
    AudioClip bgm_bigIvent;         // 大イベント
    AudioClip bgm_game1;            // ゲーム１
    AudioClip bgm_game2;            // ゲーム２
    AudioClip bgm_game3;            // ゲーム３
    AudioClip bgm_ranking;          // ランキング登録シーン
    AudioClip bgm_ending;           // エンディングシーン

    // SE
    AudioClip se_return;			// 決定音
    AudioClip se_cancel;			// キャンセル音
    AudioClip se_delete;		    // 文字消去音
    AudioClip se_moveCursor;		// カーソル移動音
    AudioClip se_input;				// 文字入力時の決定音
    AudioClip se_countDown;			// カウントダウン
    AudioClip se_moveCharctor;	    // キャラクター移動音
    AudioClip se_displayWindow;	    // メッセージウィンドウ表示音
    AudioClip se_cheer;             // 歓声
    AudioClip se_handclap;          // 拍手			

    // time
    float time;

    AudioSource audioSourceBGM;

    AudioSource[] audioSourceSE = new AudioSource[10];

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);

        bgm_title = Resources.Load<AudioClip>(bgmPath+"title");

        se_return = Resources.Load<AudioClip>(sePath+"return");
        se_cancel = Resources.Load<AudioClip>(sePath+"cancel");

        //audioSourceBGM = GetComponent<AudioSource> ();
        audioSourceBGM = this.gameObject.AddComponent<AudioSource>();

        for (int i = 0; i < 10; i++)
        {
            audioSourceSE[i] = this.gameObject.AddComponent<AudioSource>();
        }

        audioSourceBGM.loop = true;
    }

    public void PlayTitleBGM()
    {
        audioSourceBGM.clip = bgm_title;
        audioSourceBGM.Play();
    }

    public void StopBGM()
    {
        this.audioSourceBGM.Stop();
        this.audioSourceBGM.clip = null;
    }

    // ここからSE
    public void PlaySeReturn()
    {
        audioSourceSE[0].PlayOneShot(se_return);
    }

    public void PlaySeCansel()
    {
        audioSourceSE[1].PlayOneShot(se_cancel);
    }
}