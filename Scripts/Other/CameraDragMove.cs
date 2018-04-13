using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDragMove : MonoBehaviour {
	private GameObject camObj;
	private Camera cam;
	private Vector3 ResetCamera;
	private Vector3 Origin;
	private Vector3 Diference;
	private bool Drag=false;
	public float currentCameraRange;

	public float minZoom = 6;
	public float maxZoom = 20;

	void Start () {
		cam = GameObject.Find("Main Camera").GetComponent<Camera> ();
		camObj = GameObject.Find ("Camera");
		ResetCamera = camObj.transform.position;
		currentCameraRange = cam.fieldOfView;
	}
	void LateUpdate () {
		if (Input.GetMouseButton (0)) {
			Diference=(cam.ScreenToWorldPoint (Input.mousePosition))- camObj.transform.position;
			if (Drag==false){
				Drag=true;
				Origin=cam.ScreenToWorldPoint (Input.mousePosition);
			}
		} else {
			Drag=false;
		}
		if (Drag==true){
			camObj.transform.position = Origin-Diference;
		}
		//RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
		if (Input.GetMouseButton (1)) {
			camObj.transform.position=ResetCamera;
		}
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) 
		{
			currentCameraRange += 1;
		} 
		else if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
		{
			currentCameraRange -= 1;
		}
		Camera.main.fieldOfView = Mathf.Lerp (cam.fieldOfView,currentCameraRange,Time.deltaTime*2);
	}
} 
