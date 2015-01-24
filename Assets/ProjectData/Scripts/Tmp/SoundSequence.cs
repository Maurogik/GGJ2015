﻿using UnityEngine;
using System.Collections;

public class SoundSequence : MonoBehaviour {

    public float interpolationTime = 2.0f;
    public Transform[] positionSequence;
    public AudioSource audioSourceIdle;
    public AudioSource audioSourceMoving;

    private int mCurrentInd = 0;

    public void PlayNext(){
        //move
        mCurrentInd++;
        StartCoroutine (move ());
    }

    IEnumerator fadeOut(AudioSource source, float duration){
        float startTime = Time.time;
        float progress = 0.0f;
        while (startTime + interpolationTime > Time.time) {
            progress = (Time.time - startTime)/interpolationTime;
            source.volume = 1.0f - progress;
            yield return null;
        }
        source.Stop ();
    }

    IEnumerator fadeIn(AudioSource source, float duration){
        float startTime = Time.time;
        float progress = 0.0f;
        while (startTime + interpolationTime > Time.time) {
            progress = (Time.time - startTime)/interpolationTime;
            source.volume = progress;
            yield return null;
        }
        source.Play ();
    }

    IEnumerator move(){
        StartCoroutine (fadeOut (audioSourceIdle, 0.5f));
        yield return new WaitForSeconds (0.4f);
        audioSourceMoving.Play ();
        Vector3 start = transform.position;
        Vector3 target = positionSequence [mCurrentInd].position;

        float startTime = Time.time;
        float progress = 0.0f;
        while (startTime + interpolationTime > Time.time) {
            progress = (Time.time - startTime)/interpolationTime;
            transform.position = Vector3.Lerp(start, target, progress);
            yield return null;
        }
        StartCoroutine (fadeIn (audioSourceIdle, 1.0f));
    }
}