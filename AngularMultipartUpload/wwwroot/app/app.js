(function () {
    "use strict";

    angular
        .module('app', [])
        .directive('fileModel', fileModelDirective)
        .controller('appController', appController);

    function fileModelDirective($parse) {
        return {
            restrict: 'A',
            scope: {
                fileModel: '=fileModel'
            },
            link: function (scope, element, attrs) {
                element.bind('change', function () {
                    var files = this.files || undefined;
                    scope.$apply(function (scope) {
                        scope.fileModel = files;
                    });
                });
            }
        };
    }

    function appController($scope, $http, $httpParamSerializerJQLike) {
        $scope.submitFiles = submitFiles;

        function submitFiles() {
            var formData = new FormData();

            for (var i = 0; i < $scope.files.length; i++) {
                formData.append('files', $scope.files[i], $scope.files[i].name);
            }

            $http({
                url: '/api/uploads',
                method: 'POST',
                headers: {
                    'Content-Type': undefined
                },
                data: formData
            });
        }
    }
})();