using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    // The name of the scene to load
    public string sceneName;

    // This function is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
