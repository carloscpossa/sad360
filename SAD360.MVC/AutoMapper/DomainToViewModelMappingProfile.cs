
using AutoMapper;
using SAD360.Domain.Entities;
using SAD360.MVC.ViewModels;

namespace MSSGI.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<AdministradorViewModel, Administrador>();
            Mapper.CreateMap<AlternativaViewModel, Alternativa>();
            Mapper.CreateMap<AvaliacaoViewModel, Avaliacao>();
            Mapper.CreateMap<FuncionarioViewModel, Funcionario>();
            Mapper.CreateMap<FuncionarioViewModel, Administrador>();
            Mapper.CreateMap<FuncionarioViewModel, Gerente>();
            Mapper.CreateMap<GerenteViewModel, Gerente>();
            Mapper.CreateMap<QuestaoViewModel, Questao>();
            Mapper.CreateMap<QuestionarioViewModel, Questionario>();
        }
    }
}