﻿using AutoMapper;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessTokenGenerator _accessTokenGenerator;


        public RegisterUserUseCase(IUserReadOnlyRepository readOnlyRepository, 
            IUserWriteOnlyRepository writeOnlyRepository,
            IMapper mapper,
            IPasswordEncripter passwordEncripter,
            IUnitOfWork unitOfWork,
            IAccessTokenGenerator accessTokenGenerator)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _mapper = mapper;
            _passwordEncripter = passwordEncripter;
            _unitOfWork = unitOfWork;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {                    
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);

            user.Password = _passwordEncripter.Encrypt(request.Password);

            user.UserIdentifier = Guid.NewGuid();

            await _writeOnlyRepository.Add(user);

            await _unitOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = request.Name,
                Tokens = new ResponseTokensJson
                {
                    AccessToken = _accessTokenGenerator.Generate(user.UserIdentifier)
                }
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (emailExist) 
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
            }

            if ( result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);

                
            }
        }
    }
    
}
