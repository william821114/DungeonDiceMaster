using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceState {
    public int disableTurn;

    public DiceState()
    {
        this.disableTurn = 0;
    }

    public void addDisableTurn(int turn)
    {
        this.disableTurn += turn;
    }
    
    public void decreaseDisableTurn()
    {
        this.disableTurn = (this.disableTurn - 1 > 0) ? this.disableTurn : 0;
    }
 
}
