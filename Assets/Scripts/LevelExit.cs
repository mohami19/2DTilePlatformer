using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    private void OnTriggerEnter2D(Collider2D other) {
        if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player"))) {
            StartCoroutine(LoadScene());
            //Invoke("LoadScene(1)",1f);
        }
    }

    IEnumerator LoadScene(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(loadDelay);
        int nextSceneIndex = currentScene + 1;
        if (nextSceneIndex < SceneManager.sceneCount) {
            SceneManager.LoadScene(nextSceneIndex);
        } else {
            SceneManager.LoadScene(0);
        }
    }
}
