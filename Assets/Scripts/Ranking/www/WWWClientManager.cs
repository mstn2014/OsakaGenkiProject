using UnityEngine;
using System.Collections;
using WWWKit;
using System.Collections.Generic;

public class WWWClientManager{
	//private
	float timeout = 3.0f;
	MonoBehaviour mb;
	WWWClient mClient;

	public WWWClientManager(MonoBehaviour _mb){
		mb = _mb;
		mClient = new WWWClient(mb);
	}

	//-------------------------------------------------------------
	// POSTリクエスト
	// @param
	// @リクエストURL
	// @処理する関数
	//-------------------------------------------------------------
	public void POST( string url,Dictionary<string,string> post,string method ){
		mClient.URL = url;
		foreach(KeyValuePair<string,string> post_arg in post)
		{
			mClient.AddData(post_arg.Key, post_arg.Value);
		}
		mClient.Timeout = timeout;
		mClient.OnDone = (WWW www) => { mb.SendMessage ( method,www ); };
		mClient.Request();
	}

	//-------------------------------------------------------------
	// GETリクエスト
	// @param
	// @リクエストURL
	// @処理する関数
	//-------------------------------------------------------------
	public void GET( string url,string method1, string method2 = null ){
		mClient.URL = url;
		mClient.Timeout = timeout;
		mClient.OnDone = (WWW www) => { mb.SendMessage ( method1,www ); };
		if( method2 != null ){
			mClient.OnFail = (WWW www) => { mb.SendMessage( method2 );};
		}
		mClient.Request();
	}
}
