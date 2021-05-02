using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebService.Models
{
  public class User
  {
    public string Id { set; get; }

    public string FirstName { set; get; }

    public string LastName { set; get; }

    public string Username { set; get; }
    public string Password { get; set; }
  }
}