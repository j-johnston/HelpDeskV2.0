using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//added for mongo classes
using MongoDB.Driver;
using MongoDB.Kennedy;
using MongoDB.Driver.Linq;

namespace HelpDeskDAL
{
    public class DbContext : ConcurrentDataContext
    {

        public DbContext(string databaseName = "HelpdeskDB", string serverName = "localhost") : base(databaseName, serverName)
        { }

        public IQueryable<Employee> Employees
        {
            get
            {
                return this.Db.GetCollection<Employee>("employees").AsQueryable();
            }
        }

        public IQueryable<Department> Departments
        {
            get
            {
                return this.Db.GetCollection<Department>("departments").AsQueryable();
            }
        }

        public IQueryable<Problem> Problems
        {
            get
            {
                return this.Db.GetCollection<Problem>("problems").AsQueryable();
            }
        }
    }
}
