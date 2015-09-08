using SAD360.Domain.Entities;
using SAD360.Domain.Interfaces.Repositories;
using SAD360.Infra.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Infra.Data.Repositories
{
    public class AvaliacaoRepository : RepositoryBase<Avaliacao>, IAvaliacaoRepository
    {
        public IEnumerable<Avaliacao> buscaPorAvaliadorId(int id)
        {
            return this.db.Set<Avaliacao>().Where(a => a.FuncionarioAvaliadorId == id).ToList();
        }        

        public void SalvarRespostas(Avaliacao avaliacao)
        {
            using (var context = new SAD360Context()) 
            {
                Avaliacao aval = context.Set<Avaliacao>().Where(a => a.AvaliacaoId == avaliacao.AvaliacaoId).FirstOrDefault();
                aval.dataPreenchimento = avaliacao.dataPreenchimento;

                foreach (Alternativa resposta in avaliacao.respostas)
                {
                    Alternativa alt = context.Set<Alternativa>().Where(r => r.AlternativaId == resposta.AlternativaId).FirstOrDefault();
                    aval.respostas.Add(alt);
                }

                context.Entry<Avaliacao>(aval).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public IEnumerable<Avaliacao> pesquisa(int avaliadoId, DateTime dataInicioPreenchimento, DateTime dataTerminoPreenchimento, int? questionarioId)
        {
            IEnumerable<Avaliacao> avaliacoes;

            if (questionarioId != null)
            {
                avaliacoes = db.Set<Avaliacao>().Where(a => a.FuncionarioAvaliadoId == avaliadoId && a.dataPreenchimento >= dataInicioPreenchimento && a.dataPreenchimento <= dataTerminoPreenchimento && a.QuestionarioId == questionarioId).ToList();
            }
            else
            {
                avaliacoes = db.Set<Avaliacao>().Where(a => a.FuncionarioAvaliadoId == avaliadoId && a.dataPreenchimento >= dataInicioPreenchimento && a.dataPreenchimento <= dataTerminoPreenchimento).ToList();
            }
            

            return avaliacoes;
        }


        public IEnumerable<Avaliacao> buscaAvaliacaoSemPreenchimento()
        {
            return db.Set<Avaliacao>().Where(a => a.dataPreenchimento == null).ToList();
        }
    }
}
