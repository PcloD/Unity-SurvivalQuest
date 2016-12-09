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
    Slider sliderInstance;

    Animator anim;                                             
	ParticleSystem hitParticles;                
	CapsuleCollider capsuleCollider;
    SkinnedMeshRenderer myRenderer;
    bool isDead;                                
    public ParticleSystem deathParticles;                        


	void Awake ()
	{
		anim = GetComponent <Animator> ();
		hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
        myRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        enemyHealthbarManager = GameObject.Find("EnemyHealthbarsCanvas");
        waveManager = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        currentHealth = startingHealth;
	}

    void Start()
    {
        currentHealth = startingHealth;

        sliderInstance = Instantiate(healthBarSlider, gameObject.transform.position, Quaternion.identity) as Slider;
        sliderInstance.gameObject.transform.SetParent(enemyHealthbarManager.transform, false);
        sliderInstance.GetComponent<Healthbar>().enemy = gameObject;
        sliderInstance.gameObject.SetActive(false);
    }





    public void TakeDamage (int amount, Vector3 hitPoint)
	{

        if (isDead)
			return;

		currentHealth -= amount;

		hitParticles.transform.position = hitPoint;

		hitParticles.Play();

        if (currentHealth <= startingHealth)
        {
            sliderInstance.gameObject.SetActive(true);
        }
        int sliderValue = (int)Mathf.Round(((float)currentHealth / (float)startingHealth) * 100);
        sliderInstance.value = sliderValue;

        if (currentHealth <= 0)
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
        Destroy(sliderInstance.gameObject);

    }


	IEnumerator StartSinking ()
	{
        yield return new WaitForSeconds(2);
        deathParticles.Play();
		Destroy (gameObject, 2f);
	}
}