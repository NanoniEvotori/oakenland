using System.Collections;
using UnityEngine;

class DragTransform : MonoBehaviour
{

	private bool dragging = false;
	private float distance;



	void OnMouseDown()
	{
		distance = Vector3.Distance(transform.position,    Camera.main.transform.position);
		dragging = true;
	}

	void OnMouseUp()
	{
		dragging = false;
	}

	void Update()
	{
		if (dragging)
		{
			distance = Vector3.Distance(new Vector3(transform.position.x, transform.position.y +1f,transform.position.z),Camera.main.transform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
			transform.position = rayPoint;
		}
	}
}