using Expedia;
using System;

namespace ExpediaTest
{
	public class ObjectMother
	{
        public static Car Saab() //why the heck are we using Saab?? #teamMopar
        {
            return new Car(7) {Name = "Saab 9-5 Sports Sedan"};
        }

        public static Car BMW()
        {
            return new Car(10) { Name = "BMW i8" };
        }

        public static Car Plymouth() //this is completely for funzies #teamMopar
        {
            return new Car(43) { Name = "Plymouth RoadRunner Superbird" }; 
        }
	}
}
