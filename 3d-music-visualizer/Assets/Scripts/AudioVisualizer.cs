using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credits to Peer Play's "Audio Visualization" series on YouTube! https://www.youtube.com/watch?v=5pmoP1ZOoNs&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo

[RequireComponent (typeof (AudioSource))]
public class AudioVisualizer : MonoBehaviour
{
    AudioSource audioSource;
    public float[] samples = new float[512];
    public float[] frequencyBand = new float[8];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        MakeFrequencyBands();
    }

    void MakeFrequencyBands()
    {
        /*
         * 22050 Hz / 512 samples = 43 Hz per sample
         *
         * 20 - 60 hertz
         * 60 - 250 hertz
         * 250 - 500 hertz
         * 500 - 2000 hertz
         * 2000 - 4000 hertz
         * 4000 - 6000 hertz
         * 6000 - 20000 hertz
         *
         */

        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            frequencyBand[i] = average * 10;
        }
    }
}
