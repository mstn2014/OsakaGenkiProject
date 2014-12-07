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
	AudioClip bgm_nameInput;		// 名前入力時
    AudioClip bgm_game1;            // ゲーム１
    AudioClip bgm_game2;            // ゲーム２
    AudioClip bgm_game3;            // ゲーム３
    AudioClip bgm_ranking;          // ランキング登録シーン
    AudioClip bgm_ending;           // エンディングシーン
	AudioClip bgm_result;           // リザルト

    // Game2の曲
    AudioClip[] bgm_dance = new AudioClip[5];          // ゲーム２の音楽

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
	AudioClip se_Cuccess;			// 成功音
	AudioClip se_Start;				// スタート音
	AudioClip se_Miss;				// 失敗音
	AudioClip se_Question;			// 問題出題音
	AudioClip se_Ran;				// 走り音
	AudioClip se_Hanabi;			// 花火音

    // time
    float time;

    public bool mute = false;

    AudioSource audioSourceBGM;

    AudioSource[] audioSourceSE = new AudioSource[16];

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);

        bgm_title = Resources.Load<AudioClip>(bgmPath+"title");
		bgm_bigIvent = Resources.Load<AudioClip>(bgmPath+"BigIvent1BGM");
		bgm_nameInput = Resources.Load<AudioClip>(bgmPath+"name_inputBGM");
		bgm_game1 = Resources.Load<AudioClip>(bgmPath+"Game_1BGM");
		bgm_game2 = Resources.Load<AudioClip>(bgmPath+"Game_2BGM");
		bgm_game3 = Resources.Load<AudioClip>(bgmPath+"Game_3BGM");
		bgm_result = Resources.Load<AudioClip>(bgmPath+"result");

        
        bgm_dance[0] = Resources.Load<AudioClip>(bgmPath + "bonodori");
        bgm_dance[1] = Resources.Load<AudioClip>(bgmPath + "hula");
        bgm_dance[2] = Resources.Load<AudioClip>(bgmPath + "belly");
        bgm_dance[3] = Resources.Load<AudioClip>(bgmPath + "samba");
        bgm_dance[4] = Resources.Load<AudioClip>(bgmPath + "flamenco");

        se_return = Resources.Load<AudioClip>(sePath+"return");
        se_cancel = Resources.Load<AudioClip>(sePath+"cancel");
		se_moveCursor = Resources.Load<AudioClip>(sePath+"serect");
		se_Miss = Resources.Load<AudioClip>(sePath+"miss");
		se_countDown = Resources.Load<AudioClip>(sePath+"count");
		se_Start = Resources.Load<AudioClip>(sePath+"start");
		se_Cuccess = Resources.Load<AudioClip>(sePath+"cuccess");
		se_Question = Resources.Load<AudioClip>(sePath+"question");
		se_handclap = Resources.Load<AudioClip>(sePath+"kansei");
		se_Ran = Resources.Load<AudioClip>(sePath+"ran");
		se_Hanabi = Resources.Load<AudioClip>(sePath+"hanabi");

        //audioSourceBGM = GetComponent<AudioSource> ();
        audioSourceBGM = this.gameObject.AddComponent<AudioSource>();

        for (int i = 0; i < 16; i++)
        {
            audioSourceSE[i] = this.gameObject.AddComponent<AudioSource>();
            audioSourceSE[i].mute = mute;
        }

        audioSourceBGM.loop = true;

        // ミュートにするかどうか
        audioSourceBGM.mute = mute;
    }
	// タイトル
    public void PlayTitleBGM()
    {
        audioSourceBGM.clip = bgm_title;
        audioSourceBGM.Play();
    }
	// 大イベント１
	public void PlayBigIvent()
	{
		audioSourceBGM.clip = bgm_bigIvent;
		audioSourceBGM.Play();
	}
	// 名前入力
	public void PlayNameInput()
	{
		audioSourceBGM.clip = bgm_nameInput;
		audioSourceBGM.Play();
	}
	// ゲーム1
	public void PlayGame_1()
	{
		audioSourceBGM.clip = bgm_game1;
		audioSourceBGM.Play();
	}	
	// ゲーム2
	public void PlayGame_2()
	{
		audioSourceBGM.clip = bgm_game2;
		audioSourceBGM.Play();
	}	
	// ゲーム3
	public void PlayGame_3()
	{
		audioSourceBGM.clip = bgm_game3;
        audioSourceBGM.volume = 0.2f;
		audioSourceBGM.Play();
	}
	// リザルト
	public void PlayResult()
	{
		audioSourceBGM.clip = bgm_result;
		audioSourceBGM.Play();
	}

    public void PlayDanceBGM(int no)
    {
        audioSourceBGM.clip = bgm_dance[no];
        audioSourceBGM.Play();
    }

    public void StopBGM()
    {
        this.audioSourceBGM.Stop();
        this.audioSourceBGM.clip = null;
    }

	public void FadeStopBGM(float time)
	{
		iTween.ValueTo(this.gameObject,iTween.Hash("from", 1, "to", 0, "time", time,"onupdate", "Volume_change"));
	}

	public void FadePlayBGM(float time)
	{
		iTween.ValueTo(this.gameObject,iTween.Hash("from", 0, "to", 1, "time", time,"onupdate", "Volume_change"));
	}

	void Volume_change(float value)
	{
		this.audioSourceBGM.volume = value;
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

	public void PlaySeMoveCursor()
	{
		audioSourceSE[3].PlayOneShot(se_moveCursor);
	}

	public void PlaySeCountDown()
	{
		audioSourceSE[5].PlayOneShot(se_countDown);
	}

	public void PlaySeHandclap()
	{
		audioSourceSE[9].PlayOneShot(se_handclap);
	}

	public void PlaySeCuccess()
	{
		audioSourceSE[10].PlayOneShot(se_Cuccess);
	}

	public void PlaySeStart()
	{
		audioSourceSE[11].PlayOneShot(se_Start);
	}

	public void PlaySeMiss()
	{
		audioSourceSE[12].PlayOneShot(se_Miss);
	}

	public void PlaySeQuestion()
	{
		audioSourceSE[13].PlayOneShot(se_Question);
	}

	public void PlaySeRan()
	{
		audioSourceSE[14].PlayOneShot(se_Ran);
	}

	public void PlaySeHanabi()
	{
		audioSourceSE[15].PlayOneShot(se_Hanabi);
	}
}