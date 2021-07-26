using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceRequest.Application.Features.ServiceRequest.Commands.UpdateServiceRequest
{
    class UpdateServiceRequestValidator : AbstractValidator<UpdateServiceRequestCommand>
    {
        public UpdateServiceRequestValidator()
        {
            RuleFor(r => r.BuildingCode)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(15).WithMessage("{PropertyName} must not exceed 15 characters");

            RuleFor(r => r.Description)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters");

            RuleFor(r => r.Status)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();

            RuleFor(r => r.LastModifiedBy)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull();
        }
    }
}
