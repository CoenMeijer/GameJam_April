using UnityEngine;
using System.Collections;

public class ScreenBtns : MonoBehaviour {
	public Texture2D background,startBtn;

	public GUIStyle guiSkin;
	public string LvlName = "";
	
	
	private string clicked = "";
	private Rect WindowRect = new Rect(Screen.width / 2 - 100, Screen.height / 10 * 7, 200,200);
	private Rect HugeRect = new Rect(0, 0, Screen.width, Screen.height);


	private void OnGUI(){
		if (background != null)
			GUI.DrawTexture(new Rect(0,0,Screen.width , Screen.height),background);

		if (clicked == ""){
			WindowRect = GUI.Window(0, WindowRect, menuFunc, "Main Menu" ,GUIStyle.none);
		}
	}
	


	private void menuFunc(int id){
		if(GUI.Button(new Rect(0,0,200,200),startBtn,guiSkin)){
			Application.LoadLevel(1);
		}
			
	}

	void Update(){
	
	}
}
