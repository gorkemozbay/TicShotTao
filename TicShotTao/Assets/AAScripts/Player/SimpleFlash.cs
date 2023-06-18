using System.Collections;

using UnityEngine;

public class SimpleFlash : MonoBehaviour
{   
    public Material flashMaterial;
    public float duration;

    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;

    // The material that was in use, when the script started.
    private Material originalMaterial;

    // The currently running coroutine.
    private Coroutine flashRoutine;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    public void FlashOnce() 
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashOnceRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < 4; i++)
        {
            // Swap to the flashMaterial.
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(duration/2);

            // After the pause, swap back to the original material.
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(duration/2);
        }

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }

    private IEnumerator FlashOnceRoutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

}