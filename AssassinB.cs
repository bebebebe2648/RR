using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinB : Photon.PunBehaviour
{
    [SerializeField]
    Cards Cards;

    [SerializeField]
    GameObject Check_Panel;
    [SerializeField]
    YESB YES;
    
	void Update ()
    {
        if (Cards.Assassin_Card[0].Used_Card == true)
        {
            Cards.Assassin_Button.interactable = false;
        }

        if (Cards.Spy_Effect == true)
        {
            Cards.Assassin_Button.interactable = false;
        }
    }

    public void OnMouseEnter()
    {
        if (Cards.Assassin_Card[0].Card_Flag == false)
        {
            Cards.Message_Text.text = "数字の強弱を入れ替える。\n相手が王子の場合、この効果は\n無効とする。";
        }
    }

    public void OnMouseExit()
    {
        if (Cards.Assassin_Card[0].Card_Flag == false)
        {
            Cards.Message_Text.text = "ボタンを選んで押してください。\nボタンにカーソルを合わせると\n説明が出ます。\n【】内の数値の大きい方が\n勝ちです。\n4回勝てばゲームに勝利します。";
        }
    }
    
    public void OnClick()
    {
        Check_Panel.gameObject.SetActive(true);

        //ボタンを押せないように
        Cards.Assassin_Card[0].Use_Card = true;

        Cards.Clown_Button.interactable = false;
        Cards.Princess_Button.interactable = false;
        Cards.Spy_Button.interactable = false;
        Cards.Assassin_Button.interactable = false;
        Cards.Ministry_Button.interactable = false;
        Cards.Magician_Button.interactable = false;
        Cards.General_Button.interactable = false;
        Cards.Prince_Button.interactable = false;

        //説明が出ないように
        Cards.Clown_Card[0].Card_Flag = true;
        Cards.Princess_Card[0].Card_Flag = true;
        Cards.Spy_Card[0].Card_Flag = true;
        Cards.Assassin_Card[0].Card_Flag = true;
        Cards.Ministry_Card[0].Card_Flag = true;
        Cards.Magician_Card[0].Card_Flag = true;
        Cards.General_Card[0].Card_Flag = true;
        Cards.Prince_Card[0].Card_Flag = true;

        YES.YESNum = Cards.Assassin_Card[0].Card_Num;
        Cards.Message_Text.text = "【暗殺者】を選択中。";
    }

    public void Assassin_Win_Lose()
    {
        //自分の処理
        if (PhotonNetwork.player.IsMasterClient)
        {
            AssassinCard_WINorLOSE();
        }

        //相手の処理
        else
        {
            AssassinCard_WINorLOSE();
        }
    }

    public void AssassinCard_WINorLOSE()
    {
        //両方将軍効果がついてる場合
        if (Cards.Own_General_Effect == true && Cards.Other_General_Effect == true)
        {
            switch (Cards.Other_Num)
            {
                case 0:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は道化【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "NEXT BATTLE!?";

                    //勝利数を持ち越し加算
                    Cards.ADD_Win_Lose += 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    Cards.Other_General_Effect = false;
                    break;

                case 1:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は姫【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    Cards.Other_General_Effect = false;
                    break;

                case 2:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は密偵【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    Cards.Other_General_Effect = false;

                    //密偵のフラグをtrueに
                    Cards.Spy_Effect = true;
                    break;

                case 3:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は暗殺者【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "       DRAW!?";

                    //勝利数を持ち越し加算
                    Cards.ADD_Win_Lose += 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    Cards.Other_General_Effect = false;
                    break;

                case 4:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は大臣【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "     YOU WIN!";

                    //勝敗数を加算
                    Cards.WIN_Count += Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    Cards.Other_General_Effect = false;
                    break;

                case 5:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は魔術師【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    Cards.Other_General_Effect = false;
                    break;

                case 7:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は王子【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    Cards.Other_General_Effect = false;
                    break;
            }
        }

        //両方将軍効果がついてない場合
        else if (Cards.Own_General_Effect == false && Cards.Other_General_Effect == false)
        {
            switch (Cards.Other_Num)
            {
                case 0:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は道化【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "NEXT BATTLE!?";

                    //勝利数を持ち越し加算
                    Cards.ADD_Win_Lose += 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    break;

                case 1:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は姫【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();
                    break;

                case 2:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は密偵【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //密偵のフラグをtrueに
                    Cards.Spy_Effect = true;
                    break;

                case 3:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は暗殺者【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "       DRAW!?";

                    //勝利数を持ち越し加算
                    Cards.ADD_Win_Lose += 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();
                    break;

                case 4:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は大臣【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "     YOU WIN!";

                    //勝敗数を加算
                    Cards.WIN_Count += Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();
                    break;

                case 5:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は魔術師【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();
                    break;

                case 6:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は将軍【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "     YOU WIN!";

                    //勝敗数を加算
                    Cards.WIN_Count += Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //相手の将軍のフラグをtrueに
                    Cards.Other_General_Effect = true;
                    break;

                case 7:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は王子【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();
                    break;
            }
        }

        //自分にだけ将軍効果がついている場合
        else if (Cards.Own_General_Effect == true && Cards.Other_General_Effect == false)
        {
            switch (Cards.Other_Num)
            {
                case 0:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は道化【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "NEXT BATTLE!?";

                    //勝利数を持ち越し加算
                    Cards.ADD_Win_Lose += 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //自分の将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    
                    break;

                case 1:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は姫【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //自分の将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    break;

                case 2:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は密偵【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //自分の将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;

                    //密偵のフラグをtrueに
                    Cards.Spy_Effect = true;
                    break;

                case 3:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は暗殺者【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "     YOU WIN!";

                    //勝敗数を加算
                    Cards.WIN_Count += Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //自分の将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    break;

                case 4:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は大臣【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //自分の将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    break;

                case 5:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は魔術師【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "       DRAW!?";

                    //勝利数を持ち越し加算
                    Cards.ADD_Win_Lose += 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //自分の将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    break;

                case 6:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は将軍【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "     YOU WIN!";

                    //勝敗数を加算
                    Cards.WIN_Count += Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //自分の将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    //相手の将軍のフラグをtrueに
                    Cards.Other_General_Effect = true;
                    break;

                case 7:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "（将軍！）】を出しました。";
                    Cards.Other_Card.text = "相手は王子【" + Cards.Other_Num + "】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //自分の将軍のフラグをfalseに
                    Cards.Own_General_Effect = false;
                    break;
            }
        }

        //相手にだけ将軍効果がついている場合
        else if (Cards.Own_General_Effect == false && Cards.Other_General_Effect == true)
        {
            switch (Cards.Other_Num)
            {
                case 0:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は道化【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "NEXT BATTLE!?";

                    //勝利数を持ち越し加算
                    Cards.ADD_Win_Lose += 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //相手の将軍のフラグをfalseに
                    Cards.Other_General_Effect = false;
                    break;

                case 1:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は姫【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "       DRAW!?";

                    //勝利数を持ち越し加算
                    Cards.ADD_Win_Lose += 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //相手の将軍のフラグをfalseに
                    Cards.Other_General_Effect = false;
                    break;

                case 2:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は密偵【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "     YOU WIN!";

                    //勝敗数を加算
                    Cards.WIN_Count += Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //相手の将軍のフラグをfalseに
                    Cards.Other_General_Effect = false;

                    //密偵のフラグをtrueに
                    Cards.Spy_Effect = true;
                    break;

                case 3:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は暗殺者【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //相手の将軍のフラグをfalseに
                    Cards.Other_General_Effect = false;
                    break;

                case 4:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は大臣【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "     YOU WIN!";

                    //勝敗数を加算
                    Cards.WIN_Count += Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //相手の将軍のフラグをfalseに
                    Cards.Other_General_Effect = false;
                    break;

                case 5:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は魔術師【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //相手の将軍のフラグをfalseに
                    Cards.Other_General_Effect = false;
                    break;

                case 7:
                    //デバッグ用
                    Debug.Log("Owner: " + Cards.Own_Num);
                    Debug.Log("Other: " + Cards.Other_Num);

                    //お互いのカードの表示
                    Cards.Owner_Card.text = "あなたは暗殺者【" + Cards.Own_Num + "】を出しました。";
                    Cards.Other_Card.text = "相手は王子【" + Cards.Other_Num + "（将軍！）】を出しました。";

                    //結果メッセージ
                    Cards.Big_or_Small.text = "   YOU LOSE...";

                    //勝敗数を加算
                    Cards.LOSE_Count = Cards.ADD_Win_Lose;
                    //勝敗加算を初期化
                    Cards.ADD_Win_Lose = 1;

                    //自分の勝敗数表示
                    Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
                    Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
                    //相手の勝敗数表示
                    Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
                    Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

                    //相手の将軍のフラグをfalseに
                    Cards.Other_General_Effect = false;
                    break;
            }
        }
    }
}
