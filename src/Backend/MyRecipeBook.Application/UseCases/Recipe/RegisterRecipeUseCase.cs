using AutoMapper;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Service.LoggeUser;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe
{
    public class RegisterRecipeUseCase : IRegisterRecipeUseCase
    {
        private readonly IRecipeWriteOnlyRepository _repository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterRecipeUseCase(
        ILoggedUser loggedUser,
        IRecipeWriteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper
       )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggedUser = loggedUser;
            
        }

        public async Task<ResponseRegiteredRecipeJson> Execute(RequestRecipeJson request)
        {
            Validate(request);

            var loggedUser = await _loggedUser.User();

            throw new NotImplementedException();
        }

        private static void Validate(RequestRecipeJson request)
        {
            var result = new RecipeValidator().Validate(request);

            if (result.IsValid.IsFalse())
            {
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).Distinct().ToList());
            }
        }
    }
}
