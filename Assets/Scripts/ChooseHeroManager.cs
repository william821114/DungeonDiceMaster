using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHeroManager : MonoBehaviour {

	public GameObject monsterMark;

	public TextMesh[] hp;
	public TextMesh[] mp;
	public TextMesh[] def;

	public TextMesh monsterHp;
	public TextMesh monsterDef;

	private DataManager dataManager;
	private Monster monster;

	// Use this for initialization
	void Start () {
		dataManager = (DataManager)FindObjectOfType (typeof(DataManager));

		for (int i = 0; i < dataManager.team.Length; i++) {
			hp [i].text = "" + dataManager.team [i].Hp;
			mp [i].text = "" + dataManager.team [i].Mp;
			def [i].text = "" + dataManager.team [i].Def;
		}

		setHero (1);
		showMonster ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setHero(int index){
		dataManager.choosedHero = dataManager.team[index];
	}

	private void showMonster(){
		monster = dataManager.monsterPool [Random.Range (0, dataManager.monsterPool.Length)];
		dataManager.choosedMonster = monster;

		monsterHp.text = "" + monster.Hp;
		monsterDef.text = "" + monster.Def;

		monster = GameObject.Instantiate (monster, Vector3.zero, Quaternion.identity) as Monster;

		monster.transform.position = monsterMark.transform.position;
		monster.transform.rotation = monsterMark.transform.rotation;
		monster.gameObject.transform.parent = monsterMark.transform;
	}
}
