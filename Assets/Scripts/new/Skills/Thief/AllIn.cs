using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllIn: Skill {

	public override int skillWeight(int[] checkValue){

        bool isAllSix = true;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
            if (checkValue[i] != 6) isAllSix = false;
        }

        if(isAllSix)
        {
            finalCheckValue = 60;
            Debug.Log("梭哈成功! - " + finalCheckValue);
        } else
        {
            finalCheckValue = 0;
        }

        return finalCheckValue;
	}
}