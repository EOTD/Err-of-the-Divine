using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour {

	public GameObject achievementPrefab;

	public Sprite[] sprites;

	private AchievementButton activeButton;
	public ScrollRect scrollRect;

	public GameObject achievementMenu;

	public GameObject visualAchievement;

	public Sprite unlockedSprite;

	public Text textPoints;

	private int fadeTime = 2;

	private static AchievementManager instance;
	public static AchievementManager Instance {
		get { 
			if(instance == null){
				instance = GameObject.FindObjectOfType<AchievementManager>();
			}
			return AchievementManager.instance;
		}
	}


	public Dictionary<string,Achievement> achievements = new Dictionary<string, Achievement>();
	// Use this for initialization
	void Start () {

		// REMEMBER TO REMOVE
		PlayerPrefs.DeleteAll ();

		activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton> ();

		CreateAchievement ("General", "Death to them All!","Let all the Civillians die.",5,0);
		CreateAchievement ("General", "The Savior","Defeat Mercury before he devours any civillian.",5,0);
        CreateAchievement ("General", "Too Quick for You", "Defeat Mercury.", 5, 0);
        CreateAchievement ("General", "Quick as Lightning", "Obtain Mercury's Haste Skill.", 10, 0, new string[] { "Too Quick for You", "Death to them All!" });

        CreateAchievement ("Dungeon","Err!", "Test!", 10,1);
		CreateAchievement ("Dungeon","Err2!", "Test!", 10,1);


		CreateAchievement ("Other","Err3!!", "Test!",10,2);
		CreateAchievement ("Other","Err4!!", "Test!", 10,2);
		CreateAchievement ("Other","Err!a!", "Test!", 10,2);


		foreach( GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList")){
			achievementList.SetActive(false);
		}
		activeButton.Click ();

		achievementMenu.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.L)) {
			achievementMenu.SetActive (!achievementMenu.activeSelf);
		}
		//if (Input.GetKeyDown (KeyCode.W)) {
		//	EarnAchievement("Press W");
		//}

		//if (Input.GetKeyDown (KeyCode.S)) {
		//	EarnAchievement("Press S");
		//}
		//if(Input.GetMouseButton(0)){
		//	EarnAchievement("1st time Poop");
		//}
	}

	public void EarnAchievement(string title){
		if (achievements[title].EarnAchievement()) {
			GameObject achievement = (GameObject)Instantiate (visualAchievement);
			SetAchievementInfo("EarnCanvas",achievement,title);
			textPoints.text = "Points: " + PlayerPrefs.GetInt ("Points");
			StartCoroutine(FadeAchievement(achievement));
			// Do something
		}
	}

	public IEnumerator HideAchievement(GameObject achievement){
		yield return new WaitForSeconds (3);
		Destroy (achievement);
	}

	// dependencies is optional parameter
	public void CreateAchievement(string parent, string title, string description, int points, int spriteIndex, string[] dependencies = null){
		GameObject achievement = (GameObject)Instantiate (achievementPrefab);

		Achievement newAchievement = new Achievement (name, description, points, spriteIndex, achievement);

		achievements.Add (title, newAchievement);

		SetAchievementInfo (parent, achievement, title);

		if (dependencies != null) {

			// Checks each item in dependencies
			foreach( string achievementTitle in dependencies){
				Achievement dependency = achievements[achievementTitle];
				dependency.Child = title;
				newAchievement.AddDependency(achievements[achievementTitle]);

				// Dependency = Press Space <-- Child = Press W
				// NewAchievement = Press W --> Press Space
			}
		}

	}

	public void SetAchievementInfo(string parent, GameObject achievement, string title){
		achievement.transform.SetParent (GameObject.Find (parent).transform);
		achievement.transform.localScale = new Vector3 (0.95f, 0.9f, 0.9f);
		achievement.transform.GetChild (0).GetComponent<Text> ().text = title;
		achievement.transform.GetChild (1).GetComponent<Text> ().text = achievements[title].Description;
		achievement.transform.GetChild (2).GetComponent<Text> ().text = achievements[title].Points.ToString();
		achievement.transform.GetChild (3).GetComponent<Image> ().sprite = sprites[achievements[title].SpriteIndex];
	}

	public void ChangeCategory(GameObject button){
		AchievementButton achievementButton = button.GetComponent<AchievementButton> ();

		scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform> ();

		achievementButton.Click ();
		activeButton.Click ();
		activeButton = achievementButton;
	}

	private IEnumerator FadeAchievement(GameObject achievement){
		CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup> ();
		float rate = 1.0f / fadeTime;

		// Start Alpha at 0
		int startAlpha = 0;
		int endAlpha = 1;

		for(int i=0; i<2; i++){
			float progress = 0.0f;
			while (progress < 1.0f) {
				canvasGroup.alpha = Mathf.Lerp(startAlpha,endAlpha,progress);
				progress += rate * Time.deltaTime;
				yield return null;
			}
			yield return new WaitForSeconds (2);
			startAlpha = 1;
			endAlpha = 0;
		}
		Destroy (achievement);
	}
}
