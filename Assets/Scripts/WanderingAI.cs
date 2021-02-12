using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
  [SerializeField] private GameObject fireballPrefab;
  private GameObject _fireball;
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

    MoveLogic();
    ShootFireballToPlayer();
  }

  private void ShootFireballToPlayer()
  {
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    if (Physics.SphereCast(ray, 0.75f, out hit))
    {
      GameObject hitObject = hit.transform.gameObject;
      if (hitObject.tag == "Player" && _fireball == null)
      {
        _fireball = Instantiate(fireballPrefab);
        Fireball fireball = _fireball.transform.GetComponent<Fireball>();

        fireball.SetDirection(
          transform.TransformPoint(Vector3.forward * 1.5f),
          transform.rotation
        );

      }
    }
  }

  private void MoveLogic()
  {
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    transform.Translate(0, 0, speed * Time.deltaTime);

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
