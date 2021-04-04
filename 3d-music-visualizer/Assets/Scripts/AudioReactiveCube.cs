using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactiveCube : MonoBehaviour
{
    public int band;
    public float startScale, scaleMultiplier;

    public AudioVisualizer audioVisualizer;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, (audioVisualizer.frequencyBand[band] * scaleMultiplier) + startScale, transform.localScale.z);
    }
}
