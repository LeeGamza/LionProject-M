using DG.Tweening;
using UnityEngine;

public class HitParticles
{
    public HitParticles()
    {
        EventManager.Instance.OnHitEffect += HandleHitEffect;
    }

    private void HandleHitEffect(SpriteRenderer sr, Color hitColor)
    {
        sr.DOKill();

        Sequence flash = DOTween.Sequence();
        flash.Append(sr.DOColor(hitColor, 0.05f));
        flash.Append(sr.DOColor(Color.white, 0.05f));
    }
    
}
