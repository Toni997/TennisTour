using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Models.TournamentEdition;

namespace TennisTour.Application.Models.Validators.TounamentEditions
{
    public class UpsertTournamentEditionModelValidator : AbstractValidator<UpsertTournamentEditionModel>
    {
        public UpsertTournamentEditionModelValidator()
        {
            RuleFor(te => te.DateStart)
                .NotNull().WithMessage("Start Date is required");

            RuleFor(te => te.DateEnd)
                .NotNull().WithMessage("End Date is required")
                .GreaterThan(t => t.DateStart)
                .WithMessage("End Date has to be after Start Date");
        }
    }
}
