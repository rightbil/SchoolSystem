using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSystem.Utility
{
   // Creating Custom attribute CustomAttributeAll 
   [AttributeUsage(AttributeTargets.All)]
   public class CustomAttributeAll : Attribute
   {
       private string name;
       private string action;

       public CustomAttributeAll(string name, string action)
       {
           this.name = name;
           this.action = action;


       }

       public string Name
       {
           get { return name; }
       }

       public string Action
       {
           get { return action; }
       }

   }
}

    
