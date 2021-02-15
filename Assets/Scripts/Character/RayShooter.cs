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
    // реагируем на нажатие кнопки мыши
    if (Input.GetMouseButtonDown(0))
    {
      // создаем луч из камеры идущий от центра экрана
      Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
      Ray ray = _camera.ScreenPointToRay(point);
      //создаем структуру с данными о пересеченном объекте
      RaycastHit hit;

      // пускаем луч по прямой
      if (Physics.Raycast(ray, out hit))
      {
        GameObject hitObject = hit.transform.gameObject;
        switch (hitObject.tag)
        {
          case "Enemy":
            // если враг то наносим урон
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
            target.ReactToHit();
            break;

          default:
            // иначе создаем след от выстрела
            StartCoroutine(SphereIndikaor(hit.point));
            break;
        }
      }
    }
  }

  // метод запускаемые каждый раз после рендера
  // реализует графический интерфейс
  void OnGUI()
  {
    int size = 12;
    float posX = _camera.pixelWidth / 2;
    float posY = _camera.pixelHeight / 2;

    // добавляем текст как прицел поцентру камеры
    GUI.Label(new Rect(posX, posY, size, size), "*");
  }

  // создание следа от выстрела
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
