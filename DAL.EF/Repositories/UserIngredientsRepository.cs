using DAL.Contracts;
using DAL.EF.Base;
using Domain;

namespace DAL.EF.Repositories;

public class UserIngredientsRepository:  EfBaseRepository<UserIngredient, ApplicationDbContext>, IUserIngredientsRepository
{
    public UserIngredientsRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}
