using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWallsBtwRooms : MonoBehaviour
{
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;

    [SerializeField] private float _incrementValue;

    [SerializeField] private GameObject _wall_1;
    [SerializeField] private GameObject _wall_2;

    [SerializeField] private Transform _hallwayWalls;

    public Direction _direction;

    public enum Direction
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public void CheckPassages1()
    {
        Vector2 currentPoint1 = _point1.position;
        Vector2 currentPoint2 = _point2.position;

        switch (_direction)
        {
            case Direction.Left:
                while (currentPoint1 != null)
                {
                    currentPoint1.x -= _incrementValue;
                    currentPoint2.x -= _incrementValue;

                    if (!HasCollider(currentPoint1, currentPoint2))
                    {
                        InstantiateWallVertical(currentPoint1, currentPoint2);
                    }
                    else
                    {
                        return;
                    }
                }
                break;

            case Direction.Right:
                while (currentPoint1 != null)
                {
                    currentPoint1.x += _incrementValue;
                    currentPoint2.x += _incrementValue;

                    if (!HasCollider(currentPoint1, currentPoint2))
                    {
                        InstantiateWallVertical(currentPoint1, currentPoint2);
                    }
                    else
                    {
                        return;
                    }
                }
                break;

            case Direction.Top:
                while (currentPoint1 != null)
                {
                    currentPoint1.y += _incrementValue;
                    currentPoint2.y += _incrementValue;

                    if (!HasCollider(currentPoint1, currentPoint2))
                    {
                        InstantiateWallHorizontal(currentPoint1, currentPoint2);
                    }
                    else
                    {
                        return;
                    }
                }
                break;

            case Direction.Bottom:
                while (currentPoint1 != null)
                {
                    currentPoint1.y -= _incrementValue;
                    currentPoint2.y -= _incrementValue;

                    if (!HasCollider(currentPoint1, currentPoint2))
                    {
                        InstantiateWallHorizontal(currentPoint1, currentPoint2);
                    }
                    else
                    {
                        return;
                    }
                }
                break;
        }
    }

    private bool HasCollider(Vector2 point1, Vector2 point2)
    {
        Collider2D hitCollider1 = Physics2D.OverlapPoint(point1);
        Collider2D hitCollider2 = Physics2D.OverlapPoint(point2);
        return hitCollider1 != null || hitCollider2 != null;
    }

    private void InstantiateWallHorizontal(Vector2 point1, Vector2 point2)
    {
        GameObject wall1 = Instantiate(_wall_1, point1, Quaternion.identity);
        GameObject wall2 = Instantiate(_wall_1, point2, Quaternion.identity);

        wall1.transform.parent = _hallwayWalls.transform;
        wall2.transform.parent = _hallwayWalls.transform;
    }

    private void InstantiateWallVertical(Vector2 point1, Vector2 point2)
    {
        GameObject wall1 = Instantiate(_wall_2, point1, Quaternion.identity);
        GameObject wall2 = Instantiate(_wall_2, point2, Quaternion.identity);

        wall1.transform.parent = _hallwayWalls.transform;
        wall2.transform.parent = _hallwayWalls.transform;
    }


    public void CheckPassages()
    {
        StartCoroutine(CheckPassagesCor());
    }



    public IEnumerator CheckPassagesCor()
    {
        Vector2 currentPoint1 = _point1.position;
        Vector2 currentPoint2 = _point2.position;

        switch (_direction)
        {
            case Direction.Left:
                while (currentPoint1 != null)
                {
                    //Debug.Log("left");
                    currentPoint1.x -= _incrementValue;
                    currentPoint2.x -= _incrementValue;

                    if (!HasCollider(currentPoint1, currentPoint2))
                    {
                        InstantiateWallVertical(currentPoint1, currentPoint2);
                    }
                    else
                    {
                        yield break;
                    }
                    yield return null;
                }
                break;

            case Direction.Right:
                while (currentPoint1 != null)
                {
                    currentPoint1.x += _incrementValue;
                    currentPoint2.x += _incrementValue;

                    if (!HasCollider(currentPoint1, currentPoint2))
                    {
                        InstantiateWallVertical(currentPoint1, currentPoint2);
                    }
                    else
                    {
                        yield break;
                    }
                    yield return null;
                }
                break;

            case Direction.Top:
                while (currentPoint1 != null)
                {
                    currentPoint1.y += _incrementValue;
                    currentPoint2.y += _incrementValue;

                    if (!HasCollider(currentPoint1, currentPoint2))
                    {
                        InstantiateWallHorizontal(currentPoint1, currentPoint2);
                    }
                    else
                    {
                        yield break;
                    }
                    yield return null;
                }
                break;

            case Direction.Bottom:
                while (currentPoint1 != null)
                {
                    currentPoint1.y -= _incrementValue;
                    currentPoint2.y -= _incrementValue;

                    if (!HasCollider(currentPoint1, currentPoint2))
                    {
                        InstantiateWallHorizontal(currentPoint1, currentPoint2);
                    }
                    else
                    {
                        yield break;
                    }
                    yield return null;
                }
                break;
        }
    }
}
