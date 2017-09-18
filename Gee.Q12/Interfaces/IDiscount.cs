using Gee.Q12.Models;

namespace Gee.Q12.Interfaces
{
    public interface IDiscount<T> where T:Cart
    {
        void Apply(T cart);
    }
}
