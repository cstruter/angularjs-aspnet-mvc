using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CSTruter.Web.Mvc.Html
{
    public static class ValidationExtensions
    {
        public static MvcHtmlString ValidationAngularMessageFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            IEnumerable<ModelClientValidationRule> validators = ModelValidatorProviders.Providers.
                GetValidators(modelMetadata, htmlHelper.ViewContext).
                SelectMany(v => v.GetClientValidationRules());

            object formName = htmlHelper.ViewData["formName"];
            string fieldName = modelMetadata.PropertyName;

            TagBuilder ngMessages = new TagBuilder("div");
            ngMessages.Attributes.Add("ng-messages", $"{formName}.{fieldName}.$error");
            ngMessages.Attributes.Add("ng-show", $"{formName}.$submitted || {formName}.{fieldName}.$dirty");
            ngMessages.Attributes.Add("role", "alert");
            
            foreach (ModelClientValidationRule rule in validators)
            {
                TagBuilder ngMessage = new TagBuilder("div");
                ngMessages.AddCssClass("error");
                if (rule.ValidationType == "regex")
                    ngMessage.Attributes.Add("ng-message", "pattern");
                else if (rule.ValidationType == "length" || rule.ValidationType == "range")
                {
                    if (rule.ValidationParameters.Keys.Contains("min"))
                        ngMessage.Attributes.Add("ng-message", "minlength");
                    if (rule.ValidationParameters.Keys.Contains("max"))
                        ngMessage.Attributes.Add("ng-message", "maxlength");
                }
                else
                    ngMessage.Attributes.Add("ng-message", rule.ValidationType);

                ngMessage.SetInnerText(rule.ErrorMessage);
                ngMessages.InnerHtml += ngMessage;
            }

            return new MvcHtmlString(ngMessages.ToString(TagRenderMode.Normal));
        }
    }
}