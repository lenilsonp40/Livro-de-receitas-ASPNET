﻿using FluentValidation;
using MyRecipeBook.Application.SharedValidators;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                    .WithMessage(ResourceMessagesException.NAME_EMPTY);

            RuleFor(user => user.Email)
                .NotEmpty()
                    .WithMessage(ResourceMessagesException.EMAIL_EMPTY);
            //pra facilitar nos testes só vai executar se o email for informado
                When(user => string.IsNullOrEmpty(user.Email) == false, () =>
                {
                    RuleFor(user => user.Email).EmailAddress()
                    .WithMessage(ResourceMessagesException.EMAIL_INVALID);
                });

            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
                 
        }
    }
}
