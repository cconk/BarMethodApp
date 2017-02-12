namespace BarMethodApp.Controllers {

    export class HomeController {
        //register and login availability on view need a new API for security and point this controller to it
       

    }

    export class AddClassController {
        public message = 'Please enter all of the information requested below to add a new class.';
        private BarClassResource;
        public newBarMethodClass;
        public instructors;
        public selectedInstructor;
        public selectedInstructorClasses;


        //get instructors
        public getInstructors() {
            this.instructors = this.BarClassResource.query();
            console.log(this.instructors);
        }

        public getClassesByInstructor() {
            console.log(this.selectedInstructor.userName);
            this.selectedInstructorClasses = this.BarClassResource.query({ id: this.selectedInstructor.userName });
            console.log(this.selectedInstructorClasses);

        }

        // save new items to database added on the list view
        private addBarMethodClass() {
            console.log(this.selectedInstructor.userName);
            console.log(this.newBarMethodClass);
            this.BarClassResource.save({ id: this.selectedInstructor.userName }, this.newBarMethodClass).$promise;
            this.selectedInstructor = null;
            this.newBarMethodClass = null;
        }

        //constructor to create items and test get items method
        constructor(private $resource: angular.resource.IResourceService) {
            this.BarClassResource = $resource('/api/barMethodClasses/:id');
            this.getInstructors();
        }
    }

    export class EditClassController {
        //edit classes adding exercises etc.
        public message = 'Select an instructor and a class to edit';
        public message2 = 'Select an instructor and class to see what they did';

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

        public getClassesByInstructor2() {
            if (this.selectedInstructor2 != null) {
                console.log(this.selectedInstructor2.userName);
                this.selectedInstructor2Classes = this.BarClassResource.query({ id: this.selectedInstructor2.userName });
                console.log(this.selectedInstructor2Classes);
            }
        }

        public getExercises() {
            this.selectedInstructorClassExercises = null;
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
            this.ExerciseResource2.save(this.selectedExercise).$promise;
            //this.getExercises();
            //this.getExercises2();
        }

        public addExercise(newExercise) {
            console.log(this.selectedInstructorClass.id);
            this.newExercise.description = newExercise.Description;
            console.log(this.newExercise.description);
        }

        constructor(private $resource: angular.resource.IResourceService) {
            this.BarClassResource = $resource('/api/barMethodClasses/:id');
            this.ExerciseResource = $resource('/api/exercise/:id');
            this.ExerciseResource2 = $resource('/api/exercise');
            //this.$http.get('api/exercise/').then((response)=>{
            //this.exercises = response.data;
            //console.log(this.exercises);
            //})
            this.getInstructors();
            this.getClassesByInstructor();
            this.getClassesByInstructor2();
            this.getExercises();
            this.getExercises2();
            //this.getClassesByInstructor();
            //this.getExercises();
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

        constructor(private accountService: BarMethodApp.Services.AccountService, private $location: ng.ILocationService) {
            this.getExternalLogins().then((results) => {
                this.externalLogins = results;
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

        constructor(private accountService: BarMethodApp.Services.AccountService, private $location: ng.ILocationService) { }
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

        constructor(private accountService: BarMethodApp.Services.AccountService, private $location: ng.ILocationService) { }
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

        constructor(private accountService: BarMethodApp.Services.AccountService, private $location: ng.ILocationService) { }

    }

    export class ConfirmEmailController {
        public validationMessages;

        constructor(
            private accountService: BarMethodApp.Services.AccountService,
            private $http: ng.IHttpService,
            private $stateParams: ng.ui.IStateParamsService,
            private $location: ng.ILocationService
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