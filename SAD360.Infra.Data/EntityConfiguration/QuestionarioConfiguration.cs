using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.EntityConfiguration
{
    public class QuestionarioConfiguration : EntityTypeConfiguration<Questionario>
    {
        public QuestionarioConfiguration()
        {
            Property(q => q.descricao)
                .HasMaxLength(200)
                .IsRequired();

            HasRequired(q => q.administrador)
                .WithMany()
                .HasForeignKey(q => q.AdministradorId);

            HasMany<Questao>(q => q.questoes)
                .WithRequired(q => q.questionario)
                .HasForeignKey(q => q.QuestionarioId);
                
        }
    }
}
