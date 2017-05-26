using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

	public int[] gambleSkillTimes;
	public Character choosedHero;
	public Character[] team;

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
}
