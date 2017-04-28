using System.Collections;
using System.Collections.Generic;
using DiceMaster;
using UnityEngine;

// 缺動畫(攻擊、死亡、受傷害)
public class Character : MonoBehaviour {

	public int Hp;
	public int Mp;
	public int Atk;
	public int Def;
	public Dice[] battleDice;
	public Dice[] socialDice;
	public Dice[] exploreDice;
	public Skill[] skill;

	private bool isDead = false;

	// 呼叫此function來扣角色HP
	public void getHurt(int damage)
	{
		if (!isDead) {	
			if (Def - damage < 0)
				Hp = Hp - (Def - damage);

			if (Hp <= 0)
				isDead = true;
		}
	}

	// 呼叫此function來取得這個角色裝備的骰子
	public Dice[] getDice(int checkEvent){
		// battle event = 0
		// social event = 1
		// explore event = 2

		if (checkEvent == 0) {
			return battleDice;
		} else if (checkEvent == 1) {
			return socialDice;
		} else if (checkEvent == 2) {
			return exploreDice;
		} else
			return null;
	}

}
