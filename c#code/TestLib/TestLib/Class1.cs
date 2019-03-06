using System;
using System.Text;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes

namespace TestLib
{
    public class Test
    {

        /// <summary>
        /// SIMPL+ can only execute the default constructor. If you have variables that require initialization, please
        /// use an Initialize method
        /// </summary>
        public Test()
        {
        }
        public int ReturnInt()
        {
            return (10);
        }
    }
}
