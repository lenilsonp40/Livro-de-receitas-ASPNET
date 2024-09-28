using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Service.LoggeUser
{
    public interface ILoggedUser
    {
        public Task<User> User();
    }
}
