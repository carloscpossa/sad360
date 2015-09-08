using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.EntityConfiguration
{
    public class QuestaoConfiguration : EntityTypeConfiguration<Questao>
    {
        public QuestaoConfiguration()
        {
                        
            Property(q => q.texto)
                .IsRequired()
                .HasMaxLength(200);

            HasRequired(q => q.questionario)
                .WithMany()
                .HasForeignKey(q => q.QuestionarioId);

            HasMany<Alternativa>(q => q.alternativas)
                .WithRequired(q => q.questao)
                .HasForeignKey(q => q.QuestaoId)
                .WillCascadeOnDelete(true);
        }
    }
}
