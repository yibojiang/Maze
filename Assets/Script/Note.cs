using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour {
	public TextMesh text;

	public string questionString;
	public string[] answerString;
	public int choiceIndex;
	public string id;
	
//	
	// Use this for initialization
	public void SetMessage(string _message){
		text.text=_message;
	}

	public void SetData(string _id,string _qStr,string[] _aStr){
		id=_id;
		questionString=_qStr;
		answerString=_aStr;
	}

//	void OnDestroy(){
//		Debug.Log("set: "+"id = "+choiceIndex.ToString());
//
//	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Alpha0)){
			SetMessage(questionString);
		}

		if (answerString!=null && answerString.Length>=2){
			if (Input.GetKeyDown(KeyCode.Alpha1)){
				SetMessage(answerString[0]);
				choiceIndex=0;
				GameManager.instance.SetNoteChoice(id,choiceIndex);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2)){
				SetMessage(answerString[1]);
				choiceIndex=1;
				GameManager.instance.SetNoteChoice(id,choiceIndex);
			}
		}
	}
}
