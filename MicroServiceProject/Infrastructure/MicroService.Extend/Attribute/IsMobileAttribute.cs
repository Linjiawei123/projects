
using MicroService.Method.Check;
using System.ComponentModel.DataAnnotations;

namespace MicroService.Extend
{
    /// <summary>
    /// 是否手机号码
    /// </summary>
    public class IsMobileAttribute : ValidationAttribute
    {
        private readonly bool _allowEmpty;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="allowEmpty">是否允许空字符串</param>
        public IsMobileAttribute(bool allowEmpty = false)
        {
            _allowEmpty = allowEmpty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //当前字段显示名
            string thisPropertyName = validationContext.DisplayName;

            thisPropertyName ??= validationContext.MemberName;

            if (value == null)
                return ValidationResult.Success;

            if (_allowEmpty && value.ToString() == "")
                return ValidationResult.Success;

            if (ValidHepler.IsMobileNumberSimple(value.ToString()))
                return ValidationResult.Success;

            string errMessage = string.IsNullOrWhiteSpace(ErrorMessage) ? null : string.Format(ErrorMessage, thisPropertyName);

            if (string.IsNullOrWhiteSpace(errMessage))
                errMessage = thisPropertyName + "输入有误";

            return new ValidationResult(errMessage, new List<string> { thisPropertyName });
        }
    }
}
