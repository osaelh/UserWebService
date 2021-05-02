using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServiceConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      localhost.UserWebService webService = new localhost.UserWebService();
      var loggedIn = false;


      try
      {
        while (true)
        {
          Console.WriteLine("Welcome");
          Console.WriteLine("Chose your operation");
          Console.WriteLine("Enter a create user");
          Console.WriteLine("Enter b to login");
          Console.WriteLine("Enter c to list users");
          

          string input = Console.ReadLine();
          string action = input.ToLower();

          switch (action)
          {
            case "a":
              {
                Console.Write("What is the user's username: ");
                string userName = Console.ReadLine();
                Console.Write("What is the user's first name: ");
                string firstName = Console.ReadLine();
                Console.Write("What is the person's last name: ");
                string lastName = Console.ReadLine();
                Console.Write("What is the user's password: ");
                string password = Console.ReadLine();

                webService.CreatUser(firstName, lastName, userName, password);
              }
              break;
            case "b":
              {
                Console.Write("What is the user's username: ");
                string userName = Console.ReadLine();
                Console.Write("What is the user's password: ");
                string password = Console.ReadLine();

                var user = webService.login(userName, password);
                if (user.Id == null)
                {
                  Console.Write("wrong credentials");
                  
                }
                else
                {
                  loggedIn = true;
                  Console.Write("welcome" +" "+ user.FirstName);
                }


              }
              break;
            case "c":
              {
                if (loggedIn == true)
                {
                var users = webService.UsersList();
                  foreach (var user in users)
                  {
                    Console.WriteLine(user.FirstName);
                    Console.WriteLine(user.LastName);
                    Console.WriteLine(user.Username);
                  }
                }
                else
                {
                  Console.WriteLine("You need to login");
                }
              }
              break;
            default:
              Console.WriteLine("unkown action!! try again");
              break;

          }
        }
      }
      catch (Exception ex)
      {

        Console.WriteLine(ex.Message);
      }
    }
  }
}
