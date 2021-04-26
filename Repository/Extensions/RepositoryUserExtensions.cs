
using System.Linq;
using Entities.Models;


namespace Repository.Extensions
{
    public static class RepositoryUserExtensions
    {
        public static IQueryable<User> Search(this IQueryable<User> users, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return users;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return users.Where(e => e.UserName.ToLower().Contains(lowerCaseTerm));
        }

    }
}
