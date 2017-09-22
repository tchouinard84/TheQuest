using NUnit.Framework;
using System;
using System.Drawing;
using TheQuest;
using static TheQuest.CommonConstants;
using static TheQuest.Direction;

namespace TheQuestTests
{
    [TestFixture]
    public class MoverTests
    {
        private const int STARTING_X = 50;
        private const int STARTING_Y = 50;
        private Rectangle boundaries = new Rectangle(0, 0, 100, 100);
        private Mover mover;
        private Random random;
        private Game game;

        [SetUp] public void SetUp()
        {
            random = new Random();
            game = new Game(boundaries, random);
            mover = new FakeMover(game, new Point(STARTING_X, STARTING_Y));
        }

        [Test] public void EnsureMove()
        {
            AssertMove(Up,    new Point(STARTING_X, STARTING_Y - GRID_INTERVAL));
            AssertMove(Down,  new Point(STARTING_X, STARTING_Y));
            AssertMove(Down,  new Point(STARTING_X, STARTING_Y + GRID_INTERVAL));
            AssertMove(Up,    new Point(STARTING_X, STARTING_Y));

            AssertMove(Left,  new Point(STARTING_X - GRID_INTERVAL, STARTING_Y));
            AssertMove(Right, new Point(STARTING_X, STARTING_Y));
            AssertMove(Right, new Point(STARTING_X + GRID_INTERVAL, STARTING_Y));
            AssertMove(Left,  new Point(STARTING_X, STARTING_Y));
        }

        [Test] public void ShouldNotMoveLeftUpWhenNearUpLeftBoundary()
        {
            var originalLocation = new Point(boundaries.Left, boundaries.Top);
            var expectedLocation = originalLocation;

            mover = new FakeMover(game, originalLocation);
            AssertMove(Up, expectedLocation);
            AssertMove(Left, expectedLocation);
        }

        [Test] public void ShouldNotMoveRightDownWhenNearRightDownBoundary()
        {
            var originalLocation = new Point(boundaries.Right, boundaries.Bottom);
            var expectedLocation = originalLocation;

            mover = new FakeMover(game, originalLocation);
            AssertMove(Down, expectedLocation);
            AssertMove(Right, expectedLocation);
        }

        private void AssertMove(Direction direction, Point expectedLocation)
        {
            mover.Move(direction);
            Assert.That(mover.Location, Is.EqualTo(expectedLocation));
        }

        [Test] public void EnsureNearby()
        {
            Assert.That(mover.Nearby(new Point(40, 40), 10), Is.False);
            Assert.That(mover.Nearby(new Point(41, 40), 10), Is.False);
            Assert.That(mover.Nearby(new Point(40, 41), 10), Is.False);
            Assert.That(mover.Nearby(new Point(41, 41), 10), Is.True);
            Assert.That(mover.Nearby(new Point(60, 60), 10), Is.False);
            Assert.That(mover.Nearby(new Point(59, 60), 10), Is.False);
            Assert.That(mover.Nearby(new Point(60, 59), 10), Is.False);
            Assert.That(mover.Nearby(new Point(59, 59), 10), Is.True);
        }

        private class FakeMover : Mover
        {
            public FakeMover(Game game, Point location) : base(game, location) { }
        }
    }
}
