using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public int startingHealth = 100;            
	public int currentHealth;                   
	public float sinkSpeed = 2.5f;             
	public int scoreValue = 10;
    private ScoreManager scoreManager;
    private WaveManager waveManager;
    public Slider healthBarSlider;
    GameObject enemyHealthbarManager;


    Animator anim;                                             
	ParticleSystem hitParticles;                
	CapsuleCollider capsuleCollider;            
	bool isDead;                                
	bool isSinking;
    public ParticleSystem deathParticles;                        


	void Awake ()
	{
		anim = GetComponent <Animator> ();
		hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
        enemyHealthbarManager = GameObject.Find("EnemyHealthbarsCanvas");
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        currentHealth = startingHealth;
	}

	void Update ()
	{
		if(isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
	}


	public void TakeDamage (int amount, Vector3 hitPoint)
	{
		if(isDead)
			return;

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;

		hitParticles.Play();

		if(currentHealth <= 0)
		{
			Death ();
		}
	}


	void Death ()
	{
		isDead = true;

		capsuleCollider.isTrigger = true;
		anim.SetTrigger ("Dead");
        if (GetComponent<NavMeshAgent>())
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        GetComponent<Rigidbody>().isKinematic = true;
        scoreManager.AddScore(scoreValue);
        waveManager.enemiesAlive--;
        capsuleCollider.isTrigger = true;
        StartCoroutine(StartSinking());

    }


	IEnumerator StartSinking ()
	{
        yield return new WaitForSeconds(2);
        deathParticles.Play();
		Destroy (gameObject, 2f);
	}
}