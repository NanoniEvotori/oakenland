using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {
	public int index;

	public Canvas menuCanvas;
	public Image loginImage;
	public Image fadeImage;
	public Animator anim;
	public bool isTrasitionActive=false;
	public bool isFadeInOutActive;

	void Update()
	{
		if (isTrasitionActive == true)
		{
			StartCoroutine (Fading ());
		} 

		//TODO FIX FADE IN/OUT TRANSITION
		else 
		{
			if (isFadeInOutActive == true) {
				GameObject.Find ("imgFade");
				anim.SetBool ("Fade", false);
				Debug.Log ("TransitionFadeIn");
			}
		}
	}

	IEnumerator Fading()
	{
		if (isFadeInOutActive == true) {
			anim.SetBool ("Fade", true);
			yield return new WaitUntil (() => fadeImage.color.a == 1);
		}
		isTrasitionActive = false;
		SceneManager.LoadScene (index);
	}
}
