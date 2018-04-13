using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour {
	public bool TriggerR;
	public bool TriggerL;
	public bool TriggerTL;
	public bool TriggerT;
	public bool TriggerTR;
	public bool TriggerBL;
	public bool TriggerB;
	public bool TriggerBR;

	public TileController tileControl;
	public Vector3 position;
	private GameObject parentObj;

	void Start()
	{
		parentObj = gameObject.transform.parent.gameObject;
		tileControl = parentObj.GetComponent<TileController> ();
		position = gameObject.transform.position;
		SetPositions ();
	}

	void SetPositions()
	{
		if (gameObject.name == "triggerT") 
		{
			tileControl.topPosition = position;
		}
		if (gameObject.name == "triggerB") 
		{
			tileControl.botPosition = position;
		}
		if (gameObject.name == "triggerL") 
		{
			tileControl.leftPosition = position;
		}
		if (gameObject.name == "triggerR") 
		{
			tileControl.rightPosition = position;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.tag == "Tile") 
		{
			if(TriggerL)
			{
				tileControl.isTriggeredL = true;
			}
			if(TriggerR)
			{
				tileControl.isTriggeredR = true;
			}
			if(TriggerTL)
			{
				tileControl.isTriggeredTL = true;
			}
			if(TriggerT)
			{
				tileControl.isTriggeredT = true;
			}
			if(TriggerTR)
			{
				tileControl.isTriggeredTR = true;
			}
			if(TriggerBL)
			{
				tileControl.isTriggeredBL = true;
			}
			if(TriggerB)
			{
				tileControl.isTriggeredB = true;
			}
			if(TriggerBR)
			{
				tileControl.isTriggeredBR = true;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.transform.tag == "Tile") 
		{
			if(TriggerL)
			{
				tileControl.isTriggeredL = false;
			}
			if(TriggerR)
			{
				tileControl.isTriggeredR = false;
			}
			if(TriggerTL)
			{
				tileControl.isTriggeredTL = false;
			}
			if(TriggerT)
			{
				tileControl.isTriggeredT = false;
			}
			if(TriggerTR)
			{
				tileControl.isTriggeredTR = false;
			}
			if(TriggerBL)
			{
				tileControl.isTriggeredBL = false;
			}
			if(TriggerB)
			{
				tileControl.isTriggeredB = false;
			}
			if(TriggerBR)
			{
				tileControl.isTriggeredBR = false;
			}
		}
	}
}
