namespace Zenithar.BFF.Core;

public abstract class EntityBase<T>
{
    public T Id { get; }

    public EntityBase(T id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EntityBase<T>) obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Id);
    }

    public static bool operator ==(EntityBase<T>? left, EntityBase<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityBase<T>? left, EntityBase<T>? right)
    {
        return !Equals(left, right);
    }

    private bool Equals(EntityBase<T> other)
    {
        return EqualityComparer<T>.Default.Equals(Id, other.Id);
    }
}