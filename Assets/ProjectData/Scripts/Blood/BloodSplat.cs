using UnityEngine;
using System.Collections;

public class BloodSplat : MonoBehaviour {

  private float clearDuration = 10.0f;
  private float steps = 0.1f;
  public void clearBlood()
  {
    StartCoroutine(cleanRoutine());
  }

  private IEnumerator cleanRoutine()
  {
    float itCount = (clearDuration / steps);
    int nbIt = (int)itCount;
    Vector3 origScale = transform.localScale;
    while (nbIt > 0)
    {
      --nbIt;
      transform.localScale = Vector3.Lerp(origScale, Vector3.zero, 1.0f - ((float)nbIt / itCount));
      yield return new WaitForSeconds(steps);
    }
    Destroy(gameObject);
  }
}
