using System.Collections.Generic;
using FabricWars.Utils.Extensions;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace FabricWars.Jobs
{
    [BurstCompile]
    public struct Vector3IntRange : IJob
    {
        [ReadOnly] public Vector3Int start, end;
        private Vector3Int startPos, endPos;
        [WriteOnly] public NativeArray<Vector3Int> range;

        public Vector3IntRange(Vector3Int start, Vector3Int end) : this()
        {
            this.start = start;
            this.end = end;
            startPos = start.Min(end);
            endPos = startPos.Max(end);
            range = new NativeArray<Vector3Int>((endPos.x - startPos.x + 2) * (endPos.y - startPos.y + 2) * (endPos.z - startPos.z + 2), Allocator.TempJob);
        }
        
        public void Execute()
        {
            var index = 0;
            
            for (var x = startPos.x; x < endPos.x + 1; x++)
            {
                for (var y = startPos.y; y < endPos.y + 1; y++)
                {
                    if (startPos.z != 0 || endPos.z != 0)
                    {
                        for (var z = startPos.z; z < endPos.z + 1; z++)
                        {
                            range[index] = new Vector3Int(x, y, z);
                            index++;
                        }
                    }
                    else
                    {
                        range[index] = new Vector3Int(x, y, startPos.z);
                        index++;
                    }
                }
            }
        }

        public static IEnumerable<Vector3Int> Create(Vector3Int start, Vector3Int end)
        {
            var job = new Vector3IntRange(start, end);
            
            job.Execute();
            var result = new Vector3Int[job.range.Length];
            job.range.CopyTo(result);
            job.range.Dispose();
            
            return result;
        }
    }
}