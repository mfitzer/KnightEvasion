using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int maxHitPoints = 3;
    public float hitPoints; //current health

	// Use this for initialization
	void Start () {
        hitPoints = maxHitPoints;
	}

    //Decreases health and returns a bool indicating if health is 0 (dead)
    public bool decrease()
    {
        hitPoints -= 1;
        return hitPoints <= 0; //Determines if dead or not
    }

    public void resetHealth()
    {
        hitPoints = maxHitPoints;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
