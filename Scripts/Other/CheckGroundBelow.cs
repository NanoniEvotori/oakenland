using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.RegalStick.Oakenland{
	[RequireComponent(typeof(BuildingManager))]
	public class CheckGroundBelow : MonoBehaviour {
		public GameObject activeObject;
		public GameObject buildingGround;
		public GameObject objectBelow;
		private RaycastHit hitInfo;
		private bool isBuilding;
		public bool canBuild;

		void Start()
		{
			isBuilding = FindObjectOfType<BuildingManager>().isBuilding;
			buildingGround = GameObject.Find ("Tiles");
			//buildingGround.SetActive (false);
		}
		void Update()
		{
			isBuilding = FindObjectOfType<BuildingManager>().isBuilding;
			activeObject = FindObjectOfType<BuildingManager> ().newBuilding;
			if(isBuilding == true && activeObject != null)
			{
				//buildingGround.SetActive (true);
				hitInfo = new RaycastHit ();
				bool hit = Physics.Raycast (activeObject.transform.position,Vector3.down,out hitInfo);
				if(hit)
				{
					objectBelow = hitInfo.transform.gameObject;
					if(hitInfo.transform.gameObject.tag == "Tile")
					{
						canBuild = true;	
					}
					else
					{
						canBuild = false;
					}
				}
			}
		}
	}
}