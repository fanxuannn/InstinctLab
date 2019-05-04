using System;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


//@Youngs 2019年4月20日17:23:21 新技术的里程碑
//@Youngs 2019年4月25日21:48:56
//@Youngs 2019年5月4日19:57:02

/*
 *  1~2 该脚本用于添加玩家角色的各种组件
 *  3   现在该脚本作为一个生成器而不再是代理,也就是说被转入进Entity容器的不是玩家这个物体
 * 
 */

namespace ILab.Youngs
{
    [RequiresEntityConversion]
    public class PlayerProxy : MonoBehaviour,IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
        [Tooltip("这个字段里直接丢玩家的最终模型")]
        public GameObject gameObject;

        public float3 m_respawnPosition;
        public float m_playerMoveSpeed;

        
        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(gameObject);
        }


        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {

            dstManager.AddComponentData(entity, new PlayerSpawner
            {
                entity = conversionSystem.GetPrimaryEntity(gameObject)
            });

        }
    }
}
