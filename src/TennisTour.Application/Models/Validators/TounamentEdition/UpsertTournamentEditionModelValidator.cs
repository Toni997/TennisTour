using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Models.TournamentEdition;

namespace TennisTour.Application.Models.Validators.TounamentEdition
{
    public class UpsertTournamentEditionModelValidator : AbstractValidator<UpsertTournamentEditionModel>
    {
        public UpsertTournamentEditionModelValidator()
        {
            RuleFor(te => te.DateStart)
                .NotNull().WithMessage("Start Date is required");

            RuleFor(te => te.DateEnd)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("End Date is required")
                .GreaterThan(x => x.DateStart)
                .When(x => x.DateStart.HasValue)
                .WithMessage("End Date has to be after Start Date");

            RuleFor(te => te.TournamentId)
                .NotNull().WithMessage("Start Date is required")
                .NotEqual(Guid.Empty).WithMessage("Tournament not valid");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<UpsertTournamentEditionModel>.CreateWithOptions((UpsertTournamentEditionModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
