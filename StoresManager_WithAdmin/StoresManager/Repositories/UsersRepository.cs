using StoresManager.Entities;
using StoresManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoresManager.Repositories
{
    public class UsersRepository: BaseRepository<User>
    {
        

        public static bool IsManager(int UserId)
        {
            StoresRepository repo = new StoresRepository();
            Store item = repo.GetFirstOrDefault(u => u.ManagerId == UserId);

            return item != null;
        }
        //consider chanhing this bit


    }
}
