using UnityEngine;
using System.Collections;

public class UIMgr : MonoBehaviour {

  public static UIMgr Instance;

  public Camera UICamera;
  public StateUI GameUIRoot;
  public StateUI MenuUIRoot;

  private bool isInMenu = false;

  void Awake()
  {
    Instance = this;
    GameUIRoot.gameObject.SetActive(!isInMenu);
    MenuUIRoot.gameObject.SetActive(isInMenu);
  }

  public void ShowMenu()
  {
    if (GameUIRoot && MenuUIRoot)
    {
      isInMenu = true;
      GameUIRoot.gameObject.SetActive(!isInMenu);
      MenuUIRoot.gameObject.SetActive(isInMenu);
    }
  }

  public void HideMenu()
  {
    isInMenu = false;
    GameUIRoot.gameObject.SetActive(!isInMenu);
    MenuUIRoot.gameObject.SetActive(isInMenu);
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape)
      || Input.GetKeyDown(KeyCode.Space))
    {
      isInMenu = !isInMenu;
      GameUIRoot.gameObject.SetActive(!isInMenu);
      MenuUIRoot.gameObject.SetActive(isInMenu);
    }
  }

}
