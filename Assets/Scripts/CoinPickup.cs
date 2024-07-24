using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int PointsForCoinPickup = 100;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {    
            AudioSource.PlayClipAtPoint(coinPickupSFX,Camera.main.transform.position);   
            FindObjectOfType<GameSession>().AddToScore(PointsForCoinPickup);
            Destroy(gameObject);
        }
    }
}
