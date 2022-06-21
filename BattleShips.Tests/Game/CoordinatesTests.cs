using System;
using BattleShips.Game;
using Xunit;

namespace BattleShips.Tests.Game;

public class CoordinatesTests
{
    [Fact]
    public void Coordinates_ProvidedXIsLessThan1_ArgumentExceptionIsThrown()
    {
        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new Coordinates(0, 5);
        });
    }
    
    [Fact]
    public void Coordinates_ProvidedXIsGreaterThan11_ArgumentExceptionIsThrown()
    {
        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new Coordinates(12, 5);
        });
    }
    
    [Fact]
    public void Coordinates_ProvidedYIsLessThan1_ArgumentExceptionIsThrown()
    {
        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new Coordinates(5, 0);
        });
    }
    
    [Fact]
    public void Coordinates_ProvidedYIsGreaterThan11_ArgumentExceptionIsThrown()
    {
        // Act + Assert
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new Coordinates(5, 12);
        });
    }

    [Fact]
    public void GenerateRandomCoordinates_GeneratesRandomValidCoordinates()
    {
        // Act
        var sut = Coordinates.GenerateRandomCoordinates();
        
        // Assert
        Assert.InRange(sut.ShipX, 1, 11);
        Assert.InRange(sut.ShipY, 1, 11);
    }

    [Fact]
    public void CoordinatesFromString_EmptyStringProvided_NullReturnedAndMessageGotValidText()
    {
        // Arrange
        const string text = "";

        // Act
        _ = Coordinates.CoordinatesFromString(text, out var errorMessage);
        
        // Assert
        Assert.Same(errorMessage, "You passed empty coordinates, please try again!");
    }
    
    [Fact]
    public void CoordinatesFromString_ProvidedStringGotMoreThanThreeLetters_NullReturnedAndMessageGotValidText()
    {
        // Arrange
        const string text = "A123";

        // Act
        _ = Coordinates.CoordinatesFromString(text, out var errorMessage);
        
        // Assert
        Assert.Same(errorMessage, "You passed too long coordinates, please try again!");
    }
    
    [Fact]
    public void CoordinatesFromString_LetterOutOfRangeProvided_NullReturnedAndMessageGotValidText()
    {
        // Arrange
        const string text = "X2";

        // Act
        _ = Coordinates.CoordinatesFromString(text, out var errorMessage);
        
        // Assert
        Assert.Same(errorMessage, "Incorrect A-J letter provided, please try again!");
    }
    
    [Fact]
    public void CoordinatesFromString_DigitCoordinateIsZero_NullReturnedAndMessageGotValidText()
    {
        // Arrange
        const string text = "A0";

        // Act
        _ = Coordinates.CoordinatesFromString(text, out var errorMessage);
        
        // Assert
        Assert.Same(errorMessage, "Digit coordinate cannot be 0, please try again!");
    }
    
    [Fact]
    public void CoordinatesFromString_DigitCoordinateIsTooBig_NullReturnedAndMessageGotValidText()
    {
        // Arrange
        const string text = "A11";

        // Act
        _ = Coordinates.CoordinatesFromString(text, out var errorMessage);
        
        // Assert
        Assert.Contains("Digit coordinate is greater than board size, please provide value in range from 0 to 10!", errorMessage);
    }
    
    [Fact]
    public void CoordinatesFromString_TextIsA11_ReturnedCoordinateGot1AsXAnd10AsY()
    {
        // Arrange
        const string text = "A10";

        // Act
        var coordinate = Coordinates.CoordinatesFromString(text, out var _);
        
        // Assert
        Assert.Equal(1, coordinate.ShipX);
        Assert.Equal(10, coordinate.ShipY);
    }
}