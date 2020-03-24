using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasalohWebAPI.ApiModels.Lawyer;
using TasalohWebAPI.ApiModels.Lawyers;
using TasalohWebAPI.EFCore.Model;

namespace TasalohWebAPI.Validation
{
	public class LawyerValidator : AbstractValidator<CreateLawyerModel>
	{
		public LawyerValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull().Length(0, 50);
			RuleFor(x => x.NationalId).NotEmpty().NotNull();
			RuleFor(x => x.NationalId_Image).NotEmpty().NotNull();
			RuleFor(x => x.Address).NotEmpty().NotNull();
			RuleFor(x => x.SyndicateCard_Image).NotEmpty().NotNull();
			RuleFor(x => x.SyndicateNumber).NotEmpty().NotNull();
			RuleFor(x => x.University_Id).NotEmpty().NotNull();
			RuleFor(x => x.LawyerWorkingAreas).NotEmpty().NotNull();
			RuleFor(x => x.LawyerContacts).NotNull().ValidateLawyerContact();

		}
	}

	public class LawyerContactsValidator : PropertyValidator
	{
		public LawyerContactsValidator() : base("Contacts must have one main contact")
		{
		}
		protected override bool IsValid(PropertyValidatorContext context)
		{
			List<LawyerContactModel> LawyerContacts = (List<LawyerContactModel>)context.PropertyValue;
			if (LawyerContacts.Count > 0 && LawyerContacts.Any(a=>a.Is_Main == true))
				return true;

			return false;
		}
	}
}