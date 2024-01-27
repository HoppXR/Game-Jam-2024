using UnityEngine;
using System.Collections;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;
    public float grapplingSpeed = 10f; 

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    [Header("Max Height")]
    public float maxHeight = 10f;

    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();

        if (lr == null)
        {
            Debug.LogError("LineRenderer component is not assigned.");
            return;
        }

        lr.positionCount = 2; 
    }


    private void Update()
    {
        if (Input.GetKeyDown(grappleKey) && !grappling) 
            StartGrapple();

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;

        UpdateLineRenderer();
    }

    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        pm.freeze = true;

        grapplePoint = cam.position + cam.forward * maxGrappleDistance;
        Invoke(nameof(ExecuteGrapple), grappleDelayTime);
    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            StartCoroutine(MoveToGrapplePoint());
            Invoke(nameof(StopGrapple), 1f);
        }
        else
        {
            StopGrapple(); 
        }
    }

    private IEnumerator MoveToGrapplePoint()
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;

        while (elapsedTime < 1f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, grapplePoint - transform.position, out hit, grapplingSpeed * Time.deltaTime, whatIsGrappleable))
            {
                Vector3 newPos = hit.point + hit.normal * 0.1f; 
                transform.position = Vector3.Lerp(transform.position, newPos, elapsedTime);
            }
            else
            {
                transform.position = Vector3.Lerp(startPos, grapplePoint, elapsedTime);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    private void StopGrapple()
    {
        pm.freeze = false;
        grappling = false;
        grapplingCdTimer = grapplingCd;
    }

    private void UpdateLineRenderer()
    {
        if (lr != null)
        {
            if (lr.positionCount >= 2)
            {
                lr.SetPosition(0, gunTip.position); 
                lr.SetPosition(1, grapplePoint);    
            }
            else
            {
                Debug.LogError("LineRenderer position count is less than 2. Current position count: " + (lr.positionCount >= 0 ? lr.positionCount.ToString() : "N/A"));
            }
        }
        else
        {
            Debug.LogError("LineRenderer component is null. Make sure it is assigned in the Inspector.");
        }
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
