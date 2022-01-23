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
    private const float easyHueShift = 0f;
    private const float easyHueRandomFactor = 0.1f;

    private const float normalHueShift = 0.25f;
    private const float normalHueRandomFactor = 0.2f;

    private const float hardHueShift = -0.25f;
    private const float hardHueRandomFactor = 0.05f;

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


    }

    /// <summary>
    /// Generates a set of randomized colors for different difficulties.
    /// Easy : Bright colors, maybe purple like in the shown image
    /// Medium : Orange yellowish to red, medium is commonly orange
    /// Hard : dark blue to teal, add rain particles
    /// </summary>
    public void GenerateColorForDifficulty(Difficulty difficulty)
    {
        switch (difficulty.name)
        {
            case "EASY":
                ingameColorPack = ShiftColorPack(vanillaColorPack, easyHueShift, easyHueRandomFactor);
                break;
            case "NORMAL":
                ingameColorPack = ShiftColorPack(vanillaColorPack, normalHueShift, normalHueRandomFactor);
                break;
            case "HARD":
                ingameColorPack = ShiftColorPack(vanillaColorPack, hardHueShift, hardHueRandomFactor);
                break;
            default:
                Debug.LogError("Tried to access unknown difficulty : "+difficulty.name);
                ingameColorPack = vanillaColorPack;
                break;
        }
    }

    public void TweenToNewColorPack(ColorPack colorPack, float duration)
    {

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
