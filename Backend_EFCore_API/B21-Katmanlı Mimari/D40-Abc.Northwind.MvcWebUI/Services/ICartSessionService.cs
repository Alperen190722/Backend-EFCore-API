using Abc.Northwind.Entities.Concrete;

namespace D40_Abc.Northwind.MvcWebUI.Services
{
    public interface ICartSessionService
    {
        Cart GetCart();
        void SetCart(Cart cart);
    }
}
