using Dapper;
using Web_Api_dapper.Model;
using Web_Api_dapper.Model.Data;
using System.Data;
using Web_Api_dapper.Repo;

namespace Web_Api_dapper.Repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DapperDBContext context;
        public EmployeeRepo(DapperDBContext context)
        {            this.context = context;
        }

        public async Task<string> Create(Employee employee)
        {
            string response = string.Empty;
            string query = "Insert into Employe (code,name,email,phone,designation) values (@code,@name,@email,@phone,@designation)";
            var parameters = new DynamicParameters();
            parameters.Add("code", employee.code, DbType.String);
            parameters.Add("name", employee.name, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            parameters.Add("phone", employee.phone, DbType.String);
            parameters.Add("designation", employee.designation, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
            }

        public async Task<List<Employee>> GetAll()
        {
            string query = "Select * From Employe";
            using (var connectin = this.context.CreateConnection())
            {
                var emplist = await connectin.QueryAsync<Employee>(query);
                return emplist.ToList();
            }
        }

        public async Task<List<Employee>> GetAllbyrole(string role)
        {
            //string query = "exec sp_getemployeebyrole @role";
            //using (var connectin = this.context.CreateConnection())
            //{
            //    var emplist = await connectin.QueryAsync<Employee>(query,new {role});
            //    return emplist.ToList();
            //}

            string query = "sp_getemployeebyrole";
            using (var connectin = this.context.CreateConnection())
            {
                var emplist = await connectin.QueryAsync<Employee>(query, new { role }, commandType: CommandType.StoredProcedure);
                return emplist.ToList();
            }

        }

        public async Task<Employee> Getbycode(int code)
        {
            string query = "Select * From Employe where code=@code";
            using (var connectin = this.context.CreateConnection())
            {
                var emplist = await connectin.QueryFirstOrDefaultAsync<Employee>(query, new { code });
                return emplist;
            }
        }

        public async Task<string> Remove(int code)
        {
            string response = string.Empty;
            string query = "Delete From Employe where code=@code";
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, new { code });
                response = "pass";
            }
            return response;
        }

        public async Task<string> Update(Employee employee, int code)
        {
            string response = string.Empty;
            string query = "update Employe set name=@name,email=@email,phone=@phone,designation=@designation where code=@code";
            var parameters = new DynamicParameters();
            parameters.Add("code", code, DbType.Int32);
            parameters.Add("name", employee.name, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            parameters.Add("phone", employee.phone, DbType.String);
            parameters.Add("designation", employee.designation, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "pass";
            }
            return response;
        }
    }
}

//namespace Web_Api_dapper.Repo
//{
//    public class EmployeeRepo : IEmployeeRepo
//    {
//        private readonly DapperDBContext context;
//        public EmployeeRepo(DapperDBContext context)
//        {
//            this.context = context;
//        }

//        public async Task<string> Create(Employee employee)
//        {
//            string response = string.Empty;
//            string query = "Insert into Employe (name,email,phone,designation) values (@name,@email,@phone,@designation)";
//            var parameters = new DynamicParameters();
//            parameters.Add("name", employee.name, DbType.String);
//            parameters.Add("email", employee.email, DbType.String);
//            parameters.Add("phone", employee.phone, DbType.String);
//            parameters.Add("designation", employee.designation, DbType.String);
//            using (var connectin = this.context.CreateConnection())
//            {
//                await connectin.ExecuteAsync(query, parameters);
//                response = "pass";
//            }
//            return response;
//        }

//        public async Task<List<Employee>> GetAll()
//        {
//            string query = "Select * From Employe";
//            using (var connectin = this.context.CreateConnection())
//            {
//                var emplist = await connectin.QueryAsync<Employee>(query);
//                return emplist.ToList();
//            }
//        }

//        public async Task<List<Employee>> GetAllbyrole(string role)
//        {
//            //string query = "exec sp_getemployeebyrole @role";
//            //using (var connectin = this.context.CreateConnection())
//            //{
//            //    var emplist = await connectin.QueryAsync<Employee>(query,new {role});
//            //    return emplist.ToList();
//            //}

//            string query = "sp_getemployeebyrole";
//            using (var connectin = this.context.CreateConnection())
//            {
//                var emplist = await connectin.QueryAsync<Employee>(query, new { role }, commandType: CommandType.StoredProcedure);
//                return emplist.ToList();
//            }

//        }

//        public async Task<Employee> Getbycode(int code)
//        {
//            string query = "Select * From Employe where code=@code";
//            using (var connectin = this.context.CreateConnection())
//            {
//                var emplist = await connectin.QueryFirstOrDefaultAsync<Employee>(query, new { code });
//                return emplist;
//            }
//        }

//        public async Task<string> Remove(int code)
//        {
//            string response = string.Empty;
//            string query = "Delete From Employe where code=@code";
//            using (var connectin = this.context.CreateConnection())
//            {
//                await connectin.ExecuteAsync(query, new { code });
//                response = "pass";
//            }
//            return response;
//        }

//        public async Task<string> Update(Employee employee, int code)
//        {
//            string response = string.Empty;
//            string query = "update Employe set name=@name,email=@email,phone=@phone,designation=@designation where code=@code";
//            var parameters = new DynamicParameters();
//            parameters.Add("code", code, DbType.Int32);
//            parameters.Add("name", employee.name, DbType.String);
//            parameters.Add("email", employee.email, DbType.String);
//            parameters.Add("phone", employee.phone, DbType.String);
//            parameters.Add("designation", employee.designation, DbType.String);
//            using (var connectin = this.context.CreateConnection())
//            {
//                await connectin.ExecuteAsync(query, parameters);
//                response = "pass";
//            }
//            return response;
//        }
//    }
//}