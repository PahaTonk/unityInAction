using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
  private Camera _camera;

  void Start()
  {
    _camera = GetComponent<Camera>();

    //прячем курсор поцентру
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
      Ray ray = _camera.ScreenPointToRay(point);
      RaycastHit hit;

      if (Physics.Raycast(ray, out hit))
      {
        GameObject hitObject = hit.transform.gameObject;
        ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
        if (target != null)
        {
          target.ReactToHit();
        }
        else
        {
          StartCoroutine(SphereIndikaor(hit.point));
        }
      }
    }
  }

  void OnGUI()
  {
    int size = 12;
    float posX = _camera.pixelWidth / 2;
    float posY = _camera.pixelHeight / 2;

    GUI.Label(new Rect(posX, posY, size, size), "*");
  }

  private IEnumerator SphereIndikaor(Vector3 pos)
  {
    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    SphereCollider collider = sphere.GetComponent<SphereCollider>();

    collider.enabled = false;
    sphere.transform.position = pos;


    yield return new WaitForSeconds(1);

    Destroy(sphere);
  }
}
