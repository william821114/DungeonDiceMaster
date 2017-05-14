using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleSkillManager : MonoBehaviour {

	public int[] skillTimes;

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
}
