using FloodPipeWPF.MVVM.Model.Game.GameField;
using System.Numerics;

namespace FloodPipeWPF.UnitTests
{
    [TestClass]
    public sealed class CellTest
    {
        [TestMethod]
        public void CreateCell_EmptyCell_EmptyCell()
        {
            var cell = new Cell(CellType.EMPTY, new Vector2(0, 0));

            var connections = cell.RelativeConnections;
            Assert.IsNotNull(connections);
            Assert.AreEqual(connections.Count, 0);

            Assert.AreEqual(cell.Type, CellType.EMPTY);
            Assert.AreEqual(cell.Position, new Vector2(0, 0));
            Assert.AreEqual(cell.CellState, CellState.EMPTY);
            Assert.AreEqual(cell.RelativeConnections.Count, 0);
            Assert.AreEqual(cell.Rotation, 0);
        }

        [TestMethod]
        public void CreateCell_CrossCell_CrossCell()
        {
            var cell = new Cell(CellType.CROSS, new Vector2(0, 0));

            var connections = cell.RelativeConnections;
            Assert.IsNotNull(connections);
            Assert.AreEqual(connections.Count, 4);
            Assert.AreEqual(connections[0], new Vector2(0, 1));
            Assert.AreEqual(connections[1], new Vector2(0, -1));
            Assert.AreEqual(connections[2], new Vector2(1, 0));
            Assert.AreEqual(connections[3], new Vector2(-1, 0));

            Assert.AreEqual(cell.Type, CellType.CROSS);
            Assert.AreEqual(cell.Position, new Vector2(0, 0));
            Assert.AreEqual(cell.CellState, CellState.EMPTY);
            Assert.AreEqual(cell.RelativeConnections.Count, 4);
            Assert.AreEqual(cell.Rotation, 0);
        }

        [TestMethod]
        public void CreateCell_StraightCell_StraightCell()
        {
            var cell = new Cell(CellType.STRAIGHT, new Vector2(0, 0));

            var connections = cell.RelativeConnections;
            Assert.IsNotNull(connections);
            Assert.AreEqual(connections.Count, 2);
            Assert.AreEqual(connections[0], new Vector2(0, 1));
            Assert.AreEqual(connections[1], new Vector2(0, -1));

            Assert.AreEqual(cell.Type, CellType.STRAIGHT);
            Assert.AreEqual(cell.Position, new Vector2(0, 0));
            Assert.AreEqual(cell.CellState, CellState.EMPTY);
            Assert.AreEqual(cell.RelativeConnections.Count, 2);
            Assert.AreEqual(cell.Rotation, 0);
        }

        [TestMethod]
        public void CreateCell_RotatedTJunctionCell_RotatedTJunctionCell()
        {
            var cell = new Cell(CellType.T_JUNCTION, new Vector2(0, 0), 2);

            var connections = cell.RelativeConnections;

            Assert.IsNotNull(connections);
            Assert.AreEqual(connections.Count, 3);
            Assert.AreEqual(connections[0], new Vector2(0, -1));
            Assert.AreEqual(connections[1], new Vector2(-1, 0));
            Assert.AreEqual(connections[2], new Vector2(1, 0));
            Assert.AreEqual(cell.Type, CellType.T_JUNCTION);
            Assert.AreEqual(cell.Position, new Vector2(0, 0));
            Assert.AreEqual(cell.CellState, CellState.EMPTY);
            Assert.AreEqual(cell.Rotation, 2);
        }

        [TestMethod]
        public void RotateClockwise_TJunctionCell_RotatedTJunctionCell()
        {
            var cell = new Cell(CellType.T_JUNCTION, new Vector2(0, 0));

            cell.RotateClockwise();

            var connections = cell.RelativeConnections;
            Assert.IsNotNull(connections);
            Assert.AreEqual(connections.Count, 3);
            Assert.AreEqual(connections[0], new Vector2(1, 0));
            Assert.AreEqual(connections[1], new Vector2(0, -1));
            Assert.AreEqual(connections[2], new Vector2(0, 1));
            Assert.AreEqual(cell.Rotation, 1);
        }

        [TestMethod]
        public void RotateCounterClockwise_TJunctionCell_RotatedTJunctionCell()
        {
            var cell = new Cell(CellType.T_JUNCTION, new Vector2(0, 0));

            cell.RotateCounterClockwise();

            var connections = cell.RelativeConnections;
            Assert.IsNotNull(connections);
            Assert.AreEqual(connections.Count, 3);
            Assert.AreEqual(connections[0], new Vector2(-1, 0));
            Assert.AreEqual(connections[1], new Vector2(0, 1));
            Assert.AreEqual(connections[2], new Vector2(0, -1));
            Assert.AreEqual(cell.Rotation, 3);
        }

        [TestMethod]
        public void RotateClockwiseAndCounterClockwise_TJunctionCell_OriginalTJunctionCell()
        {
            var cell = new Cell(CellType.T_JUNCTION, new Vector2(0, 0));

            cell.RotateClockwise();
            cell.RotateCounterClockwise();

            var connections = cell.RelativeConnections;
            Assert.IsNotNull(connections);
            Assert.AreEqual(connections.Count, 3);
            Assert.AreEqual(connections[0], new Vector2(0, 1));
            Assert.AreEqual(connections[1], new Vector2(1, 0));
            Assert.AreEqual(connections[2], new Vector2(-1, 0));
            Assert.AreEqual(cell.Rotation, 0);
        }
    }

    [TestClass]
    public sealed class CellFunctionsTest
    {
        [TestMethod]
        public void ClearField_EmptyField_EmptyField()
        {
            var cells = new List<List<Cell>>();
            CellFunctions.ClearField(cells);
            Assert.AreEqual(cells.Count, 0);
        }

        [TestMethod]
        public void ClearField_FilledField_EmptyField()
        {
            var cells = new List<List<Cell>>();
            CellFunctions.CreateField(cells, 2, 2);
            CellFunctions.ClearField(cells);
            Assert.AreEqual(cells.Count, 0);
        }

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

        [TestMethod]
        public void CreateField_FilledFieldToSmallerField_SmallerFieldFromBig()
        {
            var cells = new List<List<Cell>>();
            CellFunctions.CreateField(cells, 10, 10);
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

        [TestMethod]
        public void IsCellPositionValid_ValidPosition_True()
        {
            var cell = new Cell(CellType.EMPTY, new Vector2(0, 0));
            Assert.IsTrue(CellFunctions.IsCellPositionValid(cell, 10, 10));
        }

        [TestMethod]
        public void IsCellPositionValid_InvalidPosition_False()
        {
            var cell = new Cell(CellType.EMPTY, new Vector2(10, 10));
            Assert.IsFalse(CellFunctions.IsCellPositionValid(cell, 10, 10));
        }

        [TestMethod]
        public void IsCellConnectedToCell_ConnectedCells_True()
        {
            var cell1 = new Cell(CellType.CROSS, new Vector2(0, 0));
            var cell2 = new Cell(CellType.CROSS, new Vector2(0, 1));
            Assert.IsTrue(CellFunctions.IsCellConnectedToCell(cell1, cell2));
        }

        [TestMethod]
        public void IsCellConnectedToCell_NotConnectedCells_False()
        {
            var cell1 = new Cell(CellType.CROSS, new Vector2(0, 0));
            var cell2 = new Cell(CellType.CROSS, new Vector2(1, 1));
            Assert.IsFalse(CellFunctions.IsCellConnectedToCell(cell1, cell2));
        }

        [TestMethod]
        public void IsCellEmpty_EmptyCell_True()
        {
            var cell = new Cell(CellType.EMPTY, new Vector2(0, 0));
            Assert.IsTrue(CellFunctions.IsCellEmpty(cell));
        }

        [TestMethod]
        public void IsCellEmpty_FilledCell_False()
        {
            var cell = new Cell(CellType.CROSS, new Vector2(0, 0), 0, CellState.FULL);
            Assert.IsFalse(CellFunctions.IsCellEmpty(cell));
        }
        
        [TestMethod]
        public void GetConnectedEmptyCells_EmptyCell_EmptyList()
        {
            var cells = new List<List<Cell>>();
            CellFunctions.CreateField(cells, 2, 2);
            var connectedEmptyCells = CellFunctions.GetConnectedEmptyCells(cells[0][0], cells);
            Assert.AreEqual(connectedEmptyCells.Count, 0);
        }
        
        [TestMethod]
        public void GetConnectedEmptyCells_FilledCell_EmptyList()
        {
            var cells = new List<List<Cell>>();
            CellFunctions.CreateField(cells, 2, 2);
            cells[0][0].CellState = CellState.FULL;
            var connectedEmptyCells = CellFunctions.GetConnectedEmptyCells(cells[0][0], cells);
            Assert.AreEqual(connectedEmptyCells.Count, 0);
        }
        
        [TestMethod]
        public void GetConnectedEmptyCells_EmptyCellWithConnectedEmptyCells_ConnectedEmptyCells()
        {
            var cells = new List<List<Cell>>();
            CellFunctions.CreateField(cells, 2, 2);
            cells[0][0].CellState = CellState.EMPTY;
            cells[0][1].CellState = CellState.EMPTY;
            cells[1][0].CellState = CellState.EMPTY;
            var connectedEmptyCells = CellFunctions.GetConnectedEmptyCells(cells[0][0], cells);
            Assert.AreEqual(connectedEmptyCells.Count, 3);
            Assert.IsTrue(connectedEmptyCells.Contains(cells[0][1]));
            Assert.IsTrue(connectedEmptyCells.Contains(cells[1][0]));
            Assert.IsTrue(connectedEmptyCells.Contains(cells[1][1]));
        }
    }
}
