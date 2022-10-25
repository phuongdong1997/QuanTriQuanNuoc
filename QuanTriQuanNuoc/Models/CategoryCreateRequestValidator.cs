using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanTriQuanNuoc.Models
{
    public class CategoryCreateRequestValidator : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryCreateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id value is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("category name is required");
        }
    }
}