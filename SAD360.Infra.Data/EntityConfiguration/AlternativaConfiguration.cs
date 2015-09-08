using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.EntityConfiguration
{
    public class AlternativaConfiguration : EntityTypeConfiguration<Alternativa>
    {
        public AlternativaConfiguration()
        {
            Property(a => a.texto)
                .IsRequired()
                .HasMaxLength(200);

            HasRequired(a => a.questao)
                .WithMany()
                .HasForeignKey(a => a.QuestaoId)
                .WillCascadeOnDelete(true);
        }
    }
}
