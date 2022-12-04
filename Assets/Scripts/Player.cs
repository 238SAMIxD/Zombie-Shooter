using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float health = 100f;
    GameObject blaster;
    [SerializeField] GameObject camera;
    float range = 100f;
    public int score = 0;
    [SerializeField] Spawn spawn;

    void Start() {
        blaster = GameObject.FindGameObjectWithTag("Blaster");
    }

    void Update() {

        if (blaster.GetComponent<Animator>().GetBool("isShooting")) {
            blaster.GetComponent<Animator>().SetBool("isShooting", false);
        }

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot() {
        RaycastHit hit;

        blaster.GetComponent<Animator>().SetBool("isShooting", true);

        if(Physics.Raycast(camera.transform.position, transform.forward, out hit, range)) {
            Zombie zombie = hit.transform.GetComponent<Zombie>();
            if(zombie != null) {
                zombie.Hit();
                score += 10 * spawn.round;
            }
        }
    }

    public void Hit(float damage) {
        health -= damage;
        if (health <= 0) {
            health = 0;
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            for(int i = 0; i < zombies.Length; i++) {
                Destroy(zombies[i]);
            }
            spawn.EndGame();
        }
    }
}
