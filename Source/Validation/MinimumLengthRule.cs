using System;
using System.Collections.Generic;

using Junior.Common;

namespace Junior.Ddd.Validation
{
	/// <summary>
	/// Validates that a value is not longer than a minimum length.
	/// </summary>
	/// <typeparam name="TValidationError">A type, such as an enum, that represents a validation error.</typeparam>
	public class MinimumLengthRule<TValidationError> : Rule<TValidationError>
		where TValidationError : IComparable
	{
		private readonly int _minimumLength;
		private readonly TValidationError _validationError;
		private readonly string _value;

		/// <summary>
		/// Initializes a new instance of the <see cref="MinimumLengthRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="minimumLength">The minimum allowable length for <paramref name="value"/>.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public MinimumLengthRule(int minimumLength, TValidationError validationError, string value, bool allowNull, params IRule<TValidationError>[] dependsOn)
			: base(dependsOn)
		{
			validationError.ThrowIfNull("validationError");
			if (!allowNull)
			{
				value.ThrowIfNull("value");
			}

			_minimumLength = minimumLength;
			_validationError = validationError;
			_value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MinimumLengthRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="minimumLength">The minimum allowable length for <paramref name="value"/>.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		public MinimumLengthRule(int minimumLength, TValidationError validationError, string value, bool allowNull, TValidationError stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(stopValidatingOn, dependsOn)
		{
			validationError.ThrowIfNull("validationError");
			if (!allowNull)
			{
				value.ThrowIfNull("value");
			}

			_minimumLength = minimumLength;
			_validationError = validationError;
			_value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MinimumLengthRule{TValidationError}"/> class.
		/// </summary>
		/// <param name="minimumLength">The minimum allowable length for <paramref name="value"/>.</param>
		/// <param name="validationError">The validation error to use if validation fails.</param>
		/// <param name="value">The value to validate.</param>
		/// <param name="allowNull">Indicates if null values are allowed.</param>
		/// <param name="stopValidatingOn">Validation will immediately fail if a rule dependency results in a validation error equal to one in <paramref name="stopValidatingOn"/>.</param>
		/// <param name="dependsOn">Rules that must pass validation before validating this rule.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="validationError"/> is null.</exception>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null and <paramref name="allowNull"/> is false.</exception>
		public MinimumLengthRule(int minimumLength, TValidationError validationError, string value, bool allowNull, IEnumerable<TValidationError> stopValidatingOn, params IRule<TValidationError>[] dependsOn)
			: base(stopValidatingOn, dependsOn)
		{
			validationError.ThrowIfNull("validationError");
			if (!allowNull)
			{
				value.ThrowIfNull("value");
			}

			_minimumLength = minimumLength;
			_validationError = validationError;
			_value = value;
		}

		/// <summary>
		/// Performs rule validation.
		/// </summary>
		/// <returns>Validation errors if the rule failed to validate.</returns>
		protected override IEnumerable<TValidationError> OnValidate()
		{
			if (_value != null && _value.Length < _minimumLength)
			{
				yield return _validationError;
			}
		}
	}
}