using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using UserWebService.Models;

namespace UserWebService
{
  /// <summary>
  /// Summary description for UserWebService
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [System.ComponentModel.ToolboxItem(false)]
  // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
   [System.Web.Script.Services.ScriptService]
  public class UserWebService : System.Web.Services.WebService
  {

    [WebMethod]
    public User CreatUser(string username , string password, string firstname, string lastname)
    {

      User user = new User();

      var time = DateTime.Now;

      string cs = ConfigurationManager.ConnectionStrings["WebService"].ConnectionString;
      using (SqlConnection con = new SqlConnection(cs))
      {
        SqlCommand cmd = new SqlCommand("sp_CreateUser", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Firstname", firstname);
        cmd.Parameters.AddWithValue("@Lastname", lastname);
        cmd.Parameters.AddWithValue("@Username", username);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@Createdate", time);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
      }

      return user;
    }


    [WebMethod]
    public User login(string username, string password)
    {

      User user = new User();

      user.Username = username;
      user.Password = password;
      string cs = ConfigurationManager.ConnectionStrings["WebService"].ConnectionString;
      /*         UserName = new SqlParameter();

               Password = new SqlParameter();*/

      SqlConnection con = new SqlConnection(cs);



      
      SqlCommand cmd = new SqlCommand("sp_login", con);

     
      cmd.CommandType = CommandType.StoredProcedure;

      SqlParameter parameter = new SqlParameter();
      cmd.Parameters.AddWithValue("@Username", username);
      cmd.Parameters.AddWithValue("@Password ", password);
      con.Open();

      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {
        user.Id = rdr[0].ToString();

        user.FirstName = rdr[1].ToString();
        user.LastName = rdr["LastName"].ToString();
        user.Username = rdr["Username"].ToString();




      }

      return user;

    }

    [WebMethod]
    public List<User> UsersList()
    {

      List<User> users = new List<User>();

      string cs = ConfigurationManager.ConnectionStrings["WebService"].ConnectionString;


      SqlConnection con = new SqlConnection(cs);


      SqlCommand cmd = new SqlCommand("sp_UsersList", con);


      cmd.CommandType = CommandType.StoredProcedure;

      con.Open();

      SqlDataReader rdr = cmd.ExecuteReader();
      while (rdr.Read())
      {

        User user = new User();

        user.Id = rdr[0].ToString();
        user.FirstName = rdr[1].ToString();
        user.LastName = rdr["LastName"].ToString();
        user.Username = rdr["Username"].ToString();


        users.Add(user);

      }

      return users;

    }








  }
}