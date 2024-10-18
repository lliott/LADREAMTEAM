using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Coins for Items Only")]
    [SerializeField] private float goldPercentIncrease = 50;
    public int objectPrice = 50;

    [Header("Skull prefab")]
    [SerializeField] private GameObject skull;

    // Anim
    private Animator animator;

    // Audio
    private AudioSource _audio;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("animator not found on " + gameObject.name);
        }

        if (TryGetComponent<AudioSource>(out AudioSource audio))
        {
            _audio = audio;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (CompareTag("Object"))
            {
                GoldenManagement.instance.IncreaseGolds(objectPrice * (int)goldPercentIncrease / 100);
                Destroy(gameObject);

            }
            else if (CompareTag("Lemming"))
            {
                LemmingController lemming = GetComponent<LemmingController>();
                if (lemming != null)
                {
                    bool isMovingRight = lemming.IsMovingRight;

                    if (animator != null)
                    {
                        animator.SetTrigger("transiSkull");
                        if (_audio != null)
                        {
                            _audio.Play();
                        }
                        StartCoroutine(DeactivateAfterAnimation(isMovingRight));
                    }
                }
                else
                {
                    Debug.LogError("LemmingController component not found on " + gameObject.name);
                }
            }
        }
    }

    private IEnumerator DeactivateAfterAnimation(bool isMovingRight)
    {
        float animationLength = GetAnimationClipLength(animator, "SkeletonToSkull");

        if (animationLength == 0f)
        {
            animationLength = 2f;
        }

        yield return new WaitForSeconds(animationLength);

        gameObject.SetActive(false);

        GameObject instantiatedSkull = Instantiate(skull, transform.position, transform.rotation);

        MoveSkull moveSkull = instantiatedSkull.GetComponent<MoveSkull>();

        if (moveSkull != null)
        {
            moveSkull.SetDirection(isMovingRight);
        }
        else
        {
            Debug.LogError("MoveSkull component not found on skull prefab.");
        }
    }

    private float GetAnimationClipLength(Animator animator, string clipName)
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
                return clip.length;
            }
        }

        Debug.LogWarning("Animation clip '" + clipName + "' not found.");
        return 0f;
    }
}
