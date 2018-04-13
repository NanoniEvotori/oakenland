using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Com.RegalStick.Oakenland{
	public class BuildingControl : MonoBehaviour, IPunObservable {
		#region private variables
		private RaycastHit hit;
		private Camera cam;
		private GameObject unitPref;
		private bool isSetBelow;
		#endregion
		#region public variables
		public bool isActive;
		public bool isSet;
		public string buildingId;
		public string userId;
		public int level;
		public int health;
		public int maxSpace;
		public int currentSpace=0;
		public float timeToBuild;
		public float timeTillFinish;
		public enum buildingType
		{
			camp, storage, farm, lumbermill, baracks
		}
		public buildingType type;
		public GameObject tileBelow;
		public List <Vector3> spawnPoints;
		public List <GameObject> units;
		public Text textUnits;
		public Text textLevel;
		#endregion
		void Awake()
		{
			unitPref = (GameObject)Resources.Load ("pref_unit");
			cam = Camera.main;
		}
		void Start()
		{
			//textUnits.text = "Units: " + units.Count;
		}
		void Update()
		{
			if(GetComponent<PhotonView>().isMine == false && PhotonNetwork.connected == true)
			{
				return;
			}		
			if (isSet == true) 
			{
				textUnits.text = "Units: " + units.Count.ToString ();
			}
		}
		#region private methods
		void CheckRaycast()
		{
			if (Physics.Raycast (gameObject.transform.position,Vector3.down, out hit,1.0f)) 
			{
				if (hit.transform.name.Contains ("Tile")) 
				{
					tileBelow = null;
				}
			}
		}
		#endregion
		#region public methods
		/// <summary>
		/// Set parameters for a camp.
		/// </summary>
		/// <param name="userId">User identification ID.</param>
		/// <param name="level">Building level.</param>
		public void SetToCamp(string userId, int level)
		{//TODO Camp building comfirmation notification
			this.userId = userId;
			buildingId = FindObjectOfType<Generator> ().GenerateId ();
			type = buildingType.camp;
			this.level = level;
			if (level == 0) 
			{
				health = 20;
				maxSpace = 5;
				timeToBuild = 0;
			}
			else if (level == 1) 
			{
				health = 120;
				maxSpace = 10;
				timeToBuild = 10;
			}
			else if (level == 2)
			{
				health = 200;
				maxSpace = 14;
				timeToBuild = 20;
			}
			else if(level == 3)
			{
				health = 250;
				maxSpace = 17;
				timeToBuild = 30;
			}
			isSet = true;
		}
		/// <summary>
		/// Sets parameters for a storage.
		/// </summary>
		/// <param name="userId">User identification ID.</param>
		/// <param name="level">Storage level.</param>
		public void SetToStorage(string userId, int level)
		{
			this.userId = userId;
			buildingId = FindObjectOfType<Generator> ().GenerateId ();
			type = buildingType.storage;
			this.level = level;
			if (level == 0) 
			{
				health = 20;
				maxSpace = 0;
				timeToBuild = 0;
			}
			else if (level == 1) 
			{
				health = 60;
				maxSpace = 200;
				timeToBuild = 10;
			}
			else if(level == 2)
			{
				health = 70;
				maxSpace = 300;
				timeToBuild = 20;
			}
			else if(level == 3)
			{
				health = 80;
				maxSpace = 400;
				timeToBuild = 30;
			}
			isSet = true;
		}
		/// <summary>
		/// Sets parameters for a farm.
		/// </summary>
		/// <param name="userId">User identification ID.</param>
		/// <param name="level">Farm level.</param>
		public void SetToFarm(string userId, int level)
		{
			this.userId = userId;
			buildingId = FindObjectOfType<Generator> ().GenerateId ();
			type = buildingType.farm;
			this.level = level;
			if (level == 0) 
			{
				health = 10;
				maxSpace = 0;
				timeToBuild = 0;
			}
			else if (level == 1) 
			{
				health = 40;
				maxSpace = 1;
				timeToBuild = 10;
			}
			else if(level == 2)
			{
				health = 40;
				maxSpace = 2;
				timeToBuild = 20;
			}
			else if(level == 3)
			{
				health = 40;
				maxSpace = 3;
				timeToBuild = 30;
			}
			isSet = true;
		}
		/// <summary>
		/// Sets parameters for a Lumbermill.
		/// </summary>
		/// <param name="userId">User identification ID.</param>
		/// <param name="level">Lumbermill level.</param>
		public void SetToLumbermill(string userId, int level)
		{
			this.userId = userId;
			buildingId = FindObjectOfType<Generator> ().GenerateId ();
			type = buildingType.lumbermill;
			this.level = level;
			if (level == 0) 
			{
				health = 10;
				maxSpace = 0;
				timeToBuild = 0;
			}
			else if (level == 1) 
			{
				health = 40;
				maxSpace = 1;
				timeToBuild = 10;
			}
			else if(level == 2)
			{
				health = 40;
				maxSpace = 2;
				timeToBuild = 20;
			}
			else if(level == 3)
			{
				health = 40;
				maxSpace = 3;
				timeToBuild = 30;
			}
			isSet = true;
		}
		/// <summary>
		/// Sets parameters for a baracks.
		/// </summary>
		/// <param name="userId">User identification ID.</param>
		/// <param name="level">Baracks level.</param>
		public void SetToBaracks(string userId, int level)
		{
			this.userId = userId;
			buildingId = FindObjectOfType<Generator> ().GenerateId ();
			type = buildingType.baracks;
			this.level = level;
			if (level == 0) 
			{
				health = 20;
				maxSpace = 0;
				timeToBuild = 0;
			}
			else if (level == 1) 
			{
				health = 60;
				maxSpace = 200;
				timeToBuild = 10;
			}
			else if(level == 2)
			{
				health = 70;
				maxSpace = 300;
				timeToBuild = 20;
			}
			else if(level == 3)
			{
				health = 80;
				maxSpace = 400;
				timeToBuild = 30;
			}
			isSet = true;
		}
		#endregion
		//TODO instantiate camp units
		public void SetSpawnPoints(int points, double radius, Vector3 center)
		{
			double slice = 2 * Math.PI / points;
			for (int i = 0; i < points; i++)
			{
				double angle = slice * i;
				int newX = (int)(center.x + radius * Math.Cos(angle));
				int newY = (int)(center.z + radius * Math.Sin(angle));
				Vector3 p = new Vector3(newX, 0, newY);
				spawnPoints.Add (p);
			}
		}
		public void SpawnCampUnits()
		{
			foreach (Vector3 a in spawnPoints) {
				GameObject unit = PhotonNetwork.Instantiate ("pref_unit", new Vector3 (a.x, 1.0f, a.z), unitPref.transform.rotation, 0);
				unit.GetComponent<UnitControl>().userId = userId;
				unit.GetComponent<UnitControl>().buildingId = buildingId;
				unit.GetComponent<UnitControl>().unitId = FindObjectOfType<Generator> ().GenerateId ();
				unit.GetComponent<UnitControl> ().defaultCampfire = gameObject;
				units.Add (unit);
			}
		}
		public void UpgradeCamp()
		{
			if (level < 3) {
				level++;
				if (level == 1) 
				{
					SetToCamp (userId, 1);
					textLevel.text = "Level: " + level.ToString ();
				}
				else if (level == 2) 
				{
					SetToCamp (userId, 2);
					textLevel.text = "Level: " + level.ToString ();
				}
				else if (level == 3) 
				{
					SetToCamp (userId, 3);
					textLevel.text = "Level: " + level.ToString ();
				}
			} 
			else 
			{
				Debug.Log ("Reached max level.");
			}
		}
		#region IPunObservable implementation

		public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
		{
			if (stream.isWriting) {
				stream.SendNext (isSet);
				stream.SendNext (isActive);
				stream.SendNext (buildingId);
				stream.SendNext (userId);
				stream.SendNext (level);
				stream.SendNext (health);
			} 
			else
			{
				isSet = (bool)stream.ReceiveNext ();
				isActive = (bool)stream.ReceiveNext ();
				buildingId = (string)stream.ReceiveNext ();
				userId = (string)stream.ReceiveNext ();
				level = (int)stream.ReceiveNext ();
				health = (int)stream.ReceiveNext ();
			}
		}
		#endregion
	}
}