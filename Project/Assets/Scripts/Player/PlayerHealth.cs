using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;
    public int currentHealth;
    public Slider healthSlider;

    Animator animator;
    PlayerMovement playerMovement;
	PlayerShooting playerShooting;  
    bool isDead;
    bool damaged;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = health;
    }

    void Update()
    {
        if (damaged)
        {
            //var fill = (healthSlider as UnityEngine.UI.Slider).GetComponentsInChildren<UnityEngine.UI.Image>();
            //fill.color = Color.Lerp(Color.red, Color.green, 0.5);

        }
    }

    public void TakeDamage(int damage)
    {
        damaged = true;
        currentHealth -= damage;
        healthSlider.value = currentHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;
		playerShooting.DisableEffects ();
        animator.SetTrigger("Die");
        playerMovement.enabled = false;
    }


}
