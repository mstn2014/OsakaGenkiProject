
スクリプト
CountDown	// GameRoopでスタートすると起動
TimeFrame	// CountDownから時間を取得し、外枠のFillAmountをいじる。

GameRoop	// ゲームループ部分
		

Question	// 問題生成・回答
ProductionMgr	// 演出担当。ラウンド終了時の演出をここに記述
ObjMgr		// 地面・ギャラリーの生成。ProductionMgrにて次ラウンド移動
EffectMgr	// コンボの表示・サークル表示・ラウンド結果文字の表示


Gallery		// 地面にアタッチ。画面外に行くと移動させる
Ground		// ギャラリーにアタッチ。画面外に行くと移動させる


CreatePrefab	// GameObjectを生成する関数が入っている。気にしなくていいです。


＜思いつく未実装部分＞
ラウンド終了後、移動のときにギャラリーとプレイヤーがぶつかる
スコアの計算
サークルとギャラリーのあたり判定（判定後アクションをとる）
生成される参加者の向きが一定
正解時、サークルが広がると同時にカメラも引く
