using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 定義 state 的 global 變數， 目前只有Battle而已，之後會更複雜
public static class State {
    public enum BattleState
    {
		RollOrder,
        PlayerTurn,
        EnemyTurn,
        BattleEnd
    }
}
