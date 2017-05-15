using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProtect: Skill {

	public override int skillWeight(int[] checkValue){
        finalCheckValue = 0;
        for (int i = 0; i < checkValue.Length; i++)
        {
            finalCheckValue += checkValue[i];
        }

        if (finalCheckValue > 10)
        {
            Debug.Log("風之守護!");
            finalCheckValue = 1;
        }

        return finalCheckValue;
    }
}