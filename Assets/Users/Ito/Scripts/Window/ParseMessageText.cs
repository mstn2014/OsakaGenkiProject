//======================================================
// textデータをメッセージにパースするクラス
//------------------------------------------------------
// @Author:K.Ito
// @Date:2014/10/22
// @Brif:@sから始まるデータを一回のウィンドウに表示します。
//======================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParseMessageText{

    const string key = "@s";

    public List<string> LoadText(string filePath)
    {
        string tempText = string.Empty;

        // リソースフォルダから読み込み
        tempText = Resources.Load<TextAsset>(filePath).text;

        // ここにパースする関数を書く
        return ParseText(tempText);
    }

    List<string> ParseText(string txt)
    {
        List<string> messageText = new List<string>();
        string tmp = string.Empty;

        // @sを検索し、その位置を返す
        int index = txt.IndexOf(key);

        while(index != -1){
            // キーインデックスを進めておく
            index += key.Length;
            // 次のキーの位置を検索
            int nextIndex = txt.IndexOf(key,index);
            // 終端まで行ったら文字の最後のインデックスを返す
            if( nextIndex == -1 ) nextIndex = txt.Length;
            // キーから次のキーまでの文字列を切り出す
            tmp = txt.Substring(index, nextIndex - index);
            // 文字の前後に改行がある場合は削除する。
            // 文字列の先頭がエスケープ文字なら削除
            while( tmp.Length != 0 && (tmp[0].ToString() == "\r" || tmp[0].ToString() == "\n")){
                tmp = tmp.Remove(0, 1); 
            }
            // 文字列の後ろがエスケープ文字なら削除
            while ( tmp.Length != 0 && (tmp[tmp.Length-1].ToString() == "\r" || tmp[tmp.Length-1].ToString() == "\n"))
            {
                tmp = tmp.Remove(tmp.Length-1);
            }

            messageText.Add( tmp );
            // インデックスの更新
            if (nextIndex != txt.Length)
            {
                index = nextIndex;
            }
            else
            {
                index = -1;
            }
        }

        return messageText;
    }
}
