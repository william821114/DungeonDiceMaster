using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Barrier {

	public int Hp;
	public int Atk;
	public int Def;

	private bool isDead = false;

	public override void check(int finalCheckValue){
		int damage = finalCheckValue - this.Def;
		Debug.Log (damage);
		if (damage > 0)
			getHurt (damage);
	}

	public void getHurt(int damage){
		if (!isDead) {
			Hp -= damage;

			if (Hp <= 0)
				isDead = true;
		}
	}
}
