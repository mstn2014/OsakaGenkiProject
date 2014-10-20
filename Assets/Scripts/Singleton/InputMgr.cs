//#define _DEBUG
using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class InputMgr: SingletonMonoBehaviourFast<InputMgr> 
{
	GameObject go;
	FadeMgr fade;

	public void Awake()
	{
		if(this != Instance)
		{
			Destroy(this);
			return;
		}
		// Unityではシーンを切り替えるとGameObject等は全部破棄される
		// 引数に指定したGameObjectは破棄されなくなり
		// Scene切替時にそのまま引き継がれます
		DontDestroyOnLoad(this);

		// Fade 生成or見つける
		go = GameObject.FindGameObjectWithTag("FadeMgr");
		if (go == null) {
            go = GameObject.Instantiate(Resources.Load("Singleton/FadeMgr")) as GameObject;
		}
		fade = go.GetComponent<FadeMgr>();
	}	

    //-----------XBOXコントローラキーコード--------------S
    //X(□)：KeyCode.JoystickButton2
    //Y(△)：KeyCode.JoystickButton3
    //A(×)：KeyCode.JoystickButton0
    //B(○)：KeyCode.JoystickButton1

    //Ｌ１：KeyCode.JoystickButton4
    //Ｒ１：KeyCode.JoystickButton5
    //Ｌ２：反応なし
    //Ｒ２：反応なし

    //Ｌ３：KeyCode.JoystickButton8
    //Ｒ３：KeyCode.JoystickButton
    //---------------------------------------------------E


    //--------------- Redボタン関係 ---------------------------S         デバッグ用に１キー割り当て

    public bool RedButtonPress{
        get{
            return Input.GetKey(KeyCode.JoystickButton1) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha1);
        }
    }

    public bool RedButtonTrigger{
        get{
            return Input.GetKey(KeyCode.JoystickButton1) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha1);
        }
    }

    public bool RedButtonRelease{
        get{
            return Input.GetKey(KeyCode.JoystickButton1) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha1);
        }
    }

    //--------------- Redボタン関係 ---------------------------E


    //--------------- Greenボタン関係 -------------------------S         デバッグ用に２キー割り当て

    public bool GreenButtonPress
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton0) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha2);
        }
    }

    public bool GreenButtonTrigger
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton0) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha2);
        }
    }

    public bool GreenButtonRelease
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton0) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha2);
        }
    }

    //--------------- Greenボタン関係 -------------------------E

    //--------------- Blueボタン関係 --------------------------S         デバッグ用に３キー割り当て

    public bool BlueButtonPress
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton2) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha3);
        }
    }

    public bool BlueButtonTrigger
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton2) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha3);
        }
    }

    public bool BlueButtonRelease
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton2) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha3);
        }
    }

    //--------------- Blueボタン関係 --------------------------E

    //--------------- Yellowボタン関係 ------------------------S         デバッグ用に４キー割り当て

    public bool YellowButtonPress
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton3) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha4);
        }
    }

    public bool YellowButtonTrigger
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton3) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha4);
        }
    }

    public bool YellowButtonRelease
    {
        get{
            return Input.GetKey(KeyCode.JoystickButton3) & !fade.IsFading() || Input.GetKey(KeyCode.Alpha4);
        }
    }

    //--------------- Yellowボタン関係 ------------------------E



	// ++++++++++++ 何かのボタン ++++++++++++++++S

	public bool KeyAnyTrigger(){
        if (RedButtonTrigger
           || YellowButtonTrigger
           || BlueButtonTrigger
           || GreenButtonTrigger)
        {
			return true;
		}
		return false;
	}

	public bool KeyAnyPress(){
        if (RedButtonPress
           || YellowButtonPress
           || BlueButtonPress
           || GreenButtonPress)
        {
			return true;
		}
		return false;
	}

	public bool KeyAnyRelease(){
        if (RedButtonRelease
           || YellowButtonRelease
           || BlueButtonRelease
           || GreenButtonRelease)
        {
			return true;
		}
		return false;
	}
}
