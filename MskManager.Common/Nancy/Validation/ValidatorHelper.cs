using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace MskManager.Common.Nancy.Validation
{
    public class ValidatorHelper
    {
        private readonly IEnumerable<IValidator> _validators;

        public ValidatorHelper(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        public ValidationResult Validate<T>(T model)
        {
            var t = _validators.Select(x => x.GetType());
            var validator = _validators.SingleOrDefault(x => x.CanValidateInstancesOfType(typeof(T)));

            return validator.Validate(model);
        }
    }
}
