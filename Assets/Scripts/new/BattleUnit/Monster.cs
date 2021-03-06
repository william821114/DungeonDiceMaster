﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiceMaster;


// 目前還缺受傷的動畫、攻擊的動畫
public class Monster : BattleUnit
{

    public int MaxHp;
    public int MaxMp;
    public int originAtk;
    public int originDef;

    public int Hp;
    public int Mp;
    public int Atk;
    public int Def;

    public string description;

    public Dice[] battleDice;
    public Dice exploreDice;
    public DiceState[] diceStates;
    public bool willGetHurt = false;
    public MonsterSkill[] skill;
    public MonsterSkillEffect mse;

	public Sprite[] monsterSkills;

    private bool isDead = false;
    private Animator monsterAnimator;

    public Character actioningCharacter;
    public StateManager stateManager;
    public BattleCheckManager bcManager;
    public Animator actioningCharacterAnimator;

    

    //public TextMesh monsterHurtValueText;


    void Awake()
    {
        stateManager = (StateManager)FindObjectOfType(typeof(StateManager));
        bcManager = (BattleCheckManager)FindObjectOfType(typeof(BattleCheckManager));
        this.diceStates = new DiceState[battleDice.Length];
        for(int i = 0; i < battleDice.Length; i ++)
        {
            this.diceStates[i] = new DiceState();
        }
        
    }

    public void check(int finalCheckValue)
    {
        int damage = finalCheckValue - this.Def;
        TextMesh HurtValue = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        HurtValue.text = "-0";

        if (damage > 0)
        {
            willGetHurt = true;
            Debug.Log("Monster: get Hurt - " + damage);

            //取得 monster底下的 hurtvalue並預先更新數字
           
            HurtValue.text = "-" + damage.ToString();


            this.Hp -= damage;

            if (this.Hp <= 0) this.Hp = 0;
            //monsterHurtValueText.text = "-" + damage;
            bcManager.turnDamage = damage;
        }
    }

    public virtual void AI(int[] checkValue)
    {
        // override
        int finalCheckValue = 0;
        Character target = stateManager.getCharacter();
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        target.check(finalCheckValue);
    }

    public Dice[] getBattleDice()
    {
        return battleDice;
    }

    public Dice getExploreDice()
    {
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

    public void recoverHP(int value)
    {
        if (!isDead)
        {           
            Hp = (Hp + value) >= MaxHp ? MaxHp : (Hp + value);
            TextMesh HealValue = this.gameObject.transform.GetChild(1).gameObject.GetComponent<TextMesh>();
            HealValue.text = "+" + value;
        }
    }

    public void turnEnd()
    {
        for (int i = 0; i < this.diceStates.Length; i++)
        {
            this.diceStates[i].decreaseDisableTurn();
        }
    }
}
