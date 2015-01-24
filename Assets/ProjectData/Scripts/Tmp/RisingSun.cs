using UnityEngine;
using System.Collections;

public class RisingSun : MonoBehaviour {

	IEnumerator rise(Light light, float time){
		float startTime = Time.time;
		while (startTime + time > Time.time) {
			light.transform.Rotate(Vector3.left, 100.0f/(Time.time - startTime));
			yield return null; //skip to next frame
		}
	}

	void OnRise(Light light) {
		StartCoroutine(rise(light, 3600.0f));
	}
}
