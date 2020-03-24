using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasalohWebAPI.ApiModels.Engineer;
using TasalohWebAPI.EFCore.Model;

namespace TasalohWebAPI.Validation
{
	public class EngineerValidator : AbstractValidator<CreateEngineerModel>
	{
		public EngineerValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull().Length(0, 50);
			RuleFor(x => x.NationalId).NotEmpty().NotNull();
			RuleFor(x => x.NationalId_Image).NotEmpty().NotNull();
			RuleFor(x => x.Address).NotEmpty().NotNull();
			RuleFor(x => x.ConsultationCertificate_Image).NotEmpty().NotNull();
			RuleFor(x => x.ConsultNumber_FPart).NotEmpty().NotNull();
			RuleFor(x => x.ConsultNumber_SPart).NotEmpty().NotNull();
			RuleFor(x => x.OfficeType_Id).NotEmpty().NotNull();
			RuleFor(x => x.Password).NotEmpty().NotNull();
			RuleFor(x => x.RecordNumber).NotEmpty().NotNull();
			RuleFor(x => x.Specialization_Id).NotEmpty().NotNull();
			RuleFor(x => x.SyndicateCard_Image).NotEmpty().NotNull();
			RuleFor(x => x.SyndicateNumber_FPart).NotEmpty().NotNull();
			RuleFor(x => x.SyndicateNumber_SPart).NotEmpty().NotNull();
			RuleFor(x => x.University_Id).NotEmpty().NotNull();
			RuleFor(x => x.EngineerContacts).NotNull().ValidateEngineerContact();

		}
	}

	public class EngineerContactsValidator : PropertyValidator
	{
		public EngineerContactsValidator() : base("Contacts must have one main contact")
		{
		}
		protected override bool IsValid(PropertyValidatorContext context)
		{
			List<EngineerContactModel> engineerContacts = (List<EngineerContactModel>)context.PropertyValue;
			if (engineerContacts.Count > 0 && engineerContacts.Any(a=>a.Is_Main == true))
				return true;

			return false;
		}
	}
}