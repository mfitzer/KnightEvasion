using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Transform cameraRig;
    Vector3 cameraRigStartPos;
    Transform player;
    Health health;
    UIManager uiManager;

    public GameObject bloodScreen;

    //SoundFx
    AudioSource sound;
    public AudioClip hit;
    public AudioSource heartbeat;

	// Use this for initialization
	void Start () {
        player = Camera.main.transform;
        health = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
        cameraRigStartPos = cameraRig.position;
        sound = GetComponent<AudioSource>();
        bloodScreen.SetActive(false);
    }

    public void hitByEnemy()
    {
        if (health.decrease()) //Player is dead (GAME OVER)
        {
            StartCoroutine(playerDead());
        }
        else
        {
            uiManager.healthIndicator.loseLife();
            heartbeat.Play();
        }
        sound.clip = hit;
        sound.Play();
    }

    IEnumerator playerDead()
    {
        uiManager.gameOver();
        yield return new WaitForSeconds(2);
        cameraRig.position = cameraRigStartPos;
        health.resetHealth();
        heartbeat.Stop();
        bloodScreen.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = player.position;

        //Track survival time
        if (GameManager.gameState == GameManager.GAMESTATE.PLAYING)
        {
            GameManager.addTime(Time.deltaTime);
        }

        if (heartbeat.isPlaying)
        {
            bloodScreen.SetActive(true);
        }
        else
        {
            bloodScreen.SetActive(false);
        }
	}
}
