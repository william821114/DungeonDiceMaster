using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillEffect
{
    public bool isDamage;
    public bool isHeal;
    public bool isMPDamage;
    public bool isHealMP;
    public bool isDodge;
    public bool isShield;
    public bool isDisable;
    public bool isSelfDisable;
    public bool isSkillActivated;
    public int damage;
    public int heal;
    public int MPDamage;
    public int healMP;
    public int dodge;
    public float shield;
    public int disable;
    public int disableTurns;
    public int selfDisable;
    public int selfDisableTurns;
    public MonsterSkill usingSkill;

    public MonsterSkillEffect()
    {
        this.isDamage = false;
        this.isHeal = false;
        this.isDodge = false;
        this.isShield = false;
        this.isDisable = false;
        this.isSelfDisable = false;
        this.isMPDamage = false;
        this.isHealMP = false;

        // 初始值設定 activated 為 false，在 Skill 中的 calSkillEffect 發動成功時，
        // 將這個參數設定為 true ，以觸發技能發動的特效
        this.isSkillActivated = false;
    }

    public void setHeal(int val)
    {
        this.isHeal = true;
        this.heal = val;
    }

    public void setDamage(int val)
    {
        this.isDamage = true;
        this.damage = val;
    }

    public void setDodge(int val)
    {
        this.isDodge = true;
        this.dodge = val;
    }

    public void setShield(float val)
    {
        this.shield = val;
    }

    public void setDisable(int val, int turn)
    {
        this.disable = val;
        this.disableTurns = turn;
    }

    public void setSelfDisable(int val, int turn)
    {
        this.selfDisable = val;
        this.selfDisableTurns = turn;
    }

    public void setMPDamage(int val)
    {
        this.MPDamage = val;
    }

    public void setHealMP(int val)
    {
        this.healMP = val;
    }

    public void setSkillActivated(bool val)
    {
        this.isSkillActivated = val;
    }

    public void setUsingSkill(MonsterSkill skill)
    {
        this.usingSkill = skill;
    }

}
