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
    public class QuestionarioAppService : AppServiceBase<Questionario>, IQuestionarioAppService
    {
        private readonly IQuestionarioService _questionarioService;

        public QuestionarioAppService(IQuestionarioService questionarioService)
            : base(questionarioService)
        {
            _questionarioService = questionarioService;
        }
    }
}
