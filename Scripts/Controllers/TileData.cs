using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Linq;
using Com.RegalStick.Oakenland;

public class TileData : MonoBehaviour {
	#region private variables
	private List<GameObject> tileList;
	private string saveTileDataURL = "http://oakenland.com/oakenland/SaveTileData.php";
	private string loadTileDataURL = "http://oakenland.com/oakenland/LoadTileData.php";
	private string deleteTileDataURL = "http://oakenland.com/oakenland/DeleteTileData.php";
	public string dataString;
	public string[] data;
	private GameObject tilePref;

	#endregion
	#region public variables
	public GameObject[] Tiles;
	#endregion
	void Start()
	{
		tilePref = (GameObject)Resources.Load ("pref_worldTile");
	}

	public void FindAllTiles()
	{
		Tiles = GameObject.FindGameObjectsWithTag ("WorldTile");
		tileList = Tiles.ToList ();
	}

	#region private methods
	IEnumerator Save(String tileId, String userId, Vector3 position)
	{
		WWWForm form = new WWWForm();
		form.AddField ("tileIdPost", tileId);
		form.AddField ("userIdPost", userId);
		form.AddField ("tileXPositionPost", position.x.ToString ());
		form.AddField ("tileZPositionPost", position.z.ToString ());

		UnityWebRequest www = UnityWebRequest.Post (saveTileDataURL,form);
		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) 
		{
			Debug.Log (www.error);
		}
	}
	IEnumerator Load()
	{
		WWW tileData = new WWW (loadTileDataURL);
		yield return tileData;
		dataString = tileData.text;
		//print (userdataString);
		data = dataString.Split (';');
		string tileId, userId;
		float positionx, positionz;
		for (int i = 0; i < data.Length-1; i++) {
			tileId = GetDataValue (data [i], "tileId");
			userId = GetDataValue (data [i], "userId");
			positionx = float.Parse (GetDataValue (data [i], "positionX"));
			positionz = float.Parse (GetDataValue (data [i], "positionZ"));
			GameObject t = PhotonNetwork.Instantiate ("pref_worldTile",new Vector3(positionx,0.0f,positionz),tilePref.transform.rotation,0);
			t.GetComponent<TileController>().id = tileId;
			t.GetComponent<TileController> ().tilePlayerid = userId;
		}


	}

	string GetDataValue(string data, string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if(value.Contains ("|")) value = value.Remove (value.IndexOf ("|"));
		return value;
	}

	IEnumerator Delete()
	{
		WWWForm form = new WWWForm();

		UnityWebRequest www = UnityWebRequest.Post (deleteTileDataURL,form);
		yield return www.SendWebRequest ();

		if (www.isNetworkError || www.isHttpError) 
		{
			Debug.Log (www.error);
		}
	}
	#endregion
	#region public methods
	/// <summary>
	/// Saves tile data.
	/// </summary>
	public void SaveData()
	{
		Debug.Log ("save");
		FindAllTiles ();
		foreach (GameObject t in Tiles)
		{
			TileController tile = t.GetComponent<TileController> ();
			StartCoroutine (Save (tile.id ,tile.tilePlayerid, tile.transform.position));
		}
	}
	/// <summary>
	/// Loads tile data.
	/// </summary>
	public void LoadData()
	{
		Debug.Log ("load");
		StartCoroutine (Load ());
	}
	/// <summary>
	/// Deletes tile data.
	/// </summary>
	public void DeleteData()
	{
		Debug.Log ("delete");
		StartCoroutine (Delete ());
	}
	#endregion

}
