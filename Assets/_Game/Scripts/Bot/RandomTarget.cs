using UnityEngine;
using UnityEngine.AI;

public class RandomTarget : Singleton<RandomTarget>
{
    public Vector3 R_point_Get(Vector3 start_point, float radius)
    {
        int randomDistan = Random.Range(3, 6);
        Vector3 dir = Random.insideUnitSphere * radius;
        dir += start_point;
        NavMeshHit navhit;
        Vector3 Final_pos = Vector3.zero;
        if (NavMesh.SamplePosition(dir, out navhit, radius, 1))
        {
            Final_pos = navhit.position;
        }
        return Final_pos;
    }
}
