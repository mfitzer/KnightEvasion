using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIndicator : MonoBehaviour {
    public List<GameObject> healthIndicators;
    public Material life;
    public Material noLife;
    int lives = 3;

	// Use this for initialization
	void Start () {
		
	}

    public void resetHealth()
    {
        for (int i = 0; i < healthIndicators.Count; i++)
        {
            healthIndicators[i].GetComponent<Renderer>().material = life;
        }
        lives = 3;
    }

    public void loseLife()
    {
        if (lives > 0)
        {
            lives--;
            healthIndicators[lives].GetComponent<Renderer>().material = noLife;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
