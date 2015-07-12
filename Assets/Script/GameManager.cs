using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


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



	public ButtonFlush bf;

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

//	public bool firstTalkPicked=false;

	public AudioClip gunshotClip;
	public AudioClip dieClip;
	public AudioSource audioSource;


	public bool[] phonePicked;
	public Dictionary<string,int> noteChoices=new Dictionary<string, int>();
	public Dictionary<string,bool> talkFlag=new Dictionary<string, bool>();
	public Dictionary<string,int> talkChoices=new Dictionary<string, int>();

	public int GetTalkChoice(string talk){
		if (!talkChoices.ContainsKey(talk) ){
			talkChoices.Add(talk,0);
		}
		return talkChoices[talk];
	}
	
	public void SetTalkChoice(string talk, int val){
		if (!talkChoices.ContainsKey(talk) ){
			talkChoices.Add(talk,val);
		}
		else{
			talkChoices[talk]=val;
		}
	}

	public int GetNoteChoice(string choice){
		if (!noteChoices.ContainsKey(choice) ){
			noteChoices.Add(choice,0);
		}
		return noteChoices[choice];
	}

	public void SetNoteChoice(string choice, int val){
		if (!noteChoices.ContainsKey(choice) ){
			noteChoices.Add(choice,val);
		}
		else{
			noteChoices[choice]=val;
		}
	}

	public bool GetTalk(string talk){
		if (!talkFlag.ContainsKey(talk) ){
			talkFlag.Add(talk,false);
		}
		return talkFlag[talk];
	}


	public void SetTalk(string talk, bool val){
		if (!talkFlag.ContainsKey(talk) ){
			talkFlag.Add(talk,val);
		}
		else{
			talkFlag[talk]=val;
		}
	}



	IEnumerator DoGameStart(){

		phone.Ring(Talk1 );



		while (!GetTalk("Talk1") ){
			yield return new WaitForEndOfFrame();
		}
		yield return new WaitForSeconds(2);

		phone.Ring(ChoiceTalk1 );
		while (!GetTalk("ChoiceTalk1") ){
			yield return new WaitForEndOfFrame();
		}


		yield return new WaitForSeconds(5);
		bf.GenerateTip("1","我不想死",null);
		while(bf.curTip!=null){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(5);

		phone.Ring(Talk2 );
		while (!GetTalk("Talk2") ){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(5);
		bf.GenerateTip("1","我不想死",null);
		while(bf.curTip!=null){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(5);
		phone.Ring(Talk3 );
		while (!GetTalk("Talk3") ){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(5);
		bf.GenerateTip("2","他是一个天才吗\n1他的却有某种才能\n2他是天生的独裁者", new string[]{"他的却有某种才能","他是天生的独裁者"});
		while(bf.curTip!=null){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(5);
		phone.Ring(Talk4 );
		while (!GetTalk("Talk4") ){
			yield return new WaitForEndOfFrame();
		}

		if (GetNoteChoice("2")==0 ){
			bf.GenerateTip("3","他很会为人处世，这\n是我完全不擅长的地\n方。但是他所使用的\n手段是我完全无法接\n受的",null);
		}
		else{
			bf.GenerateTip("4","当时他们都说我是天\n才。然而现在。。。",null);
		}


		while(bf.curTip!=null){
			yield return new WaitForEndOfFrame();
		}
		
		yield return new WaitForSeconds(5);
		phone.Ring(Talk5 );
		while (!GetTalk("Talk5") ){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(5);
		bf.GenerateTip("5","那我的女儿呢？",null);
		while(bf.curTip!=null){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(3);
		audioSource.PlayOneShot(dieClip);

		yield return new WaitForSeconds(3);

		audioSource.PlayOneShot(gunshotClip);

//		bf.GenerateTip("那我的女儿呢？");

//		我不想死
//		他是一个天才吗？ 
//		1他的却有某种才能
//		2他是天生的独裁者
//		他很会为人处世，这/n是我完全不擅长的地/n方。但是他所使用的/n手段是我完全无法接/n受的
//		当时他们都说我是天/n才。然而现在。。。
//		纸条：那我的女儿呢？

//		Debug.Log("picked");
//		yield return new WaitForSeconds(5);
//		audioSource.PlayOneShot(dieClip);
//
//		yield return new WaitForSeconds(3);
//
//		audioSource.PlayOneShot(gunshotClip);


	}

	public void Talk1(){
		StartCoroutine(DoTalk1() );
	}

	public void Talk2(){
		StartCoroutine(DoTalk2() );
	}

	public void Talk3(){
		StartCoroutine(DoTalk3() );
	}

	public void Talk4(){
		StartCoroutine(DoTalk4() );
	}

	public void Talk5(){
		StartCoroutine(DoTalk5() );
	}

	public void ChoiceTalk1(){
		StartCoroutine(DoTalkChoice1() );
	}

	public void MakeChoice(int val){
		Debug.Log("make choice: "+curTalk);
		if (curTalk==""){
			return;
		}
		SetTalkChoice(curTalk,1);
		Debug.Log("set "+curTalk+" to 1");
		curTalk="";
	}

	public string curTalk="";
	
	IEnumerator DoTalkChoice1(){
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("你的回答将都是“是”与“否”");

		yield return new WaitForSeconds(3);
		phone.PhoneTalk("如果你回答是“是”，那么请注意按下旁边的按钮");

		yield return new WaitForSeconds(3);
		phone.PhoneTalk("你同意可以保持沉默，那我们就默认你的选择是“否”");

		yield return new WaitForSeconds(2);
		phone.PhoneTalk("听清楚了吗？");

		yield return new WaitForSeconds(3);
		phone.PhoneTalk("为保证你清楚并了解刚才我们所说的，请现在按按钮确认一下");

		curTalk="TalkChoice1";
		while( GetTalkChoice("TalkChoice1")==0 ){
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForSeconds(2);
		phone.PhoneTalk("很好，那么我们可以开始了.");

		yield return new WaitForSeconds(2);
		phone.PhoneTalk("每一次通话之后，我们都会给你留有充分的时间进行思考。");

		yield return new WaitForSeconds(3);
		phone.PhoneTalk("那么，接下来请你先准备一下。");

		yield return new WaitForSeconds(3);
		phone.Putdown();
		HideSubtitle();
		SetTalk("TalkChoice1",true);

	}

	IEnumerator DoTalk1(){
		yield return new WaitForSeconds(1);
		phone.PhoneTalk("今天是你行刑的日子");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("出于人道主义原则，");
		
		yield return new WaitForSeconds(3);
		phone.PhoneTalk("我们希望你的死是完全出于自愿的，");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("完全是你自身的赎罪。");
		
		yield return new WaitForSeconds(3);
		phone.PhoneTalk("虽然我们已经知道你对自己的罪行供认不讳");
		
		yield return new WaitForSeconds(3);
		phone.PhoneTalk("但是我们还是希望通过电话来再次确认，");
		
		yield return new WaitForSeconds(3);
		phone.PhoneTalk("记住你的每句话将被录音。");



		yield return new WaitForSeconds(2);
		HideSubtitle();

		phone.Putdown();
		SetTalk("Talk1",true);
	}


	IEnumerator DoTalk5(){
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("你知道李有一个4岁的小孩吗？");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("李的妻子是家庭主妇，她并没有工作。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("他们家现在只有靠媒体大众的捐助过活。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("你能想象一个小孩在4岁时失去父亲吗？");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("你能想象这会对她的将来造成多大的影响吗？");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("你的所作所为完完全全不是一个人类能做出来的！！！！");

		
		yield return new WaitForSeconds(2);
		HideSubtitle();
		
		phone.Putdown();
		SetTalk("Talk5",true);
	}


	IEnumerator DoTalk2(){
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("现在我们再次回忆一下你的罪行。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("如果你不想继续回忆那段痛苦的经历,");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("你可以直接使用手上的小刀了解自己。");
		
		yield return new WaitForSeconds(2);
		HideSubtitle();
		
		phone.Putdown();
		SetTalk("Talk2",true);
	}
	
	IEnumerator DoTalk3(){
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("被害者，你的同事李。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("他是你们的项目负责人。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("他是个天才。然而这却让你产生了嫉妒。");
		
		yield return new WaitForSeconds(2);
		HideSubtitle();
		
		phone.Putdown();
		SetTalk("Talk3",true);
	}
	
	IEnumerator DoTalk4(){
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("李进公司的时候还是一个普通的小职员。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("他拥有超高的智商和才能。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("他是个天才。然而这却让你产生了嫉妒。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("从一个公司的办事员上升到了部门副经理的职位。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("从此成为业界的神话。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("而你通过与你们公司老总偶然的相遇，");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("获得了老板的赏识。");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("老板直接让你做了部门经理，");
		
		yield return new WaitForSeconds(2);
		phone.PhoneTalk("然而很多同事说你不过是运气好而已");
		
		yield return new WaitForSeconds(2);
		HideSubtitle();
		
		phone.Putdown();
		SetTalk("Talk4",true);
	}
	
	
	

	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R) ){
			SwitchCameraMode();
		}



	}


}
