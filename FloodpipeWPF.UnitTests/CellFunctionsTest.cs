using FloodPipeWPF.MVVM.Model.Game.GameField;
using System.Numerics;

namespace FloodPipeWPF.UnitTests
{
    [TestClass]
    public sealed class CellFunctionsTest
    {
        [TestMethod]
        public void CreateField_BasicSmallEmptyField_EmptySmallField()
        {
            var cells = new List<List<Cell>>();
            CellFunctions.CreateField(cells, 2, 2);
            Assert.AreEqual(cells.Count, 2);
            Assert.AreEqual(cells[0].Count, 2);
            Assert.AreEqual(cells[1].Count, 2);
            Assert.AreEqual(cells[0][0].Type, CellType.EMPTY);
            Assert.AreEqual(cells[0][1].Type, CellType.EMPTY);
            Assert.AreEqual(cells[1][0].Type, CellType.EMPTY);
            Assert.AreEqual(cells[1][1].Type, CellType.EMPTY);

            Assert.AreEqual(cells[0][0].Position, new Vector2(0, 0));
            Assert.AreEqual(cells[0][1].Position, new Vector2(0, 1));
            Assert.AreEqual(cells[1][0].Position, new Vector2(1, 0));
            Assert.AreEqual(cells[1][1].Position, new Vector2(1, 1));

            Assert.AreEqual(cells[0][0].CellState, CellState.EMPTY);
            Assert.AreEqual(cells[0][1].CellState, CellState.EMPTY);
            Assert.AreEqual(cells[1][0].CellState, CellState.EMPTY);
            Assert.AreEqual(cells[1][1].CellState, CellState.EMPTY);

            Assert.AreEqual(cells[0][0].Rotation, 0);
            Assert.AreEqual(cells[0][1].Rotation, 0);
            Assert.AreEqual(cells[1][0].Rotation, 0);
            Assert.AreEqual(cells[1][1].Rotation, 0);
        }
    }
}
