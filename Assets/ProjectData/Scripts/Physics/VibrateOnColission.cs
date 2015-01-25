using UnityEngine;
using System.Collections;

public class VibrateOnColission : MonoBehaviour {

    public GameObject soundPrefab;


    IEnumerator vibrateFor(float leftVal, float rightVal, float time){
        float startTime = Time.time;
        while (startTime + time > Time.time) {
            XBoxController.instance.Vibrate (leftVal, rightVal);  
            yield return null; //skip to next frame
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.layer != LayerMask.NameToLayer("Wall")) {
            return;
        }
        if (transform.childCount < 10) {
            GameObject sound = Instantiate (soundPrefab) as GameObject;
            sound.transform.parent = transform;
            sound.transform.position = transform.position;
            Destroy (sound, 10.0f);
        }
        float strength = collision.relativeVelocity.magnitude;
        Debug.Log ("Strenght : " + strength);
        //XBoxController.instance.Vibrate (strength * 0.3f, strength * 0.1f);        
        StartCoroutine(vibrateFor(strength * 0.2f, strength * 0.1f, strength * 0.1f));
    }

    void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.layer != LayerMask.NameToLayer("Wall")) {
            return;
        }
        float strength = 1.0f;//collision.relativeVelocity.magnitude;
        XBoxController.instance.Vibrate (strength * 0.01f, strength * 0.1f);        
    }


    /*void OnCollisionEnter(Collision collision) {
        float strength = collision.relativeVelocity.magnitude;
        //Debug.Log ("Strenght : " + strength);
        //XBoxController.instance.Vibrate (strength * 0.3f, strength * 0.1f);        
        //StartCoroutine(vibrateFor(strength * 0.2f, strength * 0.1f, strength * 0.1f));
    }
    
    void OnCollisionStay(Collision collision) {

        Vector3 point = avgPoint (collision.contacts);
        Vector3 pointObjectSpace = transform.TransformDirection (point - transform.position);

        float lVal = 0.0f, rVal = 0.0f;
        Debug.Log ("pt obj space : " + pointObjectSpace);

        if (pointObjectSpace.x > 0) {
            lVal = pointObjectSpace.x;
        } else {
            rVal = pointObjectSpace.x;
        }

        //Debug.DrawLine (transform.position, avgPoint, Color.red, 1.0f);
        //float strength = 1.0f;//collision.relativeVelocity.magnitude;
        XBoxController.instance.Vibrate (lVal, rVal);        
    }

    private Vector3 avgPoint(ContactPoint[] points){
        Vector3 avgPoint = Vector3.zero;
        foreach (ContactPoint point in points) {
            avgPoint += point.point;
        }
        avgPoint = avgPoint / points.Length;
        return avgPoint;
    }*/
}
