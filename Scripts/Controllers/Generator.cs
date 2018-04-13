using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Com.RegalStick.Oakenland{
public class Generator : MonoBehaviour {
	#region public methods
	public string GenerateId()
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
}
