using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fading
{
    public static IEnumerator LensDistortionIntensity(LensDistortion ld, float endVal, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        float startVal = ld.intensity.value;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            ld.intensity.value = Interpolate(startVal, endVal, t);
            yield return null;
        }

        ld.intensity.value = endVal;
    }

    public static IEnumerator ChromaticAberrationIntensity(ChromaticAberration ca, float endVal, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        float startVal = ca.intensity.value;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            ca.intensity.value = Interpolate(startVal, endVal, t);
            yield return null;
        }

        ca.intensity.value = endVal;
    }

    public static IEnumerator ChromaticVignetteIntensity(Vignette vg, float endVal, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        float startVal = vg.intensity.value;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            vg.intensity.value = Interpolate(startVal, endVal, t);
            yield return null;
        }

        vg.intensity.value = endVal;
    }

    public static IEnumerator Fade(Rigidbody2D rb, float endAngularDrag, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        float startAngularDrag = rb.angularDrag;
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            rb.angularDrag = Interpolate(startAngularDrag, endAngularDrag, t);
            yield return null;
        }

        rb.angularDrag = endAngularDrag;
    }

    public static IEnumerator FadeIn(Rigidbody2D rb, float endAngularDrag, float fadingTime, Func<float, float, float, float> Interpolate)
    {
        float frameCount = fadingTime / Time.deltaTime;
        float framesPassed = 0;

        while (framesPassed <= frameCount)
        {
            var t = framesPassed++ / frameCount;
            rb.angularDrag = Interpolate(0, endAngularDrag, t);
            yield return null;
        }

        rb.angularDrag = endAngularDrag;
    }
}
