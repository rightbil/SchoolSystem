using System;
namespace SchoolSystem.Exceptions
{
    [Serializable]
    public class InvalidNameException : Exception
    {
        public InvalidNameException() { }
        public InvalidNameException(string name)
        : base(String.Format("Invalid Name {0}", name))
        {

        }

    }
    [Serializable]
    public class InvalidDateFormatException : Exception
    {
        public InvalidDateFormatException() { }
        /*public InvalidDateFormatException(string stringToDate):base(DateTime.ParseExact(stringToDate,new CultureInfo("en-Us"))
        {}*/
    }
}
