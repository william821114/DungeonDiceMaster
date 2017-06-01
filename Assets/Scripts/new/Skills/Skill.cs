using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{

    public int needMP;
    public int finalCheckValue;

    // new skill parameter
    public bool isDamage;
    public bool isDodge;
    public bool isShield;
    public bool isHeal;

    //These methods are virtual and thus can be overriden
    //in child classes
    public virtual int skillWeight(int[] checkValue)
    {
        // 判定直出來的值是否符合條件，經過計算後回傳修正後的總累計值。
        Debug.Log("pass check value to skill");
        return finalCheckValue;
    }

    public virtual SkillEffect calSkillEffect(int[] checkValue)
    {
        SkillEffect skilleffect = new SkillEffect(this.isDamage, this.isHeal, this.isDodge, this.isShield, finalCheckValue, 0, 0, 0);
        return skilleffect;
    }
}
