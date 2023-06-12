using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Enums;
using TennisTour.Core.Models;

namespace TennisTour.Application.Models.Validators.MatchSet
{
    public class UpsertMatchSetsValidator : AbstractValidator<UpsertMatchSetsModel>
    {
        public UpsertMatchSetsValidator()
        {
            RuleFor(x => x.Winner)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .IsInEnum()
                .WithMessage("Winner is invalid");

            RuleFor(x => x.MatchSets)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .ForEach(x => x.SetValidator(new UpsertMatchSetValidator()));
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<UpsertMatchSetsModel>.CreateWithOptions((UpsertMatchSetsModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
