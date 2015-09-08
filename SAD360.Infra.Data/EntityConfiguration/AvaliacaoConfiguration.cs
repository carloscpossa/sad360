using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.EntityConfiguration
{
    public class AvaliacaoConfiguration : EntityTypeConfiguration<Avaliacao>
    {
        public AvaliacaoConfiguration()
        {            

            HasRequired(a => a.funcionarioAvaliado)
                .WithMany()
                .HasForeignKey(a => a.FuncionarioAvaliadoId);

            HasRequired(a => a.funcionarioAvaliador)
                .WithMany()
                .HasForeignKey(a => a.FuncionarioAvaliadorId);

            HasRequired(a => a.questionario)
                .WithMany()
                .HasForeignKey(a => a.QuestionarioId);


            HasMany(a => a.respostas)
            .WithMany(b => b.avaliacoes)
            .Map(x =>
            {
                x.MapLeftKey("AvaliacaoId");
                x.MapRightKey("AlternativaId");
                x.ToTable("AlternativasAvaliacao");
            });                  
        }
    }
}
