using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Com.RegalStick.Oakenland{
	public class TileGridController : MonoBehaviour {

		#region public variables
		public List<GameObject> tiles;
		public GameObject tilePrefab;
		public int lengthX=0;
		public int lengthY=0;
		public int camps=0;
		public Text textCampCount;
		#endregion

		void Start()
		{
			UpdateTileList ();
		}

		void Update()
		{
			if (Input.GetKeyDown (KeyCode.Insert)) 
			{
				camps++;	
			}
			ExpandTileList();
		}

		#region private methods
		public void UpdateTileList()
		{
			tiles = GameObject.FindGameObjectsWithTag ("WorldTile").ToList<GameObject>();
		}

		void ExpandTileList()
		{
			if (camps * 3 > tiles.Count) //TODO CAMP COUNT
			{
				if (lengthX <= lengthY) 
				{
					foreach (GameObject obj in tiles) 
					{
						if (obj && obj.GetComponent<TileController> ().isTriggeredR == false) 
						{
							GameObject tile = PhotonNetwork.InstantiateSceneObject("pref_worldTile",obj.GetComponent<TileController>().rightPosition,tilePrefab.transform.rotation,0,null);
							tile.GetComponent<TileController> ().id = FindObjectOfType<Generator> ().GenerateId ();
						}
					}	
					UpdateTileList ();
					FindObjectOfType<NavigationBaker> ().BakeNavMesh ();
					lengthX++;
				}
				else
				{
					foreach (GameObject obj in tiles) 
					{
						if (obj && obj.GetComponent<TileController> ().isTriggeredB == false) 
						{
							GameObject tile = PhotonNetwork.InstantiateSceneObject ("pref_worldTile",obj.GetComponent<TileController>().botPosition,tilePrefab.transform.rotation,0,null);
							tile.GetComponent<TileController> ().id = FindObjectOfType<Generator> ().GenerateId ();
						}
					}	
					UpdateTileList ();
					FindObjectOfType<NavigationBaker> ().BakeNavMesh ();
					lengthY++;
				}
			}
		}
		#endregion
		#region public methods
		public void IncreaseCampCount()
		{
			camps++;
			textCampCount.text = camps.ToString ();
		}
		public void StartNew()
		{
			//ResetScene ();
			PhotonNetwork.InstantiateSceneObject ("pref_worldTile",Vector3.zero,tilePrefab.transform.rotation,0,null);
			FindObjectOfType<NavigationBaker> ().BakeNavMesh ();
		}
		public void ResetScene()
		{
			PhotonNetwork.DestroyAll ();
			camps = 0;
			tiles.RemoveRange (1,tiles.Count-1);
		}
		#endregion
	}
}