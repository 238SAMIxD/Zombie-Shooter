using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour {
    [SerializeField] float damage;
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    public Spawn spawn;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        agent.destination = player.transform.position;
        if(agent.velocity.magnitude > 1.0f) {
            animator.Play("Run");
        } else {
            animator.Play("Idle");
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject == player) {
            player.GetComponent<Player>().Hit(damage);
        }
    }

    public void Hit() {
        Destroy(gameObject);
        spawn.enemies--;
    }
}
