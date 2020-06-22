using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YESB : Photon.PunBehaviour
{
    PhotonView YESView;

    [SerializeField]
    Cards Cards;

    [SerializeField]
    GameObject Check_Panel;
    [SerializeField]
    GameObject SPY_Panel;

    [SerializeField]
    Text SPY_Text;
    [SerializeField]
    Button SPYCheck_Button;

    public int YESNum = -1;
    public int EscBox = -1;

    void Awake()
    {
        YESView = GetComponent<PhotonView>();
        SPY_Panel.gameObject.SetActive(false);
    }

    void SendNum()
    {
        YESView.RPC("SENDNum", PhotonTargets.Others, YESNum);
    }

    [PunRPC]
    public void SENDNum(int yesnum)
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            //相手に自分のカードの選択終了のフラグを立てる
            Cards.Other_OK_Flag = true;

            //密偵を使われている
            if (Cards.Other_Spy_Effect == true)
            {
                Cards.Other_Num = yesnum;
                Cards.OtherNUM.text = "相手： " + yesnum;

                SPY_Panel.gameObject.SetActive(true);
                SPY_Text.text = "相手は【" + Cards.Other_Num + "】を出しました。";
                Cards.Own_Spy_Effect = false;

                if (Cards.Clown_Card[0].Used_Card != true)
                {
                    Cards.Clown_Button.interactable = true;
                }

                if (Cards.Princess_Card[0].Used_Card != true)
                {
                    Cards.Princess_Button.interactable = true;
                }

                if (Cards.Spy_Card[0].Used_Card != true)
                {
                    Cards.Spy_Button.interactable = true;
                }

                if (Cards.Assassin_Card[0].Used_Card != true)
                {
                    Cards.Assassin_Button.interactable = true;
                }

                if (Cards.Ministry_Card[0].Used_Card != true)
                {
                    Cards.Ministry_Button.interactable = true;
                }

                if (Cards.Magician_Card[0].Used_Card != true)
                {
                    Cards.Magician_Button.interactable = true;
                }

                if (Cards.General_Card[0].Used_Card != true)
                {
                    Cards.General_Button.interactable = true;
                }

                if (Cards.Prince_Card[0].Used_Card != true)
                {
                    Cards.Prince_Button.interactable = true;
                }
            }

            //密偵を使われていない
            else
            {
                Cards.Other_Num = yesnum;
                Cards.OtherNUM.text = "相手： " + yesnum;
            }
        }

        else
        {
            //相手に自分のカードの選択終了のフラグを立てる
            Cards.Other_OK_Flag = true;

            //密偵を使われている
            if (Cards.Other_Spy_Effect == true)
            {
                Cards.Other_Num = yesnum;
                Cards.OtherNUM.text = "相手： " + yesnum;

                SPY_Panel.gameObject.SetActive(true);
                SPY_Text.text = "相手は【" + Cards.Other_Num + "】を出しました。";
                Cards.Own_Spy_Effect = false;

                if (Cards.Clown_Card[0].Used_Card != true)
                {
                    Cards.Clown_Button.interactable = true;
                }

                if (Cards.Princess_Card[0].Used_Card != true)
                {
                    Cards.Princess_Button.interactable = true;
                }

                if (Cards.Spy_Card[0].Used_Card != true)
                {
                    Cards.Spy_Button.interactable = true;
                }

                if (Cards.Assassin_Card[0].Used_Card != true)
                {
                    Cards.Assassin_Button.interactable = true;
                }

                if (Cards.Ministry_Card[0].Used_Card != true)
                {
                    Cards.Ministry_Button.interactable = true;
                }

                if (Cards.Magician_Card[0].Used_Card != true)
                {
                    Cards.Magician_Button.interactable = true;
                }

                if (Cards.General_Card[0].Used_Card != true)
                {
                    Cards.General_Button.interactable = true;
                }

                if (Cards.Prince_Card[0].Used_Card != true)
                {
                    Cards.Prince_Button.interactable = true;
                }
            }

            //密偵を使われていない
            else
            {
                Cards.Other_Num = yesnum;
                Cards.OtherNUM.text = "相手： " + yesnum;
            }
        }
    }

    public void SpyEffect()
    {
        YESView.RPC("SPYEffect", PhotonTargets.Others);
    }

    [PunRPC]
    public void SPYEffect()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            Cards.Other_Spy_Effect = true;
        }

        else
        {
            Cards.Other_Spy_Effect = true;
        }
    }

    public void OnClick()
    {
        if (Cards.Clown_Card[0].Use_Card == true)
        {
            Cards.Clown_Card[0].Used_Card = true;
        }

        if (Cards.Princess_Card[0].Use_Card == true)
        {
            Cards.Princess_Card[0].Used_Card = true;
        }

        if (Cards.Spy_Card[0].Use_Card == true)
        {
            Cards.Spy_Card[0].Used_Card = true;
        }

        if (Cards.Assassin_Card[0].Use_Card == true)
        {
            Cards.Assassin_Card[0].Used_Card = true;
        }

        if (Cards.Ministry_Card[0].Use_Card == true)
        {
            Cards.Ministry_Card[0].Used_Card = true;
        }

        if (Cards.Magician_Card[0].Use_Card == true)
        {
            Cards.Magician_Card[0].Used_Card = true;
        }

        if (Cards.General_Card[0].Use_Card == true)
        {
            Cards.General_Card[0].Used_Card = true;
        }

        if (Cards.Prince_Card[0].Use_Card == true)
        {
            Cards.Prince_Card[0].Used_Card = true;
        }

        //密偵を使った
        if (Cards.Own_Spy_Effect == true)
        {
            //密偵を使われているか
            if (Cards.Other_Spy_Effect == true)
            {
                //数値が決定して、相手に送信
                if (PhotonNetwork.player.IsMasterClient)
                {
                    //自身の数値を代入
                    Cards.Own_Num = YESNum;

                    //相手に数値を送信
                    SendNum();

                    //相手に密偵フラグ立てる
                    SpyEffect();
                }

                else
                {
                    //自身の数値を代入
                    Cards.Own_Num = YESNum;

                    //相手に数値を送信
                    SendNum();

                    //相手に密偵フラグ立てる
                    SpyEffect();
                }

                Cards.Other_Spy_Effect = false;
            }

            else
            {
                //数値が決定して、相手に送信
                if (PhotonNetwork.player.IsMasterClient)
                {
                    //自身の数値を代入
                    Cards.Own_Num = YESNum;

                    //相手に数値を送信
                    SendNum();

                    //相手に密偵フラグ立てる
                    SpyEffect();
                }

                else
                {
                    //自身の数値を代入
                    Cards.Own_Num = YESNum;

                    //相手に数値を送信
                    SendNum();

                    //相手に密偵フラグ立てる
                    SpyEffect();
                }
            }
        }

        //密偵を使っていない
        else
        {
            //密偵を使われているか
            if (Cards.Other_Spy_Effect == true)
            {
                //数値が決定して、相手に送信
                if (PhotonNetwork.player.IsMasterClient)
                {
                    //自身の数値を代入
                    Cards.Own_Num = YESNum;

                    //相手に数値を送信
                    SendNum();
                }

                else
                {
                    //自身の数値を代入
                    Cards.Own_Num = YESNum;

                    //相手に数値を送信
                    SendNum();
                }

                Cards.Other_Spy_Effect = false;
            }

            else
            {
                //数値が決定して、相手に送信
                if (PhotonNetwork.player.IsMasterClient)
                {
                    //自身の数値を代入
                    Cards.Own_Num = YESNum;

                    //相手に数値を送信
                    SendNum();
                }

                else
                {
                    //自身の数値を代入
                    Cards.Own_Num = YESNum;

                    //相手に数値を送信
                    SendNum();
                }
            }
        }

        YESNum = -1;
        EscBox = -1;

        //チェックパネルを閉じる
        Check_Panel.SetActive(false);
        
        //カード選択の終了
        Cards.Own_OK_Flag = true;
    }

    public void CheckClick()
    {
        SPY_Panel.gameObject.SetActive(false);
    }
}
