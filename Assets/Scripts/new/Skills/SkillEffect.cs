using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect
{
    public bool isDamage;
    public bool isHeal;
    public bool isDodge;
    public bool isShield;
    public int damage;
    public int heal;
    public int dodge;
    public float shield;

    public SkillEffect(bool isDamage, bool isHeal, bool isDodge, bool isShield,
                       int damage, int heal, int dodge, float shield)
    {
        this.isDamage = isDamage;
        this.isHeal = isHeal;
        this.isDodge = isDodge;
        this.isShield = isShield;
        this.damage = damage;
        this.heal = heal;
        this.dodge = dodge;
        this.shield = shield;
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

}
