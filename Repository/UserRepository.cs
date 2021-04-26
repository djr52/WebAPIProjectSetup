using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Extensions;



namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
             : base(repositoryContext)
        {

        }
        public PagedList<User> GetAllUsers(UserParameters userParameters, bool trackChanges)
        {
            var user = FindAll(trackChanges)
                .Search(userParameters.SearchTerm)
                .OrderBy(e => e.UserName)
                .ToList();

            return PagedList<User>
            .ToPagedList(user, userParameters.PageNumber,userParameters.PageSize);

        }



        public User GetUser(Guid userId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(userId), trackChanges).SingleOrDefault();
        public void CreateUser(User user) => Create(user);

        public IEnumerable<User> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
            FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToList();

        public void DeleteUser(User user)
        {
            Delete(user);
        }
    }
}
