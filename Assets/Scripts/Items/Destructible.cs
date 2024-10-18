using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [Header("Coins for Items Only")]
    [SerializeField] private float goldPercentIncrease = 50;
    public int objectPrice = 50 ;

    [Header("Skull prefab")]
    [SerializeField] private GameObject skull;

    //anim
    private Animator animator;

    //Audio
    private AudioSource _audio;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }

        if (TryGetComponent<AudioSource>(out AudioSource audio)){
            _audio = audio;
        }
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // clic droit
        {
            if (CompareTag("Object"))
            {
                GoldenManagement.instance.IncreaseGolds(objectPrice*(int)goldPercentIncrease/100); //nb gold à voir 
                Destroy(gameObject);

            }
            else if (CompareTag("Lemming"))
            {
                if (animator != null)
                {
                    animator.SetTrigger("transiSkull");
                    if(_audio!=null){
                        _audio.Play();
                    }
                    StartCoroutine(DeactivateAfterAnimation());
                }
                else
                {
                    Debug.LogError("Animator component not found on " + gameObject.name);
                }
            }
        }
    }

    private IEnumerator DeactivateAfterAnimation()
    {
        float animationLength = GetAnimationClipLength(animator, "SkeletonToSkull");

        if (animationLength == 0f)
        {
            animationLength = 2f;
        }

        yield return new WaitForSeconds(animationLength);

        gameObject.SetActive(false);

        Instantiate(skull, transform.position, transform.rotation);
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
