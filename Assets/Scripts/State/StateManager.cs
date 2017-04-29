using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {
    public State.BattleState currentState;

	// Use this for initialization
	void Start () {
		
	}
	
    void setTurn(State.BattleState state)
    {
        currentState = state;

        // find enemy and heros and notify the change of state
        GameObject[] battlingObjects = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject bObj in battlingObjects)
        {
            bObj.SendMessage("onStateChange", currentState);
        }
    }

}
