using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource[] playerAttackSound;
    public AudioSource[] monsterAttackSound;
    public AudioSource[] playerHurtSound;
    public AudioSource[] monsterHurtSound;
    public AudioSource SkillActivatedSound;
    public AudioSource BattleVictorySound;
    public AudioSource playerDodgeSound;
    public AudioSource monsterAttackDodgedSound;

    void Awake()
    {
        DontDestroyOnLoad(this);
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
}
