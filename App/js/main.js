var app = angular.module('app', [
  'ui.router',
  'ngAnimate',
  'ui.bootstrap',
  'jtt_flickr'
]);

app.run(['$rootScope', '$state', '$stateParams',
  function ($rootScope,   $state,   $stateParams) {
	  $rootScope.$state = $state;
	  $rootScope.$stateParams = $stateParams;
		$rootScope.Utils = {
			keys: Object.keys
		};
  }
]);

app.config(['$stateProvider', '$urlRouterProvider',
	function ($stateProvider, $urlRouterProvider) {
		$urlRouterProvider.otherwise("/");

	  $stateProvider.state('dashboard', {
				url: '/',
				templateUrl: 'js/views/dashobard.html',
				controller: 'DashboardController'
			})
			.state('profiles', {
				url: '/profiles',
				templateUrl: 'js/views/profiles.html',
				controller: 'ProfilesController'
			})
      .state('about', {
        url: '/about',
        templateUrl: 'js/views/about.html'
      });

	  $stateProvider.state('profile', {
				abstract: true,
				url: '/profiles/{name:[a-z]{2,}}',
				templateUrl: 'js/views/profile.html',
				controller: 'ProfileController'
			})
			.state('profile.about', {
				url: '',
				templateUrl: 'js/views/profile.about.html',
				controller: 'ProfileAboutController'
			})
			.state('profile.gallery', {
				url: '/gallery',
				templateUrl: 'js/views/profile.gallery.html',
				controller: 'ProfileGalleryController'
			});
	}
]);

// === main ====================================================================
app.controller('DashboardController', ['$scope', '$stateParams', '$http',
  function ($scope, $http) {
    $scope.formData = {};
  }
]);

// === profiles ================================================================
app.controller('ProfilesController', ['$scope', '$stateParams', '$http',
	function ($scope, $stateParams, $http) {
    $http.get('/api/profile')
      .success(function (data) {
        $scope.profiles = data;
      })
      .error(function (data) {
        console.log('Error: ' + data);
      });
	}
]);

app.controller('ProfileController', ['$scope', '$stateParams', '$http', '$q',
  '$sce', function ($scope, $stateParams, $http, $q, $sce) {
    $scope.name = $stateParams.name;
    $scope._profileDfr = $q.defer();

    $http.get('/api/profile/' + $scope.name)
      .success(function(_data) {
        $scope.profile = _data;
        $scope.profile.description = $sce.trustAsHtml($scope.profile.description);
        console.log($scope.profile);
        $scope._profileDfr.resolve();
      })
      .error(function (_data) {
        console.log('Error:', _data);
      });
  }
]);

app.controller('ProfileAboutController', ['$scope', '$stateParams', '$http',
	function ($scope, $stateParams, $http) {
	}
]);

app.controller('ProfileGalleryController',
  ['$scope', '$stateParams', '$http', '$q', 'flickrFactory',
  function ($scope, $stateParams, $http, $q, flickrFactory) {
    $scope._profileDfr.promise.then(function(){
      flickrFactory.getImagesByTags({
        tags: $scope.profile.flickrTags,
        tagmode: "all"
      }).then(function(_data){
        $scope.pictures = _data.data.items;
      }).catch(function (_data) {
        console.log('Error:', _data);
      });
    });
  }
]);
