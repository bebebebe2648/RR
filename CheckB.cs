﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckB : MonoBehaviour
{
    [SerializeField]
    Cards Cards;
    [SerializeField]
    WorLP Win_Lose_Panel;

    [SerializeField]
    YESB YES;
    [SerializeField]
    Button Check_Button;

    void Update()
    {
        if (Cards.Reset_Flag == true)
        {
            Check_Button.interactable = false;
            Cards.Reset_Button.interactable = true;
        }
    }

    public void ReStartClick()
    {
        Cards.CardSet();

        Cards.Clown_Card[0].Use_Card = false;
        Cards.Clown_Card[0].Used_Card = false;
        Cards.Princess_Card[0].Use_Card = false;
        Cards.Princess_Card[0].Used_Card = false;
        Cards.Spy_Card[0].Use_Card = false;
        Cards.Spy_Card[0].Used_Card = false;
        Cards.Assassin_Card[0].Use_Card = false;
        Cards.Assassin_Card[0].Used_Card = false;
        Cards.Ministry_Card[0].Use_Card = false;
        Cards.Ministry_Card[0].Used_Card = false;
        Cards.Magician_Card[0].Use_Card = false;
        Cards.Magician_Card[0].Used_Card = false;
        Cards.General_Card[0].Use_Card = false;
        Cards.General_Card[0].Used_Card = false;
        Cards.Prince_Card[0].Use_Card = false;
        Cards.Prince_Card[0].Used_Card = false;

        Cards.Clown_Button.interactable = true;
        Cards.Princess_Button.interactable = true;
        Cards.Spy_Button.interactable = true;
        Cards.Assassin_Button.interactable = true;
        Cards.Ministry_Button.interactable = true;
        Cards.Magician_Button.interactable = true;
        Cards.General_Button.interactable = true;
        Cards.Prince_Button.interactable = true;

        YES.YESNum = -1;
        YES.EscBox = -1;

        Cards.Own_Num = -1;
        Cards.Other_Num = -1;

        Cards.WIN_Count = 0;
        Cards.LOSE_Count = 0;
        Cards.ADD_Win_Lose = 1;

        //各種フラグ
        Cards.Own_OK_Flag = false;
        Cards.Other_OK_Flag = false;

        //密偵・両将軍効果フラグをfalseに
        Cards.Own_Spy_Effect = false;
        Cards.Other_Spy_Effect = false;
        Cards.Own_General_Effect = false;
        Cards.Other_General_Effect = false;

        //リセットフラグをfalseにし、ボタンを押せないようにする
        //Checkボタンを押せるように
        Cards.Reset_Flag = false;
        Cards.Reset_Button.interactable = false;
        Check_Button.interactable = true;

        //プレイヤーの勝敗数表示
        Cards.Own_Win.text = "WIN : " + Cards.WIN_Count.ToString();
        Cards.Own_Lose.text = "LOSE : " + Cards.LOSE_Count.ToString();
        Cards.Other_Win.text = "WIN : " + Cards.LOSE_Count.ToString();
        Cards.Other_Lose.text = "LOSE : " + Cards.WIN_Count.ToString();

        Win_Lose_Panel.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        //密偵の事後処理
        if (Cards.Own_Spy_Effect == false && Cards.Other_Spy_Effect == true)
        {
            //各ボタン押せるように
            if (PhotonNetwork.player.IsMasterClient)
            {
                Push_Button();
            }

            else
            {
                Push_Button();
            }
        }

        //密偵の効果処理
        else if(Cards.Own_Spy_Effect == true && Cards.Other_Spy_Effect == false)
        {
            /*
            if (PhotonNetwork.player.IsMasterClient)
            {
                YES.SpyEffect();
            }

            else
            {
                YES.SpyEffect();
            }
            */
        }

        //両者密偵を使った場合
        else if(Cards.Own_Spy_Effect == true && Cards.Other_Spy_Effect == true)
        {
            Cards.Own_Spy_Effect = false;
            Cards.Other_Spy_Effect = false;

            //各ボタン押せるように
            if (PhotonNetwork.player.IsMasterClient)
            {
                Push_Button();
            }

            else
            {
                Push_Button();
            }
        }

        //両者密偵以外を使った場合
        else
        {
            //各ボタン押せるように
            if (PhotonNetwork.player.IsMasterClient)
            {
                Push_Button();
            }

            else
            {
                Push_Button();
            }
        }

        //説明が出るように
        Cards.Clown_Card[0].Card_Flag = false;
        Cards.Princess_Card[0].Card_Flag = false;
        Cards.Spy_Card[0].Card_Flag = false;
        Cards.Assassin_Card[0].Card_Flag = false;
        Cards.Ministry_Card[0].Card_Flag = false;
        Cards.Magician_Card[0].Card_Flag = false;
        Cards.General_Card[0].Card_Flag = false;
        Cards.Prince_Card[0].Card_Flag = false;

        Cards.Own_OK_Flag = false;
        Cards.Other_OK_Flag = false;

        //ループ阻止のフラグ
        Cards.LoopCut = true;

        Cards.Own_Num = -1;
        Cards.Other_Num = -1;

        Debug.Log("Check_Owner: " + Cards.Own_Num);
        Debug.Log("Check_Other: " + Cards.Other_Num);

        Win_Lose_Panel.gameObject.SetActive(false);
    }

    public void Push_Button()
    {
        if (Cards.Clown_Card[0].Use_Card != true)
        {
            Cards.Clown_Button.interactable = true;
        }

        if (Cards.Princess_Card[0].Use_Card != true)
        {
            Cards.Princess_Button.interactable = true;
        }

        if (Cards.Spy_Card[0].Use_Card != true)
        {
            Cards.Spy_Button.interactable = true;
        }

        if (Cards.Assassin_Card[0].Use_Card != true)
        {
            Cards.Assassin_Button.interactable = true;
        }

        if (Cards.Ministry_Card[0].Use_Card != true)
        {
            Cards.Ministry_Button.interactable = true;
        }

        if (Cards.Magician_Card[0].Use_Card != true)
        {
            Cards.Magician_Button.interactable = true;
        }

        if (Cards.General_Card[0].Use_Card != true)
        {
            Cards.General_Button.interactable = true;
        }

        if (Cards.Prince_Card[0].Use_Card != true)
        {
            Cards.Prince_Button.interactable = true;
        }
    }
}
