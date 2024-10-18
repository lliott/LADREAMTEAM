using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHelper : MonoBehaviour
{
    public Animator animator;

    public IEnumerator StartAnimAfterDelay(string clipName)
    {
        Animator animator = GetComponent<Animator>();

        yield return new WaitForSeconds(FindClipLength(clipName));

        animator.SetBool(clipName, true);
    }

    public float FindClipLength(string clipName)
    {
        if (animator == null || animator.runtimeAnimatorController == null)
        {
            return 0f;
        }

        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == clipName)
            {
                float clipLength = clip.length;
                return clipLength;
            }
        }

        return 0f;
    }

    public IEnumerator StartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
