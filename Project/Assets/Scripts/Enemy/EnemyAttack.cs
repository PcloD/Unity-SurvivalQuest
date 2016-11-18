using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

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
            inRange = true;
        }
	}

    void OnTriggerExit (Collider collide)
    {
        if (collide.gameObject == player)
        {
            inRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && inRange)
        {
            Attack();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
