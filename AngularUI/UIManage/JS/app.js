var $baseURL = "/UIManage";
var app = angular.module('starter', ['ui.router', 'app.filters', 'app.services', 'app.directives', 'app.controllers'])

    // Gets executed during the provider registrations and configuration phase. Only providers and constants can be
    // injected here. This is to prevent accidental instantiation of services before they have been fully configured.
    .config(['$stateProvider', '$locationProvider', function ($stateProvider) {

        // UI States, URL Routing & Mapping. For more info see: https://github.com/angular-ui/ui-router
        // ------------------------------------------------------------------------------------------------------------

        $stateProvider
            .state('index', {
                url: '/',
                templateUrl: $baseURL + '/index.html'

            })
            .state('login', {
                url: '/login',
                layout: 'basic',
                templateUrl: $baseURL + '/Login.html',
                controller: 'LoginCtrl'
            });
    }]);