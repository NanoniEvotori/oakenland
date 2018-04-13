using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.RegalStick.Oakenland{
	public class EventTest : MonoBehaviour {
		#region private variables
		private UnityAction someListener;
		#endregion

		void Awake()
		{
			someListener = new UnityAction (SomeFunction);
		}

		#region private methods
		void OnEnable()
		{
			EventManager.StartListening ("test", someListener);
			EventManager.StartListening ("Spawn", SomeOtherFunction);
			EventManager.StartListening ("Destroy", SomeThirdFunction);
		}
		void OnDisable()
		{
			EventManager.StopListening ("test", someListener);
			EventManager.StopListening ("Spawn", SomeOtherFunction);
			EventManager.StopListening ("Destroy", SomeThirdFunction);
		}
		void SomeFunction()
		{
			Debug.Log ("Some function was called!");
		}
		void SomeOtherFunction ()
		{
			Debug.Log ("Some Other Function was called!");
		}

		void SomeThirdFunction ()
		{
			Debug.Log ("Some Third Function was called!");
		}
		#endregion
	}
}