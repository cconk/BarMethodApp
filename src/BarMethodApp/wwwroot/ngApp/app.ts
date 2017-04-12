namespace BarMethodApp {

    angular.module('BarMethodApp', ['ui.router', 'ngResource', 'ui.bootstrap','ui','ui.filters']).config((
        $stateProvider: ng.ui.IStateProvider,
        $urlRouterProvider: ng.ui.IUrlRouterProvider,
        $locationProvider: ng.ILocationProvider
    ) => {
        // Define routes
        $stateProvider
            .state('home', {
                url: '/',
                templateUrl: '/ngApp/views/home.html',
                controller: BarMethodApp.Controllers.HomeController,
                controllerAs: 'controller'
            })
            .state('addclass', {
                url: '/addclass',
                templateUrl: '/ngApp/views/addclass.html',
                controller: BarMethodApp.Controllers.AddClassController,
                controllerAs: 'controller'
            })
            .state('editclass', {
                url: '/editclass',
                templateUrl: '/ngApp/views/editclass.html',
                controller: BarMethodApp.Controllers.EditClassController,
                controllerAs: 'controller'
            })
            .state('deleteclass', {
                url: '/deleteclass',
                templateUrl: '/ngApp/views/deleteclass.html',
                controller: BarMethodApp.Controllers.DeleteClassController,
                controllerAs: 'controller'
            })
            .state('viewclass', {
                url: '/viewclass',
                templateUrl: '/ngApp/views/viewclass.html',
                controller: BarMethodApp.Controllers.ViewClassController,
                controllerAs: 'controller'
            })
            .state('login', {
                url: '/login',
                templateUrl: '/ngApp/views/login.html',
                controller: BarMethodApp.Controllers.LoginController,
                controllerAs: 'controller'
            })
            .state('register', {
                url: '/register',
                templateUrl: '/ngApp/views/register.html',
                controller: BarMethodApp.Controllers.RegisterController,
                controllerAs: 'controller'
            })
            .state('externalRegister', {
                url: '/externalRegister',
                templateUrl: '/ngApp/views/externalRegister.html',
                controller: BarMethodApp.Controllers.ExternalRegisterController,
                controllerAs: 'controller'
            }) 
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });

    angular.module('BarMethodApp').factory('authInterceptor', (
        $q: ng.IQService,
        $window: ng.IWindowService,
        $location: ng.ILocationService
    ) =>
        ({
            request: function (config) {
                config.headers = config.headers || {};
                config.headers['X-Requested-With'] = 'XMLHttpRequest';
                return config;
            },
            responseError: function (rejection) {
                if (rejection.status === 401 || rejection.status === 403) {
                    $location.path('/login');
                }
                return $q.reject(rejection);
            }
        })
    );

    angular.module('BarMethodApp').config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });

}
