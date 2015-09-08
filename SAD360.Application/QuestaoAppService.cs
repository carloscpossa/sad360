using SAD360.Application.Interfaces;
using SAD360.Domain.Entities;
using SAD360.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD360.Application
{
    public class QuestaoAppService : AppServiceBase<Questao>, IQuestaoAppService
    {
        private readonly IQuestaoService _questaoService;
        private readonly IAlternativaService _alternativaService;

        public QuestaoAppService(IQuestaoService questaoService, IAlternativaService alternativaService)
            : base(questaoService)
        {
            _questaoService = questaoService;
            _alternativaService = alternativaService;
        }

        public IEnumerable<Questao> buscaPorQuestionarioId(int idQuestionario)
        {
            return _questaoService.buscaPorQuestionarioId(idQuestionario);
        }


        public void incluiQuestao(int questionarioId, string textoQuestao, string alternativa1, string alternativa2, string alternativa3, string alternativa4, string alternativa5)
        {            
            Questao questao = new Questao();
            questao.QuestionarioId = questionarioId;
            questao.texto = textoQuestao;

            Alternativa alt1 = new Alternativa();
            Alternativa alt2 = new Alternativa();
            Alternativa alt3 = new Alternativa();
            Alternativa alt4 = new Alternativa();
            Alternativa alt5 = new Alternativa();

            if (!string.IsNullOrEmpty(alternativa1))
            {                
                alt1.texto = alternativa1;                
            }
            else
            {
                alt1 = null;
            }

            if (!string.IsNullOrEmpty(alternativa2))
            {                
                alt2.texto = alternativa2;                
            }
            else
            {
                alt2 = null;
            }

            if (!string.IsNullOrEmpty(alternativa3))
            {                
                alt3.texto = alternativa3;                
            }
            else
            {
                alt3 = null;
            }

            if (!string.IsNullOrEmpty(alternativa4))
            {                
                alt4.texto = alternativa4;                
            }
            else
            {
                alt4 = null;
            }

            if (!string.IsNullOrEmpty(alternativa5))
            {                
                alt5.texto = alternativa5;                
            }
            else
            {
                alt5 = null;
            }

            _questaoService.incluiQuestao(questao, alt1, alt2, alt3, alt4, alt5);
        }


        public void alteraQuestao(int questaoId, string textoQuestao, string alternativa1, string alternativa2, string alternativa3, string alternativa4, string alternativa5)
        {
            Questao questao = _questaoService.GetById(questaoId);
            questao.texto = textoQuestao;
            _questaoService.Update(questao);

            IEnumerable<Alternativa> alternativas = _alternativaService.buscaPorQuestaoId(questaoId);
            if (alternativas.Count() > 0)
            {
                alternativas = alternativas.OrderBy(a => a.AlternativaId);
            }

            int countAlternativas = 0;

            if (!string.IsNullOrEmpty(alternativa1))
            {
                countAlternativas++;
            }

            if (!string.IsNullOrEmpty(alternativa2))
            {
                countAlternativas++;
            }

            if (!string.IsNullOrEmpty(alternativa3))
            {
                countAlternativas++;
            }

            if (!string.IsNullOrEmpty(alternativa4))
            {
                countAlternativas++;
            }

            if (!string.IsNullOrEmpty(alternativa5))
            {
                countAlternativas++;
            }

            //Remover as alternativas que não devem mais existir
            if (alternativas.Count() > countAlternativas)
            {
                for (int i = alternativas.Count() - 1; i >= countAlternativas - 1; i--)
                {
                    _alternativaService.Remove(alternativas.ElementAt(i));
                }
            }

            for (int i = 0; i <= countAlternativas - 1; i++)
            {
                if (i <= alternativas.Count() - 1)
                {
                    Alternativa alt = alternativas.ElementAt(i);

                    switch (i)
                    {
                        case 0:
                        {
                            alt.texto = alternativa1;
                            break;
                        }
                        case 1:
                        {
                            alt.texto = alternativa2;
                            break;
                        }
                        case 2:
                        {
                            alt.texto = alternativa3;
                            break;
                        }
                        case 3:
                        {
                            alt.texto = alternativa4;
                            break;
                        }
                        case 4:
                        {
                            alt.texto = alternativa5;
                            break;
                        }
                    }

                    _alternativaService.Update(alt);
                }
                else
                {
                    Alternativa alt = new Alternativa();
                    alt.QuestaoId = questaoId;

                    switch (i)
                    {
                        case 0:
                            {
                                alt.texto = alternativa1;
                                break;
                            }
                        case 1:
                            {
                                alt.texto = alternativa2;
                                break;
                            }
                        case 2:
                            {
                                alt.texto = alternativa3;
                                break;
                            }
                        case 3:
                            {
                                alt.texto = alternativa4;
                                break;
                            }
                        case 4:
                            {
                                alt.texto = alternativa5;
                                break;
                            }
                    }

                    _alternativaService.Add(alt);
                }
            }

        }
    }
}
