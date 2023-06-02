using System.ComponentModel.DataAnnotations;

namespace GermanCourseRegistration.EntityModels;

public abstract class Entity<TId>
{
    protected Entity(TId id) => Id = id;

    protected Entity() { }

    [Key]
    public TId Id { get; init; }
}
