using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LampStudio.GoldDiggerOnline
{
    public class Launcher : Photon.PunBehaviour
    {
        [Tooltip("The Ui Panel to let the user enter name, connect and play")]
        public GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        public GameObject progressLabel;
        /// <summary>
        /// The PUN loglevel. 
        /// </summary>
        public PhotonLogLevel logLevel = PhotonLogLevel.Informational; 
        string _gameVersion = "1";
        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        /// </summary>
        /// [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        public byte MaxPlayersPerRoom = 4;
        private void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
            // #NotImportant
            // Force LogLevel
            PhotonNetwork.logLevel = logLevel;
        }
        private void Start()
        {
            //Connect();
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public void Connect()
        {
            Debug.Log("connect !!!!");
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");
            PhotonNetwork.JoinRandomRoom();
        }
        public override void OnDisconnectedFromPhoton()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
        }
        public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
        {
            PhotonNetwork.CreateRoom("Room Random", new RoomOptions() { maxPlayers = MaxPlayersPerRoom}, null);
        }
        public override void OnJoinedRoom()
        {
            Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        }
    }
}
