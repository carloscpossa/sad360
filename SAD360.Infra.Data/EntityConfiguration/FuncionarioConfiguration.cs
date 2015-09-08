using SAD360.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.EntityConfiguration
{
    public class FuncionarioConfiguration:EntityTypeConfiguration<Funcionario>
    {
        public FuncionarioConfiguration()
        {
            Property(f => f.nome)
                .IsRequired();

            Property(f => f.senha)
                .IsRequired();

            Property(f => f.matricula)
                .IsRequired();            

            HasOptional(f => f.responsavel)
                .WithMany()
                .HasForeignKey(f => f.GerenteId);

            HasOptional(f => f.administradorCadastro)
                .WithMany()
                .HasForeignKey(f => f.AdministradorId);
        }
    }
}
