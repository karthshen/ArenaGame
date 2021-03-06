﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour
{
    public GameObject countDown;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartDelay");    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartDelay ()
    {
        Time.timeScale = 0;
        float pauseTime = Time.realtimeSinceStartup + 3.5f;
        bool hasPlayed = false;
        SoundManager.instance.PlayEffectWithAudioSource(SoundManager.instance.EffectSource, SoundManager.instance.countDown, ref hasPlayed,0.1f);
        while (Time.realtimeSinceStartup < pauseTime)
            yield return 0;
        countDown.gameObject.SetActive(false);
        GameObject.FindObjectOfType<CameraController>().CameraSpeed = 0.3f;
        Time.timeScale = 1;
    }
}
