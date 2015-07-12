using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[System.Serializable]
public class CameraGroup{
	public GameObject uiCam;
	public GameObject gameCam;
	public Transform handTransform;
}

public class GameManager : SingletonMonoBehaviourClass<GameManager> {
	public TypeWriter txtStroy;
	public TypeWriter txtSubtitle;

	public GameObject bgSubtitle;
	public GameObject bgStory;

	public bool vrMode=false;
	public GameObject handController;

	public CameraGroup camGroup;
	public CameraGroup vrCamGroup;

	private CameraGroup curCamGroup;

	public Telephone phone;

	// Use this for initialization
	void Start () {
		curCamGroup=camGroup;
		GameStart();


	}



	public void SetSubtitle(string _text){
		bgSubtitle.SetActive(true);
		txtSubtitle.SetText(_text);
	}

	public void SetStoryText(string _text){
		bgStory.SetActive(true);
		txtStroy.SetText(_text);
	}

	public void HideSubtitle(){
		bgSubtitle.SetActive(false);
	}

	public void HideStory(){
		bgStory.SetActive(false);
	}

	void SwitchCameraMode(){
		if (curCamGroup!=null){
			curCamGroup.uiCam.SetActive(false);
			curCamGroup.gameCam.SetActive(false);
		}

		if (curCamGroup==camGroup){
			curCamGroup=vrCamGroup;
		}
		else{
			curCamGroup=camGroup;
		}

		if (curCamGroup!=null){
			handController.transform.SetParent(curCamGroup.handTransform);
			curCamGroup.uiCam.SetActive(true);
			curCamGroup.gameCam.SetActive(true);
		}

	}

	public void GameStart(){
		StartCoroutine(DoGameStart() );
	}

	public bool firstTalkPicked=false;

	public AudioClip gunshotClip;
	public AudioClip dieClip;
	public AudioSource audioSource;
	IEnumerator DoGameStart(){


		while (!firstTalkPicked){
			yield return new WaitForEndOfFrame();
		}

		Debug.Log("picked");
		yield return new WaitForSeconds(5);
		audioSource.PlayOneShot(dieClip);

		yield return new WaitForSeconds(3);

		audioSource.PlayOneShot(gunshotClip);


//		txtStroy.SetText("No. 0225");
//		txtStroy.PlayText();
//		yield return new WaitForSeconds(3);
//		txtStroy.SetText("Wake up.");
//
//		yield return new WaitForSeconds(3);
//		txtStroy.SetText("Please stand in front of the screen.");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R) ){
			SwitchCameraMode();
		}
	}


}
