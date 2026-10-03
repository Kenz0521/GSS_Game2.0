using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public Image damageImage;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public void UpdateHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    public void ShowDamage()
    {
        damageImage.color = flashColour;
    }

    void Update()
    {
        damageImage.color = Color.Lerp(
            damageImage.color,
            Color.clear,
            flashSpeed * Time.deltaTime
        );
    }
}
