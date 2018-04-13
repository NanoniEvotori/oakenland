using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Com.RegalStick.Oakenland{
	[RequireComponent(typeof(NavMeshAgent))]
	public class UnitControl : MonoBehaviour, IPunObservable {
		#region private variables
		private NavMeshAgent agent{get{return GetComponent<NavMeshAgent>(); }}
		#endregion
		#region public variables
		public string unitId;
		public string buildingId;
		public string userId;
		public GameObject defaultCampfire;

		public string name="John";
		public int health=100;
		public int attack=15;
		public int level=1;
		public int lifetime=500;
		public int carryCapacity=20;
		public int currentCapacity=0;
		public string work;
		#endregion

		void Update()
		{
			if (defaultCampfire != null && work == "") {
				Debug.Log ("move");
				MoveToCampfire ();
			} 
			else
				return;
		}

		#region private methods
		#endregion
		#region public methods

		public void FindEmptyLumbermill()
		{
			BuildingControl[] a = FindObjectsOfType<BuildingControl> ();
			foreach (BuildingControl obj in a) 
			{
				if(obj.currentSpace<obj.maxSpace)
					agent.SetDestination (obj.transform.position);
			}
		}
		public void SetDestination(GameObject obj)
		{
			agent.SetDestination (obj.transform.position);	
		}
		public void MoveToCampfire()
		{
			if(defaultCampfire)
			agent.SetDestination (defaultCampfire.transform.position);
		}

		#endregion
		#region IPunObservable implementation
		public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
		{	//TODO set gameUnits photonview stream
			if (stream.isWriting) 
			{
				stream.SendNext (unitId);
				stream.SendNext (userId);
			}
			else if (stream.isReading)
			{
				unitId = (string)stream.ReceiveNext ();
				userId = (string)stream.ReceiveNext ();
			}
			//throw new System.NotImplementedException ();
		}
		#endregion
	}
}