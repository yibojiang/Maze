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



	public void GenerateTip(string _id,string _qStr,string[] _aStr){
		if (curTip!=null){
			Destroy(curTip);
		}
		Note note=(Note)Instantiate(notePrefab,tipTransform.transform.position,tipTransform.transform.rotation);
		note.SetData(_id,_qStr,_aStr);
		note.SetMessage(_qStr);

		curTip=note.gameObject;
		GetComponent<AudioSource>().PlayOneShot(tipSpawnClip);
	}

	public override void Interact(){
		this.GetComponent<AudioSource>().Play ();
		Destroy(curTip);
	}

	void Update(){
//		if (Input.GetKeyDown(KeyCode.G) ){
//			GenerateTip("我告诉你\n其实sdafsad\n1.阿萨德发撒\n2.对萨发生地方",null);
//		}
	}
}
