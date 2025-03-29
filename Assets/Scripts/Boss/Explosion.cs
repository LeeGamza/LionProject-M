using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject[] explosions;
    public Animator ExplosionAnimator;

    float lifetime;
    void Start()
    {
        lifetime = GetAnimationLength(ExplosionAnimator, "Explosion");
        StartCoroutine(ShowExplosion());
        

    }

    IEnumerator ShowExplosion()
    {
        foreach (var exlposion in explosions)
        {
            exlposion.SetActive(true);
            Destroy(exlposion, lifetime);
            yield return new WaitForSeconds(0.5f);
        }
    }

    float GetAnimationLength(Animator animator, string animationName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0;
    }
}
