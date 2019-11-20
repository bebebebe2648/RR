using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : Photon.PunBehaviour
{
    public class Card
    {
        public Button Card_Button;
        public int Card_Num;
        public bool Card_Flag;
        public bool Use_Card;
        public bool Used_Card;

        public Card(Button Card_button, int Card_num, bool Card_flag, bool Use_card, bool Used_card)
        {
            this.Card_Button = Card_button;
            this.Card_Num = Card_num;
            this.Card_Flag = Card_flag;
            this.Use_Card = Use_card;
            this.Used_Card = Used_card;
        }
    }

    //各種カードのボタン
    [SerializeField]
    public Button Clown_Button, Princess_Button, Spy_Button, Assassin_Button, Ministry_Button, Magician_Button, General_Button, Prince_Button;

    //カード・ルールなどの説明のテキスト
    [SerializeField]
    public Text Message_Text;

    public List<Card> Clown_Card = new List<Card>();
    public List<Card> Princess_Card = new List<Card>();
    public List<Card> Spy_Card = new List<Card>();
    public List<Card> Assassin_Card = new List<Card>();
    public List<Card> Ministry_Card = new List<Card>();
    public List<Card> Magician_Card = new List<Card>();
    public List<Card> General_Card = new List<Card>();
    public List<Card> Prince_Card = new List<Card>();

    PhotonView RRView;

    //勝敗判定の参照用
    [SerializeField]
    ClownB ClownB;
    [SerializeField]
    PrincessB PrincessB;
    [SerializeField]
    SpyB SpyB;
    [SerializeField]
    AssassinB AssassinB;
    [SerializeField]
    MinistryB MinistryB;
    [SerializeField]
    MagicianB MagicianB;
    [SerializeField]
    GeneralB GeneralB;
    [SerializeField]
    PrinceB PrinceB;

    //勝敗表示パネル(WorLP)
    [SerializeField]
    GameObject Win_Lose_Panel;

    //勝敗表示テキスト(BorS)
    [SerializeField]
    public Text Big_or_Small;

    //お互いが出したカードの表示テキスト(OWNT・YOURT)
    [SerializeField]
    public Text Owner_Card, Other_Card;

    //プレイヤー名の表示
    [SerializeField]
    Text Own_Name, Other_Name;

    //プレイヤーの勝敗数の表示
    [SerializeField]
    public Text Own_Win, Own_Lose, Other_Win, Other_Lose;

    //リセットボタン
    [SerializeField]
    public Button Reset_Button;

    //
    public bool Reset_Flag = false;

    //勝敗カウント
    public int WIN_Count = 0;
    public int LOSE_Count = 0;

    //追加勝敗数
    public int ADD_Win_Lose = 1;

    //各種効果のフラグ
    public bool Spy_Effect = false;
    public bool Own_General_Effect = false;
    public bool Other_General_Effect = false;

    //プレイヤーのカードの数値
    public int Own_Num = -1;
    public int Other_Num = -1;

    public void Clown()
    {
        Clown_Card.Add(new Card(Clown_Button, 0, false, false, false));
    }

    public void Princess()
    {
        Princess_Card.Add(new Card(Princess_Button, 1, false, false, false));
    }

    public void Spy()
    {
        Spy_Card.Add(new Card(Spy_Button, 2, false, false, false));
    }

    public void Assassin()
    {
        Assassin_Card.Add(new Card(Assassin_Button, 3, false, false, false));
    }

    public void Ministry()
    {
        Ministry_Card.Add(new Card(Ministry_Button, 4, false, false, false));
    }

    public void Magician()
    {
        Magician_Card.Add(new Card(Magician_Button, 5, false, false, false));
    }

    public void General()
    {
        General_Card.Add(new Card(General_Button, 6, false, false, false));
    }

    public void Prince()
    {
        Prince_Card.Add(new Card(Prince_Button, 7, false, false, false));
    }

    public void CardSet()
    {
        Clown();
        Princess();
        Spy();
        Assassin();
        Ministry();
        Magician();
        General();
        Prince();
    }

    // Start is called before the first frame update
    void Start()
    {
        Win_Lose_Panel.SetActive(false);
        Reset_Button.interactable = false;

        RRView = GetComponent<PhotonView>();

        //自分のプレイヤー名表示
        Own_Name.text = PhotonNetwork.player.NickName;
        NameSet();

        //プレイヤーの勝敗数表示
        Own_Win.text = "WIN : " + WIN_Count.ToString();
        Own_Lose.text = "LOSE : " + LOSE_Count.ToString();
        Other_Win.text = "WIN : " + LOSE_Count.ToString();
        Other_Lose.text = "LOSE : " + WIN_Count.ToString();

        Message_Text.text = "ボタンを選んで押してください。\nボタンにカーソルを合わせると\n説明が出ます。" +
                            "\n【】内の数値の大きい方が\n勝ちです。\n4回勝てばゲームに勝利します。";

        CardSet();
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了時、リセットボタンを押せるように
        if (WIN_Count >= 4 || LOSE_Count >= 4)
        {
            Reset_Flag = true;
        }
    }

    //通信して相手のプレイヤー名を表示
    public void NameSet()
    {
        RRView.RPC("NAMESet", PhotonTargets.All, PhotonNetwork.player.NickName);
    }

    [PunRPC]
    public void NAMESet(string name)
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            Other_Name.text = name;
        }

        else
        {
            Other_Name.text = name;
        }
    }

    public void WINorLOSE()
    {
        switch (Own_Num)
        {
            case 0:
                ClownB.Clown_Win_Lose();
                break;

            case 1:
                PrincessB.Princess_Win_Lose();
                break;

            case 2:
                SpyB.Spy_Win_Lose();
                break;

            case 3:
                AssassinB.Assassin_Win_Lose();
                break;

            case 4:
                MinistryB.Ministry_Win_Lose();
                break;

            case 5:
                MagicianB.Magician_Win_Lose();
                break;

            case 6:
                GeneralB.General_Win_Lose();
                break;

            case 7:
                PrinceB.Prince_Win_Lose();
                break;
        }
    }
}
