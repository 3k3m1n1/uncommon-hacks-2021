using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactiveCube : MonoBehaviour
{
    public int band;
    public float startScale, scaleMultiplier;
    public bool useBuffer;

    public AudioVisualizer audioVisualizer;

    // Update is called once per frame
    void Update()
    {
        if (useBuffer)
        {
            // use buffer band
            transform.localScale = new Vector3(transform.localScale.x, (audioVisualizer.bufferBand[band] * scaleMultiplier) + startScale, transform.localScale.z);
        } else
        {
            // use regular (jittery-looking) band
            transform.localScale = new Vector3(transform.localScale.x, (audioVisualizer.frequencyBand[band] * scaleMultiplier) + startScale, transform.localScale.z);
        }
        
    }
}
