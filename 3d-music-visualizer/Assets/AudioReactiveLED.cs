using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactiveLED : MonoBehaviour
{
    public int band = 0;
    public float fadeSpeed = 2.3f;
    public float bias = .4f;
    public AudioVisualizer audioVisualizer;

    public Light[] LEDs = new Light[16];

    // Update is called once per frame
    void Update()
    {
        foreach (Light LED in LEDs)
        {
            // when the bass exceeds a certain threshold, flash the LED lights on
            if (audioVisualizer.bufferBand[band] > bias)
            {
                LED.intensity = 10f;
                fadeSpeed = 2.3f;
            }
            else if (audioVisualizer.bufferBand[band] < bias)   // fade to darkness again
            {
                if (LED.intensity > 0f)
                {
                    LED.intensity -= fadeSpeed;
                    fadeSpeed *= .993f;  //rate slows down
                }
            }
        }

        //// map the "lower midrange" volume to the brightness of the lamp
        //float numBetween0and1 = Mathf.InverseLerp(0f, bias, audioVisualizer.bufferBand[band]);

        //spotlight.intensity = Mathf.Lerp(0f, 27f, numBetween0and1);
        //godRay.color = new Color(godRay.color.r, godRay.color.g, godRay.color.b, numBetween0and1);
    }
}
