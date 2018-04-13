using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileController : MonoBehaviour {
	public string tilePlayerid="null";
	public string id;
	public bool isTriggeredL;
	public bool isTriggeredR;
	public bool isTriggeredTL;
	public bool isTriggeredT;
	public bool isTriggeredTR;
	public bool isTriggeredBL;
	public bool isTriggeredB;
	public bool isTriggeredBR;

	public Vector3 topPosition;
	public Vector3 botPosition;
	public Vector3 leftPosition;
	public Vector3 rightPosition;

	public GameObject environmentObj;
	public List <GameObject> environmentPos;
	public GameObject particles;

	void Start()
	{
		particles.SetActive (false);
		if (id == "")
			id = GenerateTileId ();
	}
	#region public methods
	public string GenerateTileId()
	{
		Guid g = Guid.NewGuid();
		string GuidString = Convert.ToBase64String(g.ToByteArray());
		GuidString = GuidString.Replace("=","");
		GuidString = GuidString.Replace("+","");
		GuidString = GuidString.Replace("/","");
		return GuidString;
	}
	#endregion
}
