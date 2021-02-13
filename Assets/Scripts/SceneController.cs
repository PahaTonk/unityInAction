using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
  [SerializeField] private GameObject enemyPrefab;
  private GameObject _enemy;

  void Update()
  {
    if (_enemy != null) return;

    // создаем врага после смерти предыдущего
    _enemy = Instantiate(enemyPrefab);
    float randomAngle = Random.Range(0, 360);

    _enemy.transform.position = new Vector3(0, 1, 0);
    _enemy.transform.Rotate(0, randomAngle, 0);
  }
}
