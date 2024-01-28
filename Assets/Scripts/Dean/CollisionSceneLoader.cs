using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSceneLoader : MonoBehaviour
{
    // Name of the scene to load
    public string sceneToLoad;

    // Method called when the GameObject collides with another GameObject
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player or any other GameObject you specify
        if (collision.gameObject.CompareTag("Player")) // You can adjust the tag as per your GameObject's tag
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
