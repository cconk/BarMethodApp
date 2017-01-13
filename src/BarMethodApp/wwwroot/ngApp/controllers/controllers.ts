namespace BarMethodApp.Controllers {

    export class HomeController {
        //register and login availability on view need a new API for security and point this controller to it
       
    }

    export class AddClassController {
        public message = 'Please enter all of the information requested below to add a new class.';

        private BarClassResource;
        public barClass;
        public barClasses;
        

        // get items from database
        public getBarClasses() {
            this.barClasses = this.BarClassResource.query();
            //console.log(this.barClasses);
        }

        // save new items to database added on the list view
        private save() {
            this.BarClassResource.save(this.barClass).$promise.then(() => {
                this.barClass = null;
                this.getBarClasses();
            });
        }
        

        //constructor to create items and test get items method
        constructor(private $resource: angular.resource.IResourceService) {
            this.BarClassResource = $resource('/api/barMethodView/:id');
            this.getBarClasses();
            //console.log(this.barClasses);
        } 
    }

    export class EditClassController {
        //edit classes adding exercises etc.
        public message = 'Select an instructor and a class to edit';
        public message2 = 'Select an instructor and class to see what they did';

        private BarClassResource;history
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
            //console.log(this.barClasses);
        }

        public pickInstructor() {
            //console.log(this.barInstructor);
            //console.log(this.barInstructor2);
        }

        public selectBarClass(barClass) {
            this.selectedBarClass = barClass;
            console.log(this.selectedBarClass);
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
            this.exercise.Name = "";
            this.exercise.Type = "";
            this.exercise.barClassId = this.selectedBarClass.id;
            console.log(this.exercise);
            console.log(this.selectedBarClass.id);
            this.selectedBarClassId = this.selectedBarClass.id;
            console.log(this.selectedBarClassId);
            this.ExerciseResource.save({ Id: this.selectedBarClassId }, exercise).$promise;  
            this.exercise = null;
        }

        constructor(private $resource: angular.resource.IResourceService, public $http: ng.IHttpService) {
            this.BarClassResource = $resource('/api/barMethodView/:id');
            this.ExerciseResource = $resource('/api/exercise/:id');
            this.$http.get('api/exercise/').then((response)=>{
                this.exercises = response.data;
                //console.log(this.exercises);
            })
           this.getBarClasses();
        }
    }
}
