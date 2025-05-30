﻿using AuthServer.Core.DTOs;
using FluentValidation;

namespace AuthServer.API.Validations
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required.");


        }
    }
}
