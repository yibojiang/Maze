using UnityEngine;
using System.Collections;

public class ButtonFlush : InteractiveObj {

	public GameObject curTip;
	public Transform tipTransform;
	public Note notePrefab;

	public AudioClip tipSpawnClip;
	public virtual bool Interactive(){
		if (curTip!=null){
			return true;
		}
		else{
			return false;
		}

	}

	public void GenerateTip(string _message){
		if (curTip!=null){
			Destroy(curTip);
		}
		Note note=(Note)Instantiate(notePrefab,tipTransform.transform.position,tipTransform.transform.rotation);
		note.SetMessage(_message);
		curTip=note.gameObject;
		GetComponent<AudioSource>().PlayOneShot(tipSpawnClip);


	}

	public override void Interact(){
		this.GetComponent<AudioSource>().Play ();
		Destroy(curTip);
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.G) ){
			GenerateTip("独裁者");
		}
	}
}
