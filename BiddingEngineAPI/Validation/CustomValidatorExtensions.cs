using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TasalohWebAPI.ApiModels.Engineer;
using TasalohWebAPI.ApiModels.Lawyer;
using TasalohWebAPI.EFCore.Model;

namespace TasalohWebAPI.Validation
{
    public static class CustomValidatorExtensions
    {
        public static IRuleBuilderOptions<T, List<EngineerContactModel>> ValidateEngineerContact<T>(
        this IRuleBuilder<T, List<EngineerContactModel>> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new EngineerContactsValidator());
        }

        public static IRuleBuilderOptions<T, List<LawyerContactModel>> ValidateLawyerContact<T>(
        this IRuleBuilder<T, List<LawyerContactModel>> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new LawyerContactsValidator());
        }
    }
}
