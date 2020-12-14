using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAPI;
using System.Data.SqlClient;
using System.Net.Http;
using BankAPI.Models;

namespace TheBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private static HttpClient client = new HttpClient();
        private const string conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BankDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //static List<BankUser> BankUsers;
        const currentTime = DateTime.Now();

        // GET: api/<BankController>
        [HttpGet("BankUsers")]
        public List<BankUser> Get()
        {
            var result = new List<BankUser>();
            SqlConnection conn = new SqlConnection();
            const string sql = "SELECT * FROM BankUser";

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
        [HttpGet("Deposits")]
        public List<Deposit> Get(id)
        {
            var result = new List<Deposit>();
            SqlConnection conn = new SqlConnection();
            const string sql = "SELECT * FROM Deposit WHERE id =" + id;

            var databaseConnection = new SqlConnection(conn);
            databaseConnection.Open();
            var selectCommand = new SqlCommand(sql, databaseConnection);
            var reader = selectCommand.ExecuteReader();
            if (!reader.HasRows)
            { return result; }
            while (reader.Read())
            {
                result.Add(new BankUser(reader.GetInt32(0), reader.GetDateTime(1), reader.GetInt32(2)));
            }
            return result;
        }

        // POST: api/<BankController>
        [HttpPost("add-deposit")]
        public void Post(BankUser bankUser, int id)
        {
            if (bankUser.Amount == null | bankUser.Amount < 0)
            {
                 StatusCode(404);
            }
            else
            {
                List<string> data = new List<string>();
                data.Add(bankUser.Amount.ToString());
                data.Add(id.ToString());

                var response =  client.PostAsync("InterestRateFunctionURL", bankUser.Amount , id);
                
                
                var sql = "UPDATE Account SET Amount = @Amount WHERE id = @id";
                SqlParameter[] p = new SqlParameter[2];
                p[0] = new SqlParameter("@Amount", response);
                p[1] = new SqlParameter("@id", id);

                var databaseConnection = new SqlConnection(conn);
                databaseConnection.Open();

                var selectCommand = new SqlCommand(sql, databaseConnection);

         

                public void InsertIntoDepoTable()
                {

                    var sqlDepo = "INSERT INTO Deposit (id, UserId, CreatedAt, Amount )" +
                                  $"VALUES (NULL, '{id}', '{currentTime}', '{calculatedAmount}')";

                    selectCommand.ExecuteNonQuery();
                }

                StatusCode(200);
            }
        }

        // POST: api/<BankController>
        [HttpPost("create-loan")]
        public void Post(Account account, int id)
        {
            if (StatusCode(200))
            {
                try
                {
                    var response = client.PostAsync("LoanFunctionURL", account.Amount);
                    
                   

                    var sql = "INSERT INTO Loan (Id, UserId, CreatedAt, ModifiedAt, Amount)" +
                              $"VALUES (NULL, '{id}', '{currentTime}', '{currentTime}', '{response}')";

                    var databaseConnection = new SqlConnection(conn);
                    databaseConnection.Open();

                    var selectCommand = new SqlCommand(sql, databaseConnection);
                    selectCommand.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw;
                }
            }
            else
            {
                StatusCode(403);
            }
        }

        // GET api/<BankController>/5
        [HttpGet("{id}", Name = "Get")]
        public BankUser GetByID(int id)
        {
            BankUser myBankUser = new BankUser();

            string sql = "SELECT * FROM BankUser WHERE id =" + id;

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
            selectCommand.ExecuteNonQuery();
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
            var sql = "DELETE FROM BankUser where id =" + id;

            var databaseConnection = new SqlConnection(conn);
            databaseConnection.Open();

            var selectCommand = new SqlCommand(sql, databaseConnection);
            selectCommand.ExecuteNonQuery();

        }

        //POST api/<BankUserController>
        [HttpPost("InterestRate")]
        public void Post(int Amount)
        {
            return newAmount = Amount * 1.02;
        }

        //POST api/<BankUserController>
        [HttpPost("Loan")]
        public void Post(int Amount)
        {
            List<int> AccountList = new List<int>();

            var sql = "SELECT Amount FROM Account where id =" + id;

            var databaseConnection = new SqlConnection(conn);
            databaseConnection.Open();
            var selectCommand = new SqlCommand(sql, databaseConnection);
            var reader = selectCommand.ExecuteReader();
            if (!reader.HasRows)
            { return result; }
            while (reader.Read())
            {
                AccountList.Add((reader.GetInt32(0));
            }
            return result;

            if (Amount > )
            {
                
            }
        }
}
