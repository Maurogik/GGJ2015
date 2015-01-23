using UnityEngine;
using System.Collections;

public class StateUI : MonoBehaviour {

  public GameObject[] uis;
  public string defaultState;

  public void OnEnable()
  {
    Reset();
  }

  public void Reset()
  {
    SetSate(defaultState);
  }

  public void SetSate(string state)
  {
    foreach (GameObject ui in uis)
    {
      ui.SetActive(ui.name == state);
    }
  }
}
