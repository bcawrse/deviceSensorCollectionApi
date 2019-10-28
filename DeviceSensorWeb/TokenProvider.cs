using DeviceSensorWeb.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeviceSensorWeb
{
    public class TokenProvider
    {
        public string LoginUser(string UserID, string Password)
        {
            //Get user details for the user who is trying to login
            var user = UserList.SingleOrDefault(x => x.USERID == UserID);

            //Authenticate User, Check if it’s a registered user in Database 
            if (user == null)
                return null;

            //If it is registered user, check user password stored in Database
            //For demo, password is not hashed. It is just a string comparision 
            //In reality, password would be hashed and stored in Database. 
            //Before comparing, hash the password again.
            if (Password == user.PASSWORD)
            {
                //Authentication successful, Issue Token with user credentials 
                //Provide the security key which is given in 
                //Startup.cs ConfigureServices() method 
                var key = Encoding.ASCII.GetBytes
                ("txq2Pvn6hzk36pIvkg24A9N1dyXbNuXEjPwehgH8QmWVr07xnnSoKh2N66EOyeV");
                //Generate Token for user 
                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:45092/",
                    audience: "http://localhost:45092/",
                    claims: GetUserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    //Using HS256 Algorithm to encrypt Token  
                    signingCredentials: new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            else
            {
                return null;
            }
        }

        //Using hard coded collection list as Data Store for demo. 
        //In reality, User details would come from Database.
        //private List UserList = new List
        //{
        //    new User { USERID = "jsmith@email.com" },
        //    new User { USERID = "srob@email.com", PASSWORD = "test" }
        //};
        private List<User> UserList = new List<User>
        {
            new User { USERID = "test", PASSWORD = "test" },
            new User { USERID = "theorem", PASSWORD = "demo" }
        };

        //Using hard coded collection list as Data Store for demo. 
        //In reality, User data comes from Database or other Data Source 
        private IEnumerable<Claim> GetUserClaims(User user)
        {
            IEnumerable<Claim> claims = new Claim[]
            {
                new Claim("USERID", user.USERID),
                new Claim(ClaimTypes.Name, user.USERID)
            };

            return claims;
        }
    }
}