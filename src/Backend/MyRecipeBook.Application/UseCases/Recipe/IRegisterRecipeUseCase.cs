namespace MyRecipeBook.Application.UseCases.Recipe
{
    public interface IRegisterRecipeUseCase
    {
        public Task<ResponseRegiteredRecipeJson> Execute(RequestRegisterRecipeFormData request);
    }
}
