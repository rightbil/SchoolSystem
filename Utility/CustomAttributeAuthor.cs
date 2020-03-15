using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Utility
{
    /*[AttributeUsage(AttributeTargets.Assembly |
                    AttributeTargets.Class |
                    AttributeTargets.Constructor |
                    AttributeTargets.Enum |
                    AttributeTargets.Field |
                    AttributeTargets.Interface |
                    AttributeTargets.Method |
                    AttributeTargets.Parameter)]*/
    //This will make used for class and struct
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple=true)] // single use or multiuse
    // applied for class , method and field etc ; inherited will be posiible  
    //[AttributeUsage(AttributeTargets.All,Inherited = true)]
    public class CustomAttributeAuthor : Attribute
    {
            private string name;
            public double version;

            public CustomAttributeAuthor(string name, double version)
            {
                // positional parameter
                this.name = name;
                // named parameter
                this.version = version;
            }
        }

        
}