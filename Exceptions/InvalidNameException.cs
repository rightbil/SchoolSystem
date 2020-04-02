using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolSystem.Exceptions
{
    [Serializable]
  public class InvalidNameException : Exception
    {
        public InvalidNameException(){}
        public InvalidNameException(string name)
        :base(String.Format("Invalid Name {0}", name))
        {
            
        }

    }
}
