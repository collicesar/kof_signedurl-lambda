using FemsaKofSignedUR.Validators;
using FemsaKofSignedURL.Models;
using FemsaKofSignedURL.Validators.Abstractions;
using FluentAssertions;
using Xunit;
using static FemsaKofSignedURL.Constants;

namespace FemsaKofSignedURL.Tests
{
    public class RequestValidatorTests
    {
        private readonly IRequestValidator _requestValidator;

        public RequestValidatorTests()
        {
            _requestValidator = new RequestValidator();
        }

        [Fact]
        public void IsValidRequest_ValidRequest_ReturnsTrue()
        {
            // Arrange          
            var validRequest = new RequestBody { FileName = "valid_file.csv" };
            string message;

            // Act
            bool result = _requestValidator.IsValidRequest(validRequest, out message);

            // Assert
            result.Should().BeTrue();
            message.Should().Be(string.Empty);           
        }

        [Fact]
        public void IsValidRequest_NullRequest_ReturnsFalseAndErrorMessage()
        {
            // Arrange        
            RequestBody? nullRequest = null;
            string message;

            // Act
            bool result = _requestValidator.IsValidRequest(nullRequest, out message);

            // Assert
            result.Should().BeFalse();
            message.Should().Be(RequestValidation.InvalidFileName);         
        }

        [Fact]
        public void IsValidRequest_InvalidFileName_ReturnsFalseAndErrorMessage()
        {
            // Arrange            
            var invalidRequest = new RequestBody { FileName = "invalid/file.cvs" };
            string message;

            // Act
            bool result = _requestValidator.IsValidRequest(invalidRequest, out message);

            // Assert
            result.Should().BeFalse();
            message.Should().Be(RequestValidation.InvalidFileName);
        }

        [Fact]
        public void IsValidFileExtension_ValidExtension_ReturnsTrue()
        {
            // Arrange            
            string fileName = "valid_file.csv";
            string contentType;

            // Act
            bool result = _requestValidator.IsValidFileExtension(fileName, out contentType);

            // Assert
            Assert.True(result);
            contentType.Should().Be("text/csv");
        }

        [Fact]
        public void IsValidFileExtension_InvalidExtension_ReturnsFalse()
        {
            // Arrange          
            string fileName = "invalid_file.exe";
            string contentType;

            // Act
            bool result = _requestValidator.IsValidFileExtension(fileName, out contentType);

            // Assert
            Assert.False(result);
            contentType.Should().Be(RequestValidation.InvalidNameExtension);
        }
    }
}
