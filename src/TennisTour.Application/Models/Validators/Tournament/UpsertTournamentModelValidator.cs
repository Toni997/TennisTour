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
                .NotNull().NotEmpty()
                .Length(min: TournamentValidatorConfiguration.MinimumNameLength, max: TournamentValidatorConfiguration.MaximumNameLength)
                .WithMessage($"Name has to be at least {TournamentValidatorConfiguration.MinimumNameLength} characters long");

            RuleFor(t => t.Series)
                .NotNull().NotEmpty()
                .IsInEnum()
                .WithMessage("Series not valid");

            RuleFor(t => t.Surface)
                .NotNull().NotEmpty()
                .IsInEnum()
                .WithMessage("Surface not valid");

            RuleFor(t => t.NumberOfRounds)
                .NotNull().NotEmpty()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Number of rounds can't be negative");
        }
    }
}
