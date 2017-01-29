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
            this.selectedInstructorClasses = this.BarClassResource.get({ id: this.selectedInstructor.userName });
            console.log(this.selectedInstructorClasses);

        }

        // save new items to database added on the list view
        private addBarMethodClass() {
            console.log(this.selectedInstructor.userName);
            this.selectedInstructor = null;

            //this.BarClassResource.save(this.newBarMethodClass).$promise.then(() => {
            //    this.newBarMethodClass = null;
            //});
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
        public barClass;
        public barClasses;
        public barInstructor;
        public selectedBarClass;
        public barInstructor2;
        public selectedBarClass2;
        private ExerciseResource;
        public exercise;
        public exercises;
        public selectedExercise;
        public selectedBarClassId;


        public getBarClasses() {
            this.barClasses = this.BarClassResource.query();
            console.log(this.barClasses);
        }

        public getExercises() {
            this.exercises = this.ExerciseResource.query();
            console.log(this.exercises);
        }

    
        public selectBarClass() {
            //this.selectedBarClass = barClass;
            //this.exercises = this.selectedBarClass.exercises;
            console.log(this.selectedBarClass.$$hashKey);
            
        }

        public selectBarClass2(barClass2) {
            this.selectedBarClass2 = barClass2;
            console.log(this.selectedBarClass2);
        }

        private update(exercise) {
            this.selectedExercise = exercise;
            console.log(this.selectedExercise);
            console.log(this.selectedBarClass.id);
            this.$http.put('api/exercise/', this.selectedExercise);
            this.selectedExercise = null;
           
        }

        private save(exercise) {
            console.log(this.exercise);
            this.exercise = exercise;
            
            //this.exercise.Name = "";
            //this.exercise.Type = "";
            //this.exercise.Id = "";
            //this.exercise.barClassId = this.selectedBarClass.id;
            console.log(this.exercise);
            console.log(this.selectedBarClass.$$hashKey);
            
            
            this.ExerciseResource.save({ Id: this.selectedBarClass.$$hashKey }, exercise).$promise;  
            this.exercise = null;
        }

        constructor(private $resource: angular.resource.IResourceService, public $http: ng.IHttpService) {
            this.BarClassResource = $resource('/api/barMethodClasses/:id');
            this.ExerciseResource = $resource('/api/exercise/:id');
            this.$http.get('api/exercise/').then((response)=>{
                this.exercises = response.data;
                //console.log(this.exercises);
            })
            this.getBarClasses();
            this.getExercises();
        }
    }
}
