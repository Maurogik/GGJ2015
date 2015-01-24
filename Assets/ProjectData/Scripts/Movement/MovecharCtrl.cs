﻿using UnityEngine;
using System.Collections;

public class MovecharCtrl : MonoBehaviour {

    private CharacterController mCtrl;
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;

    // Use this for initialization
	void Start () {
        mCtrl = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 move = new Vector3 (XBoxController.instance.GetLeftStick ().x, 0.0f, XBoxController.instance.GetLeftStick ().y);
        move = transform.TransformDirection (move);
        move.y = 0;
        move.Normalize ();
        float valX = XBoxController.instance.GetRightStick ().x * Time.deltaTime * rotationSpeed;
        float valY = XBoxController.instance.GetRightStick ().y * Time.deltaTime * rotationSpeed;
        //transform.Rotate( new Vector3( -rotX, rotY, 0.0f) );
        Vector3 lookAt = new Vector3 (valX * Time.deltaTime, valY * Time.deltaTime, 1.0f);
        lookAt *= rotationSpeed;
        transform.LookAt (transform.position + transform.TransformDirection (lookAt));
        mCtrl.SimpleMove (move * speed);
	}
}