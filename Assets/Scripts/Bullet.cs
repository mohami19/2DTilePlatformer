using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float shotSpeed = 10f;
    PlayerMovement player;
    float xSpeed;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * shotSpeed;
    }

    void Update() {
        rb.velocity = new Vector2(xSpeed,0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger");
        if (other.tag == "Enemies") {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
