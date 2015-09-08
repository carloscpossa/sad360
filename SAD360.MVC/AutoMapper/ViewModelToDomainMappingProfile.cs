
using AutoMapper;
using SAD360.Domain.Entities;
using SAD360.MVC.ViewModels;

namespace MSSGI.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Administrador, AdministradorViewModel>();
            Mapper.CreateMap<Alternativa, AlternativaViewModel>();
            Mapper.CreateMap<Avaliacao, AvaliacaoViewModel>();
            Mapper.CreateMap<Funcionario, FuncionarioViewModel>();
            Mapper.CreateMap<Gerente, GerenteViewModel>();
            Mapper.CreateMap<Questao, QuestaoViewModel>();
            Mapper.CreateMap<Questionario, QuestionarioViewModel>();
            
        }
    }
}