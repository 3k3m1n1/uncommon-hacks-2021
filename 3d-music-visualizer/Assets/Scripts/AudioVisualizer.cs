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
    public float[] bufferBand = new float[8];
    float[] bufferDecrease = new float[8];    // speed to smooth frequency values back down, instead of jittering

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        Simplify512BandsTo8();
        BufferBandValues();
    }

    void Simplify512BandsTo8()
    {
        /*
         * 22050 Hz / 512 samples = 43 Hz per sample
         *
         * sub bass: 20 - 60 hertz
         * bass: 60 - 250 hertz
         * low midrange: 250 - 500 hertz
         * midrange: 500 - 2000 hertz
         * upper midrange: 2000 - 4000 hertz
         * presence: 4000 - 6000 hertz
         * brilliance: 6000 - 20000 hertz
         *
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

    // this function smoothly lowers the amplitude values for each frequency band (bar)
    // looks way better and less jittery!
    void BufferBandValues()
    {
        for (int g = 0; g < 8; g++)
        {
            if (frequencyBand[g] > bufferBand[g])
            {
                bufferBand[g] = frequencyBand[g];
                bufferDecrease[g] = .005f;
            }

            if (frequencyBand[g] < bufferBand[g])
            {
                //bufferBand[g] -= bufferDecrease[g];
                //bufferDecrease[g] *= 1.2f;

                // suggestion in the comments: creates a softer effect where the buffer slows down the more it decreases
                bufferDecrease[g] = (bufferBand[g] - frequencyBand[g]) / 8;
                bufferBand[g] -= bufferDecrease[g];
            }
        }
    }
}
