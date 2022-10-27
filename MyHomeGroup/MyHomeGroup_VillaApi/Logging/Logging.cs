﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHomeGroup_VillaApi.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if (type.ToLower() == "error")
            {
                Console.WriteLine("ERROR -" + message);

            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
