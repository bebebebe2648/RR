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

    void SendOK()
    {
        YESView.RPC("Sendok", PhotonTargets.Others);
    }

    [PunRPC]
    public void Sendok()
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            //NT.OtherFlag = true;

            if (Cards.Spy_Effect == true)
            {
                if(Cards.Clown_Card[0].Used_Card != true)
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
        }

        else
        {
            //NT.OtherFlag = true;

            if (Cards.Spy_Effect == true)
            {
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
        }
    }

    void SendNum()
    {
        YESView.RPC("SENDNum", PhotonTargets.All, YESNum);
    }

    [PunRPC]
    public void SENDNum(int yesnum)
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            Cards.Other_Num = yesnum;
            Debug.Log("OtherNum = YESNum" + yesnum);

            if (Cards.Spy_Effect == true)
            {
                SPY_Panel.gameObject.SetActive(true);
                SPY_Text.text = "相手は【" + Cards.Other_Num + "】を出しました。";
            }
            
            Cards.Spy_Effect = false;
        }

        else
        {
            Cards.Other_Num = yesnum;
            Debug.Log("OtherNum = YESNum" + yesnum);

            if (Cards.Spy_Effect == true)
            {
                SPY_Panel.gameObject.SetActive(true);
                SPY_Text.text = "相手は【" + Cards.Other_Num + "】を出しました。";
            }

            Cards.Spy_Effect = false;
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

        //NT.OwnFlag = true;

        SendOK();

        if (PhotonNetwork.player.IsMasterClient)
        {
            //数値が送られてきてる場合の退避処理（後押し）
            if (Cards.Own_Num != Cards.Other_Num && Cards.Other_Num == EscBox)
            {
                EscBox = Cards.Other_Num;

                //自身の数値を代入
                Cards.Own_Num = YESNum;
                Debug.Log("OwnNum = YESNum" + YESNum);

                //相手に数値を送信、かつ自身の相手の数値を代入してしまう処理
                SendNum();

                //退避してた数値を戻す
                Cards.Other_Num = EscBox;
            }

            //数値が送られてきてない場合の処理（先押し）
            else if (Cards.Own_Num == Cards.Other_Num && Cards.Other_Num == EscBox)
            {
                //自身の数値を代入
                Cards.Own_Num = YESNum;
                Debug.Log("OwnNum = YESNum" + YESNum);

                //相手に数値を送信、かつ自身の相手の数値を代入してしまう処理
                SendNum();
            }
        }

        else
        {
            //数値が送られてきてる場合の退避処理（後押し）
            if (Cards.Own_Num != Cards.Other_Num && Cards.Other_Num == EscBox)
            {
                EscBox = Cards.Other_Num;

                //自身の数値を代入
                Cards.Own_Num = YESNum;
                Debug.Log("OwnNum = YESNum" + YESNum);

                //相手に数値を送信、かつ自身の相手の数値を代入してしまう処理
                SendNum();

                //退避してた数値を戻す
                Cards.Other_Num = EscBox;
            }

            //数値が送られてきてない場合の処理（先押し）
            else if (Cards.Own_Num == Cards.Other_Num && Cards.Other_Num == EscBox)
            {
                //自身の数値を代入
                Cards.Own_Num = YESNum;
                Debug.Log("OwnNum = YESNum" + YESNum);

                //相手に数値を送信、かつ自身の相手の数値を代入してしまう処理
                SendNum();
            }
        }

        YESNum = -1;
        EscBox = -1;

        Check_Panel.SetActive(false);
    }

    public void CheckClick()
    {
        SPY_Panel.gameObject.SetActive(false);
    }
}
