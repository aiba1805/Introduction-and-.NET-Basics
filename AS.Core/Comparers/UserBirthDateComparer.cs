using System.Collections.Generic;
using AS.Core.Models;

namespace AS.Core.Comparers
{
    public class UserBirthDateComparer : IComparer<User>
    {
        public int Compare(User x, User y)
        {
            if (x?.BirthDate > y?.BirthDate) return 1;
            if (x?.BirthDate < y?.BirthDate) return -1;
            return 0;
        }
    }
}