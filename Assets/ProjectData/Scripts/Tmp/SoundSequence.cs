using UnityEngine;
using System.Collections;

public class SoundSequence : MonoBehaviour {

    public float interpolationTime = 2.0f;
    public Transform[] positionSequence;
    public AudioClip idleClip;
    public AudioClip movingClip;
    public AudioSource audioSource;

    private int mCurrentInd = 0;

    public void PlayNext(){
        //Move audio source
        //play moving sound
        //play idle sound when arrived
        audioSource.clip = movingClip;
        audioSource.Play ();
        //move
        mCurrentInd++;
        StartCoroutine (move ());

    }

    IEnumerator move(){
        Vector3 start = transform.position;
        Vector3 target = positionSequence [mCurrentInd].position;

        float startTime = Time.time;
        float progress = 0.0f;
        while (startTime + interpolationTime > Time.time) {
            progress = (Time.time - startTime)/interpolationTime;
            transform.position = Vector3.Lerp(start, target, progress);
            yield return null;
        }
        audioSource.clip = idleClip;
        audioSource.Play ();
    }
}
