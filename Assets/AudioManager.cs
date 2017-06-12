using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public AudioSource[] playerAttackSound;
    public AudioSource[] monsterAttackSound;
    public AudioSource[] playerHurtSound;
    public AudioSource[] monsterHurtSound;
    public AudioSource SkillActivatedSound;
    public AudioSource BattleVictorySound;
    public AudioSource playerDodgeSound;
    public AudioSource monsterAttackDodgedSound;

    public AudioSource BGM_battle;
    public AudioSource BGM_title;
    public AudioSource BGM_prebattle;
    public AudioSource BGM_victory;
    public AudioSource BGM_gameover;

    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded; // subscribe
    }

    public void playPlayerAttack()
    {
        playerAttackSound[0].Play();
    }

    public void playMonsterAttack()
    {
        monsterAttackSound[0].Play();
    }

    public void playPlayerHurt()
    {
        playerHurtSound[0].Play();
    }

    public void playMonsterHurt()
    {
        monsterHurtSound[0].Play();
    }

    public void playSkillActivated()
    {
        SkillActivatedSound.Play();
    }

    public void playBattleVictory()
    {
        BattleVictorySound.Play();
    }

    public void playPlayerDodge()
    {
        playerDodgeSound.Play();
    }

    public void playMonsterAttackDodged()
    {
        monsterAttackDodgedSound.Play();
    }

    public void stopAllBGM()
    {
        BGM_battle.Stop();
        BGM_title.Stop();
        BGM_prebattle.Stop();
        BGM_victory.Stop();
        BGM_gameover.Stop();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);

        switch(scene.name)
        {
            case "Start":
                stopAllBGM();
                BGM_title.Play();
                break;
            case "Battle":
                stopAllBGM();
                BGM_battle.Play();
                break;
            case "BeforeBattle":
                stopAllBGM();
                BGM_prebattle.Play();
                break;
            case "Loot":
                stopAllBGM();
                BGM_victory.Play();
                break;
            case "GameOver":
                stopAllBGM();
                BGM_gameover.Play();
                break;
        }
    }

}
