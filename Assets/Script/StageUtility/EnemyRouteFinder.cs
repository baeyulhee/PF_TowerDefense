using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRouteFinder
{
    public static NavMeshPath GetPath(Vector3 startPos, Vector3 endPos)
    {
        NavMeshPath path = new();

        if (NavMesh.CalculatePath(startPos, endPos, NavMesh.AllAreas, path))
        {
            return path;
        }
        else
            return new NavMeshPath();
    }

    public static List<Vector3> GetCenterPath(Vector3 startPos, Vector3 endPos, float offset = 0.5f)
    {
        NavMeshPath rawPath = GetPath(startPos, endPos);
        List<Vector3> newCorners = new();

        if (rawPath.corners.Length == 0)
            return newCorners;

        newCorners.Add(rawPath.corners[0]);

        for (int i = 1; i < rawPath.corners.Length - 1; i++)
        {
            Vector3 prev = rawPath.corners[i - 1];
            Vector3 curr = rawPath.corners[i];
            Vector3 next = rawPath.corners[i + 1];

            Vector3 dir = (next - prev).normalized;
            Vector3 perp = new Vector3(-dir.z, 0, dir.x);

            Vector3 left = curr;
            Vector3 right = curr;

            for (int j = 1; j < 10; j++)
            {
                left = curr + perp * offset * j;
                if (!NavMesh.SamplePosition(left, out NavMeshHit hit, 0.1f, NavMesh.AllAreas))
                    break;
            }
            for (int j = 1; j < 10; j++)
            {
                right = curr - perp * offset * j;
                if (!NavMesh.SamplePosition(right, out NavMeshHit hit, 0.1f, NavMesh.AllAreas))
                    break;
            }

            newCorners.Add((left + right) * 0.5f);
        }

        newCorners.Add(rawPath.corners[rawPath.corners.Length - 1]);

        return newCorners;
    }
}
