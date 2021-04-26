using System.Linq;
using Entities.Models;


namespace Repository.Extensions
{
    public static class RepositoryOrganizationExtension
    {
        public static IQueryable<Organization> FilterCity(this IQueryable<Organization>organizations, string cityTerm)
        {
            if(string.IsNullOrWhiteSpace(cityTerm))
                return organizations;
            return organizations.Where(e => (e.City == cityTerm ));
        }



        public static IQueryable<Organization> Search(this IQueryable<Organization> organizations, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return organizations;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return organizations.Where(e => e.OrgName.ToLower().Contains(lowerCaseTerm));
        }
    }
}
