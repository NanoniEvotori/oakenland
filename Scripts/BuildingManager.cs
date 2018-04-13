using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

namespace Com.RegalStick.Oakenland{
	public class BuildingManager : Photon.PunBehaviour {
		#region private variables
		private GameObject campPrefab;
		private GameObject storagePrefab;
		private GameObject farmPrefab;
		private GameObject lumbermillPrefab;
		private GameObject barracksPrefab;
		private Plane buildingPlane;
		private Ray ray;
		private float distance;
		private Vector3 position;
		private string SaveBuildingDataURL = "http://localhost/oakenland/SaveBuildingData.php";
		#endregion
		#region public variables
		public bool isBuilding = false;
		public List<BuildingControl> campsList;
		public List<BuildingControl> storagesList;
		public List<BuildingControl> farmsList;
		public List<BuildingControl> lumbermillsList;
		public List<BuildingControl> baracksList;
		public GameObject newBuilding;
		#endregion

		void Awake()
		{
			campPrefab = (GameObject)Resources.Load ("pref_camp");
			storagePrefab = (GameObject)Resources.Load ("pref_storage");
			farmPrefab = (GameObject)Resources.Load ("pref_farm");
			lumbermillPrefab = (GameObject)Resources.Load ("pref_lumbermill");
			barracksPrefab = (GameObject)Resources.Load ("pref_lumbermill");
			buildingPlane = new Plane (Vector3.up, new Vector3 (0.0f, 0.2f, 0.0f));
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		}

		void Update()
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(buildingPlane.Raycast(ray,out distance))
			{
				position = ray.GetPoint(distance);	
				if (newBuilding)
				{
					newBuilding.transform.position = position;
				}
			}
			if (isBuilding == true) {
				if (Input.GetKeyDown (KeyCode.Mouse0) && FindObjectOfType<CheckGroundBelow> ().canBuild == true) 
				{
					BuildingControl bc = newBuilding.GetComponent<BuildingControl> ();
					if (bc.type.ToString () == "camp") 
					{
						campsList.Add (bc);
						bc.SetSpawnPoints (bc.maxSpace,4,bc.gameObject.transform.position);
						bc.SpawnCampUnits ();
					}
					if (bc.type.ToString () == "storage") 
					{
						storagesList.Add (bc);
					}
					if (bc.type.ToString () == "farm") 
					{
						farmsList.Add (bc);
					}
					if (bc.type.ToString () == "lumbermill") 
					{
						lumbermillsList.Add (bc);
					}
					if (bc.type.ToString () == "baracks") 
					{
						baracksList.Add (bc);
					}
					newBuilding = null;
					isBuilding = false;
				}
			}
		}

		#region private methods
		IEnumerator Save(String buildingId, String tileId, String playerId, Vector3 position)
		{
			WWWForm form = new WWWForm();
			form.AddField ("playerIdPost", playerId);
			form.AddField ("tileIdPost", tileId);
			form.AddField ("tilePositionXPost", position.x.ToString ());
			form.AddField ("tilePositionZPost", position.z.ToString ());

			UnityWebRequest www = UnityWebRequest.Post (SaveBuildingDataURL,form);
			yield return www.SendWebRequest ();

			if (www.isNetworkError || www.isHttpError) 
			{
				Debug.Log (www.error);
			}
		}
		#endregion
		#region public building methods
		public void BuildCamp()
		{
			FindObjectOfType<CursorControl> ().CursorToCenter ();
			int money = 100;
			if(money>=20)
			{
				newBuilding = (GameObject)PhotonNetwork.Instantiate("pref_camp",position,campPrefab.transform.rotation,0,null);
				//TODO Save player camp to the database.
				BuildingControl bc = newBuilding.GetComponent<BuildingControl> ();
				//PhotonView pw = newBuilding.GetComponent<PhotonView> ();
				bc.SetToCamp (FindObjectOfType<GameManager> ().userId,0);
				isBuilding = true;
			}
		}
		public void BuildStorage()
		{
			FindObjectOfType<CursorControl> ().CursorToCenter ();
			int money = 100;
			if(money>=20)
			{
				newBuilding = (GameObject)PhotonNetwork.Instantiate("pref_storage",position,storagePrefab.transform.rotation,0,null);
				//TODO Save player camp to the database.
				BuildingControl bc = newBuilding.GetComponent<BuildingControl> ();
				//PhotonView pw = newBuilding.GetComponent<PhotonView> ();
				bc.SetToStorage (FindObjectOfType<GameManager> ().userId,0);
				isBuilding = true;
			}
		}
		public void BuildFarm()
		{
			FindObjectOfType<CursorControl> ().CursorToCenter ();
			int money = 100;
			if(money>=20)
			{
				newBuilding = (GameObject)PhotonNetwork.Instantiate("pref_farm",position,farmPrefab.transform.rotation,0,null);
				//TODO Save player camp to the database.
				BuildingControl bc = newBuilding.GetComponent<BuildingControl> ();
				//PhotonView pw = newBuilding.GetComponent<PhotonView> ();
				bc.SetToFarm (FindObjectOfType<GameManager> ().userId,0);
				isBuilding = true;
			}
		}
		public void BuildLumberMill()
		{
			FindObjectOfType<CursorControl> ().CursorToCenter ();
			int money = 100;
			if(money>=20)
			{
				newBuilding = (GameObject)PhotonNetwork.Instantiate("pref_lumbermill",position,lumbermillPrefab.transform.rotation,0,null);
				//TODO Save player camp to the database.
				BuildingControl bc = newBuilding.GetComponent<BuildingControl> ();
				//PhotonView pw = newBuilding.GetComponent<PhotonView> ();
				bc.SetToLumbermill (FindObjectOfType<GameManager> ().userId,0);
				isBuilding = true;
			}
		}
		public void BuildBaracks()
		{
			FindObjectOfType<CursorControl> ().CursorToCenter ();
			int money = 100;
			if(money>=20)
			{
				newBuilding = (GameObject)PhotonNetwork.Instantiate("pref_baracks",position,barracksPrefab.transform.rotation,0,null);
				//TODO Save player camp to the database.
				BuildingControl bc = newBuilding.GetComponent<BuildingControl> ();
				//PhotonView pw = newBuilding.GetComponent<PhotonView> ();
				bc.SetToBaracks (FindObjectOfType<GameManager> ().userId,0);
				isBuilding = true;
			}
		}
		public void UpgradeSelected()
		{
			
		}
		#endregion
	}
}