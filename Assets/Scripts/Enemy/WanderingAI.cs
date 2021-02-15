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

    CheckRay();
  }

  private void ShootFireballToPlayer(RaycastHit hit)
  {
    GameObject hitObject = hit.transform.gameObject;
    if (_fireball == null)
    {
      // если луч пересекся с персонаже, то создаем шар и стреляем
      // по направлению персонажа
      _fireball = Instantiate(fireballPrefab);
      Fireball fireball = _fireball.transform.GetComponent<Fireball>();

      fireball.SetDirection(
        transform.TransformPoint(Vector3.forward * 1.5f),
        transform.rotation
      );

    }
  }

  // Уворачиваемся от столкновения с пердметами
  private void collisionAvoidance(RaycastHit hit)
  {
    if (hit.distance <= obstacleRange)
    {
      float angle = Random.Range(-110, 100);

      transform.Rotate(0, angle, 0);
    }
  }

  // логика слежения за персонажем
  private void FollowCharacter(RaycastHit hit)
  {
    if (hit.distance <= 15)
    {
      MoveLogic(hit.distance > 2 ? speed : 0);
      transform.LookAt(hit.transform);
    }
  }

  // перемещаем врага
  private void MoveLogic(float _speed)
  {
    transform.Translate(0, 0, _speed * Time.deltaTime);
  }

  private void CheckRay()
  {
    //создаем луч
    Ray ray = new Ray(transform.position, transform.forward);
    //создаем структуру с данными о пересеченном объекте
    RaycastHit hit;

    //пускаем луч-сферу вперед
    if (Physics.SphereCast(ray, 0.75f, out hit))
    {
      GameObject hitObject = hit.transform.gameObject;

      switch (hitObject.tag)
      {
        case "Player":
          FollowCharacter(hit);
          ShootFireballToPlayer(hit);
          break;

        default:
          MoveLogic(speed);
          collisionAvoidance(hit);
          break;
      }
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
