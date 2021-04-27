using System.Linq.Dynamic.Core;
using System.Linq;
using Entities.Models;
using System;
using System.Reflection;
using System.Text;
using Repository.Extensions.Utility;


namespace Repository.Extensions
{
    public static class RepositoryOrganizationExtension
    {

        public static IQueryable<Organization> Sort(this IQueryable<Organization> organizations, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return organizations.OrderBy(e => e.OrgName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Organization>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return organizations.OrderBy(e => e.OrgName);

            return organizations.OrderBy(orderQuery);
        }


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
