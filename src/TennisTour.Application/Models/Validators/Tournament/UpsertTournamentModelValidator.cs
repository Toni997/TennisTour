using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.TodoItem;
using TennisTour.Application.Models.Tournament;
using TennisTour.Core.Enums;

namespace TennisTour.Application.Models.Validators.Tournament
{
    public class UpsertTournamentModelValidator : AbstractValidator<UpsertTournamentModel>
    {
        public UpsertTournamentModelValidator()
        {
            RuleFor(t => t.Name)
                .NotNull().WithMessage("Name is required")
                .Length(min: TournamentValidatorConfiguration.MinimumNameLength, max: TournamentValidatorConfiguration.MaximumNameLength)
                .WithMessage($"Name has to be at least {TournamentValidatorConfiguration.MinimumNameLength} characters long");

            RuleFor(t => t.Series)
                .NotNull().WithMessage("Series is required")
                .IsInEnum()
                .WithMessage("Series not valid");

            RuleFor(t => t.Surface)
                .NotNull().WithMessage("Surface is required")
                .IsInEnum()
                .WithMessage("Surface not valid");

            RuleFor(t => t.NumberOfRounds)
                .NotNull().WithMessage("Number of Rounds is required")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Number Of Rounds can't be less than 1");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<UpsertTournamentModel>.CreateWithOptions((UpsertTournamentModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
