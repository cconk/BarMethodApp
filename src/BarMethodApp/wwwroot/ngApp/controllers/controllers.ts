namespace BarMethodApp.Controllers {

    export class HomeController {
        //register and login availability on view need a new API for security and point this controller to it
        public message = 'Welcome to Korganize. An app to help you design, review and share Bar Method choreographies';

        private BarClassResource;
        public instructors;
        public selectedInstructor;
        public selectedInstructorClasses;
        public selectedInstructorClass;
        public selectedInstructorClassExercises;
        private ExerciseResource;

        public getInstructors() {
            this.instructors = this.BarClassResource.query();
            console.log(this.instructors);
        }

        public getClassesByInstructor() {
            
            if (this.selectedInstructor != null) {
                console.log(this.selectedInstructor.userName);
                this.selectedInstructorClasses = this.BarClassResource.query({ id: this.selectedInstructor.userName });
                console.log(this.selectedInstructorClasses);
                this.selectedInstructorClassExercises = null;
            }
        }

        public getExercises() {
            if (this.selectedInstructorClass != null) {
                console.log(this.selectedInstructorClass.id);
                this.selectedInstructorClassExercises = this.ExerciseResource.query({ id: this.selectedInstructorClass.id });
                console.log(this.selectedInstructorClassExercises);
            }
        }

        constructor(public $state: ng.ui.IStateService, private $resource: angular.resource.IResourceService) {
            this.BarClassResource = $resource('/api/barMethodClasses/:id');
            this.ExerciseResource = $resource('/api/exercise/:id');
            this.getInstructors();

        }

    }

    export class AddClassController {
        public message = 'Please enter all of the information requested below to add a new class';
        private BarClassResource;
        public newBarMethodClass;
        public instructors;
        public selectedInstructor;
        public selectedInstructorClasses;
        public currentUserName;

        //get instructors
        public getInstructors() {
            this.instructors = this.BarClassResource.query();
            console.log(this.instructors);
        }

        public getUserName() {
            this.currentUserName = this.accountService.getUserName();
            console.log(this.currentUserName);
        }

        public getClassesByInstructor() {
            console.log(this.currentUserName);
            this.selectedInstructorClasses = this.BarClassResource.query({ id: this.currentUserName });
            console.log(this.selectedInstructorClasses);
        }

        // save new items to database added on the list view
        private addBarMethodClass() {
            console.log(this.currentUserName);
            console.log(this.newBarMethodClass);
            this.BarClassResource.save({ id: this.currentUserName }, this.newBarMethodClass).$promise;
            this.selectedInstructor = null;
            this.newBarMethodClass = null;
        }

        //constructor to create items and test get items method
        constructor(public $state: ng.ui.IStateService, private accountService: BarMethodApp.Services.AccountService, private $resource: angular.resource.IResourceService) {
            this.BarClassResource = $resource('/api/barMethodClasses/:id');
            this.getInstructors();
            this.getUserName();
        }
    }

    export class EditClassController {
        //edit classes adding exercises etc.
        public message = 'Select a class to edit';
        public message2 = 'Select an instructor and class to review';

        private BarClassResource;
        public instructors;
        public selectedInstructor;
        public selectedInstructorClasses;
        public selectedInstructorClass;
        public selectedInstructorClassExercises;
        public selectedInstructor2;
        public selectedInstructor2Classes;
        public selectedInstructor2Class;
        public selectedInstructor2ClassExercises;
        private ExerciseResource;
        private ExerciseResource2;
        public newExercise;
        public selectedExercise;
        public currentUserName;

        public getInstructors() {
            this.instructors = this.BarClassResource.query();
            console.log(this.instructors);
        }

        public getUserName() {
            this.currentUserName = this.accountService.getUserName();
            console.log(this.currentUserName);
        }

        public getClassesByInstructor() {
                console.log(this.currentUserName);
                this.selectedInstructorClasses = this.BarClassResource.query({ id: this.currentUserName });
                console.log(this.selectedInstructorClasses);
                this.selectedInstructorClassExercises = null;
        }

        public getClassesByInstructor2() {
            if (this.selectedInstructor2 != null) {
                console.log(this.selectedInstructor2.userName);
                this.selectedInstructor2Classes = this.BarClassResource.query({ id: this.selectedInstructor2.userName });
                console.log(this.selectedInstructor2Classes);
                this.selectedInstructor2ClassExercises = null;
            }
        }

        public getExercises() {
             if (this.selectedInstructorClass != null) {
                console.log(this.selectedInstructorClass.id);
                this.selectedInstructorClassExercises = this.ExerciseResource.query({ id: this.selectedInstructorClass.id });
                console.log(this.selectedInstructorClassExercises);
             }
        }
             
        public getExercises2(){   
           if (this.selectedInstructor2Class != null) {
                console.log(this.selectedInstructor2Class.id);
                this.selectedInstructor2ClassExercises = this.ExerciseResource.query({ id: this.selectedInstructor2Class.id });
                console.log(this.selectedInstructor2ClassExercises);
            }
        }
    
        public updateExercise(exercise) {
            this.selectedExercise = exercise;
            console.log(this.selectedExercise);
            this.ExerciseResource2.save(this.selectedExercise).$promise.then(() => {
                this.getExercises();
                this.getExercises2();    
            });
        }

        public addExercise(newExercise) {
            console.log(this.selectedInstructorClass.id);
            this.newExercise = newExercise;
            console.log(this.newExercise);
            this.ExerciseResource.save({ id: this.selectedInstructorClass.id }, this.newExercise).$promise.then(() => {
                this.getExercises();
                this.newExercise.description = null;
            });
            console.log(this.newExercise.description);
        }

        public deleteExercise(exercise) {
            this.selectedExercise = exercise;
            this.ExerciseResource.delete({ id: this.selectedExercise.id }).$promise.then(() => {
                this.getExercises(); 
                this.getExercises2();   
                });
        }

        constructor(public $state: ng.ui.IStateService, private accountService: BarMethodApp.Services.AccountService, private $resource: angular.resource.IResourceService) {
            this.BarClassResource = $resource('/api/barMethodClasses/:id');
            this.ExerciseResource = $resource('/api/exercise/:id');
            this.ExerciseResource2 = $resource('/api/exercise');
            this.getInstructors();
            this.getUserName();
            this.getClassesByInstructor();
        }
    }

    export class DeleteClassController {
        public message = 'Select a class to delete';
        private BarClassResource;
        public instructors;
        public selectedInstructor;
        public selectedInstructorClasses;
        public selectedInstructorClass;

        public getInstructors() {
            this.instructors = this.BarClassResource.query();
            console.log(this.instructors);
        }

        public getClassesByInstructor() {
            if (this.selectedInstructor != null) {
                console.log(this.selectedInstructor.userName);
                this.selectedInstructorClasses = this.BarClassResource.query({ id: this.selectedInstructor.userName });
                console.log(this.selectedInstructorClasses);
            }
        }

        public deleteBarMethodClass() {
            if (this.selectedInstructorClass != null) {
                console.log(this.selectedInstructorClass.id);
                this.BarClassResource.delete({ id: this.selectedInstructorClass.id }).$promise.then(() => {
                    this.getClassesByInstructor();
                    this.selectedInstructorClass = null;
                });
            }
        }

        constructor(public $state: ng.ui.IStateService, private $resource: angular.resource.IResourceService) {
            this.BarClassResource = $resource('/api/barMethodClasses/:id');
            this.getInstructors();
        }
    }

    export class ViewClassController {
        public message = 'Select a class to view or teach';
        public BarClassResource;
        public instructors;
        public selectedInstructor;
        public selectedInstructorClasses;
        public selectedInstructorClass;
        public selectedInstructorClassExercises;
        public ExerciseResource;
        public currentUserName;

        public getInstructors() {
            this.instructors = this.BarClassResource.query();
            console.log(this.instructors);
        }

        public getUserName() {
            this.currentUserName = this.accountService.getUserName();
            console.log(this.currentUserName);
        }

        public getClassesByInstructor() {
            console.log(this.currentUserName);
            this.selectedInstructorClasses = this.BarClassResource.query({ id: this.currentUserName });
            console.log(this.selectedInstructorClasses);
            this.selectedInstructorClassExercises = null;
        }

        public getExercises() {
            if (this.selectedInstructorClass != null) {
                console.log(this.selectedInstructorClass.id);
                this.selectedInstructorClassExercises = this.ExerciseResource.query({ id: this.selectedInstructorClass.id });
                console.log(this.selectedInstructorClassExercises);
            }
        }

        constructor(public $state: ng.ui.IStateService, private $resource: angular.resource.IResourceService, private accountService: BarMethodApp.Services.AccountService) {
            this.BarClassResource = $resource('/api/barMethodClasses/:id');
            this.ExerciseResource = $resource('/api/exercise/:id');
            this.getInstructors();
            this.getUserName();
        }
    }

    export class AccountController {
        public externalLogins;

        public getUserName() {
            return this.accountService.getUserName();
        }

        public getClaim(type) {
            return this.accountService.getClaim(type);
        }

        public isLoggedIn() {
            return this.accountService.isLoggedIn();
        }

        public logout() {
            this.accountService.logout();
            this.$location.path('/');
        }

        public getExternalLogins() {
            return this.accountService.getExternalLogins();
        }

        constructor(public $state: ng.ui.IStateService, private accountService: BarMethodApp.Services.AccountService, private $location: ng.ILocationService) {
            this.getExternalLogins().then((results) => {
                this.externalLogins = results;
                console.log(this.externalLogins);
            });
        }
    }

    angular.module('BarMethodApp').controller('AccountController', AccountController);


    export class LoginController {
        public loginUser;
        public validationMessages;

        public login() {
            this.accountService.login(this.loginUser).then(() => {
                this.$location.path('/');
            }).catch((results) => {
                this.validationMessages = results;
            });
            console.log(this.loginUser);
        }

        constructor(public $state: ng.ui.IStateService, private accountService: BarMethodApp.Services.AccountService, private $location: ng.ILocationService) { }
    }

    export class RegisterController {
        public registerUser;
        public validationMessages;

        public register() {
            this.accountService.register(this.registerUser).then(() => {
                this.$location.path('/');
            }).catch((results) => {
                this.validationMessages = results;
            });
        }

        constructor(public $state: ng.ui.IStateService, private accountService: BarMethodApp.Services.AccountService, private $location: ng.ILocationService) { }
    }

    export class ExternalRegisterController {
        public registerUser;
        public validationMessages;

        public register() {
            this.accountService.registerExternal(this.registerUser.email)
                .then((result) => {
                    this.$location.path('/');
                }).catch((result) => {
                    this.validationMessages = result;
                });
        }

        constructor(public $state: ng.ui.IStateService, private accountService: BarMethodApp.Services.AccountService, private $location: ng.ILocationService) { }

    }

    export class ConfirmEmailController {
        public validationMessages;

        constructor(
            private accountService: BarMethodApp.Services.AccountService,
            private $http: ng.IHttpService,
            private $stateParams: ng.ui.IStateParamsService,
            private $location: ng.ILocationService,
            public $state: ng.ui.IStateService 
        ) {
            let userId = $stateParams['userId'];
            let code = $stateParams['code'];
            accountService.confirmEmail(userId, code)
                .then((result) => {
                    this.$location.path('/');
                }).catch((result) => {
                    this.validationMessages = result;
                });
        }
    }
}