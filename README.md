# GameDesign Final Project - Dungeon Dice Master

## 開發環境： 
**Unity 5.6.1**

## 最新方向更新：
* 2017/05/19 Update: **取消Roguelike探索元素以及其他額外特殊事件，專注於戰鬥系統的實作。轉型為打塔通關的挑戰模式。**

## 遊戲性相關討論：

### 玩家角色
1. 隊伍模式：3 v.s. 1
  * 保持目前的戰鬥流程，玩家可以同時操作三名角色與一隻敵人交戰。
2.  單人模式：1 v.s. 1
  ** Discuss: **
  - [ ] 整個Demo只有一個角色，但增加可使用的技能。
  - [ ] 提供三位角色給玩家選擇，在 Game Over 後才能切換角色。
  - [ ] 提供三位角色給玩家選擇，在每場戰鬥結束之後都有機會更換角色。

### 遊戲流程
1. 顯示戰鬥對象的訊息
  * 數值
  * 特殊能力
  * 擊敗方式
2. 展開戰鬥輪替
3. 戰鬥結束，玩家勝利進入4\.，失敗則 Game Over 。 
4. 獎勵結算畫面 
  * 回復 HP
  * 回復 MP
  * 新骰子
  * 賭技次數
  > Discuss: 同時出現多個獎勵，讓玩家進行 2 選 1 或 3 選 1 。

### **技能**
> Discuss: 最終版的 demo 預計只有戰鬥，為了發揮我們的戰鬥系統的優勢以及樂趣，技能跟賭技是否還需要調整？

### **怪物設計**
* Prototype 3 Goal: 展示兩隻以上的怪物。
> Discuss: 除了增加特殊技能外，要如何增加怪物的多樣性？
> 最終所需怪物數量至少要五種。

### **骰子**
> Discuss: 是否仍然要加入特殊骰的設定？

### **其他**
* 大成功/大失敗 的機制加入與否
  > 例如所有骰子皆為最大點數的時候一擊必殺，皆為最小的時候直接死亡。

## 實作相關討論：
* 流程圖中哪些步驟要有下一步的確認按鈕？哪一些要讓動畫自動進行？
* 戰技 / 賭技選擇結束後，是否要有反悔用的「返回」按鈕？
* 技能選擇要使用點按式還是拖拉式的操作？
* 角色數值的 UI 要綁定在角色物件上，還是維持現有的 UI Manager 管理形式？
