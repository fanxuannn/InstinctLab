using System.Collections.Generic;
using Unity.Entities;

public struct EnemySpawnerData : IComponentData
{
    /* 存储模型,因为要产生的怪物不止一种 */
    public List<Entity> entity;

    /* 生成条件和控制 */
    public int type;
    public float spawnTime;
    public float spawnNumber;
    

}