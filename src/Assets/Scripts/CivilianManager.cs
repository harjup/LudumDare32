using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;

public class CivilianManager : Singleton<CivilianManager>
{
    private float _leftBounds;
    private float _rightBounds;
    private float _vertical;

    private GameObject _civilianPrefab;

    private void Awake()
    {
        var spawnMarkers = FindObjectsOfType<SpawnMarker>();
        var leftBoundsMarker = spawnMarkers.First(s => s.Bounds == BoundsDirection.Left);
        _leftBounds = leftBoundsMarker.transform.position.x;
        _vertical = leftBoundsMarker.transform.position.y;

        _rightBounds = spawnMarkers.First(s => s.Bounds == BoundsDirection.Right).transform.position.x;

        _civilianPrefab = Resources.Load<GameObject>("Prefabs/Actors/Civilian");
    }

    public void SpawnCivilians()
    {
        CleanUpCivilians();

        for (int i = 0; i < 10; i++)
        {
            var xPos = Random.Range(_leftBounds, _rightBounds);
            var yPos = _vertical;

            Instantiate(_civilianPrefab, new Vector3(xPos, yPos), Quaternion.identity);
        }
    }

    private void CleanUpCivilians()
    {
        var civilians = FindObjectsOfType<Civilian>();
        foreach (var civilian in civilians)
        {
            civilian.CleanUp();
        }
    }
}