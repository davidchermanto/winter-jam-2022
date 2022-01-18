using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorThemeManager : MonoBehaviour
{
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

    private float easyHueShift = 0f;
    private float easyHueRandomFactor = 0.1f;

    private float normalHueShift = 0.4f;
    private float normalHueRandomFactor = 0.2f;

    private float hardHueShift = -0.25f;
    private float hardHueRandomFactor = 0.05f;

    private void Setup()
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
                break;
            case "NORMAL":
                break;
            case "HARD":
                break;
            default:
                break;
        }
    }

    private ColorPack ShiftColorPack(ColorPack colorPack, float hueShift, float hueRandomFactor)
    {
        // TODO: Shift

        return colorPack;
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
