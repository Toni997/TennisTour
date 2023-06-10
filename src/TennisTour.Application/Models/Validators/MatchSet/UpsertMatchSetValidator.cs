using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.MatchSet;
using TennisTour.Core.Models;

namespace TennisTour.Application.Models.Validators.MatchSet
{
    public class UpsertMatchSetValidator : AbstractValidator<UpsertMatchSetModel>
    {
        public UpsertMatchSetValidator()
        {
            RuleFor(x => x.ContenderOneGamesCount)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Games won is required")
                .InclusiveBetween(0, 7).WithMessage("Set score has to be between 1 and 7")
            .Must((model, ContenderOneGamesCount) =>
                model.ContenderTwoGamesCount == 5 || model.ContenderTwoGamesCount == 6)
            .When(x => x.ContenderOneGamesCount == 7, ApplyConditionTo.CurrentValidator)
            .WithMessage("One can win 7 games only if the opponent won 5 or 6 games");

            RuleFor(x => x.ContenderTwoGamesCount)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Games won is required")
                .InclusiveBetween(0, 7).WithMessage("Set score has to be between 1 and 7")
                .Must((model, contenderTwoGamesCount) =>
                    model.ContenderOneGamesCount == 5 || model.ContenderOneGamesCount == 6)
                .When(x => x.ContenderTwoGamesCount == 7, ApplyConditionTo.CurrentValidator)
                .WithMessage("One can win 7 games only if the opponent won 5 or 6 games");

            RuleFor(x => x.LoserTiebreakPoints)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0).When(x => x.LoserTiebreakPoints.HasValue)
                .WithMessage("Loser tiebreak points won can't be lower than 0")
                .Must((model, loserTiebreakPoints) =>
                    model.ContenderOneGamesCount == 7 && model.ContenderTwoGamesCount == 6 ||
                    model.ContenderOneGamesCount == 6 && model.ContenderTwoGamesCount == 7
                ).When(x => x.LoserTiebreakPoints.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Tiebreak is only played on 7-6 or 6-7 set score");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<UpsertMatchSetModel>.CreateWithOptions((UpsertMatchSetModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
