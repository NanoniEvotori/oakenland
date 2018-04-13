using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

namespace Com.RegalStick.Oakenland{
	[RequireComponent(typeof (GameManager))]
	public class UserLoginManager : MonoBehaviour {
		#region private variables
		private string userdataString;
		private string CreateUserURL = "http://oakenland.com/oakenland/CreateUser.php";
		private string LoginURL = "http://oakenland.com/oakenland/Login.php";
		private string GetUserIdURL = "http://oakenland.com/oakenland/GetUserId.php";
		private ClickSound clickSound{get {return GameObject.FindObjectOfType<ClickSound> ();}}
		#endregion
		#region public variables
		public GameManager gameControl {get{return GameObject.FindObjectOfType<GameManager> ();}}
		public SceneControl sceneControl {get{return GameObject.FindObjectOfType<SceneControl> ();}}
		public Image mMenu;
		public Image mMenuOnline;
		public Image mLogin;
		public Image mCreateAccount;
		public Image mJournal;
		public Image mNotification;
		public Image mOptions;
		public Image mCredits;
		public InputField inputUsername;
		public InputField inputPassword;
		public InputField createUsername;
		public InputField createPassword;
		public Text notificationMessage;
		public Text errorMessage;
		public Text onlineMessage;
		public Button btnYes;
		public Button btnNo;
		public AudioSource menuMusic;
		public Text textMuteMusic;
		public string loginResponse;
		public string userId;
		public string [] users;
		#endregion

		void Update()
		{
			if (errorMessage.text != "")
				StartCoroutine (ClearInfoMessage (3.0f));
		}

		#region private methods

		IEnumerator CreateUser(string username, string password, int isActive, int type, int level)
		{
			WWWForm form1 = new WWWForm ();
			form1.AddField ("usernamePost", username);
			UnityWebRequest www1 = UnityWebRequest.Post (GetUserIdURL,form1);
			yield return www1.SendWebRequest ();
			if (www1.isNetworkError || www1.isHttpError)
			{
				Debug.Log (www1.error);
			}
			else 
			{
				userId = www1.downloadHandler.text;
				if(userId=="null"){
					WWWForm form2 = new WWWForm ();
					GenerateRandomUserId ();
					string deviceId = SystemInfo.deviceUniqueIdentifier;
					form2.AddField ("userIdPost", userId);
					form2.AddField ("deviceIdPost", deviceId);
					form2.AddField ("usernamePost", username);
					form2.AddField ("passwordPost", password);
					form2.AddField ("isActivePost", isActive);
					form2.AddField ("typePost", type);
					form2.AddField ("levelPost", level);

					UnityWebRequest www2 = UnityWebRequest.Post (CreateUserURL,form2);
					Debug.Log ("sendData");
					yield return www2.SendWebRequest ();

					if (www2.isNetworkError || www2.isHttpError) {
						Debug.Log (www2.error);
					}
					else 
					{
						OpenMenu ();		
						errorMessage.text = "User has been created successfully";
					}
				}
				else
				{
					errorMessage.text = "User already exist.";
					Debug.Log ("user exist: " + userId);
				}
			}
		}
		IEnumerator LoginToServer()
		{
			WWWForm form = new WWWForm ();
			form.AddField ("usernamePost", inputUsername.text);
			form.AddField ("passwordPost", inputPassword.text);
			UnityWebRequest www = UnityWebRequest.Post (LoginURL,form);
			yield return www.SendWebRequest ();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log (www.error);
			}
			else 
			{
				loginResponse = www.downloadHandler.text;
				errorMessage.text = "User not found";
			}
			if (loginResponse == "login success") {
				OnConnectionSuccess ();

				WWWForm form2 = new WWWForm ();
				form2.AddField ("usernamePost", inputUsername.text);
				UnityWebRequest www2 = UnityWebRequest.Post (GetUserIdURL,form);
				yield return www2.SendWebRequest ();
				if (www2.isNetworkError || www2.isHttpError)
				{
					Debug.Log (www2.error);
				}
				else 
				{
					userId = www2.downloadHandler.text;
				}

				gameControl.username = inputUsername.text;
				gameControl.userId = userId;
			} 
			else 
			{
				errorMessage.text = "Bad username or password";
			}
		}

		IEnumerator ClearInfoMessage(float seconds)
		{
			yield return new WaitForSeconds (seconds);
			errorMessage.text = "";
		}

		#endregion
		#region public methods
		public void Create()
		{
			StartCoroutine (CreateUser (inputUsername.text, inputPassword.text, 1, 1, 0));
		}
		public void NotCreate()
		{
			mLogin.gameObject.SetActive (true);
			mNotification.gameObject.SetActive (false);
			inputUsername.text = "";
			inputPassword.text = "";
		}
		public void Login()
		{
			StartCoroutine (LoginToServer());
		}
		public void ConnectToGame()
		{
			sceneControl.isTrasitionActive = true;
			//TODO CONNECT;
		}
		void GenerateRandomUserId()
		{
			Guid g = Guid.NewGuid();
			string GuidString = Convert.ToBase64String(g.ToByteArray());
			GuidString = GuidString.Replace("=","");
			GuidString = GuidString.Replace("+","");
			GuidString = GuidString.Replace("/","");
			userId = GuidString;
		}
			
		public void OpenMenu()
		{
			clickSound.PlaySound ();
			CloseMenus ();
			mMenu.gameObject.SetActive (true);
		}
		public void OpenOnlineMenu()
		{
			CloseMenus ();
			mMenuOnline.gameObject.SetActive (true);		
		}
		public void OpenNotificationMenu ()
		{
			clickSound.PlaySound ();
			CloseMenus ();
			mNotification.gameObject.SetActive (true);
		}
		public void OnButtonLogin()
		{
			clickSound.PlaySound ();
			CloseMenus ();
			mLogin.gameObject.SetActive (true);
			inputUsername = GameObject.Find ("inputLoginName").GetComponent<InputField>();
			inputPassword = GameObject.Find ("inputLoginPassword").GetComponent<InputField>();
		}
		public void OnButtonCreateAccount()
		{
			clickSound.PlaySound ();
			CloseMenus ();
			mCreateAccount.gameObject.SetActive (true);
			inputUsername = GameObject.Find ("inputCreateName").GetComponent<InputField>();
			inputPassword = GameObject.Find ("inputCreatePassword").GetComponent<InputField>();
		}
		public void OnButtonOptions()
		{
			clickSound.PlaySound ();
			CloseMenus ();
			mOptions.gameObject.SetActive (true);
		}
		public void OnConnectionSuccess()
		{
			OpenOnlineMenu ();
			onlineMessage.text = "Welcome, " + inputUsername.text;
		}
		void CloseMenus()
		{
			mMenu.gameObject.SetActive (false);
			mLogin.gameObject.SetActive (false);
			mCreateAccount.gameObject.SetActive (false);
			mJournal.gameObject.SetActive (false);
			mNotification.gameObject.SetActive (false);
			mOptions.gameObject.SetActive (false);
			mCredits.gameObject.SetActive (false);
		}
		public void musicEnableDisable()
		{
			if (menuMusic.isPlaying) {
				menuMusic.Pause ();
				textMuteMusic.text = "Enable music";
			} 
			else 
			{
				menuMusic.Play ();
				textMuteMusic.text = "Disable music";
			}
			Debug.Log ("Mute/Unmute");
		}
		#endregion
	}
}

/*
	IEnumerator GetUserdata()
	{
		WWW userData = new WWW ("http://localhost/oakenland/UserData.php");
		yield return userData;
		userdataString = userData.text;
		//print (userdataString);
		users = userdataString.Split (';');
		//print (GetDataValue (users[0], "username"));
	}
	string GetDataValue(string data, string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if(value.Contains ("|")) value = value.Remove (value.IndexOf ("|"));
		return value;
	}
*/
