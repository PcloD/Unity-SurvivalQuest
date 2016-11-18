using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float timeBetweenAttacks = 0.4f;
    public int attackDamage = 10;
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    bool inRange;
    float timer;

	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collide) {
	    if (collide.gameObject == player)
        {

        }
	}
}
