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
        /// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
        /// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
        /// Typically this is used for the OnConnectedToMaster() callback.
        /// </summary>
        bool isConnecting;
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
            isConnecting = true;
            Debug.Log("connect !!!! ");
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                Debug.Log("PhotonNetwork.ConnectUsingSettings");
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
            // keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
            
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN ");
            // we don't want to do anything if we are not attempting to join a room. 
            // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
            // we don't want to do anything.
            if (isConnecting)
            {
                Debug.Log("PhotonNetwork.JoinRandomRoom() " );
                // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
                PhotonNetwork.JoinRandomRoom();
            }
        }
        public override void OnDisconnectedFromPhoton()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");
        }
        public override void OnJoinedLobby()
        {
            if (PhotonNetwork.insideLobby)
            {
                Debug.Log("Inside a Lobby");
                Debug.Log(PhotonNetwork.GetRoomList().ToString());
            }
        }
        public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
        {
            Debug.Log("OnPhotonCreateRoomFailed");
            PhotonNetwork.CreateRoom("Room Random", new RoomOptions() { maxPlayers = MaxPlayersPerRoom }, null);
        }
        public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            Debug.Log("OnPhotonJoinRoomFailed");
            PhotonNetwork.CreateRoom("Room Random", new RoomOptions() { maxPlayers = MaxPlayersPerRoom }, null);
        }
        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("OnPhotonRandomJoinFailed");
            PhotonNetwork.CreateRoom("Room Random", new RoomOptions() { maxPlayers = MaxPlayersPerRoom }, null);
        }
        public override void OnJoinedRoom()
        {
            // #Critical: We only load if we are the first player, else we rely on  PhotonNetwork.automaticallySyncScene to sync our instance scene.
            if (PhotonNetwork.room.PlayerCount == 1)
            {
                Debug.Log("We load the 'Room for 1' ");


                // #Critical
                // Load the Room Level. 
                PhotonNetwork.LoadLevel("Room for 1");
            }
        }
    }
}
