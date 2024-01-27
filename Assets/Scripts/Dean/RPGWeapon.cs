using UnityEngine;

public class RPGWeapon : MonoBehaviour
{
    public GameObject thunderboltPrefab;
    public Transform firePoint;
    public GameObject rocketHead;
    public float shootForce = 10f;
    public float cooldown = 1f;

    private float cooldownTimer = 0f;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0f)
        {
            ShootThunderbolt();
            cooldownTimer = cooldown;
        }

        if (cooldownTimer <= 0f && rocketHead != null && !rocketHead.activeSelf)
        {
            rocketHead.SetActive(true);
        }
    }

    void ShootThunderbolt()
    {
        if (rocketHead != null)
        {
            rocketHead.SetActive(false);
        }

        GameObject thunderbolt = Instantiate(thunderboltPrefab, firePoint.position, Quaternion.identity);

        Rigidbody rb = thunderbolt.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(firePoint.forward * shootForce, ForceMode.Impulse);
        }
    }
}
