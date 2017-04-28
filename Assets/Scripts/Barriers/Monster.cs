using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 目前還缺受傷的動畫、攻擊的動畫
public class Monster : Barrier {

	public int Hp;
	public int Atk;
	public int Def;

	private bool isDead = false;
	private Animator monsterAnimator;

	void Start(){
		monsterAnimator = this.GetComponent<Animator> ();
	}

	public override void check(int finalCheckValue){
		int damage = finalCheckValue - this.Def;
		Debug.Log (damage);

		if (damage > 0)
			getHurt (damage);
	}

	public void getHurt(int damage){
		if (!isDead) {
			Hp -= damage;

			if (Hp <= 0) {
				isDead = true;
				PlayDieAnimation ();
			}
		}
	}

	// 播死亡的fade out動畫
	private void PlayDieAnimation(){
		monsterAnimator.SetTrigger ("Die");
	} 

	// fade out的最後一個frame會呼叫這個，來清掉物件
	private void cleanDeadBody(){
		Destroy (this.gameObject);
	}
}
