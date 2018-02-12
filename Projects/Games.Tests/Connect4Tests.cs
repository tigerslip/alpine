using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Connect4;
using NUnit.Framework;

namespace Games.Tests
{
    public class Connect4Tests
    {
        [Test]
        public void CorrectNumberOfRows()
        {
            var game = new Connect4Game();

            var rows = Functions.DrawRows(game);

            Assert.AreEqual(6, rows.Length);
        }

        public void AssertIsWinner(Connect4Game game, Player expectedWinner)
        {
            var hasWinner = Functions.TryGetWinner(game, out var winner);
            Assert.IsTrue(hasWinner);
            Assert.AreEqual(expectedWinner, winner);
        }

        [Test]
        public void DetermineWinner()
        {
            var game = new Connect4Game();

            game.Columns[0][0] = Space.Black;
            game.Columns[0][1] = Space.Black;
            game.Columns[0][2] = Space.Black;
            game.Columns[0][3] = Space.Black;

            AssertIsWinner(game, Player.Black);

            var game2 = new Connect4Game();

            game2.Columns[0][0] = Space.Red;
            game2.Columns[1][1] = Space.Red;
            game2.Columns[2][2] = Space.Red;
            game2.Columns[3][3] = Space.Red;

            AssertIsWinner(game2, Player.Red);

            var game3 = new Connect4Game();

            game3.Columns[1][1] = Space.Red;
            game3.Columns[1][2] = Space.Red;
            game3.Columns[1][3] = Space.Red;
            game3.Columns[1][4] = Space.Red;

            AssertIsWinner(game3, Player.Red);

            var game4 = new Connect4Game();

            game4.Columns[0][0] = Space.Red;
            game4.Columns[1][1] = Space.Red;
            game4.Columns[2][2] = Space.Red;
            game4.Columns[3][3] = Space.Red;

            AssertIsWinner(game2, Player.Red);
        }
    }
}
