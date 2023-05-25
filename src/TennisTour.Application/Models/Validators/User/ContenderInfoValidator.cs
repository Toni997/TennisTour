using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.User;

namespace TennisTour.Application.Models.Validators.User
{
    public class ContenderInfoValidator : AbstractValidator<ContenderInfoModel>
    {
        public ContenderInfoValidator() {
            RuleFor(e => e.FirstName).Cascade(CascadeMode.Stop).NotEmpty().Must(e => !e.Contains(' ')).WithMessage("First name should not have any space.")
                .Must(e => Char.IsUpper(e.First())).WithMessage("First name should start with an uppercase letter.");
            RuleFor(e => e.LastName).Cascade(CascadeMode.Stop).NotEmpty().Must(e => !e.Contains(' ')).WithMessage("Last name should not have any space.")
               .Must(e => Char.IsUpper(e.First())).WithMessage("Last name should start with an uppercase letter.");
            RuleFor(e => e.DateOfBirth).NotNull().GreaterThan(DateTime.Now.AddYears(-UserValidatorConfiguration.MinYearFromNowBirth)).LessThan(DateTime.Now.AddYears(-UserValidatorConfiguration.MaxYearFromNowBirth));
            RuleFor(e => e.HeightCm).NotNull().GreaterThan(UserValidatorConfiguration.MinHeight).LessThan(UserValidatorConfiguration.MaxHeight);
            RuleFor(e => e.TurnedProOn).NotNull().LessThanOrEqualTo(DateTime.Now);
            RuleFor(e => e.RetiredOn).LessThanOrEqualTo(DateTime.Now);
            RuleFor(e => e.WeightKg).NotNull().LessThan(UserValidatorConfiguration.MaxWeight).GreaterThan(UserValidatorConfiguration.MinWeight);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ContenderInfoModel>.CreateWithOptions((ContenderInfoModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
