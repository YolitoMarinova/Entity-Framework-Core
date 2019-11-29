namespace Composite.Data.Interfaces
{
    using Composite.Data.Models;

    public interface IGiftOperations
    {
        void Add(GiftBase gift);

        void Remove(GiftBase gift);
    }
}
