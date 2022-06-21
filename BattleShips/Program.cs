// See https://aka.ms/new-console-template for more information

using BattleShips.Game;
using BattleShips.Services;

new GameRunner(new Logger(), new UserInputProvider()).Play();
