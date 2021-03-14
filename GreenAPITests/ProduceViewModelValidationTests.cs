using GoGreen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace GreenAPITests
{
    public class ProduceViewModelValidationTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void ValidationTest(string name, decimal? price, int? stock, bool expectedResult, int expectedValidationResults)
        {
            ProduceViewModel model = new ProduceViewModel()
            {
                Name = name,
                Price = price,
                Stock = stock,
            };

            ValidationContext context = new ValidationContext(model);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isModelStateValid = Validator.TryValidateObject(model, context, results, true);

            Assert.Equal(expectedResult, isModelStateValid);
            Assert.Equal(expectedValidationResults, results.Count);
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { "Apple", new Decimal(0.0), 0, true, 0 },
            new object[] { null, new Decimal(0.0), 0, false, 1 },
            new object[] { null, null, 0, false, 2 },
            new object[] { "Apple", null, 0, false, 1},
            new object[] { "Apple", new Decimal(0.0), null, false, 1 },
            new object[] { null, new Decimal(0.0), null, false, 2 },
            new object[] { "Apple", null, null, false, 2 },
            new object[] { "Apple", new Decimal(0.0), -1, false, 1 },
        };
    }
}
