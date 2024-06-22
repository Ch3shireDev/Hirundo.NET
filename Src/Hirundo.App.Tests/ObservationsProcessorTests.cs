using Hirundo.Commons.Models;
using Hirundo.Databases;
using Hirundo.Processors.Computed;
using Hirundo.Processors.Observations;
using Moq;

namespace Hirundo.App.Tests
{
    public class ObservationsProcessorTests
    {
        private ObservationsProcessor processor = null!;
        private Mock<IDatabaseBuilder> databaseBuilder = null!;
        private Mock<IComputedValuesCalculatorBuilder> calculatorBuilder = null!;
        private Mock<IObservationConditionsBuilder> conditionsBuilder = null!;

        private Mock<IDatabase> database = null!;
        private Mock<IComputedValuesCalculator> calculator = null!;
        private Mock<IObservationCondition> conditions = null!;

        [SetUp]
        public void Setup()
        {
            database = new Mock<IDatabase>();
            calculator = new Mock<IComputedValuesCalculator>();
            conditions = new Mock<IObservationCondition>();

            databaseBuilder = new Mock<IDatabaseBuilder>();
            databaseBuilder.Setup(x => x.NewBuilder()).Returns(databaseBuilder.Object);
            databaseBuilder.Setup(x => x.WithDatabaseParameters(It.IsAny<IList<IDatabaseParameters>>())).Returns(databaseBuilder.Object);
            databaseBuilder.Setup(x => x.WithCancellationToken(It.IsAny<CancellationToken?>())).Returns(databaseBuilder.Object);
            databaseBuilder.Setup(x => x.Build()).Returns(database.Object);

            calculatorBuilder = new Mock<IComputedValuesCalculatorBuilder>();
            calculatorBuilder.Setup(x => x.NewBuilder()).Returns(calculatorBuilder.Object);
            calculatorBuilder.Setup(x => x.WithComputedValues(It.IsAny<IList<IComputedValuesCalculator>>())).Returns(calculatorBuilder.Object);
            calculatorBuilder.Setup(x => x.WithCancellationToken(It.IsAny<CancellationToken?>())).Returns(calculatorBuilder.Object);
            calculatorBuilder.Setup(x => x.Build()).Returns(calculator.Object);

            conditionsBuilder = new Mock<IObservationConditionsBuilder>();
            conditionsBuilder.Setup(x => x.NewBuilder()).Returns(conditionsBuilder.Object);
            conditionsBuilder.Setup(x => x.WithObservationConditions(It.IsAny<IList<IObservationCondition>>())).Returns(conditionsBuilder.Object);
            conditionsBuilder.Setup(x => x.WithCancellationToken(It.IsAny<CancellationToken?>())).Returns(conditionsBuilder.Object);
            conditionsBuilder.Setup(x => x.Build()).Returns(conditions.Object);

            processor = new ObservationsProcessor(databaseBuilder.Object, calculatorBuilder.Object, conditionsBuilder.Object);
        }

        [Test]
        public void GetObservations_ProcessorGivesCorrectFlow()
        {
            // Arrange
            Observation[] observations1 = [new(["x"], [1])];
            Observation[] observations2 = [new(["y"], [2])];
            Observation[] observations3 = [new(["z"], [3])];

            database.Setup(x => x.GetObservations()).Returns(observations1);
            calculator.Setup(x => x.Calculate(It.IsAny<Observation[]>())).Returns(observations2);
            conditions.Setup(x => x.Filter(It.IsAny<Observation[]>())).Returns(observations3);

            // Act
            var config = new ApplicationParameters();
            var observations = processor.GetObservations(config);

            // Assert
            Assert.That(observations, Is.EqualTo(observations3));
            database.Verify(x => x.GetObservations(), Times.Once);
            calculator.Verify(x => x.Calculate(observations1), Times.Once);
            conditions.Verify(x => x.Filter(observations2), Times.Once);
        }

        [Test]
        public void GetObservations_AtSecondRunUsesCachedValuesFromDataSource()
        {
            // Arrange
            Observation[] observations1 = [new(["x"], [1])];
            Observation[] observations2 = [new(["y"], [2])];
            Observation[] observations3 = [new(["z"], [3])];

            database.Setup(x => x.GetObservations()).Returns(observations1);
            calculator.Setup(x => x.Calculate(It.IsAny<Observation[]>())).Returns(observations2);
            conditions.Setup(x => x.Filter(It.IsAny<Observation[]>())).Returns(observations3);

            // Act
            var config = new ApplicationParameters();
            var results1 = processor.GetObservations(config);
            var results2 = processor.GetObservations(config);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(results1, Is.EqualTo(observations3));
                Assert.That(results2, Is.EqualTo(observations3));

                database.Verify(x => x.GetObservations(), Times.Once);
            });
        }

        [Test]
        public void GetObservations_AtSecondRunWithChangedConfig_UsesNewValuesFromDataSource()
        {
            // Arrange
            Observation[] observations1 = [new(["x"], [1])];
            Observation[] observations2 = [new(["y"], [2])];
            Observation[] observations3 = [new(["z"], [3])];

            database.Setup(x => x.GetObservations()).Returns(observations1);
            calculator.Setup(x => x.Calculate(It.IsAny<Observation[]>())).Returns(observations2);
            conditions.Setup(x => x.Filter(It.IsAny<Observation[]>())).Returns(observations3);

            // Act
            var config1 = new ApplicationParameters
            {
                Databases = new DatabaseParameters
                {
                    Databases = [new AccessDatabaseParameters { Path = "example path 1" }]
                }
            };
            var results1 = processor.GetObservations(config1);

            var config2 = new ApplicationParameters
            {
                Databases = new DatabaseParameters
                {
                    Databases = [new AccessDatabaseParameters { Path = "example path 2" }]
                }
            };
            var results2 = processor.GetObservations(config2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(results1, Is.EqualTo(observations3));
                Assert.That(results2, Is.EqualTo(observations3));

                database.Verify(x => x.GetObservations(), Times.Exactly(2));
            });
        }

        [Test]
        public void GetObservations_GivenMutatingCalculator_CachedDataDoesntChange()
        {
            // Arrange
            Observation[] observations1 = [new(["x"], [1])];
            Observation[] observations2 = [new(["y"], [2])];
            Observation[] observations3 = [new(["z"], [3])];

            var acceptedValues = new List<object?>();

            database.Setup(x => x.GetObservations()).Returns(observations1);
            calculator.Setup(x => x.Calculate(It.IsAny<Observation[]>()))
                .Callback<IList<Observation>>(x =>
                {
                    acceptedValues.Add(x.First().GetValue("x"));
                    x[0].Values[0] = 100;
                })
                .Returns(observations2);
            conditions.Setup(x => x.Filter(It.IsAny<Observation[]>())).Returns(observations3);

            // Act
            var config = new ApplicationParameters();
            var results1 = processor.GetObservations(config);
            var results2 = processor.GetObservations(config);
            var results3 = processor.GetObservations(config);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(results1, Is.EqualTo(observations3));
                Assert.That(results2, Is.EqualTo(observations3));

                database.Verify(x => x.GetObservations(), Times.Once);
                int[] expectedValues = [1, 1, 1];
                Assert.That(acceptedValues, Is.EquivalentTo(expectedValues));
            });
        }

        [Test]
        public void GetObservations_GivenNotEmptyRingRequirement_ReturnsOnlyValidRings()
        {
            // Arrange
            processor.RequireNonEmptyRing = true;

            Observation[] observations1 = [new(["x"], [1]) { Ring = "ABC" }, new(["x"], [2]),];
            Observation[] observations2 = [new(["y"], [2])];
            Observation[] observations3 = [new(["z"], [3])];

            var acceptedValues = new List<object?>();

            database.Setup(x => x.GetObservations()).Returns(observations1);
            calculator.Setup(x => x.Calculate(It.IsAny<Observation[]>())).Returns(observations2);
            conditions.Setup(x => x.Filter(It.IsAny<Observation[]>())).Returns(observations3);

            // Act
            var config = new ApplicationParameters();
            var results = processor.GetObservations(config);

            // Assert
            Assert.Multiple(() =>
            {
                database.Verify(x => x.GetObservations(), Times.Once);
                calculator.Verify(x => x.Calculate(It.IsAny<Observation[]>()), Times.Once);
                var givenObservations = calculator.Invocations[0].Arguments[0] as Observation[];
                Assert.That(givenObservations, Has.Length.EqualTo(1));
                Assert.That(givenObservations![0].Ring, Is.EqualTo("ABC"));
            });
        }
    }
}