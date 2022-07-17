using System;
using System.Linq;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class MatchTests
{
    private readonly Player _playerA;
    private readonly Player _playerB;
    private readonly Match _sut;
    private readonly PlayerBoard _playerBPlayerBoard;
    private readonly OpponentBoard _playerAOpponentBoard;

    public MatchTests()
    {
        var opponentBoardFactory = new OpponentBoardFactory();
        var playerBoardFactory = new PlayerBoardFactory();

        _playerAOpponentBoard = opponentBoardFactory.Create();
        _playerA = new Player(playerBoardFactory.Create(), _playerAOpponentBoard);

        _playerBPlayerBoard = playerBoardFactory.Create();
        _playerB = new Player(_playerBPlayerBoard, opponentBoardFactory.Create());
        _sut = new Match(_playerA, _playerB);
    }
    
    [Fact]
    public void Play_NoCoordinatesProvided_ArgumentNullExceptionThrown()
    {
        // Act + Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Play(null!, Guid.NewGuid()));
    }
    
    [Fact]
    public void Play_ProvidedPlayerIdIsDifferentThanCurrentRoundPlayerId_ArgumentExceptionIsThrown()
    {
        // Act + Assert
        Assert.Throws<ArgumentException>(() => _sut.Play(Coordinates.GenerateRandomCoordinates(), _playerB.Id));
    }
    
    [Fact]
    public void Play_PlayerMissesAndTriesToPlayOnceAgain_ArgumentExceptionIsThrownBecauseItsOtherPlayerRound()
    {
        // Arrange
        var firstEmptyPoint = _playerBPlayerBoard.BoardPoints.First(x => x.PointType == PointType.Empty);
        var firstEmptyPointCoordinates = new Coordinates(firstEmptyPoint.Point.X, firstEmptyPoint.Point.Y);
        _sut.Play(firstEmptyPointCoordinates, _playerA.Id);
        
        // Act + Assert
        Assert.Throws<ArgumentException>(() => _sut.Play(firstEmptyPointCoordinates, _playerA.Id));
    }
    
    [Fact]
    public void Play_PlayerDoesNotMiss_CanPlaySecondTimeForSamePlayerAndDoesNotThrowException()
    {
        // Arrange
        var firstShipPoint = _playerBPlayerBoard.BoardPoints.First(x => x.PointType == PointType.Ship);
        var firstCoordinateWithShip = new Coordinates(firstShipPoint.Point.X, firstShipPoint.Point.Y);
        _sut.Play(firstCoordinateWithShip, _playerA.Id);
        
        // Act + Assert
        try
        {
            _sut.Play(firstCoordinateWithShip, _playerA.Id);
            Assert.True(true);
        }
        catch
        {
            Assert.False(true, "Exception shouldn't be thrown");
        }
    }
    
    [Fact]
    public void Play_PlayerDoesNotMiss_PointChangesToSinkOnPlayersOpponentBoardAndOnOpponentPlayersBoardAlsoToSink()
    {
        // Arrange
        var firstShipPoint = _playerBPlayerBoard.BoardPoints.First(x => x.PointType == PointType.Ship);
        var firstCoordinateWithShip = new Coordinates(firstShipPoint.Point.X, firstShipPoint.Point.Y);
        
        // Act
        _sut.Play(firstCoordinateWithShip, _playerA.Id);
        
        // Assert
        _playerAOpponentBoard.BoardPoints.First(x => x.Point.Equals(firstShipPoint.Point))
            .PointType
            .Should()
            .Be(PointType.Sink);
        _playerBPlayerBoard.BoardPoints.First(x => x.Point.Equals(firstShipPoint.Point))
            .PointType
            .Should()
            .Be(PointType.Sink);
    }
    
    [Fact]
    public void Play_PlayerDoesMiss_PointChangesToMissedOnlyOnPlayerAOpponentBoard()
    {
        // Arrange
        var firstEmptyPoint = _playerBPlayerBoard.BoardPoints.First(x => x.PointType == PointType.Empty);
        var firstEmptyCoordinates = new Coordinates(firstEmptyPoint.Point.X, firstEmptyPoint.Point.Y);
        
        // Act
        _sut.Play(firstEmptyCoordinates, _playerA.Id);
        
        // Assert
        _playerAOpponentBoard.BoardPoints.First(x => x.Point.Equals(firstEmptyPoint.Point))
            .PointType
            .Should()
            .Be(PointType.Missed);
    }
}