using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AudioReactiveProjector : MonoBehaviour
{
    public int speedBand = 1;
    public float speedBias = 2.6f;
    public float speedDecrease = .005f;
    public float rateOfChange = 1.03f;
    public float minPlaybackSpeed = 1.0f;
    public float maxPlaybackSpeed = 2.78f;

    public AudioVisualizer audioVisualizer;
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        // spike the playback speed when the "bass" band passes a certain threshold
        if (audioVisualizer.bufferBand[speedBand] > speedBias)
        {
            videoPlayer.playbackSpeed = maxPlaybackSpeed;
            speedDecrease = .05f;
        }
        else if (audioVisualizer.bufferBand[speedBand] < speedBias)          // falloff
        {
            if (videoPlayer.playbackSpeed > minPlaybackSpeed)
            {
                //videoPlayer.playbackSpeed -= speedDecrease;
                //speedDecrease *= rateOfChange;

                // rate slows down instead of speeding up
                videoPlayer.playbackSpeed -= speedDecrease;
                speedDecrease *= .94f;

                // (tried to adapt from the AudioVisualizer.cs one but this doesn't work)
                //videoPlayer.playbackSpeed = (videoPlayer.playbackSpeed - audioVisualizer.frequencyBand[speedBand]) / 8;
                //videoPlayer.playbackSpeed -= speedDecrease;
            }
        }
    }

}
