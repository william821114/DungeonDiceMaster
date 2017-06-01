using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

	public int[] gambleSkillTimes;
	public Character choosedHero;
	public Character[] team;
	public Monster choosedMonster;
	public Monster[] monsterPool;

	private static DataManager dataManagerInstance;

	void Awake(){
		DontDestroyOnLoad (this);

		if (dataManagerInstance == null) {
			dataManagerInstance = this;
			for(int i=0; i<team.Length; i++) {
				team[i] =  Instantiate(team[i], new Vector3(0f, 10f, 0f), Quaternion.identity);
			}

			choosedHero = team [1];
		} else {
			DestroyObject(gameObject);
		}
	}
}
