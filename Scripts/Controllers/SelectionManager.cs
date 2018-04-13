using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Com.RegalStick.Oakenland{
	public class SelectionManager : MonoBehaviour {
		#region private variables
		private Camera cam;
		private RaycastHit hit;
		public GameManager gameManager{get {return FindObjectOfType<GameManager> ();}}
		public BuildingControl buildingControl;
		public UserInferfaceController userInterface{get{return FindObjectOfType<UserInferfaceController> (); }}
		#endregion
		#region public variables
		public GameObject currentPointing;
		public GameObject currentSelectedObject;
		#endregion
		void Start () {
			cam = Camera.main;
		}
		void Update () {
			CheckRaycast();
		}
		#region private methods
		void CheckRaycast()
		{
			if (Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out hit)) 
			{
				buildingControl = hit.transform.GetComponent<BuildingControl> ();
				if (hit.transform.name.Contains ("BackgroundPlane")) 
				{
					return;
					//currentPointing = null;
				}
				else 
				{
					currentPointing = hit.transform.gameObject;
				}
				if (Input.GetMouseButtonDown (0)) 
				{
					currentSelectedObject = currentPointing;
					CheckOwner ();
				}
			}
		}
		void CheckOwner()
		{
			if (buildingControl && buildingControl.userId == gameManager.userId) {
				userInterface.SetActiveBuildingPanel ();
			} 
			else if (hit.transform.tag == "Unit") {
				userInterface.SetActiveUnitPanel ();
				//TODO indicate unit selection
			} 
			else 
			{
				if (hit.transform.tag == "Tile") 
				{
					userInterface.DeactivateBuildingPanel ();
					userInterface.DeactivateUnitPanel ();
					/*
					GameObject p = hit.transform.parent.GetComponent<TileController> ().particles;
					p.SetActive (!p.activeInHierarchy);
					*/
				}
				else if (hit.transform.tag == "") 
				{
					userInterface.DeactivateBuildingPanel ();
					userInterface.DeactivateUnitPanel ();
					//TODO SELECTION INDICATOR
					/*
					GameObject p = hit.transform.parent.GetComponent<TileController> ().particles;
					p.SetActive (!p.activeInHierarchy);
					*/
				}
			}
		}
		#endregion
		#region public methods
		public void SetCurrentSelected(GameObject obj)
		{
			currentSelectedObject = obj;
			buildingControl = null;
		}
		public void ClearCurrentSelected()
		{
			currentSelectedObject = null;
		}
		#endregion
	}
}