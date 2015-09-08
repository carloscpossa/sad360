using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SAD360.Infra.Data.EntityConfiguration;
using SAD360.Domain.Entities;

namespace SAD360.Infra.Data.Contexto
{
    public class SAD360Context:DbContext
    {
        public SAD360Context()
            :base("SAD360")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();            

            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new FuncionarioConfiguration());
            modelBuilder.Configurations.Add(new QuestionarioConfiguration());
            modelBuilder.Configurations.Add(new QuestaoConfiguration());
            modelBuilder.Configurations.Add(new AlternativaConfiguration());
            modelBuilder.Configurations.Add(new AvaliacaoConfiguration());
                       
        }
    }
}
