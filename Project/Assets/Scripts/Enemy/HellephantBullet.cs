using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HellephantBullet : MonoBehaviour {
	
	public float speed = 600.0f;
	public float life = 3;
	public ParticleSystem normalTrailParticles;
	public ParticleSystem ImpactParticles;
	public int damage = 20;
	public Color bulletColor;

	Vector3 velocity;
    Vector3 force;
	Vector3 newPos;
	Vector3 oldPos;
	Vector3 direction;
	bool hasHit = false;
	RaycastHit lastHit;
	float timer;

	void Awake() {
	}

	void Start() {
		newPos = transform.position;
		oldPos = newPos;

		normalTrailParticles.startColor = bulletColor;
		ImpactParticles.startColor = bulletColor;
		normalTrailParticles.gameObject.SetActive(true);
	}

	void Update() {
		if (hasHit) {
			return;
		}
			
		timer += Time.deltaTime;

		if (timer >= life) {
			Dissipate();
		}

        velocity = transform.forward;
		//velocity.y = 0;
		velocity = velocity.normalized * speed;

		newPos += velocity * Time.deltaTime;
	
		direction = newPos - oldPos;
		float distance = direction.magnitude;

		if (distance > 0) {
            RaycastHit[] hits = Physics.RaycastAll(oldPos, direction, distance);

		    for (int i = 0; i < hits.Length; i++) {
		        RaycastHit hit = hits[i];

				if (ShouldIgnoreHit(hit)) {
					continue;
				}
					
				OnHit(hit);

				lastHit = hit;

				if (hasHit) {
					newPos = hit.point;
					break;
				}
		    }
		}

		oldPos = transform.position;
		transform.position = newPos;
	}

	bool ShouldIgnoreHit (RaycastHit hit) {
		if (lastHit.point == hit.point || lastHit.collider == hit.collider || hit.collider.tag == "Enemy")
			return true;
		
		return false;
	}


	void OnHit(RaycastHit hit) {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        if (hit.transform.tag == "Environment") {
			newPos = hit.point;
			ImpactParticles.transform.position = hit.point;
			ImpactParticles.transform.rotation = rotation;
			ImpactParticles.Play();
			hasHit = true;
			DelayedDestroy();
        }

        if (hit.transform.tag == "Player") {
			ImpactParticles.transform.position = hit.point;
			ImpactParticles.transform.rotation = rotation;
			ImpactParticles.Play();

			PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();

			if (playerHealth != null) {
				playerHealth.TakeDamage(damage);
			}
    		hasHit = true;
			DelayedDestroy();
        }
	}
		
	void Dissipate() {
		normalTrailParticles.enableEmission = false;
		normalTrailParticles.transform.parent = null;
		Destroy(normalTrailParticles.gameObject, normalTrailParticles.duration);
		Destroy(gameObject);
	}

	void DelayedDestroy() {
		normalTrailParticles.gameObject.SetActive(false);
		Destroy(gameObject, 2);
	}
}