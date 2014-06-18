using UnityEngine;
using System.Collections;

public class StartTimer : MonoBehaviour {
	//--Properties--//
	public GUIText startText;
	
	void Start () 
	{
		StartCoroutine(Countdown());
	}
	
	private IEnumerator Countdown()
	{
		// Countdown
		yield return new WaitForSeconds(1);
		Debug.Log("3");
		startText.text = "3";
		yield return new WaitForSeconds(1);
		Debug.Log("2");
		startText.text = "2";
		yield return new WaitForSeconds(1);
		Debug.Log("1");
		startText.text = "1";
		yield return new WaitForSeconds(1);
		Debug.Log("GO!");
		startText.text = "GO!";
		yield return new WaitForSeconds(1);
		Destroy(startText);
		yield return null;
	}
}
