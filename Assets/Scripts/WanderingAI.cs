using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
  public float speed = 3f;
  public float obstacleRange = 5f;
  private bool _alive;

  void Start()
  {
    _alive = true;
  }

  void Update()
  {
    if (!_alive) return;

    transform.Translate(0, 0, speed * Time.deltaTime);

    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    if (Physics.SphereCast(ray, 0.75f, out hit, obstacleRange))
    {
      float angle = Random.Range(-110, 100);

      transform.Rotate(0, angle, 0);
    }
  }

  public bool getAlive()
  {
    return _alive;
  }

  public void SetAlive(bool toggle)
  {
    _alive = toggle;
  }
}
