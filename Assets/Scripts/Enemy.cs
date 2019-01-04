using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {
    NavMeshAgent navAgent;
    Transform player;
    Animator anim;
    Health health;
    float distance; //Distance between enemy and player
    public float approachDistance = 6; //Slowly advancing
    public float attackDistance = 3;
    bool approachingPlayer = false;
    bool playerReached = false;
    bool dead = false;
    public float deadActiveTime = 20f;

    //SoundFx
    AudioSource sound;
    public AudioClip walkingSFX;
    public AudioClip approachingSFX;
    public AudioSource secondAudio;
    public AudioClip hitSFX;
    public AudioClip deathSFX;
    public float hitVolumeReduc = 0.5f;

	// Use this for initialization
	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = Camera.main.transform;
        health = GetComponent<Health>();
        sound = GetComponent<AudioSource>();
	}

    void followPlayer()
    {
        navAgent.SetDestination(player.position); //Follow player
        distance = Vector3.Distance(player.position, transform.position);

        if (distance < approachDistance) //In approaching range
        {
            approachingPlayer = true;

            if (distance < attackDistance)
            {
                playerReached = true;
            }
            else
            {
                playerReached = false;
            }
        }
        else //Outside attacking range
        {
            approachingPlayer = false;
            playerReached = false;
        }
    }

    //Controls animations and sounds
    void controlAnimations()
    {
        if (approachingPlayer)
        {
            anim.SetBool("approaching", true);

            if (playerReached) //In attacking range
            {
                anim.SetBool("attack", true);

                if (anim.GetBool("hit")) //Stop movement if the enemy was hit
                {
                    navAgent.isStopped = true;
                }
                else
                {
                    navAgent.isStopped = false;
                }
            }
            else //Outside attacking range (approaching)
            {
                navAgent.isStopped = false;
                anim.SetBool("attack", false);
                sound.clip = approachingSFX;
                sound.loop = true;
                sound.volume = 1;
                if (!sound.isPlaying)
                {
                    sound.Play();
                }
            }
        }
        else
        {
            navAgent.isStopped = false;
            anim.SetBool("approaching", false);
            sound.clip = walkingSFX;
            sound.loop = true;
            sound.volume = 1;
            if (!sound.isPlaying)
            {
                sound.Play();
            }
        }
    }

    //Checks if the enemy actually hit the player or if it was just the sword passing through the collider
    public bool checkHit()
    {
        if (!dead && anim.GetBool("attack") && !anim.GetBool("hit")) //Enemy is attacking
        {
            return true;
        }
        else //Enemy is not attacking
        {
            return false;
        }
    }

    public void hitByPlayer()
    {
        sound.clip = hitSFX;
        sound.loop = false;
        sound.enabled = enabled;
        sound.volume = hitVolumeReduc;
        sound.Play();
        if (!anim.GetBool("hit") && !dead) //Not playing hit animation and enemy not dead
        {
            if (health.decrease()) //Enemy defeated
            {
                anim.SetTrigger("die");
                anim.SetBool("approaching", false);
                anim.SetBool("attack", false);
                anim.SetBool("hit", false);
                navAgent.isStopped = true;
                navAgent.enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                dead = true;
                GameManager.defeatedEnemy();
                secondAudio.clip = deathSFX;
                secondAudio.loop = false;
                secondAudio.Play();
                StartCoroutine(destroyEnemy(deadActiveTime));
            }
            else
            {
                anim.SetBool("hit", true);
            }
        }
    }

    IEnumerator destroyEnemy(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); //Get rid of dead enemy
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.gameState == GameManager.GAMESTATE.PLAYING && !dead) //Game in play and Enemy not dead
        {
            followPlayer();
            controlAnimations();
        }
        else if (GameManager.gameState == GameManager.GAMESTATE.GAMEOVER) //Gameover, destroy the Enemy
        {
            StartCoroutine(destroyEnemy(1));
        }
    }
}
