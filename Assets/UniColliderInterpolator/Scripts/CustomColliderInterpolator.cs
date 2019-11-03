using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UniColliderInterpolator
{
    public class CustomColliderInterpolator : MonoBehaviour
    {
        private const string ColliderObjectPrefix = "UCI_";
        [SerializeField] private float _divisionUnityLength = 0.5f;

        public async void Generate()
        {
            var colliderObjectName = $"{ColliderObjectPrefix}{gameObject.name}";
            var child = transform.Find(colliderObjectName);
            if (child != null)
            {
                DestroyImmediate(child.gameObject);
            }

            var meshCollider = gameObject.AddComponent<MeshCollider>();
            var bounds = meshCollider.bounds;

            Vector3[] gridPositions;
            int xDivisionCount, yDivisionCount, zDivisionCount;
            BoundsDivider.Divide(bounds, _divisionUnityLength, out gridPositions, out xDivisionCount,
                out yDivisionCount, out zDivisionCount);

            var tmpColliders = new Collider[1];
            var positionCount = gridPositions.Length;
            var hasColliderPosition = new bool[xDivisionCount * yDivisionCount * zDivisionCount];
            for (var i = 0; i < positionCount; i++)
            {
                var position = gridPositions[i];
                if (Physics.OverlapSphereNonAlloc(position, _divisionUnityLength / 2f, tmpColliders) == 0)
                {
                    continue;
                }

                hasColliderPosition[i] = true;
            }

            var colliderObject = new GameObject(colliderObjectName);
            colliderObject.transform.SetParent(transform);

            while (hasColliderPosition.Any(p => p))
            {
                await Task.Delay(1);
                var combineIndex = 0;
                var combineCountMax = int.MinValue;
                var xCombineEdgeCount = 0;
                var yCombineEdgeCount = 0;
                var zCombineEdgeCount = 0;
                for (var i = 0; i < positionCount; i++)
                {
                    if (!hasColliderPosition[i])
                    {
                        continue;
                    }

                    var checkIndex = i;
                    var xEdgeCount = 0;
                    while (checkIndex >= 0 && checkIndex < positionCount && hasColliderPosition[checkIndex])
                    {
                        checkIndex += zDivisionCount * yDivisionCount;
                        xEdgeCount++;

                        if (checkIndex % (zDivisionCount * yDivisionCount) == 0)
                        {
                            break;
                        }
                    }

                    checkIndex = i;
                    var yEdgeCount = 0;
                    while (checkIndex >= 0 && checkIndex < positionCount && hasColliderPosition[checkIndex])
                    {
                        checkIndex += zDivisionCount;
                        yEdgeCount++;

                        if (checkIndex % (zDivisionCount * yDivisionCount) / zDivisionCount == 0)
                        {
                            break;
                        }
                    }

                    checkIndex = i;
                    var zEdgeCount = 0;
                    while (checkIndex >= 0 && checkIndex < positionCount && hasColliderPosition[checkIndex])
                    {
                        checkIndex++;
                        zEdgeCount++;

                        if (checkIndex % zDivisionCount == 0)
                        {
                            break;
                        }
                    }

                    var margeCount = xEdgeCount * yEdgeCount * zEdgeCount;
                    if (margeCount <= combineCountMax)
                    {
                        continue;
                    }

                    combineIndex = i;
                    xCombineEdgeCount = xEdgeCount;
                    yCombineEdgeCount = yEdgeCount;
                    zCombineEdgeCount = zEdgeCount;
                    combineCountMax = margeCount;
                }

                for (var x = 0; x < xCombineEdgeCount; x++)
                {
                    for (var y = 0; y < yCombineEdgeCount; y++)
                    {
                        for (var z = 0; z < zCombineEdgeCount; z++)
                        {
                            var index = combineIndex + zDivisionCount * yDivisionCount * x + zDivisionCount * y + z;
                            hasColliderPosition[index] = false;
                        }
                    }
                }

                var collider = colliderObject.AddComponent<BoxCollider>();
                collider.center = gridPositions[combineIndex] +
                                  new Vector3(xCombineEdgeCount, yCombineEdgeCount, zCombineEdgeCount) *
                                  _divisionUnityLength / 2f - Vector3.one * _divisionUnityLength / 2f;
                collider.size = new Vector3(xCombineEdgeCount, yCombineEdgeCount, zCombineEdgeCount) *
                                _divisionUnityLength;
            }

            DestroyImmediate(meshCollider);
        }
    }
}