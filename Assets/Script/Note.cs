using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
	public TextMesh text;
	// Use this for initialization
	public void SetMessage(string _message){
		text.text=_message;
	}
}
