using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using System.Collections.Generic;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod]
        public void TestThatLocationOfCarCanBeGotten()
        {
            IDatabase mockDB = mocks.DynamicMock<IDatabase>();
            String carLocation = "New York City";
            String otherCarLocation = "Denver";
            using (mocks.Ordered())
            {
                Expect.Call(mockDB.getCarLocation(43)).Return(carLocation);
                Expect.Call(mockDB.getCarLocation(12)).Return(otherCarLocation);
                mockDB.Stub(x => x.getCarLocation(Arg<int>.Is.Anything)).Return("Unknown");
            }
            mocks.ReplayAll();
            Car target = new Car(10);
            target.Database = mockDB;
            String result;
            result = target.getCarLocation(43);
            Assert.AreEqual(carLocation, result);
            result = target.getCarLocation(12);
            Assert.AreEqual(otherCarLocation, result);
            result = target.getCarLocation(10);
            Assert.AreEqual("Unknown", result);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatCarGetsMileageFromDatabase()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            int Miles = 250;
            Expect.Call(mockDB.Miles).PropertyBehavior();
            mocks.ReplayAll();
            mockDB.Miles = Miles;
            var target = new Car(10);
            target.Database = mockDB;
            int mileCount = target.Mileage;
            Assert.AreEqual(mileCount, Miles);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatObjectMotherBMWThingWorks()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            int Miles = 500;
            Expect.Call(mockDB.Miles).PropertyBehavior();
            mocks.ReplayAll();
            mockDB.Miles = Miles;
            var target = ObjectMother.BMW();
            target.Database = mockDB;
            int mileCount = target.Mileage;
            Assert.AreEqual(mileCount, Miles);
            mocks.VerifyAll();
        }
        
	}
}
