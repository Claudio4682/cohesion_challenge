using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRequest.Application.Features.ServiceRequest.Commands.CreateServiceRequest
{
    public class AddServiceRequestValidator : AbstractValidator<AddServiceRequestCommand>
    {
        public AddServiceRequestValidator()
        {
            RuleFor(r => r.BuildingCode)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(15).WithMessage("{PropertyName} must not exceed 15 characters");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(r => r.CreatedBy)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
        }
    }
}
