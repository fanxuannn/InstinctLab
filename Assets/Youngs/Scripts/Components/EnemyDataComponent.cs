using Unity.Entities;

public struct EnemyAttributes : IComponentData
{
    /* 存储模型 */
    public Entity entity;

    /* 怪物属性 */
    public float hp;
    public float moveSpeed;
    public float armor;

}