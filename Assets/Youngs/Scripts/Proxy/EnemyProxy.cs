using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.SceneManagement;

using Random = Unity.Mathematics.Random;

//@Youngs 2019年5月4日13:02:06


namespace ILab.Youngs
{
    //这是一个Spawner,因此它被转入到实体之中时,它作为一个生成组件而存在,而不是被当作要产生的目标直接使用
    [RequiresEntityConversion]
    public class EnemyProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        [Tooltip("这个字段里直接丢怪物的最终模型")]
        public GameObject m_gameObject;
        public float3 m_respawnPosition;
        /* 添加需要的属性 */

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(m_gameObject);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new EnemyAttributes
            {
                entity = conversionSystem.GetPrimaryEntity(m_gameObject),
            });
            dstManager.SetComponentData(entity, new Translation
            {
                Value = m_respawnPosition,
            });
        }
    }
}
