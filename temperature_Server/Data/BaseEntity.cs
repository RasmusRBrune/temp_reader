using System.Security.Principal;

namespace temperature_Server.Data
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; set; }
        object IEntity.Id
        {
            get { return this.Id; }
            set { this.Id = (TId)value; }
        }

    }
    public interface IEntity<TId> : IEntity
    {
        new TId Id { get; set; }
    }

    public interface IEntity
    {
        object Id { get; set; }
    }
}
