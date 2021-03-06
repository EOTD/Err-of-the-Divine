﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Achievement  {
	private string name;
	public string Name {
		get { return name; }
		set { name = value; }
	}

	private string description;
	public string Description {
		get { return description; }
		set { description = value; }
	}

	private bool unlocked;
	public bool Unlocked {
		get { return unlocked; }
		set { unlocked = value; }
	}

	private int points;

	public int Points {
		get { return points; }
		set { points = value; }
	}

	private int spriteIndex;
	public int SpriteIndex {
		get { return spriteIndex; }
		set { spriteIndex = value; }
	}

	private GameObject achievementRef;

	private List<Achievement> dependencies = new List<Achievement> ();

	private string child;

	public string Child {
		get { return child; }
		set { child = value; }
	}

	public Achievement( string name, string description, int points, int spriteIndex, GameObject achievementRef){
		this.name = name;
		this.description = description;
		this.unlocked = false;
		this.points = points;
		this.spriteIndex = spriteIndex;
		this.achievementRef = achievementRef;
		LoadAchievement ();
	}

	// Series of Achievements by achieving other achievements to get a certain achievement.
	public void AddDependency(Achievement dependency){
		dependencies.Add (dependency);
	}

	public bool EarnAchievement(){
		if (!unlocked && !dependencies.Exists (x => x.unlocked == false)) {
			// Singleton to instance from the Achievement Manager. Swaps the sprite with the earned one that is linked on the AchievementManager inspector
			achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
			SaveAchievement(true);

			if(child != null){
				AchievementManager.Instance.EarnAchievement(child);
			}
			return true;
		} else
			return false;
	}

	public void SaveAchievement(bool value){
		unlocked = value;

		int tmpPoints = PlayerPrefs.GetInt ("Points");

		PlayerPrefs.SetInt ("Points", tmpPoints += points);
		PlayerPrefs.SetInt (name, value ? 1 : 0);

		PlayerPrefs.Save ();
	}

	public void LoadAchievement(){
		unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

		if (unlocked) {
			AchievementManager.Instance.textPoints.text = "Points: " +PlayerPrefs.GetInt ("Points");
			achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;
		}
	}
}
	
