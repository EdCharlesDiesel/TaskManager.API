using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Authentication;

namespace TaskManager.API.Services
{
    public interface IUserService
    {
        UserMaster Authenticate(string username, string password);
        UserMaster RegisterUser(UserMaster user, string password);
        bool CheckUserAvailabity(string userName);

        bool isUserExists(int userId);

        IEnumerable<UserMaster> GetAll();

        UserMaster GetById(string userId);

        void Update(UserMaster userParam, string password = null);
        void Delete(int userId);

    }
}
