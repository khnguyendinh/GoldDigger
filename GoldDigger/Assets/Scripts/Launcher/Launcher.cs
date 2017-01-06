using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LampStudio.GoldDiggerOnline
{

    public class Launcher : MonoBehaviour
    {
        string _gameVersion = "1";
        private void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
        }
        private void Start()
        {
            Connect();
        }

        private void Connect()
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }
    }
}
