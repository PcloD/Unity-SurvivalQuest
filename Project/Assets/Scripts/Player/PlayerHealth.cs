using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public Text healthText;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponentInChildren<PlayerMovement>();
		playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = health;
        healthText.text = health.ToString();
    }

    void Update()
    {
        if (damaged && currentHealth <= health/3)
        {
            //var fill = (healthSlider as UnityEngine.UI.Slider).GetComponentsInChildren<UnityEngine.UI.Image>().FirstOrDefault(t => t.name == "Fill");
            //fill.color = Color.Lerp(Color.red, fill.color, 0.5f);

        }
    }

    public void TakeDamage(int damage)
    {
        damaged = true;
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth.ToString();


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
