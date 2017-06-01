using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;


// 目前還缺受傷的動畫、攻擊的動畫
public class Monster : BattleUnit {

    public int MaxHp;
    public int MaxMp;
    public int originAtk;
    public int originDef;

    public int Hp;
    public int Mp;
	public int Atk;
	public int Def;

	public Dice[] battleDice;
	public Dice exploreDice;
    public bool willGetHurt = false;

	private bool isDead = false;
	private Animator monsterAnimator;

    public Character actioningCharacter;
    public StateManager stateManager;
    public Animator actioningCharacterAnimator;
    //public TextMesh monsterHurtValueText;


	void Start () {
		stateManager = (StateManager)FindObjectOfType (typeof(StateManager));
	}
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

    public void check(int finalCheckValue)
    {
        int damage = finalCheckValue - this.Def;
        if(damage > 0)
        {
            willGetHurt = true;
            Debug.Log("Monster: get Hurt - " + damage);
            this.Hp -= damage;
            //monsterHurtValueText.text = "-" + damage;
        }
    }

    public void AI(int[] checkValue)
    {
        //先做一個最簡單的ai，隨機選一個人，直接把finalcheckvalue當傷害打出去
        int finalCheckValue = 0;

        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }
			
		Character target = stateManager.getCharacter();
        target.check(finalCheckValue);
    }

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

    */

    public void damageMp(int value)
    {
        this.Mp = (Mp - value > 0) ? (Mp - value) : 0;
    }
}
