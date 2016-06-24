(function () {

	angular
		.module('app.person')
		.controller('PersonController', PersonController);

	function PersonController()
	{
	    this.save = function ($event, isValid) {
			if (!isValid) {
				$event.preventDefault();
			}
		}
	}

})();