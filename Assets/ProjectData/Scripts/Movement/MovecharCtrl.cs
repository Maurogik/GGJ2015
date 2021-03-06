﻿using UnityEngine;
using System.Collections;

public class MovecharCtrl : MonoBehaviour {

    private CharacterController mCtrl;
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float grabity = 4.5f;
    public AudioSource stepSource;
    public Light cheatLight;
    public GameObject lightPrefab;

    public GameObject[] firstEnable;

    private bool firstApressed = false;

    private float walkVolume;
    private Vector3 mLastStep;

    private float mAccumLight = 0.0f;
    private float mMaxIntensity = 0.2f;

    private SoundSequence mSequence;

    // Use this for initialization
	void Start () {
        mSequence = FindObjectOfType<SoundSequence> ();
        walkVolume = stepSource.volume;
        stepSource.volume = 0.0f;
        mLastStep = transform.position;
        mCtrl = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = new Vector3 (XBoxController.instance.GetLeftStick ().x, 0.0f, XBoxController.instance.GetLeftStick ().y);
        move = transform.TransformDirection (move);
        /*if (XBoxController.instance.GetButtonA () && mCtrl.isGrounded) {
            move.y = 60.0f;
        } else {*/
            move.y = -grabity;
        //}
        move.Normalize ();
        float valX = XBoxController.instance.GetRightStick ().x * Time.deltaTime * rotationSpeed;
        float valY = XBoxController.instance.GetRightStick ().y * Time.deltaTime * rotationSpeed;
        //transform.Rotate( new Vector3( -rotX, rotY, 0.0f) );
        Vector3 lookAt = new Vector3 (valX * Time.deltaTime, valY * Time.deltaTime, 1.0f);
        lookAt *= rotationSpeed;
        transform.LookAt (transform.position + transform.TransformDirection (lookAt));
        mCtrl.Move (move * speed * Time.deltaTime);
        move.y = 0.0f;
        /*if (move.magnitude > 0.1f) {
            stepSource.volume = walkVolume;
        } else {
            stepSource.volume = 0.0f;
        }*/
        stepSource.volume = walkVolume * move.magnitude;

        if (XBoxController.instance.GetButtonADown ()) {
            if(firstApressed == false){
                firstApressed = true;
                foreach(GameObject obj in firstEnable){
                    obj.SetActive(true);
                }
            }

            GameObject light = Instantiate (lightPrefab) as GameObject;
            StartCoroutine(lightTarget(light));
            //mAccumLight += 0.1f * Time.deltaTime;
            
        } else if (XBoxController.instance.GetButtonBDown ()) {
            if(firstApressed == false){
                firstApressed = true;
                foreach(GameObject obj in firstEnable){
                    obj.SetActive(true);
                }
                GameObject light = Instantiate (lightPrefab) as GameObject;
                StartCoroutine(lightTarget(light));
                //mAccumLight += 0.1f * Time.deltaTime;
            }
        } else {
            //mAccumLight -= 0.1f * Time.deltaTime;
        }

        //mAccumLight = Mathf.Min (mAccumLight, mMaxIntensity);
        cheatLight.intensity = 0.0f;//mAccumLight;
        GameState.LightAccumCheat = mAccumLight;
	}

    IEnumerator lightTarget(GameObject light){
        Debug.Log ("Light target : " + light, light);
        Vector3 startPos = transform.position;
        Vector3 targetPos = mSequence.getNextPos();
        //GameObject light = Instantiate (lightPrefab) as GameObject;
        float startTime = Time.time;
        float duration = 5.0f;
        while (startTime + duration > Time.time) {
            float progress = (Time.time - startTime)/duration;
            light.transform.position = Vector3.Lerp(startPos, targetPos, progress);
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        Destroy(light);
    }

    IEnumerator fadeOut(AudioSource source, float playDuration, float fadeduration){
        source.volume = walkVolume;
        yield return new WaitForSeconds (playDuration);
        float startTime = Time.time;
        float progress = 0.0f;
        while (startTime + fadeduration > Time.time) {
            progress = (Time.time - startTime)/fadeduration;
            source.volume = Mathf.Lerp(walkVolume, 0.0f, progress);
            yield return null;
        }
    }
    
    /*IEnumerator jump(){
        float startTime = Time.time;
        while(startTime + jumpDuration > Time.time){
            Vector3 pos = transform.position;
            yield return null;
        }
    }*/
}
