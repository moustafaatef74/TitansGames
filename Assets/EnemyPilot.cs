using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPilot : MonoBehaviour
{
    public bool dead = false;
    public int health = 100;
    private bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        InvokeRepeating("fire", 0.0f, 3f);
        inRange = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControlModified>().inRange;


    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            dead = true;
        }
    }
    void fire()
    {
        print("pew");
    }
}
