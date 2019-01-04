using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
    public GameObject wielder; //Who is wielding the sword
    Player player;
    Enemy enemy;
    public bool isPlayer; //Determines if sword is player sword or enemy sword
    string opponent;
    InputManager inputManager;

    //SoundFx
    AudioSource sound;
    public AudioClip swordClash;

    // Use this for initialization
    void Start () {
		if (isPlayer)
        {
            opponent = "Enemy";
            player = wielder.GetComponent<Player>();
            sound = GetComponent<AudioSource>();
        }
        else
        {
            opponent = "Player";
            enemy = wielder.GetComponent<Enemy>();
        }

        inputManager = FindObjectOfType<InputManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(opponent))
        {
            if (isPlayer) //Opponent is enemy
            {
                hitEnemy(other.GetComponent<EnemyCollider>().enemy);
            }
            else //Opponent is player
            {
                hitPlayer(other.GetComponent<Player>());
            }
        }
        else if (other.CompareTag("Sword"))
        {
            if (isPlayer) //Player sword hit another sword
            {
                inputManager.triggerHapticFeedBack(false, 0, true, 4000);

                sound.clip = swordClash;
                sound.Play();
            }
        }
    }

    void hitEnemy(Enemy enemy)
    {
        inputManager.triggerHapticFeedBack(false, 0, true, 4000);
        enemy.hitByPlayer();
    }

    void hitPlayer(Player player)
    {
        if (enemy.checkHit()) //Enemy was attacking
        {
            player.hitByEnemy();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
