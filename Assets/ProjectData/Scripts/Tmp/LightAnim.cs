using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LightAnim : MonoBehaviour {

    public GameObject lightPrefab;
    public GameObject concentrationSystem;
    public float concentrationDuration;
    public float moveDuration;
    public float maxDist = 95.0f;
    public float rotateSpeed = 10.0f;

    private float step = 45.0f;

    public void Start(){
        StartCoroutine (LightExplosion());
    }

    IEnumerator LightConcentration(){
        concentrationSystem.SetActive (true);
        yield return new WaitForSeconds (concentrationDuration);
        concentrationSystem.SetActive (false);
    }

    IEnumerator LightExplosion(){
        // raycast
        List<Vector3> targets = new List<Vector3> ();

        for (float xRot = 0.0f; xRot < 360.0f; xRot += step) {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            transform.Rotate(Vector3.right, xRot);
            for (float yRot = 0.0f; yRot < 360.0f; yRot += step) {
                transform.Rotate(Vector3.up, step);
                Vector3 direction = transform.forward;
                RaycastHit hitInfo;
                if(Physics.Raycast(transform.position, direction, out hitInfo)){
                    targets.Add(hitInfo.point);
                } else {
                    targets.Add(transform.position + direction * maxDist);
                }
            }
        }
        //instantiate
        List<GameObject> lights = new List<GameObject> ();
        for(int i = 0; i < targets.Count; ++i){
            GameObject light = Instantiate(lightPrefab) as GameObject;
            light.transform.parent = transform;
            light.transform.localPosition = Vector3.zero;
            lights.Add(light);
        }

        float startTime = Time.time;
        while (startTime + moveDuration > Time.time) {
            float progress = (Time.time - startTime)/moveDuration;
            //move
            for(int i = 0; i < targets.Count; ++i) {
                Vector3 point = targets[i];
                GameObject light = lights[i];
                light.transform.position = Vector3.Lerp(transform.position, point, progress * 0.9f);
            }
            yield return null;
        }
        StartCoroutine (LightRotation ());

    }

    IEnumerator LightRotation(){
        while (true) {
            Vector3 rotEuler = transform.rotation.eulerAngles;
            rotEuler.y += Time.deltaTime * rotateSpeed;
            transform.rotation = Quaternion.Euler(rotEuler);
            yield return null;
        }
    }

}
