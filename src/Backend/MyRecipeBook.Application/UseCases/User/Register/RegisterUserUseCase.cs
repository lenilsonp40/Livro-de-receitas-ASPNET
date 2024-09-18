using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            Validate(request);

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
            };
        }

        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            if ( result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage);

                throw new Exception();

                
            }
        }
    }
    
}
