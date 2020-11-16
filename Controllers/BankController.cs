using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAPI;
using System.Data.SqlClient;
using BankAPI.Models;

namespace TheBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private const string conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BankDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //static List<BankUser> BankUsers;

        // GET: api/<BankController>
        [HttpGet("BankUsers")]
        public List<BankUser> Get()
        {
            var result = new List<BankUser>();
            SqlConnection ko = new SqlConnection();
            const string sql = "select * from BankUser";

            var databaseConnection = new SqlConnection(conn);
            databaseConnection.Open();
            var selectCommand = new SqlCommand(sql, databaseConnection);
            var reader = selectCommand.ExecuteReader();
            if (!reader.HasRows)
            { return result; }
            while (reader.Read())
            {
                result.Add(new BankUser(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2)));
            }
            return result;
        }

        // GET: api/<BankController>
        //[HttpGet("Amount")]
        //public BankUser Get(int amount, int id)
        //{
        //    return ;
        //}

        // GET api/<BankController>/5
        [HttpGet("{id}", Name = "Get")]
        public BankUser GetByID(int id)
        {
            BankUser myBankUser = new BankUser();

            string sql = "select * from BankUser where id =" + id;

            var databaseConnection = new SqlConnection(conn);
            databaseConnection.Open();
            var selectCommand = new SqlCommand(sql, databaseConnection);
            var reader = selectCommand.ExecuteReader();

            if (!reader.HasRows)
            { return myBankUser; }
            while (reader.Read())
            {
                myBankUser = new BankUser(reader.GetInt32(0), reader.GetDateTime(1), reader.GetDateTime(2));

            }

            return myBankUser;
        }

        //POST api/<BankUserController>
        [HttpPost]
        public void Post(BankUser myBankUser)
        {
            var sql = "INSERT INTO BankUser (Id, UserId, CreatedAt, ModifiedAt)" +
                      $"VALUES (NULL, '{myBankUser.UserId}', '{myBankUser.CreatedAt}', '{myBankUser.ModifiedAt}')";

            var databaseConnection = new SqlConnection(conn);
            databaseConnection.Open();

            var selectCommand = new SqlCommand(sql, databaseConnection);
            var reader = selectCommand.ExecuteReader();
        }

        // PUT api/<BankController>/5
        [HttpPut("{id}")]
        public void Put(int id, BankUser bankUserInput)
        {
            var sql = "UPDATE BankUser SET UserId = @UserId, CreatedAt = @CreatedAt, ModifiedAt = @ModifiedAt WHERE id = @id";
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@UserId", bankUserInput.UserId);
            p[1] = new SqlParameter("@CreatedAt", bankUserInput.CreatedAt);
            p[2] = new SqlParameter("@ModifiedAt", bankUserInput.ModifiedAt);
            p[3] = new SqlParameter("@id", id);

            var databaseConnection = new SqlConnection(conn);
            databaseConnection.Open();

            var selectCommand = new SqlCommand(sql, databaseConnection);
            selectCommand.ExecuteNonQuery();

        }

        // DELETE api/<BankController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var sql = "DELETE from BankUser where id =" + id;

            var databaseConnection = new SqlConnection(conn);
            databaseConnection.Open();

            var selectCommand = new SqlCommand(sql, databaseConnection);
            selectCommand.ExecuteNonQuery();

        }
    }
}
