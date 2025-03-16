using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Video;

public class VideoAudioController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    void Start()
    {
        // Đặt VideoPlayer xuất âm thanh qua AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // Chạy video
        videoPlayer.Play();
        audioSource.Play();
    }
}

