using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public KeyCode attackKey = KeyCode.Mouse0;
    public float attackRate = 0.5f;
    public float punchDistance = 0.5f;
    public float knockbackForce = 5f;
    private float nextAttackTime = 0f;

    public int punchDamage = 10;

    public Transform leftHand;
    public Transform rightHand;
    public float punchAnimationDuration = 0.2f;
    private bool isPunching = false;

    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (!PauseMenu.GameIsPaused && Input.GetKeyDown(attackKey) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void Attack()
    {
        if (!isPunching)
        {
            Transform handToThrow = Random.Range(0, 2) == 0 ? leftHand : rightHand;
            StartCoroutine(PunchAnimation(handToThrow));

            if (playerHealth != null)
            {
                // playerHealth.TakeDamage(punchDamage);
            }
        }
    }



    private System.Collections.IEnumerator PunchAnimation(Transform hand)
    {
        isPunching = true;

        float elapsedTime = 0f;
        Vector3 initialHandPosition = hand.localPosition;

        while (elapsedTime < punchAnimationDuration)
        {
            float t = elapsedTime / punchAnimationDuration;

            hand.localPosition = Vector3.Lerp(initialHandPosition, initialHandPosition + Vector3.forward * punchDistance, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        hand.localPosition = initialHandPosition;

        isPunching = false;
    }
}