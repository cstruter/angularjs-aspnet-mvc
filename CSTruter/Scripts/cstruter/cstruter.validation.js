(function () {

    angular
        .module('cstruter.validation', [])
            .directive('val', validation);

    function validation($compile) {
        return {
            restrict: 'A',
            require: ['^form', 'ngModel'],
            link: function (scope, element, attrs, controller) {

                // Make DOM changes

                var messageElement = buildMessages(element, attrs, controller[0]);
                setAttributes(element, attrs);

                // Prevent directive from being fired again for the same element

                element.removeAttr('data-val');

                // Apply DOM changes

                $compile(element)(scope.$parent);
                $compile(messageElement)(scope);
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

    function buildMessages(element, attrs, form) {
        var errors = [],
            set = function (name, property, message) {
                attrs[name] && errors.push({
                    property: property, message: message || attrs[name]
                });
            },

            // ng-messages root element for ng-message elements

            messageElement = angular.element('<div />').attr({
                'ng-messages': form.$name + '.' + attrs.name + '.$error',
                'ng-show': form.$name + '.$submitted || ' + form.$name + '.' + attrs.name + '.$dirty',
                'role': 'alert'
            });

        // Mapping ASP.NET MVC error messages to ng-messages

        set('valRegex', 'pattern');
        set('valMinlengthMin', 'minlength', attrs.valMinlength);
        set('valMaxlengthMax', 'maxlength', attrs.valMaxlength);
        set('valRequired', 'required');
        set('valEmail', 'email');
        set('valNumber', 'number');
        set('valRange', 'minlength');
        set('valRange', 'maxlength');

        // Append ng-message to ng-messages

        angular.forEach(errors, function (error) {
            messageElement.append(
                angular.element('<div />').attr({
                    // CSS error bases class and sub classes for each error type
                    'class': 'error error-' + error.property,
                    'ng-message': error.property
                }).text(error.message)
            );
        });

        // Add ng-messages to DOM, just after its validating element

        element.after(messageElement);

        return messageElement;
    }

})();