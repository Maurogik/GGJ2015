using UnityEngine;
using System.Collections;

public class SendMessageOnTrigger : MonoBehaviour {

    public bool onlyOnce = true;
    public GameObject target;
    public string message;
    private bool done = false;

    void OnTriggerEnter(Collider other) {
        if (done) {
            return;
        }
        target.SendMessage (message);
        if (onlyOnce) {
            done = true;
        }
    }
}
