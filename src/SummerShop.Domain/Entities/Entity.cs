namespace SummerShop.Domain.Entities;

public abstract class Entity : IEntity
{
    public int Id { get; }
}

interface IEntity
{
    int Id { get; }
}