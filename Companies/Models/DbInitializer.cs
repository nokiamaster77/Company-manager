using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Companies.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<CompanyContext>
    {
        protected override void Seed(CompanyContext db)
        {
            var ids = new Guid[7];
            for (var i = 0; i < 7; i++)
            {
                ids[i] = Guid.NewGuid();
            }

            var companies = new List<Company>
            {
                new Company{CompanyId=ids[0], Name = "Company1", Earning = 25},
                new Company{CompanyId=ids[1], Name = "Company2", Earning = 13, ParentId = ids[0]},
                new Company{CompanyId=ids[2], Name = "Company3", Earning = 5, ParentId = ids[1]},
                new Company{CompanyId=ids[3], Name = "Company4", Earning = 10, ParentId = ids[0]},
                new Company{CompanyId=ids[4], Name = "Company5", Earning = 10},
                new Company{CompanyId=ids[5], Name = "Company6", Earning = 15, ParentId = ids[4]},
                new Company{CompanyId=ids[6], Name = "Company7", Earning = 5, ParentId = ids[4]}
            };

            db.Companies.AddRange(companies);
            db.SaveChanges();
        }
    }
}