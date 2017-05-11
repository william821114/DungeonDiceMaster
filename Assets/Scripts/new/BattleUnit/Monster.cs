using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;


// 目前還缺受傷的動畫、攻擊的動畫
public class Monster : BattleUnit {

    public int MaxHp;
    public int originAtk;
    public int originDef;

    public int Hp;
	public int Atk;
	public int Def;

	public Dice[] battleDice;
	public Dice exploreDice;

	private bool isDead = false;
	private Animator monsterAnimator;

    public Character actioningCharacter;
    public StateManager stateManager;
    public Animator actioningCharacterAnimator;

	/*
    void Start(){
		monsterAnimator = this.GetComponent<Animator> ();

        Hp = MaxHp;
        Atk = originAtk;
        Def = originDef;
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
            PlayHurtAnimation();



            if (Hp <= 0) {
				isDead = true;
				PlayDieAnimation ();
                stateManager.setBattleEnd(true);

            }
		}
	}*/

	public Dice[] getBattleDice(){
		return battleDice;
	}

	public Dice getExploreDice(){
		return exploreDice;
	}

	/*
	// 播死亡的fade out動畫
	private void PlayDieAnimation(){
		monsterAnimator.SetTrigger ("Die");
	} 

    private void PlayHurtAnimation()
    {
        monsterAnimator.SetTrigger("Hurt");
    }

	// fade out的最後一個frame會呼叫這個，來清掉物件
	private void cleanDeadBody(){
		Destroy (this.gameObject);
	}

    // 怪物每一回合的行動
    private void AI()
    {
        // 這邊還要加上動畫
        monsterAnimator.SetTrigger("Attack");
        actioningCharacter.SendMessage("getHurt", Atk);
    }

    public void MonsterAttackEnd()
    {
        actioningCharacterAnimator.SetTrigger("Hurt");
    }

    public void MonsterTurnEnd()
    {
        if (!stateManager.getState().Equals(State.BattleState.BattleEnd))
        {
            stateManager.SendMessage("setTurn", State.BattleState.PlayerTurn);
        }
    }

    public void onStateChange(State.BattleState state)
    {
        
        if (state.Equals(State.BattleState.EnemyTurn))
        {
            Debug.Log("Deer : It's my turn!");
            AI();

            
           
        }
    }*/
}
