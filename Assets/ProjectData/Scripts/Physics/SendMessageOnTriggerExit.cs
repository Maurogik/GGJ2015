using UnityEngine;
using System.Collections;

public class SendMessageOnTriggerExit : MonoBehaviour {

    public bool onlyOnce = true;
    public GameObject target;
    public string message;
    private bool done = false;

    void OnTriggerExit(Collider other) {
        if (done) {
            return;
        }
        target.SendMessage (message);
        if (onlyOnce) {
            done = true;
        }
    }
}
