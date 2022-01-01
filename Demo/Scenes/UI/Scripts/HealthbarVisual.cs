using UnityEngine.UI;
using UnityEngine;

public class HealthbarVisual : MonoBehaviour
{
    public Slider healthbar, damage;
    public float healthValueSpeed, displayValueSpeed;
    public AnimationCurve healthCurveDamage, damageCurveDamage, healthCurveHeal, damageCurveHeal;

    private float health = 100f;

    public void Damage()
    {
        health -= 20f;
        health = Mathf.Clamp(health, 0f, 100f);

        mTween.CancelTween(this);
        mTween.NewTween(this, healthValueSpeed).SliderValueTo(healthbar, health, healthCurveDamage);
        mTween.NewTween(this, displayValueSpeed).SliderValueTo(damage, health, damageCurveDamage);
    }
    public void Heal()
    {
        health += 20f;
        health = Mathf.Clamp(health, 0f, 100f);

        if (health < 0f)
            health = 0f;

        mTween.CancelTween(this);
        mTween.NewTween(this, displayValueSpeed).SliderValueTo(healthbar, health, healthCurveHeal);
        mTween.NewTween(this, healthValueSpeed).SliderValueTo(damage, health, damageCurveHeal);
    }
}
