using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptUtils
{

    public static Color GetAverageColor(List<Color> colors)
    {
        if (colors == null || colors.Count == 0)
        {
            return Color.clear; // Default to clear if no colors are provided
        }

        float r = 0f, g = 0f, b = 0f, a = 0f;

        foreach (Color color in colors)
        {
            r += color.r;
            g += color.g;
            b += color.b;
            a += color.a;
        }

        int count = colors.Count;
        return new Color(r / count, g / count, b / count, a / count);
    }

    public static Color GetRandomShiftedColor(Color baseColor, float shiftValue)
    {
        float shiftedR = UnityEngine.Random.Range(baseColor.r - shiftValue, baseColor.r + shiftValue);
        float shiftedG = UnityEngine.Random.Range(baseColor.g - shiftValue, baseColor.g + shiftValue);
        float shiftedB = UnityEngine.Random.Range(baseColor.b - shiftValue, baseColor.b + shiftValue);

        return new Color(shiftedR, shiftedG, shiftedB, baseColor.a);
    }

    public static Color GetColorButNoAlpha(Color color)
    {
        return new Color (color.r, color.g, color.b, 0f);
    }

    public static ParticleSystem.MinMaxGradient GetColorButNoAlpha(ParticleSystem.MinMaxGradient color)
    {
        Color actualColor = color.color;

        return new ParticleSystem.MinMaxGradient(new Color (actualColor.r, actualColor.g, actualColor.b, 0f));
    }

    public static Color GetColorButFullAlpha(Color color)
    {
        return new Color (color.r, color.g, color.b, 1f);
    }

    public static ParticleSystem.MinMaxGradient GetColorButFullAlpha(ParticleSystem.MinMaxGradient color)
    {
        Color actualColor = color.color;

        return new ParticleSystem.MinMaxGradient(new Color (actualColor.r, actualColor.g, actualColor.b, 1f));
    }

    public static int GetNumberFromString(string chars)
    {
        int n = 0;
        foreach (char c in chars)
        {
            if (c != ' ') // ignore empty characters
            {
                n += c; // Add ASCII value
            }
        }
        return n;
    }

    public static Color GetRandomColorFromSeed()
    {
        //old implementation
        // byte r = (Byte) UnityEngine.Random.Range(50, 255); // Make sure Colours don't get too dark to see
        // byte g = (Byte) UnityEngine.Random.Range(50, 255);
        // byte b = (Byte) UnityEngine.Random.Range(50, 255);

        //tweak s and v for different vibes
        var h = UnityEngine.Random.Range(0f, 1f);
        var s = UnityEngine.Random.Range(0.5f,1f);
        var v = UnityEngine.Random.Range(0.6f,1f);

        return Color.HSVToRGB (h, s, v);
    }

    public static Color GetComplimentaryColor(Color baseColor)
    {
        float r = 1f - baseColor.r + 0.3f; // Invert the red channel & make it looks slightly brighter for prettiness
        float g = 1f - baseColor.g + 0.3f; // Invert the green channel & make it looks slightly brighter for prettiness
        float b = 1f - baseColor.b + 0.3f; // Invert the blue channel & make it looks slightly brighter for prettiness

        return new Color(r, g, b, baseColor.a); // Preserve the alpha channel
    }

    public static (Color, Color) GetSplitComplementary(Color baseColor)
    {
        // Convert the base color from RGB to HSV for easier getting of values
        Color.RGBToHSV(baseColor, out float h, out float s, out float v);
        
        // 180° in normalized space is 0.5, 30° is ~0.0833.
        float splitOffset = 30f / 360f; // ≈ 0.0833
        float compHue = (h + 0.5f) % 1f; // Complementary hue.
        
        float splitHue1 = (compHue - splitOffset + 1f) % 1f; // Left split complement.
        float splitHue2 = (compHue + splitOffset) % 1f;       // Right split complement.
        
        // Convert back to RGB.
        Color splitComp1 = Color.HSVToRGB(splitHue1, s - 0.4f, 1);
        Color splitComp2 = Color.HSVToRGB(splitHue2, s, 1);
        
        return (splitComp1, splitComp2);
    }

    public static void PlaySound(AudioSource audioSource, AudioClip sound)
    {

        if (audioSource == null)
        {
            audioSource = GameObject.Find("Game Tracker").GetComponent<AudioSource>();
        }

        audioSource.resource = sound;
        audioSource.Play();
    }

    public static IEnumerator AudioSourcePitchLerp(AudioSource audioSource, float pitch, float duration)
    {
        float timeElapsed = 0;

        float initialPitch = audioSource.pitch;

        while (timeElapsed < duration) 
        {
            if (audioSource != null) // I like to destroy
            {
                audioSource.pitch = Mathf.Lerp(initialPitch, pitch, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            else 
            {
                break;
            }
        }
        yield return null;
    }

    public static IEnumerator PositionLerp(Transform thingToMove, Vector3 vectorFrom, Vector3 vectorTo, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration) 
        {
            if (thingToMove != null) // I like to destroy
            {
                thingToMove.position = Vector3.Slerp(vectorFrom, vectorTo, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            else 
            {
                break;
            }
        }
        yield return null;
    }

    public static IEnumerator RotationLerp(Transform thingToMove, Quaternion rotationFrom, Quaternion rotationTo, float duration)
    {
        float timeElapsed = 0;

        while (timeElapsed < duration) 
        {
            if (thingToMove != null) // I like to destroy
            {
                thingToMove.rotation = Quaternion.Slerp(rotationFrom, rotationTo, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            else 
            {
                break;
            }
        }
        yield return null;
    }
}
