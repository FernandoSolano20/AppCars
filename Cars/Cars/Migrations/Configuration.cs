namespace Cars.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Cars.Models.CarsDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Cars.Models.CarsDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var agencies = new List<Models.Agency>
            {
                new Models.Agency() { NameAgency = "Mapache Rent a Car San Sebastián" },
                new Models.Agency() { NameAgency = "Alamo Rent a Car" },
                new Models.Agency() { NameAgency = "Pepe" }
            };

            agencies.ForEach(agency => context.Agencies.AddOrUpdate(data => data.ID, agency));
            context.SaveChanges();

            var brands = new List<Models.Brand>
            {
                new Models.Brand()
                {
                    AgencyID = agencies.Single(agency => agency.ID == 16).ID,
                    BrandName = "BMW PRUEBAAAAAAAAAAAAAAAAAAAAA"
                }
                /*new Models.Brand()
                {
                    AgencyID = agencies.Single(student => student.ID == 1).ID,
                    BrandName = "BMW",
                },
                new Models.Brand()
                {
                    AgencyID = agencies.Single(student => student.ID == 2).ID,
                    BrandName = "Audi",
                },
                new Models.Brand()
                {
                    AgencyID = agencies.Single(student => student.ID == 2).ID,
                    BrandName = "Ferrari",
                }*/
            };

            foreach(Models.Brand brand in brands)
            {
                var brandInDB = context.Brands.Where(
                    data =>
                        data.AgencyID == brand.AgencyID).SingleOrDefault();
                if (brandInDB == null)
                {
                    context.Brands.Add(brand);
                }
            }
            context.SaveChanges();
            
            /*
            context.Brands.AddOrUpdate(data => data.BrandID,
                new Models.Brand() { AgencyID = }
            );*/

            /*StudentID = students.Single(s => s.LastName == "Alexander").ID, 
            CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, 
                    Grade = Grade.A*/
            
        }
    }
}
