using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;

namespace MyRecipeBook.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(deste => deste.Password, opt => opt.Ignore());
        }

        private void DomainToResponse()
        {
            CreateMap<Domain.Entities.User, ResponseUserProfileJson>();
                
        }
    }
}
