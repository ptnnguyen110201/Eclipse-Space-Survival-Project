using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct VisibilityJob : IJobParallelFor
{
    public NativeArray<Vector3> ItemPositions;
    public Rect ViewBounds;
    public NativeArray<bool> Results;

    public void Execute(int index)
    {
        Vector3 position = this.ItemPositions[index];
        this.Results[index] = this.ViewBounds.Contains(new Vector2(position.x, position.y));
    }
}
