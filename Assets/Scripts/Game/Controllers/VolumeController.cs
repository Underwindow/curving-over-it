using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeController : MonoBehaviour
{
    private Volume volume;
    private ChromaticAberration ca = null;
    private Vignette vignette = null;
    private ColorAdjustments colAds = null;
    private LensDistortion ld = null;
    private bool isCAReady = false;

    public void VolumeEffectsChaos()
    {
        volume = GetComponent<Volume>();
        volume.enabled = true;
        volume.profile.TryGet(out ca);
        volume.profile.TryGet(out ld);
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out colAds);
        
        var cam = GetComponent<Camera>();
        
        var data = cam.GetUniversalAdditionalCameraData();
            data.volumeLayerMask = LayerMask.GetMask("Default");

        EffectsFade();
    }

    private void EffectsFade()
    {
        if (ca != null && ld != null && vignette != null && colAds != null)
        {
            ca.active = true;
            ld.active = true;
            vignette.active = true;

            StartCoroutine(Fading.LensDistortionIntensity(ld, -1, 7, Mathf.Lerp));
            StartCoroutine(Fading.ChromaticAberrationIntensity(ca, .4f, 7, Mathf.Lerp));
            StartCoroutine(Fading.ChromaticVignetteIntensity(vignette, .841f, 7, Mathf.Lerp));

            StartCoroutine(ChromaticAberrationChaosDelay(14.69f));
        }

    }
    public IEnumerator ChromaticAberrationChaosDelay(float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        ca.intensity.value = 1;
        isCAReady = true;

        while (isCAReady)
        {
            colAds.hueShift.value = Random.Range(-180, 180);
            yield return new WaitForSeconds(Time.unscaledDeltaTime * 15);
            ca.intensity.value = Random.value;
        }
    }

    private void OnDestroy()
    {
        isCAReady = false;
    }

    private void OnDisable()
    {
        isCAReady = false;
    }

    private void OnApplicationQuit()
    {
        isCAReady = false;
    }
}
