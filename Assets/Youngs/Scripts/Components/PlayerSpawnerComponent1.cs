using System.Collections.Generic;
using Unity.Entities;

namespace ILab.Youngs
{
    public struct PlayerSpawner : IComponentData
    {
        /* 存储玩家可能的所有的模型 */
        public Entity entity;

    }

}
