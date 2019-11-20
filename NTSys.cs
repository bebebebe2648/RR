using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NTSys : Photon.PunBehaviour
{
    PhotonView NameView;

    WorLP WLP;
    BorS bors;
    OWNT OWN;
    YOURT YOU;

    [SerializeField]
    Text OwnName;
    [SerializeField]
    Text OtherName;
    [SerializeField]
    Text OwnWin;
    [SerializeField]
    Text OwnLose;
    [SerializeField]
    Text OtherWin;
    [SerializeField]
    Text OtherLose;

    [SerializeField]
    Button RSB;

    public bool OwnFlag = false;
    public bool OtherFlag = false;

    public int OwnerNum = -1; //将軍使用後
    public int OtherNum = -1; //将軍使用後
    public int OwnEsc = -1;   //将軍使用前
    public int OtherEsc = -1; //将軍使用前

    public int OwnWIN = 0;
    public int OwnLOSE = 0;
    
    //加算勝敗数
    public int WLCal = 1;

    //
    public bool SpyE = false;
    public bool OwnGeneE = false;
    public bool OtherGeneE = false;

    public bool RSBF = false;
    
    // Use this for initialization
    void Start()
    {
        NameView = GetComponent<PhotonView>();
        WLP = GameObject.Find("WorLP").GetComponent<WorLP>();
        bors = GameObject.Find("BorS").GetComponent<BorS>();
        OWN = GameObject.Find("OWNT").GetComponent<OWNT>();
        YOU = GameObject.Find("YOURT").GetComponent<YOURT>();

        RSB.interactable = false;

        OwnName.text = PhotonNetwork.player.NickName;
        NameSet();

        OwnWin.text = "WIN : " + OwnWIN.ToString();
        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
        OtherWin.text = "WIN : " + OwnLOSE.ToString();
        OtherLose.text = "LOSE : " + OwnWIN.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (OwnWIN >= 4 || OwnLOSE >= 4)
        {
            RSBF = true;
        }
    }

    void NameSet()
    {
        NameView.RPC("NAMESet", PhotonTargets.Others, PhotonNetwork.player.NickName);
    }

    [PunRPC]
    public void NAMESet(string name)
    {
        if (PhotonNetwork.player.IsMasterClient)
        {
            OtherName.text = name;
        }

        else
        {
            OtherName.text = name;
        }
    }

    void GeneCal()
    {
        if (OwnGeneE == true)
        {
            OwnEsc = OwnerNum;
            OwnerNum += 2;
        }

        if (OtherGeneE == true)
        {
            OtherEsc = OtherNum;
            OtherNum += 2;
        }
    }

    public void WinorLose()
    {
        WLP.WorL.SetActive(true);

        //ホスト側の処理
        if (PhotonNetwork.player.IsMasterClient)
        {
            GeneCal();

            //特殊勝利条件：姫【1】と王子【7】の場合
            if (OwnerNum == 1 && OtherNum == 7 || OwnerNum == 7 && OtherNum == 1 ||
                OwnEsc == 1 && OtherEsc == 7 || OwnEsc == 7 && OtherEsc == 1 ||
                OwnEsc == 1 && OtherNum == 7 || OwnEsc == 7 && OtherNum == 1 ||
                OwnerNum == 1 && OtherEsc == 7 || OwnerNum == 7 && OtherEsc == 1)
            {
                //どちらかが将軍【6】が出していた場合
                if (OwnGeneE == true || OtherGeneE == true)
                {
                    //両方が将軍【6】を出していた場合
                    if (OwnGeneE == true && OtherGeneE == true)
                    {
                        //ホストが姫【1】、クライアントが王子【7】の場合
                        if (OwnEsc == 1 && OtherEsc == 7)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                            YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n     YOU WIN!";
                            OwnGeneE = false;
                            OtherGeneE = false;
                            RSBF = true;
                        }

                        //ホストが王子【7】、クライアントが姫【1】の場合
                        else if (OwnEsc == 7 && OtherEsc == 1)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                            YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n   YOU LOSE...";
                            OwnGeneE = false;
                            OtherGeneE = false;
                            RSBF = true;
                        }
                    }

                    //ホストが将軍【6】を出していた場合
                    else if (OwnGeneE == true)
                    {
                        //ホストが姫【1】、クライアントが王子【7】の場合
                        if (OwnEsc == 1 && OtherNum == 7)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                            YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n     YOU WIN!";
                            OwnGeneE = false;
                            RSBF = true;
                        }

                        //ホストが王子【7】、クライアントが姫【1】の場合
                        else if (OwnEsc == 7 && OtherNum == 1)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                            YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n   YOU LOSE...";
                            OwnGeneE = false;
                            RSBF = true;
                        }
                    }

                    //クライアントが将軍【6】を出していた場合
                    else if (OtherGeneE == true)
                    {
                        //ホストが姫【1】、クライアントが王子【7】の場合
                        if (OwnerNum == 1 && OtherEsc == 7)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                            YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n     YOU WIN!";
                            OtherGeneE = false;
                            RSBF = true;
                        }

                        //ホストが王子【7】、クライアントが姫【1】の場合
                        else if (OwnerNum == 7 && OtherEsc == 1)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                            YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n   YOU LOSE...";
                            OtherGeneE = false;
                            RSBF = true;
                        }
                    }
                }

                //どちらも将軍【6】を出していない場合
                else
                {
                    //ホストが姫【1】、クライアントが王子【7】の場合
                    if (OwnerNum == 1 && OtherNum == 7)
                    {
                        Debug.Log("Owner: " + OwnerNum);
                        Debug.Log("Other: " + OtherNum);

                        OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                        YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                        bors.BS.text = "BATTLE ENDED\n     YOU WIN!";
                        RSBF = true;
                    }

                    //ホストが王子【7】、クライアントが姫【1】の場合
                    else if (OwnerNum == 7 && OtherNum == 1)
                    {
                        Debug.Log("Owner: " + OwnerNum);
                        Debug.Log("Other: " + OtherNum);

                        OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                        YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                        bors.BS.text = "BATTLE ENDED\n   YOU LOSE...";
                        RSBF = true;
                    }
                }
            }

            //特殊勝利以外の処理
            else
            {
                //どちらかが将軍【6】が出していた場合
                if (OwnGeneE == true || OtherGeneE == true)
                {
                    //両方が将軍【6】を出していた場合
                    if (OwnGeneE == true && OtherGeneE == true)
                    {
                        //両方、将軍【6】が使われてるとき
                        //ホストとクライアントの数値が同じ場合
                        if (OwnEsc == OtherEsc)
                        {
                            //道化【0】を使った場合
                            if (OwnEsc == 0 && OtherEsc == 0)
                            {
                                Debug.Log("Owner: " + OwnerNum);
                                Debug.Log("Other: " + OtherNum);

                                OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                bors.BS.text = "NEXT BATTLE!?";

                                WLCal += 1;

                                OwnGeneE = false;
                                OtherGeneE = false;
                            }

                            //道化【0】を使っていない場合
                            else
                            {
                                switch (OwnEsc)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;
                                }
                            }
                        }

                        //両方、将軍【6】が使われてるとき
                        //ホストの数値が大きい場合
                        else if (OwnerNum > OtherNum)
                        {
                            //クライアントが道化【0】を使った場合
                            if (OtherEsc == 0)
                            {
                                switch (OwnEsc)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        SpyE = true;
                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        //勝敗数計算
                                        OwnWIN += WLCal;
                                        //勝敗数の表示
                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        //加算値を初期化
                                        WLCal = 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;
                                }
                            }

                            //クライアントが道化【0】を使っていない場合
                            else
                            {
                                //ホストが大臣【4】で勝った場合
                                if (OwnEsc == 4)
                                {
                                    //ホストが大臣【4】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】
                                    //負けるパターンは暗殺者【3】のみ
                                    switch (OtherEsc)
                                    {
                                        case 1:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU 2WIN!";

                                            //追加勝敗数
                                            WLCal += 1;
                                            //勝敗数計算
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            //加算値を初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU 2WIN!";

                                            //追加勝敗数
                                            WLCal += 1;
                                            //勝敗数計算
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            //加算値を初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        //ホストが負ける処理
                                        case 3:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU LOSE...";

                                            //勝敗数計算
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN  : " + OwnLOSE.ToString();

                                            //加算値を初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;
                                    }
                                }

                                //ホストが大臣【4】以外で勝った場合
                                else
                                {
                                    switch (OwnEsc)
                                    {
                                        //ホストが密偵【2】で勝つ場合
                                        //勝つパターンは姫【1】
                                        //暗殺者【3】に勝つ処理は別に書く
                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU WIN!";

                                            //勝敗数計算
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            //加算値を初期化
                                            WLCal = 1;

                                            SpyE = true;
                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        //ホストが暗殺者【3】の場合
                                        //負けるパターンは姫【1】・密偵【2】
                                        //大臣【4】に勝つ処理は別に書く
                                        case 3:
                                            switch (OtherEsc)
                                            {
                                                //ホストが負ける処理
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが魔術師【5】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                        case 5:
                                            switch (OtherEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが王子【7】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】
                                        case 7:
                                            switch (OtherEsc)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        //両方、将軍【6】が使われてるとき
                        //ホストの数値が小さい場合
                        else if (OwnerNum < OtherNum)
                        {
                            //ホストが道化【0】を使った場合
                            if (OwnEsc == 0)
                            {
                                switch (OtherEsc)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        //勝敗数計算
                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        //勝敗数初期化
                                        WLCal = 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;
                                }
                            }

                            //ホストが道化【0】を使っていない場合
                            else
                            {
                                //クライアントが大臣【4】で勝った場合
                                if (OtherEsc == 4)
                                {
                                    //クライアントが大臣【4】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】
                                    //負けるパターンは暗殺者【3】のみ
                                    switch (OwnEsc)
                                    {
                                        case 1:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            //追加勝敗数
                                            WLCal += 1;
                                            //勝敗数計算
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            //加算勝敗数初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            //追加勝敗数
                                            WLCal += 1;
                                            //勝敗数計算
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            //加算勝敗数初期化
                                            WLCal = 1;

                                            SpyE = true;
                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        //クライアントが負ける処理
                                        case 3:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU WIN!";

                                            //勝敗数計算
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            //加算勝敗数初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;
                                    }
                                }

                                //クライアントが大臣【4】以外で勝った場合
                                else
                                {
                                    switch (OtherEsc)
                                    {
                                        //クライアントが密偵【2】で勝つ場合
                                        //勝つパターンは姫【1】のみ
                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU LOSE...";

                                            //勝敗数計算
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            //加算勝敗数初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        //クライアントが暗殺者【3】で勝つ場合
                                        //負けるパターンは姫【1】・密偵【2】
                                        //大臣【4】に勝つ処理は別に書く
                                        case 3:
                                            switch (OwnEsc)
                                            {
                                                //クライアントが負ける処理
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }

                                            break;

                                        //クライアントが魔術師【5】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                        case 5:
                                            switch (OwnEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが王子【7】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】
                                        case 7:
                                            switch (OwnEsc)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    //ホストだけが将軍【6】を出していた場合
                    else if (OwnGeneE == true)
                    {
                        //ホストが将軍【6】を出していたとき
                        //ホストとクライアントの数値が同じ場合
                        //OwnerNumの数値は+2されている
                        if (OwnerNum == OtherNum)
                        {
                            //ホストが将軍【6】・王子【7】以外の場合
                            //クライアントが道化【0】・姫【1】以外の場合
                            switch (OwnEsc)
                            {
                                //ホストが道化【0（+2）】の場合
                                //クライアントは密偵【2】になる
                                case 0:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;

                                //ホストが姫【1（+2）】の場合
                                //クライアントは暗殺者【3】になる
                                case 1:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;

                                //ホストが密偵【2（+2）】の場合
                                //クライアントは大臣【4】になる
                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    SpyE = true;
                                    OwnGeneE = false;
                                    break;

                                //ホストが暗殺者【3（+2）】の場合
                                //クライアントは魔術師【5】になる
                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;

                                //ホストが大臣【4（+2）】の場合
                                //クライアントは将軍【6】になる
                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;

                                //ホストが魔術師【5（+2）】の場合
                                //クライアントは王子【7】になる
                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;
                            }
                        }

                        //ホストが将軍【6】を出していたとき
                        //ホストの数値が大きい場合
                        //OwnerNumの数値は+2されている
                        else if (OwnerNum > OtherNum)
                        {
                            //クライアントが道化【0】を使った場合
                            if (OtherNum == 0)
                            {
                                //元の数値を使う
                                switch (OwnEsc)
                                {
                                    case 0:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        SpyE = true;
                                        OwnGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;

                                        OwnGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;
                                }
                            }

                            //クライアントが道化【0】を使っていない場合
                            else
                            {
                                //ホストが将軍【6】を出していたとき
                                //ホストが大臣【4】で勝った場合
                                //OwnerNumの数値は+2されている
                                //元の数値を使う
                                if (OwnEsc == 4)
                                {
                                    //ホストが大臣【4】を出していて
                                    //勝つパターンは姫【1】・密偵【2】・大臣【4】・魔術師【5】
                                    switch (OtherNum)
                                    {
                                        case 1:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "    YOU 2WIN!";

                                            WLCal += 1;
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;

                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "    YOU 2WIN!";

                                            WLCal += 1;
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;

                                        //ホストが負ける処理
                                        case 3:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "   YOU LOSE...";

                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;

                                        case 4:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "    YOU 2WIN!";

                                            WLCal += 1;
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;

                                        case 5:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "     YOU WIN!";

                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;
                                    }
                                }

                                //ホストが将軍【6】を出していたとき
                                //ホストが大臣【4】以外で勝った場合
                                //OwnerNumの数値は+2されている
                                //元の数値を使う
                                else
                                {
                                    switch (OwnEsc)
                                    {
                                        case 0:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "NEXT BATTLE!?";

                                            WLCal += 1;

                                            OwnGeneE = false;
                                            break;

                                        //ホストが姫【1（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 1:
                                            switch (OtherNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが密偵【2（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        //負けるパターンは暗殺者【3】のみ
                                        case 2:
                                            switch (OtherNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが暗殺者【3（+2）】で勝つ場合
                                        //負けるパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                        case 3:
                                            switch (OtherNum)
                                            {
                                                //ホストが負ける処理
                                                case 1:
                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 2:
                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 3:
                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 4:
                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU 2LOSE...";

                                                    WLCal += 1;
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが魔術師【5（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                        case 5:
                                            switch (OtherNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 6:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;
                                            }
                                            break;

                                        //ホストが王子【7（+2）】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                        case 7:
                                            switch (OtherNum)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 6:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;

                                                case 7:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        //ホストが将軍【6】を出していたとき
                        //ホストの数値が小さい場合
                        //OwnerNumの数値は+2されている
                        else if (OwnerNum < OtherNum)
                        {
                            //ホストが道化【0】を使った場合
                            //元の数値を使う
                            if (OwnEsc == 0)
                            {
                                switch (OtherNum)
                                {
                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        OwnLOSE += WLCal;
                                        WLCal = 1;

                                        OwnGeneE = false;
                                        break;

                                    case 6:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = true;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;
                                }
                            }

                            //ホストが道化【0】を使っていない場合
                            else
                            {
                                //ホストが将軍【6】を出していたとき
                                //クライアントが大臣【4】で勝った場合
                                //勝つパターンは姫【1】のみ
                                //OwnerNumの数値は+2されている
                                if (OtherNum == 4)
                                {
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "   YOU 2LOSE...";

                                    WLCal += 1;
                                    OwnLOSE += WLCal;

                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                    WLCal = 1;

                                    OwnGeneE = false;
                                }

                                //ホストが将軍【6】を出していたとき
                                //クライアントが大臣【4】以外で勝った場合
                                //OwnerNumの数値は+2されている
                                //元の数値を使う
                                else
                                {
                                    switch (OtherNum)
                                    {
                                        //クライアントが魔術師【5】の場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 5:
                                            switch (OwnEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが将軍【6】の場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        //負けるパターンは暗殺者【3】のみ
                                        case 6:
                                            switch (OwnEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;

                                                //クライアントが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;
                                            }
                                            break;

                                        //クライアントが王子【7】の場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】
                                        case 7:
                                            switch (OwnEsc)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    //クライアントだけが将軍【6】を出していた場合
                    else if (OtherGeneE == true)
                    {
                        //クライアントが将軍【6】を出していたとき
                        //ホストとクライアントの数値が同じ場合
                        //OtherNumの数値は+2されている
                        if (OwnerNum == OtherNum)
                        {
                            //クライアントが将軍【6】・王子【7】以外の場合
                            //ホストが道化【0】・姫【1】以外の場合
                            switch (OwnerNum)
                            {
                                //クライアントが道化【0（+2）】の場合
                                //ホストは密偵【2】になる
                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    SpyE = true;
                                    OtherGeneE = false;
                                    break;

                                //クライアントが姫【1（+2）】の場合
                                //ホストは暗殺者【3】になる
                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OtherGeneE = false;
                                    break;

                                //クライアントが密偵【2（+2）】の場合
                                //ホストは大臣【4】になる
                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OtherGeneE = false;
                                    break;

                                //クライアントが暗殺者【3（+2）】の場合
                                //ホストは魔術師【5】になる
                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OtherGeneE = false;
                                    break;

                                //クライアントが大臣【4（+2）】の場合
                                //ホストは将軍【6】になる
                                case 6:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = true;
                                    OtherGeneE = false;
                                    break;

                                //クライアントが魔術師【5（+2）】の場合
                                //ホストは王子【7】になる
                                case 7:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OtherGeneE = false;
                                    break;
                            }
                        }

                        //クライアントが将軍【6】を出していたとき
                        //ホストの数値が大きい場合
                        //OtherNumの数値は+2されている
                        else if (OwnerNum > OtherNum)
                        {
                            //クライアントが道化【0】を使った場合
                            if (OtherEsc == 0)
                            {
                                switch (OwnerNum)
                                {
                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;

                                        OtherGeneE = false;
                                        break;

                                    case 6:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = true;
                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;
                                }
                            }

                            //クライアントが道化【0】を使っていない場合
                            else
                            {
                                //クライアントが将軍【6】を出していたとき
                                //ホストが大臣【4】で勝った場合
                                //OtherNumの数値は+2されている
                                if (OwnerNum == 4)
                                {
                                    //ホストが大臣【4】で勝つ場合
                                    //勝つパターンは姫【1】のみ
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "    YOU 2WIN!";

                                    WLCal += 1;
                                    OwnWIN += WLCal;

                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                    WLCal = 1;

                                    OtherGeneE = false;
                                }

                                //ホストが大臣【4】以外で勝った場合
                                //OtherNumの数値は+2されている
                                else
                                {
                                    switch (OwnerNum)
                                    {
                                        //ホストが魔術師【5】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 5:
                                            switch (OtherEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが将軍【6】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 6:
                                            switch (OtherEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = true;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが王子【7】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】
                                        case 7:
                                            switch (OtherEsc)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        //クライアントが将軍【6】を出していたとき
                        //ホストの数値が小さい場合
                        //OtherNumの数値は+2されている
                        else if (OwnerNum < OtherNum)
                        {
                            //ホストが道化【0】を使った場合
                            if (OwnerNum == 0)
                            {
                                switch (OtherEsc)
                                {
                                    case 0:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;

                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;
                                }
                            }

                            //ホストが道化【0】を使っていない場合
                            else
                            {
                                //クライアントが将軍【6】を出していたとき
                                //クライアントが大臣【4】で勝った場合
                                //OtherNumの数値は+2されている
                                if (OtherEsc == 4)
                                {
                                    //クライアントが大臣【4】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・大臣【4】・魔術師【5】
                                    //負けるパターンは暗殺者【3】のみ
                                    switch (OwnerNum)
                                    {
                                        case 1:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            WLCal += 1;
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            OtherGeneE = false;
                                            break;

                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            WLCal += 1;
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            SpyE = true;
                                            OtherGeneE = false;
                                            break;

                                        //クライアントが負ける処理
                                        case 3:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU WIN!";

                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OtherGeneE = false;
                                            break;

                                        case 4:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            WLCal += 1;
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            OtherGeneE = false;
                                            break;

                                        case 5:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU LOSE...";

                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            OtherGeneE = false;
                                            break;
                                    }
                                }

                                //クライアントが大臣【4】以外で勝った場合
                                //OtherNumの数値は+2されている
                                else
                                {
                                    switch (OtherEsc)
                                    {
                                        case 0:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "NEXT BATTLE!?";

                                            WLCal += 1;

                                            OtherGeneE = false;
                                            break;

                                        //クライアントが姫【1（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 1:
                                            switch (OwnerNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが密偵【2（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 2:
                                            switch (OwnerNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが暗殺者【3（+2）】で勝つ場合
                                        //勝つパターンは将軍【6】
                                        case 3:
                                            switch (OwnerNum)
                                            {
                                                //クライアントが負ける処理
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "    YOU 2WIN!";

                                                    WLCal += 1;
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが魔術師【5（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                        case 5:
                                            switch (OwnerNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 6:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが王子【7（+2）】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】・王子【7】
                                        case 7:
                                            switch (OwnerNum)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 6:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                case 7:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                //どちらも将軍【6】が出されていない場合
                else
                {
                    //将軍【6】は使われていないとき
                    //ホストとクライアントの数値が同じ場合
                    if (OwnerNum == OtherNum)
                    {
                        //どちらも道化【0】を使った場合
                        if (OwnerNum == 0 && OtherNum == 0)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                            YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                            bors.BS.text = "NEXT BATTLE!?";

                            WLCal += 1;
                        }

                        //どちらも道化【0】を使っていない場合
                        else
                        {
                            switch (OwnerNum)
                            {
                                case 1:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 6:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = true;
                                    OtherGeneE = true;
                                    break;

                                case 7:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;
                            }
                        }
                    }

                    //将軍【6】は使われていないとき
                    //ホストの数値が大きい場合
                    else if (OwnerNum > OtherNum)
                    {
                        //クライアントが道化【0】を使った場合
                        if (OtherNum == 0)
                        {
                            switch (OwnerNum)
                            {
                                case 1:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    SpyE = true;
                                    break;

                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "     YOU WIN!";

                                    OwnWIN += WLCal;

                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                    WLCal = 1;
                                    break;

                                case 6:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    OwnGeneE = true;
                                    break;

                                case 7:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;
                            }
                        }

                        //クライアントが道化【0】を使っていない場合
                        else
                        {
                            //ホストが大臣【4】で勝った場合
                            if (OwnerNum == 4)
                            {
                                //ホストが大臣【4】を出していて
                                //勝つパターンは姫【1】・密偵【2】
                                //負けるパターンは暗殺者【3】のみ
                                switch (OtherNum)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "    YOU 2WIN!";

                                        WLCal += 1;
                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "    YOU 2WIN!";

                                        WLCal += 1;
                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;
                                        break;

                                    //ホストが負ける処理
                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;
                                        break;
                                }
                            }

                            //ホストが大臣【4】以外で勝った場合
                            else
                            {
                                switch (OwnerNum)
                                {
                                    //ホストが密偵【2】で勝つ場合
                                    //勝つパターンは姫【1】のみ
                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;

                                        SpyE = true;
                                        break;

                                    case 3:
                                        switch (OtherNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //ホストが魔術師【5】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                    case 5:
                                        switch (OtherNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //ホストが将軍【6】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・大臣【4】・魔術師【5】
                                    case 6:
                                        switch (OtherNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;

                                            //ホストが負ける処理
                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;

                                            case 5:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //ホストが王子【7】で勝つ場合
                                    //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                    case 7:
                                        switch (OtherNum)
                                        {
                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 5:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 6:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OtherGeneE = true;
                                                break;
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    //将軍【6】は使われていないとき
                    //ホストの数値が小さい場合
                    else if (OwnerNum < OtherNum)
                    {
                        //ホストが道化【0】を使った場合
                        if (OwnerNum == 0)
                        {
                            switch (OtherNum)
                            {
                                case 1:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "   YOU LOSE...";

                                    OwnLOSE += WLCal;

                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                    WLCal = 1;
                                    break;

                                case 6:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    OtherGeneE = true;
                                    break;

                                case 7:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;
                            }
                        }

                        //ホストが道化【0】を使っていない場合
                        else
                        {
                            //クライアントが大臣【4】で勝った場合
                            if (OtherNum == 4)
                            {
                                //クライアントが大臣【4】で勝つ場合
                                //勝つパターンは姫【1】・密偵【2】
                                switch (OwnerNum)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU 2LOSE...";

                                        WLCal += 1;
                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU 2LOSE...";

                                        WLCal += 1;
                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;

                                        SpyE = true;
                                        break;

                                    //クライアントが負ける処理
                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;
                                        break;
                                }
                            }

                            //クライアントが大臣【4】以外で勝った場合
                            else
                            {
                                switch (OtherNum)
                                {
                                    //クライアントが密偵【2】で勝つ場合
                                    //勝つパターンは姫【1】のみ
                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;
                                        break;

                                    //クライアントが暗殺者【3】で勝つ場合
                                    //負けるパターンは姫【1】・密偵【2】
                                    case 3:
                                        switch (OwnerNum)
                                        {
                                            //クライアントが負ける処理
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            //クライアントが負ける処理
                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                SpyE = true;
                                                break;
                                        }
                                        break;

                                    //クライアントが魔術師【5】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                    case 5:
                                        switch (OwnerNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //クライアントが将軍【6】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・大臣【4】・魔術師【5】
                                    case 6:
                                        switch (OwnerNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                OtherGeneE = true;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                SpyE = true;
                                                OtherGeneE = true;
                                                break;

                                            //クライアントが負ける処理
                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OtherGeneE = true;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                OtherGeneE = true;
                                                break;

                                            case 5:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //クライアントが王子【7】で勝つ場合
                                    //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                    case 7:
                                        switch (OwnerNum)
                                        {
                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                SpyE = true;
                                                break;

                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 5:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 6:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            //初期化
            OwnerNum = -1;
            OtherNum = -1;
        }

        //クライアント側の処理
        else
        {
            GeneCal();

            //特殊勝利条件：姫【1】と王子【7】の場合
            if (OwnerNum == 1 && OtherNum == 7 || OwnerNum == 7 && OtherNum == 1 ||
                OwnEsc == 1 && OtherEsc == 7 || OwnEsc == 7 && OtherEsc == 1 ||
                OwnEsc == 1 && OtherNum == 7 || OwnEsc == 7 && OtherNum == 1 ||
                OwnerNum == 1 && OtherEsc == 7 || OwnerNum == 7 && OtherEsc == 1)
            {
                //どちらかが将軍【6】が出していた場合
                if (OwnGeneE == true || OtherGeneE == true)
                {
                    //両方が将軍【6】を出していた場合
                    if (OwnGeneE == true && OtherGeneE == true)
                    {
                        //ホストが姫【1】、クライアントが王子【7】の場合
                        if (OwnEsc == 1 && OtherEsc == 7)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                            YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n     YOU WIN!";
                            OwnGeneE = false;
                            OtherGeneE = false;
                            RSBF = true;
                        }

                        //ホストが王子【7】、クライアントが姫【1】の場合
                        else if (OwnEsc == 7 && OtherEsc == 1)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                            YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n   YOU LOSE...";
                            OwnGeneE = false;
                            OtherGeneE = false;
                            RSBF = true;
                        }
                    }

                    //ホストが将軍【6】を出していた場合
                    else if (OwnGeneE == true)
                    {
                        //ホストが姫【1】、クライアントが王子【7】の場合
                        if (OwnEsc == 1 && OtherNum == 7)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                            YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n     YOU WIN!";
                            OwnGeneE = false;
                            RSBF = true;
                        }

                        //ホストが王子【7】、クライアントが姫【1】の場合
                        else if (OwnEsc == 7 && OtherNum == 1)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                            YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n   YOU LOSE...";
                            OwnGeneE = false;
                            RSBF = true;
                        }
                    }

                    //クライアントが将軍【6】を出していた場合
                    else if (OtherGeneE == true)
                    {
                        //ホストが姫【1】、クライアントが王子【7】の場合
                        if (OwnerNum == 1 && OtherEsc == 7)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                            YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n     YOU WIN!";
                            OtherGeneE = false;
                            RSBF = true;
                        }

                        //ホストが王子【7】、クライアントが姫【1】の場合
                        else if (OwnerNum == 7 && OtherEsc == 1)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                            YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                            bors.BS.text = "BATTLE ENDED\n   YOU LOSE...";
                            OtherGeneE = false;
                            RSBF = true;
                        }
                    }
                }

                //どちらも将軍【6】を出していない場合
                else
                {
                    //ホストが姫【1】、クライアントが王子【7】の場合
                    if (OwnerNum == 1 && OtherNum == 7)
                    {
                        Debug.Log("Owner: " + OwnerNum);
                        Debug.Log("Other: " + OtherNum);

                        OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                        YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                        bors.BS.text = "BATTLE ENDED\n     YOU WIN!";
                        RSBF = true;
                    }

                    //ホストが王子【7】、クライアントが姫【1】の場合
                    else if (OwnerNum == 7 && OtherNum == 1)
                    {
                        Debug.Log("Owner: " + OwnerNum);
                        Debug.Log("Other: " + OtherNum);

                        OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                        YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                        bors.BS.text = "BATTLE ENDED\n   YOU LOSE...";
                        RSBF = true;
                    }
                }
            }

            //特殊勝利以外の処理
            else
            {
                //どちらかが将軍【6】が出していた場合
                if (OwnGeneE == true || OtherGeneE == true)
                {
                    //両方が将軍【6】を出していた場合
                    if (OwnGeneE == true && OtherGeneE == true)
                    {
                        //両方、将軍【6】が使われてるとき
                        //ホストとクライアントの数値が同じ場合
                        if (OwnerNum == OtherNum)
                        {
                            //道化【0】を使った場合
                            if (OwnEsc == 0 && OtherEsc == 0)
                            {
                                Debug.Log("Owner: " + OwnerNum);
                                Debug.Log("Other: " + OtherNum);

                                OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                bors.BS.text = "NEXT BATTLE!?";

                                WLCal += 1;

                                OwnGeneE = false;
                                OtherGeneE = false;
                            }

                            //道化【0】を使っていない場合
                            else
                            {
                                switch (OwnEsc)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "       DRAW!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;
                                }
                            }
                        }

                        //両方、将軍【6】が使われてるとき
                        //ホストの数値が大きい場合
                        else if (OwnerNum > OtherNum)
                        {
                            //クライアントが道化【0】を使った場合
                            if (OtherEsc == 0)
                            {
                                switch (OwnEsc)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        SpyE = true;
                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        //勝敗数計算
                                        OwnWIN += WLCal;
                                        //勝敗数の表示
                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        //加算値を初期化
                                        WLCal = 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;
                                }
                            }

                            //クライアントが道化【0】を使っていない場合
                            else
                            {
                                //ホストが大臣【4】で勝った場合
                                if (OwnEsc == 4)
                                {
                                    //ホストが大臣【4】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】
                                    //負けるパターンは暗殺者【3】のみ
                                    switch (OtherEsc)
                                    {
                                        case 1:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU 2WIN!";

                                            //追加勝敗数
                                            WLCal += 1;
                                            //勝敗数計算
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            //加算値を初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU 2WIN!";

                                            //追加勝敗数
                                            WLCal += 1;
                                            //勝敗数計算
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            //加算値を初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        //ホストが負ける処理
                                        case 3:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU LOSE...";

                                            //勝敗数計算
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN  : " + OwnLOSE.ToString();

                                            //加算値を初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;
                                    }
                                }

                                //ホストが大臣【4】以外で勝った場合
                                else
                                {
                                    switch (OwnEsc)
                                    {
                                        //ホストが密偵【2】で勝つ場合
                                        //勝つパターンは姫【1】
                                        //暗殺者【3】に勝つ処理は別に書く
                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU WIN!";

                                            //勝敗数計算
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            //加算値を初期化
                                            WLCal = 1;

                                            SpyE = true;
                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        //ホストが暗殺者【3】の場合
                                        //負けるパターンは姫【1】・密偵【2】
                                        //大臣【4】に勝つ処理は別に書く
                                        case 3:
                                            switch (OtherEsc)
                                            {
                                                //ホストが負ける処理
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが魔術師【5】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                        case 5:
                                            switch (OtherEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが王子【7】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】
                                        case 7:
                                            switch (OtherEsc)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算値を初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        //両方、将軍【6】が使われてるとき
                        //ホストの数値が小さい場合
                        else if (OwnerNum < OtherNum)
                        {
                            //ホストが道化【0】を使った場合
                            if (OwnEsc == 0)
                            {
                                switch (OtherEsc)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        //勝敗数計算
                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        //勝敗数初期化
                                        WLCal = 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        //勝敗数加算
                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = false;
                                        break;
                                }
                            }

                            //ホストが道化【0】を使っていない場合
                            else
                            {
                                //クライアントが大臣【4】で勝った場合
                                if (OtherEsc == 4)
                                {
                                    //クライアントが大臣【4】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】
                                    //負けるパターンは暗殺者【3】のみ
                                    switch (OwnEsc)
                                    {
                                        case 1:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            //追加勝敗数
                                            WLCal += 1;
                                            //勝敗数計算
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            //加算勝敗数初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            //追加勝敗数
                                            WLCal += 1;
                                            //勝敗数計算
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            //加算勝敗数初期化
                                            WLCal = 1;

                                            SpyE = true;
                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        //クライアントが負ける処理
                                        case 3:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU WIN!";

                                            //勝敗数計算
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            //加算勝敗数初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;
                                    }
                                }

                                //クライアントが大臣【4】以外で勝った場合
                                else
                                {
                                    switch (OtherEsc)
                                    {
                                        //クライアントが密偵【2】で勝つ場合
                                        //勝つパターンは姫【1】のみ
                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU LOSE...";

                                            //勝敗数計算
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            //加算勝敗数初期化
                                            WLCal = 1;

                                            OwnGeneE = false;
                                            OtherGeneE = false;
                                            break;

                                        //クライアントが暗殺者【3】で勝つ場合
                                        //負けるパターンは姫【1】・密偵【2】
                                        //大臣【4】に勝つ処理は別に書く
                                        case 3:
                                            switch (OwnEsc)
                                            {
                                                //クライアントが負ける処理
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    //勝敗数計算
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }

                                            break;

                                        //クライアントが魔術師【5】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                        case 5:
                                            switch (OwnEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが王子【7】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】
                                        case 7:
                                            switch (OwnEsc)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    //勝敗数計算
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    //加算勝敗数初期化
                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    //ホストだけが将軍【6】を出していた場合
                    else if (OwnGeneE == true)
                    {
                        //ホストが将軍【6】を出していたとき
                        //ホストとクライアントの数値が同じ場合
                        //OwnerNumの数値は+2されている
                        if (OwnerNum == OtherNum)
                        {
                            //ホストが将軍【6】・王子【7】以外の場合
                            //クライアントが道化【0】・姫【1】以外の場合
                            switch (OwnEsc)
                            {
                                //ホストが道化【0（+2）】の場合
                                //クライアントは密偵【2】になる
                                case 0:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;

                                //ホストが姫【1（+2）】の場合
                                //クライアントは暗殺者【3】になる
                                case 1:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;

                                //ホストが密偵【2（+2）】の場合
                                //クライアントは大臣【4】になる
                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    SpyE = true;
                                    OwnGeneE = false;
                                    break;

                                //ホストが暗殺者【3（+2）】の場合
                                //クライアントは魔術師【5】になる
                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;

                                //ホストが大臣【4（+2）】の場合
                                //クライアントは将軍【6】になる
                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;

                                //ホストが魔術師【5（+2）】の場合
                                //クライアントは王子【7】になる
                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = false;
                                    break;
                            }
                        }

                        //ホストが将軍【6】を出していたとき
                        //ホストの数値が大きい場合
                        //OwnerNumの数値は+2されている
                        else if (OwnerNum > OtherNum)
                        {
                            //クライアントが道化【0】を使った場合
                            if (OtherNum == 0)
                            {
                                //元の数値を使う
                                switch (OwnEsc)
                                {
                                    case 0:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        SpyE = true;
                                        OwnGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;

                                        OwnGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;
                                }
                            }

                            //クライアントが道化【0】を使っていない場合
                            else
                            {
                                //ホストが将軍【6】を出していたとき
                                //ホストが大臣【4】で勝った場合
                                //OwnerNumの数値は+2されている
                                //元の数値を使う
                                if (OwnEsc == 4)
                                {
                                    //ホストが大臣【4】を出していて
                                    //勝つパターンは姫【1】・密偵【2】・大臣【4】・魔術師【5】
                                    switch (OtherNum)
                                    {
                                        case 1:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "    YOU 2WIN!";

                                            WLCal += 1;
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;

                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "    YOU 2WIN!";

                                            WLCal += 1;
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;

                                        //ホストが負ける処理
                                        case 3:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "   YOU LOSE...";

                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;

                                        case 4:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "    YOU 2WIN!";

                                            WLCal += 1;
                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;

                                        case 5:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "     YOU WIN!";

                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OwnGeneE = false;
                                            break;
                                    }
                                }

                                //ホストが将軍【6】を出していたとき
                                //ホストが大臣【4】以外で勝った場合
                                //OwnerNumの数値は+2されている
                                //元の数値を使う
                                else
                                {
                                    switch (OwnEsc)
                                    {
                                        case 0:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                            YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                            bors.BS.text = "NEXT BATTLE!?";

                                            WLCal += 1;

                                            OwnGeneE = false;
                                            break;

                                        //ホストが姫【1（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 1:
                                            switch (OtherNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが密偵【2（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        //負けるパターンは暗殺者【3】のみ
                                        case 2:
                                            switch (OtherNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが暗殺者【3（+2）】で勝つ場合
                                        //負けるパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                        case 3:
                                            switch (OtherNum)
                                            {
                                                //ホストが負ける処理
                                                case 1:
                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 2:
                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 3:
                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 4:
                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU 2LOSE...";

                                                    WLCal += 1;
                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが魔術師【5（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                        case 5:
                                            switch (OtherNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 6:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;
                                            }
                                            break;

                                        //ホストが王子【7（+2）】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                        case 7:
                                            switch (OtherNum)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 6:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;

                                                case 7:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        //ホストが将軍【6】を出していたとき
                        //ホストの数値が小さい場合
                        //OwnerNumの数値は+2されている
                        else if (OwnerNum < OtherNum)
                        {
                            //ホストが道化【0】を使った場合
                            //元の数値を使う
                            if (OwnEsc == 0)
                            {
                                switch (OtherNum)
                                {
                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        OwnLOSE += WLCal;
                                        WLCal = 1;

                                        OwnGeneE = false;
                                        break;

                                    case 6:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        OtherGeneE = true;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "（+2）" + "】を出しました。";
                                        YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = false;
                                        break;
                                }
                            }

                            //ホストが道化【0】を使っていない場合
                            else
                            {
                                //ホストが将軍【6】を出していたとき
                                //クライアントが大臣【4】で勝った場合
                                //勝つパターンは姫【1】のみ
                                //OwnerNumの数値は+2されている
                                if (OtherNum == 4)
                                {
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "   YOU 2LOSE...";

                                    WLCal += 1;
                                    OwnLOSE += WLCal;

                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                    WLCal = 1;

                                    OwnGeneE = false;
                                }

                                //ホストが将軍【6】を出していたとき
                                //クライアントが大臣【4】以外で勝った場合
                                //OwnerNumの数値は+2されている
                                //元の数値を使う
                                else
                                {
                                    switch (OtherNum)
                                    {
                                        //クライアントが魔術師【5】の場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 5:
                                            switch (OwnEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが将軍【6】の場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        //負けるパターンは暗殺者【3】のみ
                                        case 6:
                                            switch (OwnEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;

                                                //クライアントが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    OtherGeneE = true;
                                                    break;
                                            }
                                            break;

                                        //クライアントが王子【7】の場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】
                                        case 7:
                                            switch (OwnEsc)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OwnGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "（+2）" + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }

                    //クライアントだけが将軍【6】を出していた場合
                    else if (OtherGeneE == true)
                    {
                        //クライアントが将軍【6】を出していたとき
                        //ホストとクライアントの数値が同じ場合
                        //OtherNumの数値は+2されている
                        if (OwnerNum == OtherNum)
                        {
                            //クライアントが将軍【6】・王子【7】以外の場合
                            //ホストが道化【0】・姫【1】以外の場合
                            switch (OwnerNum)
                            {
                                //クライアントが道化【0（+2）】の場合
                                //ホストは密偵【2】になる
                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    SpyE = true;
                                    OtherGeneE = false;
                                    break;

                                //クライアントが姫【1（+2）】の場合
                                //ホストは暗殺者【3】になる
                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OtherGeneE = false;
                                    break;

                                //クライアントが密偵【2（+2）】の場合
                                //ホストは大臣【4】になる
                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OtherGeneE = false;
                                    break;

                                //クライアントが暗殺者【3（+2）】の場合
                                //ホストは魔術師【5】になる
                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OtherGeneE = false;
                                    break;

                                //クライアントが大臣【4（+2）】の場合
                                //ホストは将軍【6】になる
                                case 6:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = true;
                                    OtherGeneE = false;
                                    break;

                                //クライアントが魔術師【5（+2）】の場合
                                //ホストは王子【7】になる
                                case 7:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OtherGeneE = false;
                                    break;
                            }
                        }

                        //クライアントが将軍【6】を出していたとき
                        //ホストの数値が大きい場合
                        //OtherNumの数値は+2されている
                        else if (OwnerNum > OtherNum)
                        {
                            //クライアントが道化【0】を使った場合
                            if (OtherEsc == 0)
                            {
                                switch (OwnerNum)
                                {
                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;

                                        OtherGeneE = false;
                                        break;

                                    case 6:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OwnGeneE = true;
                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;
                                }
                            }

                            //クライアントが道化【0】を使っていない場合
                            else
                            {
                                //クライアントが将軍【6】を出していたとき
                                //ホストが大臣【4】で勝った場合
                                //OtherNumの数値は+2されている
                                if (OwnerNum == 4)
                                {
                                    //ホストが大臣【4】で勝つ場合
                                    //勝つパターンは姫【1】のみ
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                    bors.BS.text = "    YOU 2WIN!";

                                    WLCal += 1;
                                    OwnWIN += WLCal;

                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                    WLCal = 1;

                                    OtherGeneE = false;
                                }

                                //ホストが大臣【4】以外で勝った場合
                                //OtherNumの数値は+2されている
                                else
                                {
                                    switch (OwnerNum)
                                    {
                                        //ホストが魔術師【5】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 5:
                                            switch (OtherEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが将軍【6】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 6:
                                            switch (OtherEsc)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                //ホストが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = true;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //ホストが王子【7】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】
                                        case 7:
                                            switch (OtherEsc)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }

                        //クライアントが将軍【6】を出していたとき
                        //ホストの数値が小さい場合
                        //OtherNumの数値は+2されている
                        else if (OwnerNum < OtherNum)
                        {
                            //ホストが道化【0】を使った場合
                            if (OwnerNum == 0)
                            {
                                switch (OtherEsc)
                                {
                                    case 0:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 4:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;

                                    case 5:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;

                                        OtherGeneE = false;
                                        break;

                                    case 7:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                        bors.BS.text = "NEXT BATTLE!?";

                                        WLCal += 1;

                                        OtherGeneE = false;
                                        break;
                                }
                            }

                            //ホストが道化【0】を使っていない場合
                            else
                            {
                                //クライアントが将軍【6】を出していたとき
                                //クライアントが大臣【4】で勝った場合
                                //OtherNumの数値は+2されている
                                if (OtherEsc == 4)
                                {
                                    //クライアントが大臣【4】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・大臣【4】・魔術師【5】
                                    //負けるパターンは暗殺者【3】のみ
                                    switch (OwnerNum)
                                    {
                                        case 1:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            WLCal += 1;
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            OtherGeneE = false;
                                            break;

                                        case 2:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            WLCal += 1;
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            SpyE = true;
                                            OtherGeneE = false;
                                            break;

                                        //クライアントが負ける処理
                                        case 3:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "     YOU WIN!";

                                            OwnWIN += WLCal;

                                            OwnWin.text = "WIN : " + OwnWIN.ToString();
                                            OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                            WLCal = 1;

                                            OtherGeneE = false;
                                            break;

                                        case 4:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU 2LOSE...";

                                            WLCal += 1;
                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            OtherGeneE = false;
                                            break;

                                        case 5:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は大臣【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "   YOU LOSE...";

                                            OwnLOSE += WLCal;

                                            OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                            OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                            WLCal = 1;

                                            OtherGeneE = false;
                                            break;
                                    }
                                }

                                //クライアントが大臣【4】以外で勝った場合
                                //OtherNumの数値は+2されている
                                else
                                {
                                    switch (OtherEsc)
                                    {
                                        case 0:
                                            Debug.Log("Owner: " + OwnerNum);
                                            Debug.Log("Other: " + OtherNum);

                                            OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                            YOU.YT.text = "相手は道化【" + OtherNum + "（+2）" + "】を出しました。";
                                            bors.BS.text = "NEXT BATTLE!?";

                                            WLCal += 1;

                                            OtherGeneE = false;
                                            break;

                                        //クライアントが姫【1（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 1:
                                            switch (OwnerNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は姫【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが密偵【2（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】
                                        case 2:
                                            switch (OwnerNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は密偵【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが暗殺者【3（+2）】で勝つ場合
                                        //勝つパターンは将軍【6】
                                        case 3:
                                            switch (OwnerNum)
                                            {
                                                //クライアントが負ける処理
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "     YOU WIN!";

                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                //クライアントが負ける処理
                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "    YOU 2WIN!";

                                                    WLCal += 1;
                                                    OwnWIN += WLCal;

                                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが魔術師【5（+2）】で勝つ場合
                                        //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                        case 5:
                                            switch (OwnerNum)
                                            {
                                                case 1:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 6:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;

                                        //クライアントが王子【7（+2）】で勝つ場合
                                        //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】・王子【7】
                                        case 7:
                                            switch (OwnerNum)
                                            {
                                                case 2:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    SpyE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                case 3:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 4:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 5:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;

                                                case 6:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OwnGeneE = true;
                                                    OtherGeneE = false;
                                                    break;

                                                case 7:
                                                    Debug.Log("Owner: " + OwnerNum);
                                                    Debug.Log("Other: " + OtherNum);

                                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                    YOU.YT.text = "相手は王子【" + OtherNum + "（+2）" + "】を出しました。";
                                                    bors.BS.text = "   YOU LOSE...";

                                                    OwnLOSE += WLCal;

                                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                    WLCal = 1;

                                                    OtherGeneE = false;
                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                //どちらも将軍【6】が出されていない場合
                else
                {
                    //将軍【6】は使われていないとき
                    //ホストとクライアントの数値が同じ場合
                    if (OwnerNum == OtherNum)
                    {
                        //どちらも道化【0】を使った場合
                        if (OwnerNum == 0 && OtherNum == 0)
                        {
                            Debug.Log("Owner: " + OwnerNum);
                            Debug.Log("Other: " + OtherNum);

                            OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                            YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                            bors.BS.text = "NEXT BATTLE!?";

                            WLCal += 1;
                        }

                        //どちらも道化【0】を使っていない場合
                        else
                        {
                            switch (OwnerNum)
                            {
                                case 1:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;

                                case 6:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;

                                    OwnGeneE = true;
                                    OtherGeneE = true;
                                    break;

                                case 7:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "       DRAW!?";

                                    WLCal += 1;
                                    break;
                            }
                        }
                    }

                    //将軍【6】は使われていないとき
                    //ホストの数値が大きい場合
                    else if (OwnerNum > OtherNum)
                    {
                        //クライアントが道化【0】を使った場合
                        if (OtherNum == 0)
                        {
                            switch (OwnerNum)
                            {
                                case 1:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    SpyE = true;
                                    break;

                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "     YOU WIN!";

                                    OwnWIN += WLCal;

                                    OwnWin.text = "WIN : " + OwnWIN.ToString();
                                    OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                    WLCal = 1;
                                    break;

                                case 6:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    OwnGeneE = true;
                                    break;

                                case 7:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は道化【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;
                            }
                        }

                        //クライアントが道化【0】を使っていない場合
                        else
                        {
                            //ホストが大臣【4】で勝った場合
                            if (OwnerNum == 4)
                            {
                                //ホストが大臣【4】を出していて
                                //勝つパターンは姫【1】・密偵【2】
                                //負けるパターンは暗殺者【3】のみ
                                switch (OtherNum)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "    YOU 2WIN!";

                                        WLCal += 1;
                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "    YOU 2WIN!";

                                        WLCal += 1;
                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;
                                        break;

                                    //ホストが負ける処理
                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;
                                        break;
                                }
                            }

                            //ホストが大臣【4】以外で勝った場合
                            else
                            {
                                switch (OwnerNum)
                                {
                                    //ホストが密偵【2】で勝つ場合
                                    //勝つパターンは姫【1】のみ
                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;

                                        SpyE = true;
                                        break;

                                    case 3:
                                        switch (OtherNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //ホストが魔術師【5】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                    case 5:
                                        switch (OtherNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //ホストが将軍【6】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・大臣【4】・魔術師【5】
                                    case 6:
                                        switch (OtherNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;

                                            //ホストが負ける処理
                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;

                                            case 5:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //ホストが王子【7】で勝つ場合
                                    //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                    case 7:
                                        switch (OtherNum)
                                        {
                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 5:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            case 6:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは王子【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OtherGeneE = true;
                                                break;
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    //将軍【6】は使われていないとき
                    //ホストの数値が小さい場合
                    else if (OwnerNum < OtherNum)
                    {
                        //ホストが道化【0】を使った場合
                        if (OwnerNum == 0)
                        {
                            switch (OtherNum)
                            {
                                case 1:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は姫【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 2:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 3:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 4:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;

                                case 5:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "   YOU LOSE...";

                                    OwnLOSE += WLCal;

                                    OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                    OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                    WLCal = 1;
                                    break;

                                case 6:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;

                                    OtherGeneE = true;
                                    break;

                                case 7:
                                    Debug.Log("Owner: " + OwnerNum);
                                    Debug.Log("Other: " + OtherNum);

                                    OWN.OT.text = "あなたは道化【" + OwnerNum + "】を出しました。";
                                    YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                    bors.BS.text = "NEXT BATTLE!?";

                                    WLCal += 1;
                                    break;
                            }
                        }

                        //ホストが道化【0】を使っていない場合
                        else
                        {
                            //クライアントが大臣【4】で勝った場合
                            if (OtherNum == 4)
                            {
                                //クライアントが大臣【4】で勝つ場合
                                //勝つパターンは姫【1】・密偵【2】
                                switch (OwnerNum)
                                {
                                    case 1:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU 2LOSE...";

                                        WLCal += 1;
                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;
                                        break;

                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU 2LOSE...";

                                        WLCal += 1;
                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;

                                        SpyE = true;
                                        break;

                                    //クライアントが負ける処理
                                    case 3:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は大臣【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "     YOU WIN!";

                                        OwnWIN += WLCal;

                                        OwnWin.text = "WIN : " + OwnWIN.ToString();
                                        OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                        WLCal = 1;
                                        break;
                                }
                            }

                            //クライアントが大臣【4】以外で勝った場合
                            else
                            {
                                switch (OtherNum)
                                {
                                    //クライアントが密偵【2】で勝つ場合
                                    //勝つパターンは姫【1】のみ
                                    case 2:
                                        Debug.Log("Owner: " + OwnerNum);
                                        Debug.Log("Other: " + OtherNum);

                                        OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                        YOU.YT.text = "相手は密偵【" + OtherNum + "】を出しました。";
                                        bors.BS.text = "   YOU LOSE...";

                                        OwnLOSE += WLCal;

                                        OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                        OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                        WLCal = 1;
                                        break;

                                    //クライアントが暗殺者【3】で勝つ場合
                                    //負けるパターンは姫【1】・密偵【2】
                                    case 3:
                                        switch (OwnerNum)
                                        {
                                            //クライアントが負ける処理
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;
                                                break;

                                            //クライアントが負ける処理
                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は暗殺者【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                SpyE = true;
                                                break;
                                        }
                                        break;

                                    //クライアントが魔術師【5】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・暗殺者【3】・大臣【4】
                                    case 5:
                                        switch (OwnerNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は魔術師【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //クライアントが将軍【6】で勝つ場合
                                    //勝つパターンは姫【1】・密偵【2】・大臣【4】・魔術師【5】
                                    case 6:
                                        switch (OwnerNum)
                                        {
                                            case 1:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは姫【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                OtherGeneE = true;
                                                break;

                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                SpyE = true;
                                                OtherGeneE = true;
                                                break;

                                            //クライアントが負ける処理
                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "     YOU WIN!";

                                                OwnWIN += WLCal;

                                                OwnWin.text = "WIN : " + OwnWIN.ToString();
                                                OtherLose.text = "LOSE : " + OwnWIN.ToString();

                                                WLCal = 1;

                                                OtherGeneE = true;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                OtherGeneE = true;
                                                break;

                                            case 5:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は将軍【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;
                                        }
                                        break;

                                    //クライアントが王子【7】で勝つ場合
                                    //勝つパターンは密偵【2】・暗殺者【3】・大臣【4】・魔術師【5】・将軍【6】
                                    case 7:
                                        switch (OwnerNum)
                                        {
                                            case 2:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは密偵【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                SpyE = true;
                                                break;

                                            case 3:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは暗殺者【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 4:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは大臣【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 5:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは魔術師【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;
                                                break;

                                            case 6:
                                                Debug.Log("Owner: " + OwnerNum);
                                                Debug.Log("Other: " + OtherNum);

                                                OWN.OT.text = "あなたは将軍【" + OwnerNum + "】を出しました。";
                                                YOU.YT.text = "相手は王子【" + OtherNum + "】を出しました。";
                                                bors.BS.text = "   YOU LOSE...";

                                                OwnLOSE += WLCal;

                                                OwnLose.text = "LOSE : " + OwnLOSE.ToString();
                                                OtherWin.text = "WIN : " + OwnLOSE.ToString();

                                                WLCal = 1;

                                                OwnGeneE = true;
                                                break;
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }
        //初期化
        OwnerNum = -1;
        OtherNum = -1;
    }
}