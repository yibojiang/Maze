using UnityEngine;
using System.Collections;

public class Telephone : InteractiveObj {
	public AudioSource audioSource;

	public AudioClip ringClip;
	public AudioClip pickupClip;
	public AudioClip putdownClip;

	public AudioClip playerSpeechClip;
	public AudioClip otherSpeechClip;

	public System.Action action;

	void Awake(){
		audioSource=GetComponent<AudioSource>();
	}
	// Use this for initialization
	void Start () {
		Ring(FirstTalk);
	}

	public void Talk(string _text){
		audioSource.PlayOneShot(playerSpeechClip);
		GameManager.instance.SetSubtitle(_text);
	}

	public void PhoneTalk(string _text){
		audioSource.PlayOneShot(otherSpeechClip);
		GameManager.instance.SetSubtitle(_text);
	}

	public void Ring(System.Action _action){
		action=_action;
		audioSource.clip=ringClip;
		audioSource.Play();
		audioSource.loop=true;
	}

	public void Pickup(){
		audioSource.Stop();
		audioSource.PlayOneShot(pickupClip);

	}

	public void Putdown(){
		audioSource.PlayOneShot(putdownClip);
	}

	public override bool Interactive(){
		if (audioSource.isPlaying){
			return true;
		}
		else{
			return false;
		}
	}

	public override void Interact(){
		if (audioSource.isPlaying ){
			Pickup();
			if (action!=null){
				action();
			}
		}
    }

	void FirstTalk(){
		StartCoroutine(DoFirstTalk());
	}

	IEnumerator DoFirstTalk(){
		yield return new WaitForSeconds(1);
		PhoneTalk("No. 0225");
		
		yield return new WaitForSeconds(2);
		Talk("Yes ?");
		
		yield return new WaitForSeconds(3);
		PhoneTalk("You got any messages ?");
		
		yield return new WaitForSeconds(2);
		Talk("What messages ?");
		
		yield return new WaitForSeconds(3);
		PhoneTalk("If anyone gives you something, DO NOT accept it.");
		
		yield return new WaitForSeconds(3);
		PhoneTalk("For your safety,");
		
		yield return new WaitForSeconds(3);
		PhoneTalk("DO NOT try to escape, remember we are always watching you.");
		
		yield return new WaitForSeconds(3);
		Talk("Ok.");
		
		yield return new WaitForSeconds(2);
		PhoneTalk("If you got any problems, just tell us");
		
		yield return new WaitForSeconds(2);
		PhoneTalk("And we can help you.");
		
		yield return new WaitForSeconds(3);
		Talk("Understood.");
		
		yield return new WaitForSeconds(2);
		PhoneTalk("Good, we are brothers.");
		
		yield return new WaitForSeconds(2);
		GameManager.instance.HideSubtitle();
		GameManager.instance.firstTalkPicked=true;
		Putdown();
	}
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.F) ){
//			if (audioSource.isPlaying ){
//				Interact();
//			}
//			else{
//				Ring(FirstTalk);
//			}
//		}
	}
}
