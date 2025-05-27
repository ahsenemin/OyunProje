using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float minStartDelay = 1f;
    public float maxStartDelay = 5f;
    public float shakeDuration = 2f;
    public float shakeAmount = 5f;
    public float fallTime = 2f;
    public float resetDelay = 3f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;
    private bool isResetting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        originalPosition = transform.position;
        originalRotation = transform.rotation;

        float randomDelay = Random.Range(minStartDelay, maxStartDelay);
        StartCoroutine(ShakeAndFallSequence(randomDelay));
    }

    IEnumerator ShakeAndFallSequence(float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(ShakePlatform());

        rb.isKinematic = false;
        rb.useGravity = true;

        yield return new WaitForSeconds(fallTime);

        StartCoroutine(ResetPlatformAfterDelay(resetDelay));
    }

    IEnumerator ShakePlatform()
    {
        float elapsed = 0f;
        Quaternion startRot = transform.rotation;

        while (elapsed < shakeDuration)
        {
            float z = Mathf.Sin(Time.time * 40f) * shakeAmount;
            transform.rotation = startRot * Quaternion.Euler(0f, 0f, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = startRot;
    }

    IEnumerator ResetPlatformAfterDelay(float delay)
    {
        if (isResetting) yield break;
        isResetting = true;

        yield return new WaitForSeconds(delay);

        // Durdur
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        // Geri dön (istersen Lerp ile yumuşatabilirim)
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        yield return new WaitForSeconds(1f); // biraz bekle sonra tekrar çalıştır

        isResetting = false;

        // Tekrar baştan başlasın
        float randomDelay = Random.Range(minStartDelay, maxStartDelay);
        StartCoroutine(ShakeAndFallSequence(randomDelay));
    }


}
