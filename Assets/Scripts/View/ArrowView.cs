using System;
using System.Collections;
using UnityEngine;



[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class ArrowView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveDuration = 0.2f;
    [SerializeField] private float blockedShakeAmount = 0.08f;
    [SerializeField] private float blockedShakeDuration = 0.12f;

    private int arrowId;
    private GameController gameController;
    private Coroutine activeAnimationCoroutine;

    public void Initialize(ArrowData arrowData, GameController controller, BoardView boardView)
    {
        arrowId = arrowData.Id;
        gameController = controller;

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        transform.rotation = Quaternion.Euler(0f, 0f, DirectionUtility.ToZRotation(arrowData.Direction));
    }

    private void OnMouseDown()
    {
        Debug.Log("Arrow clicked: " + arrowId);

        if (gameController == null)
        {
            return;
        }

        bool didStartRemoval = gameController.TryRemoveArrow(arrowId);

        if (!didStartRemoval)
        {
            PlayBlockedFeedback();
        }
    }

    public void PlayExitAnimation(Vector3 targetPosition, Action onComplete)
    {
        if (activeAnimationCoroutine != null)
        {
            StopCoroutine(activeAnimationCoroutine);
        }

        activeAnimationCoroutine = StartCoroutine(PlayExitAnimationCoroutine(targetPosition, onComplete));
    }

    private void PlayBlockedFeedback()
    {
        if (activeAnimationCoroutine != null)
        {
            StopCoroutine(activeAnimationCoroutine);
        }

        activeAnimationCoroutine = StartCoroutine(PlayBlockedFeedbackCoroutine());
    }

    private IEnumerator PlayExitAnimationCoroutine(Vector3 targetPosition, Action onComplete)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition;
        activeAnimationCoroutine = null;
        onComplete?.Invoke();
    }

    private IEnumerator PlayBlockedFeedbackCoroutine()
    {
        Vector3 originalPosition = transform.position;
        float halfDuration = blockedShakeDuration * 0.5f;

        transform.position = originalPosition + Vector3.right * blockedShakeAmount;
        yield return new WaitForSeconds(halfDuration);

        transform.position = originalPosition - Vector3.right * blockedShakeAmount;
        yield return new WaitForSeconds(halfDuration);

        transform.position = originalPosition;
        activeAnimationCoroutine = null;
    }

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
