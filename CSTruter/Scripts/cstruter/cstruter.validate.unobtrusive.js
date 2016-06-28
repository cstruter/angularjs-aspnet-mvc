(function () {

    angular
        .module('cstruter.validate.unobtrusive', [])
            .directive('val', validation);

    function validation($compile) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs) {

                // Make DOM changes
                setAttributes(element, attrs);

                // Prevent directive from being fired again for the same element
                element.removeAttr('data-val');

                // Apply DOM changes
                $compile(element)(scope.$parent);
            }
        };
    }

    function setAttributes(element, attrs) {
        var attributes = {},
            set = function (name, key, value) {
                attrs[name] && (attributes[key] = value || attrs[name]);
            };

        // Attribute mappings, ASP.NET MVC to ng-attributes
        set('valRegex', 'ng-pattern', '/^' + attrs.valRegexPattern + '$/');
        set('valMinlengthMin', 'ng-minlength');
        set('valMaxlengthMax', 'ng-maxlength');
        set('valRequired', 'ng-required', true);
        set('valRange', 'ng-minlength', attrs.valRangeMin);
        set('valRange', 'ng-maxlength', attrs.valRangeMax);

        // Assign Attributes 
        element.attr(attributes);
    }

})();