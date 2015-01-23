using UnityEngine;
using System.Collections;

public class InstanceManager : MonoBehaviour {

  private static InstanceManager sInstance;

  public Transform defaultInstanceRoot = null;

  void Awake()
  {
    if (sInstance == null)
    {
      sInstance = this;
      if (defaultInstanceRoot == null)
      {
        defaultInstanceRoot = transform;
      }
    }
  }

  private static void checkInstance()
  {
    if (sInstance == null)
    {
      GameObject managerGo = new GameObject();
      managerGo.name = "InstanceManager(Auto)";
      managerGo.AddComponent<InstanceManager>();
    }
  }

  public static GameObject InstantiateObject(GameObject prefab)
  {
    return InstantiateObject(prefab, Vector3.zero, Quaternion.identity);
  }

  public static GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion orientation)
  {
    checkInstance();
    GameObject instance = Instantiate(prefab, position, orientation) as GameObject;
    instance.transform.parent = sInstance.defaultInstanceRoot;
    return instance;
  }

}
