using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.SceneManagement;

using Random = Unity.Mathematics.Random;

namespace ILab.Youngs
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class PlayerSpawnSystem : JobComponentSystem
    {
        BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;

        protected override void OnCreate()  
        {
            //Enabled = SceneManager.GetActiveScene().name.StartsWith("HelloCube_08");

            m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
        }

        struct SpawnJob : IJobForEachWithEntity<PlayerSpawner, LocalToWorld>
        {
            public EntityCommandBuffer.Concurrent CommandBuffer;

            [BurstCompile]
            public void Execute(Entity entity, int index, [ReadOnly] ref PlayerSpawner spawner, [ReadOnly] ref LocalToWorld location)
            {
             
                Entity instance = CommandBuffer.Instantiate(index, spawner.entity);

                CommandBuffer.AddComponent(index, instance, new PlayerData { moveSpeed = 5f, respawnPosition = new float3(0, 0, 0) });
                CommandBuffer.AddComponent(index, instance, new PlayerInput { });
            
                CommandBuffer.SetComponent(index, instance, new Translation { Value = new float3(0, 5f, 0) });

                CommandBuffer.DestroyEntity(index, entity); //摧毁生成器,可以有多种其他的实现方式,前期开发简单摧毁即可
            }
        }


        protected override JobHandle OnUpdate(JobHandle inputDependencies)
        {

            var job = new SpawnJob
            {
                CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()

            }.Schedule(this, inputDependencies);

          
            m_EntityCommandBufferSystem.AddJobHandleForProducer(job);

            return job;
        }
    }
}
