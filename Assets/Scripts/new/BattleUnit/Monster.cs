using System.Collections;
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

    public Dice[] battleDice;
    public Dice exploreDice;
    public bool willGetHurt = false;
    public MonsterSkill[] skill;

    private bool isDead = false;
    private Animator monsterAnimator;

    public Character actioningCharacter;
    public StateManager stateManager;
    public Animator actioningCharacterAnimator;

    //public TextMesh monsterHurtValueText;


    void Start()
    {
        stateManager = (StateManager)FindObjectOfType(typeof(StateManager));
    }

    public void check(int finalCheckValue)
    {
        int damage = finalCheckValue - this.Def;
        if (damage > 0)
        {
            willGetHurt = true;
            Debug.Log("Monster: get Hurt - " + damage);

            //取得 monster底下的 hurtvalue並預先更新數字
            TextMesh HurtValue = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
            HurtValue.text = "-" + damage.ToString();


            this.Hp -= damage;

            if (this.Hp <= 0) this.Hp = 0;
            //monsterHurtValueText.text = "-" + damage;
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
        }
    }
}
