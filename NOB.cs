using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOB : Photon.PunBehaviour
{
    [SerializeField]
    Cards Cards;

    [SerializeField]
    GameObject Check_Panel;
    [SerializeField]
    YESB YES_Button;
    
    public void OnClick()
    {
        //選択した数値をリセット
        YES_Button.YESNum = -1;

        //表示リセット
        Cards.OwnNUM.text = "自分： ";

        Cards.Own_Spy_Effect = false;

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

        //チェックパネルを閉じる
        Check_Panel.gameObject.SetActive(false);

        //初期メッセージ表示
        Cards.Message_Text.text = "ボタンを選んで押してください。\nボタンにカーソルを合わせると\n説明が出ます。" +
                                  "\n【】内の数値の大きい方が\n勝ちです。\n4回勝てばゲームに勝利します。";
    }
}
