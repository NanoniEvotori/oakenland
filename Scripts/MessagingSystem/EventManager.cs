using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.RegalStick.Oakenland{
	public class EventManager : MonoBehaviour {
		#region private variables
		private Dictionary <string, UnityEvent> eventDictionary;
		private static EventManager eventManager;
		#endregion
		#region eventmanager instance
		public static EventManager instance
		{
			get
			{
				if (!eventManager) 
				{
					eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

					if (!eventManager) 
					{
						Debug.Log ("There needs to be one active EventManager script on a GameObject.");
					}
					else 
					{
						eventManager.Init ();
					}
				}
				return eventManager;
			}
		}
		#endregion
		#region private methods
		void Init()
		{
			if (eventDictionary == null) 
			{
				eventDictionary = new Dictionary<string, UnityEvent> ();
			}
		}
		#endregion
		#region public methods
		public static void StartListening (string eventName, UnityAction listener)
		{
			UnityEvent thisEvent = null;
			if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
			{
				thisEvent.AddListener (listener);
			} 
			else
			{
				thisEvent = new UnityEvent ();
				thisEvent.AddListener (listener);
				instance.eventDictionary.Add (eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, UnityAction listener)
		{
			if (eventManager == null) return;
			UnityEvent thisEvent = null;
			if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
			{
				thisEvent.RemoveListener (listener);
			}
		}

		public static void TriggerEvent (string eventName)
		{
			UnityEvent thisEvent = null;
			if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
			{
				thisEvent.Invoke ();
			}
		}
		#endregion
	}
}
