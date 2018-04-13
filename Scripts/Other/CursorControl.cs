using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour {
	public void CursorToCenter(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.lockState = CursorLockMode.None;  
	}
}
