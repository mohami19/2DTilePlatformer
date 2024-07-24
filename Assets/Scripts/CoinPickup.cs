using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {    
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);   
            Destroy(gameObject);
        }
    }
}
