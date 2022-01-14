using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;
        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();

        var layers = GetComponentsInChildren<ParallaxLayer>(false);

        for (int i = 0; i < layers.Length; i++)
        {
            ParallaxLayer layer = layers[i];

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                layer.transform.position -= (Vector3)((Vector2)layer.transform.position - (Vector2)parallaxCamera.Position) * (layer.parallaxFactor);
                parallaxLayers.Add(layer);
            }
        }
    }
    void Move(Vector3 delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
        }
    }
}
