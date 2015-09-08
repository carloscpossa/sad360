namespace SAD360.Infra.Data.Migrations
{
    using SAD360.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SAD360.Infra.Data.Contexto.SAD360Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SAD360.Infra.Data.Contexto.SAD360Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Set<Administrador>().AddOrUpdate(
                    
                new Administrador 
                {
                    FuncionarioId=1,
                    matricula="admin",
                    nome="Administrador",
                    senha="12345",
                    setor="Desenvolvimento"
                }


                );
                

                
        }
    }
}
