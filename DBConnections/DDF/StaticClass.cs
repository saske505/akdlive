using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnections.DDF
{
    public static class StaticClass
    {

        private static UserDDF.UserCredentials s_User;

        public static UserDDF.UserCredentials s_LogedOnUser
        {
            get { return s_User; }
            set { s_User = value; }

        }

        public static bool ValidateLogedinUser()
        {
            if (s_User != null && s_User.ID != "") 
                return true;

            return false;
        }

        public static bool ValidateAKDAdmin()
        {
            if (s_User.Level == 1)
                return true;

            return false;
        }

        public static bool ValidateclientAdmin()
        {
            if (s_User.Level == 2)
                return true;

            return false;
        }

        public static bool ValidateCourier()
        {
            if (s_User.Level == 4)
                return true;

            return false;
        }


    }
}
