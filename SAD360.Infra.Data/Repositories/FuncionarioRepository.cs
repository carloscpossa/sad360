using SAD360.Domain.Entities;
using SAD360.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.Repositories
{    
    public class FuncionarioRepository : RepositoryBase<Funcionario>, IFuncionarioRepository
    {
        public Funcionario buscaPorMatriculaSenha(string matricula, string senha)
        {
            return db.Set<Funcionario>().Where(f => f.matricula == matricula && f.senha == senha).FirstOrDefault();
        }

        public void Update(Funcionario obj, bool administrador, bool gerente)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            if (administrador)
            {
                var sql = " Update Funcionario Set Discriminator = @Tipo Where FuncionarioId = @Id ";

                db.Database.ExecuteSqlCommand(sql,
                    new SqlParameter("Tipo", "Administrador"),
                    new SqlParameter("Id", obj.FuncionarioId));
            }
            else
            {
                if (gerente)
                {
                    var sql = " Update Funcionario Set Discriminator = @Tipo Where FuncionarioId = @Id ";

                    db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("Tipo", "Gerente"),
                        new SqlParameter("Id", obj.FuncionarioId));
                }
                else
                {
                    var sql = " Update Funcionario Set Discriminator = @Tipo Where FuncionarioId = @Id ";

                    db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("Tipo", "Funcionario"),
                        new SqlParameter("Id", obj.FuncionarioId));
                }
            }
        }


        public IEnumerable<Funcionario> buscaPorGerente(int idGerente)
        {
            return this.db.Set<Funcionario>().Where(f => f.GerenteId == idGerente).ToList();
        }
    }
}
