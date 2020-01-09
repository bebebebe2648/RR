using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOB : Photon.PunBehaviour
{
    PhotonView BackView;

    [SerializeField]
    Cards Cards;

    [SerializeField]
    GameObject Check_Panel;
    [SerializeField]
    YESB YES_Button;

    public int NOBox = -1;

    void Awake()
    {
        BackView = GetComponent<PhotonView>();
    }

    //先押しで相手が押してない場合
    void BackNum()
    {
        BackView.RPC("BackNUM", PhotonTargets.All);
    }

    [PunRPC]
    public void BackNUM()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            Cards.Other_Num = -1;
        }

        else
        {
            Cards.Other_Num = -1;
        }
    }
    
    //先押しで相手が押している場合
    void BackNumPlus()
    {
        BackView.RPC("BackNUMPlus", PhotonTargets.All);
    }

    [PunRPC]
    public void BackNUMPlus()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            Cards.Other_Num = -1;
            YES_Button.EscBox = -1;
        }

        else
        {
            Cards.Other_Num = -1;
            YES_Button.EscBox = -1;
        }
    }
    
    public void OnClick()
    {
        
        if (PhotonNetwork.player.IsMasterClient)
        {
            //先押しで相手が押してない場合
            if(Cards.Own_Num == Cards.Other_Num && YES_Button.EscBox == -1)
            {
                Debug.Log("!");
                //初期化
                YES_Button.YESNum = -1;
                //NT.OwnerNum = -1;

                BackNum();
            }

            //先押しで相手が押している場合
            if(Cards.Own_Num == -1 && Cards.Other_Num != -1 && YES_Button.EscBox == -1)
            {
                Debug.Log("!!");
                //初期化
                YES_Button.YESNum = -1;

                //相手からの数値を保持
                NOBox = Cards.Other_Num;

                BackNumPlus();

                //保持した数値を戻す
                Cards.Other_Num = NOBox;

                //初期化
                NOBox = -1;
            }

            //後押しの場合
            if(Cards.Own_Num != Cards.Other_Num && YES_Button.EscBox != -1)
            {
                Debug.Log("!!!");
                //初期化
                YES_Button.YESNum = -1;
                //NT.OwnerNum = -1;

                //初期化
                YES_Button.EscBox = -1;
                
                //相手からの数値を保持
                NOBox = Cards.Other_Num;

                BackNum();

                //保持した数値を戻す
                Cards.Other_Num = NOBox;

                //初期化
                NOBox = -1;
            }
        }

        else
        {
            //先押しで相手が押してない場合
            if (Cards.Own_Num == Cards.Other_Num && YES_Button.EscBox == -1)
            {
                Debug.Log("!");
                //初期化
                YES_Button.YESNum = -1;

                BackNum();
            }

            //先押しで相手が押している場合
            if (Cards.Own_Num == -1 && Cards.Other_Num != -1 && YES_Button.EscBox == -1)
            {
                Debug.Log("!!");
                //初期化
                YES_Button.YESNum = -1;
                //NT.OwnerNum = -1;

                //相手からの数値を保持
                NOBox = Cards.Other_Num;

                BackNumPlus();

                //保持した数値を戻す
                Cards.Other_Num = NOBox;

                //初期化
                NOBox = -1;
            }

            //後押しの場合
            if (Cards.Own_Num != Cards.Other_Num && YES_Button.EscBox != -1)
            {
                Debug.Log("!!!");
                //初期化
                YES_Button.YESNum = -1;

                //初期化
                YES_Button.EscBox = -1;
                
                //相手からの数値を保持
                NOBox = Cards.Other_Num;

                BackNumPlus();

                //保持した数値を戻す
                Cards.Other_Num = NOBox;

                //初期化
                NOBox = -1;
            }
        }

        //ボタンを押せるように
        Cards.Clown_Card[0].Use_Card = false;
        Cards.Clown_Button.interactable = true;
        Cards.Princess_Card[0].Use_Card = false;
        Cards.Princess_Button.interactable = true;
        Cards.Spy_Card[0].Use_Card = false;
        Cards.Spy_Button.interactable = true;
        Cards.Assassin_Card[0].Use_Card = false;
        Cards.Assassin_Button.interactable = true;
        Cards.Ministry_Card[0].Use_Card = false;
        Cards.Ministry_Button.interactable = true;
        Cards.Magician_Card[0].Use_Card = false;
        Cards.Magician_Button.interactable = true;
        Cards.General_Card[0].Use_Card = false;
        Cards.General_Button.interactable = true;
        Cards.Prince_Card[0].Use_Card = false;
        Cards.Prince_Button.interactable = true;

        //説明が出るように
        Cards.Clown_Card[0].Card_Flag = false;
        Cards.Princess_Card[0].Card_Flag = false;
        Cards.Spy_Card[0].Card_Flag = false;
        Cards.Assassin_Card[0].Card_Flag = false;
        Cards.Ministry_Card[0].Card_Flag = false;
        Cards.Magician_Card[0].Card_Flag = false;
        Cards.General_Card[0].Card_Flag = false;
        Cards.Prince_Card[0].Card_Flag = false;

        Check_Panel.gameObject.SetActive(false);

        Cards.Message_Text.text = "ボタンを選んで押してください。\nボタンにカーソルを合わせると\n説明が出ます。" +
                                  "\n【】内の数値の大きい方が\n勝ちです。\n4回勝てばゲームに勝利します。";
    }
}
