using ArabicToRomanLibrary;
using System.Text.RegularExpressions;

namespace ArabicToRomanLibraryTests
{
    [Collection("ConverterTestsFixtureCollection")]
    public class ConverterClassicTests
    {
        private readonly ConverterTestsFixture fixture;

        public ConverterClassicTests(ConverterTestsFixture converterTestFixture)
        {
            fixture = converterTestFixture;
        }

        [Fact]
        public void ToRman_When_1_Should_Return_I()
        {
            // arrange
            string expected = "I";

            // act
            string actual = fixture.Converter.ToRoman(1);


            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToRman_When_5_Should_Return_V()
        {
            // arrange
            string expected = "V";

            // act
            string actual = fixture.Converter.ToRoman(5);


            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToRman_When_10_Should_Return_X()
        {
            // arrange
            string expected = "X";

            // act
            string actual = fixture.Converter.ToRoman(10);


            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToRman_When_50_Should_Return_L()
        {
            // arrange
            string expected = "L";

            // act
            string actual = fixture.Converter.ToRoman(50);


            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToRman_When_100_Should_Return_C()
        {
            // arrange
            string expected = "C";

            // act
            string actual = fixture.Converter.ToRoman(100);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToRman_When_500_Should_Return_D()
        {
            // arrange
            string expected = "D";

            // act
            string actual = fixture.Converter.ToRoman(500);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToRman_When_1000_Should_Return_M()
        {
            // arrange
            string expected = "M";

            // act
            string actual = fixture.Converter.ToRoman(1000);

            // assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(2, "II")]
        [InlineData(3, "III")]
        [InlineData(4, "IV")]
        [InlineData(20, "XX")]
        [InlineData(30, "XXX")]
        [InlineData(40, "XL")]
        [InlineData(200, "CC")]
        [InlineData(300, "CCC")]
        [InlineData(400, "CD")]
        [InlineData(900, "CM")]
        [InlineData(2000, "MM")]
        [InlineData(3000, "MMM")]
        public void ToRoman_ShouldComposeByAdditionUntilThreeConsecutiveCharacters(float arabic, string roman)
        {
            string expected = roman;

            // act
            string actual = fixture.Converter.ToRoman(arabic);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(NumberBetween1And3999))]
        public void ToRoman_Should_Return_IVXLCDM(float arabic)
        {
            // arrange is no necessary

            // act
            string actual = fixture.Converter.ToRoman(arabic);

            // assert
            Assert.True(Regex.IsMatch(actual, @"^M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$", RegexOptions.Compiled));
        }


        public static IEnumerable<object[]> NumberBetween1And3999()
        {
            List<object[]> numbers = new();
            for (float i = 1; i < 4000; i++)
            {
                numbers.Add(new object[] { i });
            }

            return numbers;
        }       
    }


    [Collection("ConverterTestsFixtureCollection")]
    public class ConverterExcelBehaviorTests
    {
        private readonly ConverterTestsFixture fixture;

        public ConverterExcelBehaviorTests(ConverterTestsFixture converterTestsFixture)
        {
            fixture = converterTestsFixture;
        }

        [Fact]
        public void ToRoman_When_0_ShouldReturn_EmptyString()
        {
            // Arrange
            string expected = string.Empty;

            // Act
            string actual = fixture.Converter.ToRoman(0);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToRoman_When_Float_Should_IgnoreDecimalPart()
        {
            // Arrange
            string expected = "II";

            // Act
            string actual = fixture.Converter.ToRoman(2.99F);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToRoman_When_Negative_ShouldThrow_ArgumentOutOfRangeException()
        {
            // Arrange is not needed

            // Act
            Action action = () => fixture.Converter.ToRoman(-1);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        [Fact]
        public void ToRoman_When_SuperiorOrEqualTo4000_ShouldThrow_ArgumentOutOfRangeException()
        {
            // Arrange is not needed

            // Act
            Action action = () => fixture.Converter.ToRoman(4000);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }
    }


    public class ConverterTestsFixture : IDisposable
    {
        public Converter Converter { get; private set; }

        public ConverterTestsFixture()
        {
            Converter = new();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Nothing to dispose
        }
    }

    [CollectionDefinition("ConverterTestsFixtureCollection")]
    public class ConverterTestsFixtureCollection : ICollectionFixture<ConverterTestsFixture>
    {
        // Dummy class to define the collection
    }

}