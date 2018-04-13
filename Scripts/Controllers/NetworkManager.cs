using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;

namespace Com.RegalStick.Oakenland
{
	public class NetworkManager : Photon.PunBehaviour 
	{
		#region public
		public bool isServer;
		public bool isClient;
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
		[Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
		public byte MaxPlayersPerRoom = 10;
		[Tooltip("The UI Label to inform the user that the connection is in progress")]
		public GameObject progressLabel;
		#endregion
		#region private
		private string _gameVersion = "1";
		#endregion
	
		void Awake()
		{
			PhotonNetwork.logLevel = Loglevel;
			// #Critical
			// There is no need to join lobby to get the list of rooms.
			PhotonNetwork.autoJoinLobby = false;
			// #Critical
			// This makes sure we can use PhotonNetwork.LoadLevel() on the master clients in the same room sync their level automativally
			PhotonNetwork.automaticallySyncScene = true;

			PhotonNetwork.autoCleanUpPlayerObjects = false;
		}

		void Start()
		{
			Connect ();
		}

		#region public methods

		public void Connect()
		{
			progressLabel.SetActive(true);
			//progressLabel.GetComponent<Text>().text = "Connecting to the online server.";
			// #Critical we must first and foremost connect to Photon Online Server
			PhotonNetwork.ConnectUsingSettings (_gameVersion);
		}

		#endregion

		#region Photon.PunBehaviour CallBacks

		public override void OnConnectedToMaster()
		{
			ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable() {{"username",FindObjectOfType<GameManager> ().username},{"ID",FindObjectOfType<GameManager> ().userId}};
			PhotonNetwork.player.SetCustomProperties (hash,null,false);
			progressLabel.GetComponent<Text>().text = "Joining random room.";
			Debug.Log ("OnConnectedToMaster() was called by PUN");
			PhotonNetwork.JoinRandomRoom ();
		}

		public override void OnDisconnectedFromPhoton()
		{
			progressLabel.SetActive(false);
			Debug.Log ("OnDisconnectedFromPhoton() was called by PUN");
		}

		public override void OnJoinedRoom()
		{
			string id = (string)PhotonNetwork.player.CustomProperties ["ID"];
			progressLabel.SetActive (false);
			FindObjectOfType<NavigationBaker> ().BakeNavMesh ();
			Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
		}

		public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
		{
			Debug.Log ("OnRandomJoinFailed(). Creating new room.");
			PhotonNetwork.CreateRoom (null, new RoomOptions () { MaxPlayers = MaxPlayersPerRoom },null);
			//TODO https://doc.photonengine.com/en-us/pun/current/demos-and-tutorials/pun-basics-tutorial/lobby
		}
			
		#endregion

	}
}