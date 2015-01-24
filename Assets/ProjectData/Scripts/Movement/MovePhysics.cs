using UnityEngine;
using System.Collections;

public class MovePhysics : MonoBehaviour {

    public float speed = 5.0f;
    public float rotationSpeed = 45.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 move = new Vector3 (XBoxController.instance.GetLeftStick ().x, 0.0f, XBoxController.instance.GetLeftStick ().y);
        move = transform.TransformDirection (move);
        move = move * Time.deltaTime * speed;
        move.y = rigidbody.velocity.y;
        //rigidbody.AddForce (move);
        rigidbody.velocity = move;

        float rotY = XBoxController.instance.GetRightStick ().x * Time.deltaTime * rotationSpeed;
        float rotX = XBoxController.instance.GetRightStick ().y * Time.deltaTime * rotationSpeed;
        transform.Rotate(new Vector3( 0.0f, rotY, 0.0f));
	}
}
