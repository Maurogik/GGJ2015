using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

  public StateUI target;
  public string state;

  private Color originalColor;
  private bool clicked = false;
  private TextMesh textMesh;

  private float startCountTime = 0.0f;

	// Use this for initialization
	void Start () {
    textMesh = GetComponent<TextMesh>();
    originalColor = textMesh.color;
	}
	
	// Update is called once per frame
	void Update () {

    Vector2 mousePos = Input.mousePosition;
    RaycastHit hit;
    Ray ray = UIMgr.Instance.UICamera.ScreenPointToRay(mousePos);
    int layerMask = 1 << LayerMask.NameToLayer("UI");
    bool hover = Physics.Raycast(ray, out hit, 100.0f, layerMask) && hit.collider.gameObject == gameObject;
    if (hover)
    {
      if (!clicked)
      {
        textMesh.color = Color.gray;
      }
      if (Input.GetMouseButtonDown(0))
      {
        textMesh.color = Color.black;
        clicked = true;
        startCountTime = Time.realtimeSinceStartup;
      }
    }
    else if(!clicked)
    {
      textMesh.color = originalColor;
    }

    if (clicked && Time.realtimeSinceStartup > startCountTime + 0.1f)
    {
      resetColor();
    }
	}

  void resetColor()
  {
    Debug.Log("Button Clicked");
    clicked = false;
    textMesh.color = originalColor;
    target.SetSate(state);
  }
}
