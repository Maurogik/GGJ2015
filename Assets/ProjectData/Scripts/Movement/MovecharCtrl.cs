using UnityEngine;
using System.Collections;

public class MovecharCtrl : MonoBehaviour {

    private CharacterController mCtrl;
    public float speed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float grabity = 4.5f;
    public AudioSource stepSource;

    private float walkVolume;
    private Vector3 mLastStep;
    

    // Use this for initialization
	void Start () {
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
