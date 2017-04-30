using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    public State.BattleState currentState;

    //暫時寫在這 應該有更好的寫法
    public Image BattleResult;
    public Text BattleResultText;


    public Button backButton;
    public Button nextButton;

    public Transform skillButtons;

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
            Debug.Log(bObj.name);
            bObj.SendMessage("onStateChange", currentState);
        }
    }

    public void setBattleEnd(bool win)
    {
        currentState = State.BattleState.BattleEnd;
        BattleResult.gameObject.SetActive(true);
        BattleResultText.text = (win ? "Win" : "Lose");

    }

    public void prepareForPlayerTurn()
    {
        nextButton.gameObject.SetActive(true);
        skillButtons.gameObject.SetActive(true);
    }

    public State.BattleState getState()
    {
        return currentState;
    }

}
