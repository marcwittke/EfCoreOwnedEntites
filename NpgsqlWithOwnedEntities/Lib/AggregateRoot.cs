namespace NpgsqlWithOwnedEntities.Lib
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot()
        {
        }

        protected AggregateRoot(int id) : base(id)
        {
        }

        public int TenantId { get; set; }
    }
}