using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Com.RegalStick.Oakenland{
	public class GameManager : Photon.PunBehaviour{
		public string username;
		public string userId;
		public Text uname {get { return GameObject.Find ("textUserId").GetComponent<Text> ();}}
		public Text id {get{ return GameObject.Find ("textUsername").GetComponent<Text> ();}}
		public GameObject []list;
		public int food=50;
		public int water=10;
		public int wood=10;
		public int rocks=10;

		public Text foodText;
		public Text waterText;
		public Text woodText;
		public Text rocksText;

		void Start()
		{
			foodText = GameObject.Find ("textFood").GetComponent<Text> ();
			waterText = GameObject.Find ("textWater").GetComponent<Text> ();
			woodText = GameObject.Find ("textWood").GetComponent<Text> ();
			rocksText = GameObject.Find ("textRocks").GetComponent<Text> ();
			foodText.text = "Food: " + food.ToString ();
			waterText.text = "Water: " + water.ToString ();
			woodText.text = "Wood: " + wood.ToString ();
			rocksText.text = "Rocks: " + rocks.ToString ();
			}
		void Update()
		{
			if (Input.GetKeyDown (KeyCode.Escape)) 
			{
				LeaveRoom ();
			}
				
			if (SceneManager.GetActiveScene ().buildIndex == 1) 
			{
				if (uname.text == "") 
				{
					uname.text =  "Username: " + username;
					id.text =  "ID: " + userId;
				}
			}
		}

		#region photon messages

		public override void OnPhotonPlayerConnected(PhotonPlayer other)
		{
			Debug.Log ("OnPhotonPlayerConnected() " + other.ID);
			//TODO Load userdata OnPhotonPlayerConnected()

			//BAD METHOD
			/*
			BuildingControl[] a = FindObjectsOfType<BuildingControl> ();
			for (int i = 0; i < a.Length-1; i++) 
			{
				Debug.Log ("searching:" + a[i].userId + "==" + other.CustomProperties["ID"]+".");
				if(a[i].userId==(string)other.CustomProperties["ID"])
				{
					Debug.Log ("changing owner");
					a [i].gameObject.GetComponent <PhotonView> ().TransferOwnership (other.ID);
				}
			}
			*/

			if (PhotonNetwork.isMasterClient) 
			{
				Debug.Log ("OnPhotonPlayerConnected() isMasterClient" + PhotonNetwork.isMasterClient);
				//LoadLevel ();
			}
		}

		public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
		{
			Debug.Log("OnPhotonPlayerDisconnected() " + other.ID); // seen when other disconnects
			//TODO Save userdata OnPhotonPlayerDisconnected()

			if (PhotonNetwork.isMasterClient) 
			{
				Debug.Log("OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
				//LoadLevel ();

			}
		}

		public override void OnLeftRoom()
		{
			Destroy(FindObjectOfType<NetworkManager> ());
			SceneManager.LoadScene (0);
			Destroy (gameObject);
		}

		#endregion

		#region public methods

		public void LeaveRoom()
		{
			PhotonNetwork.LeaveRoom ();
			PhotonNetwork.LeaveLobby ();
			OnLeftRoom ();
		}

		#endregion

		#region Private Methods

		void LoadLevel()
		{
			if (!PhotonNetwork.isMasterClient) 
			{
				Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
			}
			Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.PlayerCount);
			PhotonNetwork.LoadLevel("main");
		}

		#endregion
	}
}
