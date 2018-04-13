using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

namespace Com.RegalStick.Oakenland{
	public class BuildingManager : Photon.PunBehaviour {
		#region private variables
		private Plane buildingPlane;
		private Ray ray;
		private float distance;
		private Vector3 position;
		private string SaveBuildingDataURL = "http://localhost/oakenland/SaveBuildingData.php";
		#endregion
		#region public variables
		public bool isBuilding = false;
		public GameObject campPrefab;
		public GameObject storagePrefab;
		public GameObject farmPrefab;
		public GameObject lumbermillPrefab;
		public GameObject barracksPrefab;
		public GameObject newBuilding;
		private Vector3 buildingPlanePosition{get{return Vector3.zero; }}
		public List<BuildingControl> camps;
		public List<BuildingControl> storages;
		public List<BuildingControl> farms;
		public List<BuildingControl> lumbermills;
		public List<BuildingControl> baracks;
		#endregion

		void Start()
		{
			campPrefab = (GameObject)Resources.Load ("pref_camp");
			buildingPlane = new Plane (Vector3.up, buildingPlanePosition + new Vector3 (0.0f, 0.2f, 0.0f));
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
						bc.SetSpawnPoints (bc.maxSpace/2,3,bc.gameObject.transform.position);
						bc.SetSpawnPoints (bc.maxSpace/2,4,bc.gameObject.transform.position);
						bc.SpawnCampUnits ();
					}
					if (bc.type.ToString () == "lumbermill") 
					{
						
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
			Debug.Log ("Upgrade");
			GameObject sm = FindObjectOfType<SelectionManager> ().currentSelectedObject;
			if (sm.tag == "Building")
				sm.GetComponent<BuildingControl> ().UpgradeCamp ();
		}
		#endregion
	}
}