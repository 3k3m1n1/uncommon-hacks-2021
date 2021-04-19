using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactiveLamp : MonoBehaviour
{
    public int band = 2;
    public float bias = 4.6f;
    public AudioVisualizer audioVisualizer;

    public Light spotlight;
    public SpriteRenderer godRay;

    // Update is called once per frame
    void Update()
    {
        // map the "lower midrange" volume to the brightness of the lamp
        float numBetween0and1 = Mathf.InverseLerp(0f, bias, audioVisualizer.bufferBand[band]);

        spotlight.intensity = Mathf.Lerp(0f, 27f, numBetween0and1);
        godRay.color = new Color(godRay.color.r, godRay.color.g, godRay.color.b, numBetween0and1);
    }
}
