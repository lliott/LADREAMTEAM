using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lemming"))
        {
            Animator lemmingAnimator = collision.GetComponent<Animator>();
            LemmingController lemmingController = collision.GetComponent<LemmingController>();

            collision.gameObject.SetActive(false);
        }
    }

    private IEnumerator DeactivateAfterAnimation(Animator lemmingAnimator, GameObject lemming)
    {
        float animationLength = GetAnimationClipLength(lemmingAnimator, "SkeletonDeath");

        if (animationLength == 0f)
        {
            animationLength = 2f;
        }

        yield return new WaitForSeconds(animationLength);

        lemming.SetActive(false);
    }

    private float GetAnimationClipLength(Animator animator, string clipName)
    {
        if (animator == null || animator.runtimeAnimatorController == null)
        {
            Debug.LogError("Animator or Animator Controller is not assigned.");
            return 0f;
        }

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning("Animation clip '" + clipName + "' not found.");
        return 0f;
    }
}
