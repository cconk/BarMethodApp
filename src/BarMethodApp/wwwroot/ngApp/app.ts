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
            .state('notFound', {
                url: '/notFound',
                templateUrl: '/ngApp/views/notFound.html'
            });

        // Handle request for non-existent route
        $urlRouterProvider.otherwise('/notFound');

        // Enable HTML5 navigation
        $locationProvider.html5Mode(true);
    });

    

}
