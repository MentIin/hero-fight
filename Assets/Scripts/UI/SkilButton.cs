using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkilButton : MonoBehaviour
{
    public HeroMovement myHero;
    public Image darkBackground;
    public Sprite activeImage;
    public Sprite disactiveImage;
    public float cooldown = 3f;
    private float cooldownTick;
    private Image image;
    private Button button;
    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }
    void Start()
    {
        cooldown = myHero.skillCountdown;
        cooldownTick = cooldown;
        darkBackground.enabled = false;
    }

    public void PointerDown()
    {
        if (cooldownTick < cooldown || !myHero.canMove) return;
        cooldownTick = 0f;
        button.enabled = false;
        darkBackground.enabled = true;
        image.sprite = disactiveImage;
    }
    void Update()
    {
        if (cooldownTick < cooldown)
        {
            cooldownTick = cooldown - myHero.skillCountdownTick;
            image.fillAmount = cooldownTick / cooldown;
        }
        else if (!button.enabled)
        {
            button.enabled = true;
            darkBackground.enabled = false;
            image.sprite = activeImage;
        };
    }

}
