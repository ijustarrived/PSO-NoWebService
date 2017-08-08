using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSO.Entities
{
    public class Globals
    {
        //Used as flag for updating user lock.
        //When logout is clicked, sometimes, it keeps updating a fe times right after clicking due to client interval running
        private static bool SessionIsAlive = true;

        public static void SetSessionIsAlive(bool isAlive)
        {
            SessionIsAlive = isAlive;
        }

        public static bool GetSessionIsAlive()
        {
            return SessionIsAlive;
        }
    }
}