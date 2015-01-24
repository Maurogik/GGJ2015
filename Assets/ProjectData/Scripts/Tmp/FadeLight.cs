using UnityEngine;
using System.Collections;

public class FadeLight : MonoBehaviour {

    public Light target;
    public float duration;
    public float targetIntensity;

    public void FadeIn(){
        Debug.Log ("fade in");
        StartCoroutine (_fadeIn ());
    }

	IEnumerator _fadeIn(){
        float startTime = Time.time;
        while (startTime + duration > Time.time) {
            float progress = (Time.time - startTime)/duration;
            target.intensity = Mathf.Lerp(0.0f, targetIntensity, progress);
            yield return null;
        }
    }
}
