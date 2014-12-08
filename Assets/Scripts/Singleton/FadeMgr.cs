using UnityEngine;
using System.Collections;

public class FadeMgr : SingletonMonoBehaviourFast<FadeMgr>
{
    /// <summary>暗転用黒テクスチャ</summary>
    private Texture2D blackTexture;
    /// <summary>フェード中の透明度</summary>
    private float fadeAlpha = 0;
    /// <summary>フェード中かどうか</summary>
    private bool isFading = false;

	int finish_flg=0;
    const float interval = 0.3f;
	SoundMgr m_sound;          		// サウンド

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);

		//ここで黒テクスチャ作る
		this.blackTexture = new Texture2D(1,1);
		//this.blackTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
		this.blackTexture.SetPixel(0, 0, new Color(0, 0, 0, 1));
		this.blackTexture.Apply();

    }

	void Start () {
		// 入力クラスの取得
		GlobalSetting gs = Resources.Load<GlobalSetting>("Setting/GlobalSetting");
		m_sound = gs.SoundMgr;
	}

	// ゲーム終了用
	private IEnumerator Finish(float interval){
		//だんだん暗く
		this.isFading = true;
		float time = 0;
		m_sound.FadeStopBGM(0.5f);
		while (time <= interval)
		{
			this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}
	}

	public void FinishLevel(float interval)
	{
		finish_flg = 1;
		StartCoroutine(Finish(interval));
	}

	public bool IsFading(){
		return isFading;
	}

	public float GetGUIColorAlpha(){
		return this.fadeAlpha;
	}

    public void OnGUI()
    {
        if (!this.isFading)
            return;

		if(finish_flg==0){
	        //透明度を更新して黒テクスチャを描画
	        GUI.color = new Color(1, 1, 1, this.fadeAlpha);
	        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.blackTexture);
		}
		else if(finish_flg==1){
			//透明度を更新して黒テクスチャを描画
			GUI.color = new Color(0, 0, 0, this.fadeAlpha);
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.blackTexture);
		}
	}

    /// <summary>
    /// 画面遷移
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    public void LoadLevel(string scene)
    {
        StartCoroutine(TransScene(scene));
    }

	/// <summary>
	/// フェードインからのフェードアウト
	/// </summary>
	/// <param name='scene'>シーン名</param>
	/// <param name='interval'>暗転にかかる時間(秒)</param>
	public void FadeInOut(float waitTime,float interval,float displayTime,bool isEnd)
	{
		StartCoroutine(Disp(waitTime,interval,displayTime,isEnd));
	}


    /// <summary>
    /// シーン遷移用コルーチン
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(string scene)
    {
        if (!isFading)
        {
            //だんだん暗く
            this.isFading = true;
            float time = 0;
            while (time <= interval)
            {
                time += Time.deltaTime;
                this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
                
                yield return 0;
            }
            time = 0;

            // 暗転してから一秒待つ
            yield return new WaitForSeconds(1.0f);

            //シーン切替
            Application.LoadLevel(scene);

            
		    m_sound.FadePlayBGM(0.5f);

            //だんだん明るく

            while (time <= interval)
            {
                time += Time.deltaTime;
                this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
                yield return 0;
            }

            this.isFading = false;
        }
    }

	/// <summary>
	/// シーン遷移用コルーチン
	/// </summary>
	/// <param name='scene'>シーン名</param>
	/// <param name='interval'>暗転にかかる時間(秒)</param>
	private IEnumerator Disp(float waitTime, float interval,float displayTime,bool isEnd)
	{
		this.isFading = true;
		this.fadeAlpha = 1.0f;
		// waitTime秒」待ってから開始
		yield return new WaitForSeconds( waitTime );
		m_sound.FadePlayBGM(0.5f);

		//だんだん明るく
		float time = 0;
		while (time <= interval)
		{
			this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}
		
		yield return new WaitForSeconds( displayTime );
		m_sound.FadeStopBGM(0.5f);

		if( !isEnd ){
			//だんだん暗く
			time = 0;
			while (time <= interval)
			{
				this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
				time += Time.deltaTime;
				yield return 0;
			}
		}else if( isEnd ){
			this.isFading = false;
		}


		//this.isFading = false;
	}
}
