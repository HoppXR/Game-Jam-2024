using UnityEngine;

public class ObjectDisappearingScript : MonoBehaviour
{
    public float disappearanceTime = 50f; // Time in seconds before the object disappears

    private float timer = 0f;
    private bool isDisappearing = false;

    void Update()
    {
        // Increment the timer if the object is marked for disappearance
        if (isDisappearing)
        {
            timer += Time.deltaTime;

            // Check if the timer exceeds the disappearance time
            if (timer >= disappearanceTime)
            {
                // Object has stayed long enough, so it disappears
                gameObject.SetActive(false);
            }
        }
    }

    void OnEnable()
    {
        // Reset the timer and mark the object for disappearance when it's enabled
        timer = 0f;
        isDisappearing = true;
    }

    void OnDisable()
    {
        // Reset the state when the object is disabled
        isDisappearing = false;
    }
}
