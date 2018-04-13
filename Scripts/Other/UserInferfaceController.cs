using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Com.RegalStick.Oakenland{
	public class UserInferfaceController : MonoBehaviour {
		#region private variables
		public GameObject adminPanel;
		public GameObject constructionPanel;
		public GameObject buildingControlPanel;
		public GameObject unitControlPanel;
		public GameObject resourcePanel;

		#endregion
		#region public variables
		//public GameObject active;
		#endregion

		void Awake()
		{
			adminPanel = GameObject.Find ("panelAdmin");
			constructionPanel = GameObject.Find ("panelConstruction");
			buildingControlPanel = GameObject.Find ("panelBuildingControl");
			unitControlPanel = GameObject.Find ("panelUnitControl");
			resourcePanel = GameObject.Find ("panelResource");
		}
		void Start()
		{
			DeactivateBuildingPanel ();
			DeactivateUnitPanel ();
			DeactivateResourcePanel ();
		}

		#region private methods

		#endregion

		#region public methods
		public void SetActiveAdminPanel()
		{
			adminPanel.SetActive (true);
		}
		public void SetActiveConstructionPanel()
		{
			constructionPanel.SetActive (true);
		}
		public void SetActiveBuildingPanel()
		{
			unitControlPanel.SetActive (false);
			buildingControlPanel.SetActive (true);
		}
		public void SetActiveUnitPanel()
		{
			buildingControlPanel.SetActive (false);
			unitControlPanel.SetActive (true);
		}
		public void SetActiveResourcesPanel()
		{
			resourcePanel.SetActive (true);
		}
		public void DeactivateAdminPanel()
		{
			adminPanel.SetActive (false);
		}
		public void DeactivateConstructionPanel()
		{
			constructionPanel.SetActive (false);
		}
		public void DeactivateBuildingPanel()
		{
			buildingControlPanel.SetActive (false);
		}
		public void DeactivateUnitPanel()
		{
			unitControlPanel.SetActive (false);
		}
		public void DeactivateResourcePanel()
		{
			resourcePanel.SetActive (false);
		}
		public void DeactivateAll()
		{
			adminPanel.SetActive (false);
			constructionPanel.SetActive (false);
			buildingControlPanel.SetActive (false);
			resourcePanel.SetActive (false);
		}
		/// <summary>
		/// Return bool of the selected panel.
		/// </summary>
		/// <returns><c>true</c> if building panel is active, <c>false</c> if unit panel is active.</returns>
		/// <param name="type">1 - building panel, 2 - unit panel.</param>
		public bool isActive(int type)
		{
			if (type == 1) 
				return buildingControlPanel.activeInHierarchy;
			if (type == 2) 
				return unitControlPanel.activeInHierarchy;
			return false;
		}
		#endregion
	}
}