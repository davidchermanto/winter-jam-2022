using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorThemeManager : MonoBehaviour
{
    // Yes
    public static ColorThemeManager Instance;

    // Anologous Color palette formula, but very subtle
    // For the purposes of this I'll generate an 2 antagonist (?) extra colors
    [Header("Helper Variables")]
    private Color mainBright = new Color(0.9019f, 0.8274f, 0.9294f);
    private Color secondBright = new Color(0.6627f, 0.498f, 0.705f);
    private Color mainDark = new Color(0.4235f, 0.27f, 0.4941f);
    private Color secondDark = new Color(0.5764f, 0.4f, 0.639f);
    private Color antaDark = new Color(0.317f, 0.3f, 0.568f);
    private Color antaLight = new Color(0.572f, 1f, 1f);

    private ColorPack vanillaColorPack;

    [Header("Settings Variables")]
    private const float neutralHue = 0f;
    private const float neutralHueRandomFactor = 0.5f;

    private const float easyHueShift = 0f;
    private const float easyHueRandomFactor = 0.15f;

    private const float normalHueShift = 0.4f;
    private const float normalHueRandomFactor = 0.15f;

    private const float hardHueShift = -0.3f;
    private const float hardHueRandomFactor = 0.15f;

    [Header("Dynamic Variables")]
    private ColorPack ingameColorPack;
    private ColorPack previousColorPack;

    private void Awake()
    {
        Instance = this;
    }

    public void Setup()
    {
        vanillaColorPack = new ColorPack(mainBright, secondBright, mainDark, secondDark, antaDark, antaLight);

        // Randomizes initial colors
        ingameColorPack = ShiftColorPack(vanillaColorPack, neutralHue, neutralHueRandomFactor);

        previousColorPack = ingameColorPack;
    }

    /// <summary>
    /// Generates a set of randomized colors for different difficulties.
    /// Easy : Bright colors, maybe purple like in the shown image
    /// Medium : Orange yellowish to red, medium is commonly orange
    /// Hard : dark blue to teal, add rain particles
    /// </summary>
    public void GenerateColorForDifficulty(Difficulty difficulty)
    {
        previousColorPack = ingameColorPack;

        switch (difficulty.name)
        {
            case "EASY":
                TweenToNewColorPack(ShiftColorPack(vanillaColorPack, easyHueShift, easyHueRandomFactor), Constants.colorChangeDuration);
                break;
            case "NORMAL":
                TweenToNewColorPack(ShiftColorPack(vanillaColorPack, normalHueShift, normalHueRandomFactor), Constants.colorChangeDuration);
                break;
            case "HARD":
                TweenToNewColorPack(ShiftColorPack(vanillaColorPack, hardHueShift, hardHueRandomFactor), Constants.colorChangeDuration);
                break;
            case "menu":
                TweenToNewColorPack(ShiftColorPack(vanillaColorPack, neutralHue, neutralHueRandomFactor), Constants.colorChangeDuration);
                break;
            default:
                Debug.LogError("Tried to access unknown difficulty : "+difficulty.name);
                ingameColorPack = vanillaColorPack;
                break;
        }
    }

    public void TweenToNewColorPack(ColorPack colorPack, float duration)
    {
        StartCoroutine(ColorLerp(colorPack, duration));
    }

    private IEnumerator ColorLerp(ColorPack colorPack, float duration)
    {
        float timer = 0;

        while(timer < 1)
        {
            timer += Time.deltaTime / duration;

            ingameColorPack.brightOne = Color.Lerp(previousColorPack.brightOne, colorPack.brightOne, timer);
            ingameColorPack.brightTwo = Color.Lerp(previousColorPack.brightTwo, colorPack.brightTwo, timer);
            ingameColorPack.darkOne = Color.Lerp(previousColorPack.darkOne, colorPack.darkOne, timer);
            ingameColorPack.darkTwo = Color.Lerp(previousColorPack.darkTwo, colorPack.darkTwo, timer);
            ingameColorPack.antaOne = Color.Lerp(previousColorPack.antaOne, colorPack.antaOne, timer);
            ingameColorPack.antaTwo = Color.Lerp(previousColorPack.antaOne, colorPack.antaTwo, timer);

            yield return new WaitForEndOfFrame();
        }
    }

    public ColorPack GetColorPackVanilla()
    {
        return vanillaColorPack;
    }

    public ColorPack GetColorPack()
    {
        return ingameColorPack;
    }

    private ColorPack ShiftColorPack(ColorPack colorPack, float hueShift, float hueRandomFactor)
    {
        float hueRandomConstant = hueShift + Random.Range(0, hueRandomFactor);

        colorPack.brightOne = ShiftColor(colorPack.brightOne, hueRandomConstant);
        colorPack.brightTwo = ShiftColor(colorPack.brightTwo, hueRandomConstant);
        colorPack.darkOne = ShiftColor(colorPack.darkOne, hueRandomConstant);
        colorPack.darkTwo = ShiftColor(colorPack.darkTwo, hueRandomConstant);
        colorPack.antaOne = ShiftColor(colorPack.antaOne, hueRandomConstant);
        colorPack.antaTwo = ShiftColor(colorPack.antaTwo, hueRandomConstant);

        return colorPack;
    }

    private Color ShiftColor(Color color, float hueShift)
    {
        HSVColor newColor = RGBToHSV(color);

        newColor.hue += hueShift;

        if(newColor.hue > 1)
        {
            newColor.hue -= 1;
        }
        else if(newColor.hue < 0)
        {
            newColor.hue += 1;
        }

        Color finalColor = HSVToRGB(newColor);

        return finalColor;
    }

    private HSVColor RGBToHSV(Color color)
    {
        HSVColor newColor = new HSVColor();

        Color.RGBToHSV(color, out newColor.hue, out newColor.saturation, out newColor.value);

        return newColor;
    }

    private Color HSVToRGB(HSVColor color)
    {
        return Color.HSVToRGB(color.hue, color.saturation, color.value);
    }
}
