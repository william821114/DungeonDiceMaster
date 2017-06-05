using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect
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

    public SkillEffect(bool isDamage, bool isHeal, bool isDodge, bool isShield,
                       bool isDisable, bool isSelfDisable, bool isMPDamage, bool isHealMP)
    {
        this.isDamage = isDamage;
        this.isHeal = isHeal;
        this.isDodge = isDodge;
        this.isShield = isShield;
        this.isDisable = isDisable;
        this.isSelfDisable = isSelfDisable;
        this.isMPDamage = isMPDamage;
        this.isHealMP = isHealMP;

        // 初始值設定 activated 為 false，在 Skill 中的 calSkillEffect 發動成功時，
        // 將這個參數設定為 true ，以觸發技能發動的特效
        this.isSkillActivated = false;
    }

    public void setHeal(int val)
    {
        this.heal = val;
    }

    public void setDamage(int val)
    {
        this.damage = val;
    }

    public void setDodge(int val)
    {
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

}
